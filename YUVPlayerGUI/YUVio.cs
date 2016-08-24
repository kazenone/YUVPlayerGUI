using System;
using System.IO;

namespace YUVImgProc
{
    /// <summary>
    /// YUVファイルを取り扱う際のIO関係の処理メソッドをまとめたクラス
    /// </summary>
    class YUVio
    {
        /// <summary>
        /// 横のサイズ
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 縦のサイズ
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// U,V成分の横のサイズ
        /// </summary>
        public int Widthc { get; set; }

        /// <summary>
        /// U,V成分の縦のサイズ
        /// </summary>
        public int Heightc { get; set; }

        /// <summary>
        /// 画素数
        /// </summary>
        public int Pixel { get; set; }

        /// <summary>
        /// U,V成分の画素数
        /// </summary>
        public int Pixelc { get; set; }

        /// <summary>
        /// カラーフォーマット(420 or 444)
        /// </summary>
        public int ColorFormat { get; set; }

        /// <summary>
        /// 読み込める最大フレーム番号
        /// </summary>
        public int MaxFrameCount { get; set; }

        /// <summary>
        /// 現在読み込んでいるフレーム番号
        /// </summary>
        public int ReadFrameCount { get; set; }

        /// <summary>
        /// インスタンスが保持しているファイルストリーム
        /// </summary>
        private FileStream fs;

        /// <summary>
        /// 画像を読み込むためのインスタンスを生成する
        /// </summary>
        /// <param name="fPath">YUV画像のファイルパス</param>
        /// <param name="width">YUV画像の横幅</param>
        /// <param name="height">YUV画像の縦幅</param>
        /// <param name="colorFormat">カラーフォーマット(420 or 444)</param>
        public YUVio(string fPath, int width, int height, int colorFormat)
        {
            try
            {
                OpenImage(fPath, width, height, colorFormat);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 指定されたYUV画像を開き、使用できるようにする。
        /// </summary>
        /// <param name="fPath">YUV画像のファイルパス</param>
        /// <param name="width">YUV画像の横幅</param>
        /// <param name="height">YUV画像の縦幅</param>
        /// <param name="colorFormat">カラーフォーマット(420 or 444)</param>
        /// <exception cref="YUVIOException"/>
        public void OpenImage(string fPath, int width, int height, int colorFormat)
        {
            if (colorFormat != 420 && colorFormat != 444)
            {
                throw new YUVIOException("カラーフォーマットの形式が不正です");
            }

            Width = width;
            Height = height;
            Pixel = width * height;
            ColorFormat = colorFormat;
            ReadFrameCount = -1;
            FileInfo fi = new System.IO.FileInfo(fPath);
            long fSize = fi.Length;

            if (ColorFormat == 444)
            {
                Widthc = width;
                Heightc = height;
                Pixelc = Pixel;
            }
            else
            {
                Widthc = width / 2;
                Heightc = height / 2;
                Pixelc = Pixel / 4;
            }

            if (fSize % (Pixel + Pixelc * 2) == 0)
            {
                MaxFrameCount = (int)fSize / (Pixel + Pixelc * 2);
                MaxFrameCount--;
            }
            else
            {
                throw new YUVIOException("画像サイズとファイルサイズが一致しません");
            }

            try
            {
                fs = new FileStream(fPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
            catch
            {
                Console.WriteLine("ファイルを開くことが出来ませんでした");
                throw;
            }

        }

        /// <summary>
        /// ファイルを最初から読めるようにする
        /// </summary>
        public void ReloadFile()
        {
            fs.Position = 0;
        }

        /// <summary>
        /// 現在インスタンスが保持している、ファイルストリームを閉じる
        /// </summary>
        public void CloseImege()
        {
            fs.Close();
        }

        /// <summary>
        /// 指定されたYUV画像の読み込みを行い、YUVインスタンスを新規生成して返します。
        /// </summary>
        /// <exception cref="YUVIOException"/>
        public YUV ReadImage()
        {
            YUV img = new YUV(Width, Height, ColorFormat);

            if (MaxFrameCount < ReadFrameCount)
            {
                throw new YUVIOException("これ以上画像を読み込めません");
            }

            ReadFrameCount++;
            try
            {
                fs.Read(img.Y, 0, Pixel);
                fs.Read(img.U, 0, Pixelc);
                fs.Read(img.V, 0, Pixelc);
            }
            catch
            {
                throw;
            }

            return img;
        }

        /// <summary>
        /// 指定されたYUV画像の読み込みを行い、YUVインスタンスに画像を書き込みます。
        /// </summary>
        /// <exception cref="YUVIOException"/>
        public YUV ReadImage(YUV img)
        {


            if (MaxFrameCount < ReadFrameCount)
            {
                throw new YUVIOException("これ以上画像を読み込めません");
            }

            ReadFrameCount++;
            try
            {
                fs.Read(img.Y, 0, Pixel);
                fs.Read(img.U, 0, Pixelc);
                fs.Read(img.V, 0, Pixelc);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("画像領域の領域外を参照しました");
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return img;
        }

        /// <summary>
        /// <para>引数のパスの位置に新規ファイルを作成し、インスタンスの画像データを書き出します</para>
        /// <para>処理終了後ファイルアクセスを解除します(※動画では使用不可)</para>
        /// </summary>
        /// <param name="outPath"></param>
        public static void WriteImage(YUV img, string outPath)
        {
            try
            {
                using (FileStream outFs = new FileStream(outPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    outFs.Write(img.Y, 0, img.Pixel);
                    outFs.Write(img.U, 0, img.Pixelc);
                    outFs.Write(img.V, 0, img.Pixelc);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// <para>引数のパスの位置に指定されたファイルストリームに、インスタンスの画像データを書き出します</para>
        /// </summary>
        /// <param name="outFs"></param>
        public static void WriteImage(YUV img, FileStream outFs)
        {
            try
            {
                outFs.Write(img.Y, 0, img.Pixel);
                outFs.Write(img.U, 0, img.Pixelc);
                outFs.Write(img.V, 0, img.Pixelc);
            }
            catch
            {
                throw;
            }
        }
    }

    [Serializable]
    public class YUVIOException : Exception
    {
        public YUVIOException() { }
        public YUVIOException(string message) : base(message) { }
        public YUVIOException(string message, Exception inner) : base(message, inner) { }
        protected YUVIOException(
           System.Runtime.Serialization.SerializationInfo info,
           System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
