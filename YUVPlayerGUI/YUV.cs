using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace YUVImgProc
{
    /// <summary>
    /// 8bit深度のYUV画像のデータを保持するためのインスタンスを提供するクラス
    /// </summary>
    [Serializable()]
    class YUV
    {
        /// <summary>
        /// 輝度データ
        /// </summary>
        public byte[] Y;

        /// <summary>
        /// 色差Uデータ
        /// </summary>
        public byte[] U;

        /// <summary>
        /// 色差Vデータ
        /// </summary>
        public byte[] V;

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
        /// <para>YUV画像を保持するインスタンスを作成する</para>
        /// </summary>
        /// <param name="width">横幅</param>
        /// <param name="height">縦幅</param>
        /// <param name="colorFormat">カラーフォーマット(420 or 444)</param>
        public YUV(int width, int height, int colorFormat)
        {
            if (colorFormat != 420 && colorFormat != 444)
            {
                throw new YUVException("カラーフォーマットの形式が不正です");
            }

            Width = width;
            Height = height;
            Pixel = width * height;
            ColorFormat = colorFormat;
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

            Y = new byte[Pixel];
            U = new byte[Pixelc];
            V = new byte[Pixelc];

        }


    }


    [Serializable]
    public class YUVException : Exception
    {
        public YUVException() { }
        public YUVException(string message) : base(message) { }
        public YUVException(string message, Exception inner) : base(message, inner) { }
        protected YUVException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }


}
