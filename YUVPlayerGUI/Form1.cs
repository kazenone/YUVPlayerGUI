using System;
using System.Threading;
using System.Windows.Forms;

namespace YUVPlayerGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                // ドラッグ中のファイルやディレクトリの取得
                string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string d in drags)
                {
                    if (!System.IO.File.Exists(d))
                    {
                        // ファイル以外であればイベント・ハンドラを抜ける
                        return;
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            fPathTextBox.Text = fileNames[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //入力確認
            if (!Validation())
            {
                label7.Text = "必須項目が入力されていません";
                return;
            }

            //引数取り込み
            string fpath = fPathTextBox.Text;
            int width = decimal.ToInt32(widthNumericUpDown.Value);
            int height = decimal.ToInt32(heightNumericUpDown.Value);
            int fps = decimal.ToInt32(fpsNumericUpDown.Value);
            string colorFormat = CFComboBox.SelectedItem.ToString();
            bool loopFlag = loopFlagCBOX.Checked;
            bool frameLock = frameLockCBox.Checked;

            

            //エラー文クリア
            label7.Text = "";

            //Form2クラスのインスタンスを作成する
            Form2 f = new Form2(fpath, width, height, fps, colorFormat, loopFlag, frameLock);

            //Form2を表示する
            f.ShowDialog(this);

            //エラーが出た時は表示
            label7.Text = f.receiveText;

            //フォームが必要なくなったところで、Disposeを呼び出す
            f.Dispose();

            //ガベージコレクションを起動
            GC.Collect();

        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(fPathTextBox.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(widthNumericUpDown.Text) || decimal.ToInt32(widthNumericUpDown.Value) == 0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(heightNumericUpDown.Text) || decimal.ToInt32(heightNumericUpDown.Value) == 0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(fpsNumericUpDown.Text) || decimal.ToInt32(fpsNumericUpDown.Value) == 0)
            {
                return false;
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CFComboBox.SelectedIndex = 0;
        }

        private void widthNumericUpDown_Enter(object sender, EventArgs e)
        {
            widthNumericUpDown.Select(0, widthNumericUpDown.Value.ToString().Length);
        }

        private void heightNumericUpDown_Enter(object sender, EventArgs e)
        {
            heightNumericUpDown.Select(0, heightNumericUpDown.Value.ToString().Length);
        }

        private void fpsNumericUpDown_Enter(object sender, EventArgs e)
        {
            fpsNumericUpDown.Select(0, fpsNumericUpDown.Value.ToString().Length);
        }

        private void widthNumericUpDown_Click(object sender, EventArgs e)
        {
            widthNumericUpDown.Select(0, widthNumericUpDown.Value.ToString().Length);
        }

        private void heightNumericUpDown_Click(object sender, EventArgs e)
        {
            heightNumericUpDown.Select(0, heightNumericUpDown.Value.ToString().Length);
        }

        private void fpsNumericUpDown_Click(object sender, EventArgs e)
        {
            fpsNumericUpDown.Select(0, fpsNumericUpDown.Value.ToString().Length);
        }
    }
}
