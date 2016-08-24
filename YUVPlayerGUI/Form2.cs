using OpenCvSharp;
using System;
using System.Windows.Forms;
using YUVImgProc;
using OpenCvSharp.Extensions;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Drawing;

namespace YUVPlayerGUI
{
    public partial class Form2 : Form
    {

        Bitmap[] Bitmaps;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private int nextFrame;
        private string fpath;
        private int width;
        private int height;
        private int fps;
        private int colorFormat;
        private bool loopFlag;
        private bool frameLock;
        public string receiveText = null;
        private Task imgReadTask;
        private CancellationTokenSource _tokenSource = null;

        public Form2(string fpath, int width, int height, int fps, string colorFormat, bool loopFlag, bool frameLock)
        {
            InitializeComponent();
            nextFrame = 0;

            this.fpath = fpath;
            this.width = width;
            this.height = height;
            this.fps = fps;
            this.colorFormat = int.Parse(colorFormat);
            this.loopFlag = loopFlag;
            this.frameLock = frameLock;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            YUVio imgReader=null;
            try
            {
                imgReader = new YUVio(fpath, width, height, colorFormat);
            }
            catch(Exception e2)
            {
                //エラーメッセージ取得後終了
                receiveText = e2.Message;
                this.Close();
                return;
            }

            if (!frameLock)
            {
                Bitmaps = new Bitmap[imgReader.MaxFrameCount];

                imgReadTask = YUVReadTask(imgReader, Bitmaps);
                timer.Interval = (int)(1000 / fps);
                timer.Elapsed += new ElapsedEventHandler(timer_Tick);
                timer.AutoReset = true;
                timer.Start();
            }
            else
            {
                Mat matImages = new Mat();
                matImages = YUV2Mat.YUV2MatBGR(imgReader.ReadImage(), 1, 0);
                pictureBox1.Image = matImages.ToBitmap();
            }


        }

        delegate void SetFocusDelegate();

        void SetFocus()
        {
            pictureBox1.Image = Bitmaps[nextFrame];
            pictureBox1.Refresh();
        }

        private void timer_Tick(object sender, ElapsedEventArgs e)
        {

            if (Bitmaps[nextFrame] != null)
            {
                Invoke(new SetFocusDelegate(SetFocus));
            }
            else
            {
                return;
            }

            if (nextFrame + 1 == Bitmaps.Length)
            {
                if (loopFlag)
                {
                    nextFrame = 0;
                }
                else
                {
                    timer.Stop();
                }
            }
            else
            {
                nextFrame++;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            TaskCancel();
            timer.Dispose();
            Bitmaps = null;
        }

        /// <summary>
        /// YUV動画をMat配列に格納するためのTask
        /// </summary>
        /// <param name="imgReader">Mat配列に格納したいYUVioインスタンス</param>
        /// <param name="matImgs">変換された結果を入れる配列</param>
        /// <returns></returns>
        private async Task YUVReadTask(YUVio imgReader, Bitmap[] Bitmaps)
        {
            if (_tokenSource == null) _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

            // ワーカースレッドが生成される
            Task task = Task.Factory.StartNew(() =>
            {
                Object locker = new Object();
                Parallel.For(0, imgReader.MaxFrameCount, i =>
                {
                    int procIndex;
                    YUV img;
                    Mat mat;
                    lock (locker)
                    {
                        //排他制御をかけて、YUVをファイルから読み出す
                        img = imgReader.ReadImage();
                        procIndex = imgReader.ReadFrameCount;
                    }
                    //変換を行い配列に格納
                    mat = YUV2Mat.YUV2MatBGR(img, 1, 1);
                    Bitmaps[procIndex] = mat.ToBitmap();

                });
            }, token).ContinueWith(t =>
            {
                // TODO:あとしまつ
                _tokenSource.Dispose();
                _tokenSource = null;
                if (t.IsCanceled)
                {
                    throw new TaskCanceledException();
                }
            });

            // 処理が完了するのを非同期的に待機
            await task;

        }

        public void TaskCancel()
        {
            // 即例外を投げてキャンセルさせる
            if (_tokenSource != null) _tokenSource.Cancel(true);
        }
    }
}
