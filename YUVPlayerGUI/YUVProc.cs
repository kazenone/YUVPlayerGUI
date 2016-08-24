using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace YUVImgProc
{
    /// <summary>
    /// YUV型のインスタンスに対して何らかの処理を行うメソッドをまとめたクラス
    /// </summary>
    class YUVProc
    {
        /// <summary>
        /// 渡した引数を0～255に丸めます。
        /// </summary>
        /// <param name="idv">丸めたい変数</param>
        /// <returns>丸めた結果</returns>
        public static byte RoundingByte(int idv)
        {
            byte ucv;
            int iv;
            iv = idv;
            if (idv < 0)
            {
                iv = 0;
            }
            else if (idv > 255)
            {
                iv = 255;
            }
            ucv = (byte)iv;
            return ucv;
        }

        /// <summary>
        /// 渡した引数を0～255に丸めます。
        /// </summary>
        /// <param name="idv">丸めたい変数</param>
        /// <returns>丸めた結果</returns>
        public static byte RoundingByte(double dv)
        {
            byte ucv;
            int iv = (int)(dv + 0.5);
            if (iv < 0)
            {
                iv = 0;
            }
            else if (iv > 255)
            {
                iv = 255;
            }
            ucv = (byte)iv;
            return ucv;
        }

        /// <summary>
        /// YUV型の画素値情報全体を指定した値で埋めます。
        /// </summary>
        /// <param name="img"></param>
        /// <param name="yValue">Yを埋める値</param>
        /// <param name="uValue">Uを埋める値</param>
        /// <param name="vValue">Vを埋める値</param>
        public static void SetvalueImage(YUV img, int yValue, int uValue, int vValue)
        {
            byte yRoundingValue = RoundingByte(yValue);
            byte uRoundingValue = RoundingByte(uValue);
            byte vRoundingValue = RoundingByte(vValue);

            for (int i = 0; i < img.Y.Length; i++)
            {
                img.Y[i] = yRoundingValue;
            }

            for (int i = 0; i < img.V.Length; i++)
            {
                img.U[i] = uRoundingValue;
                img.V[i] = vRoundingValue;
            }
        }

        /// <summary>
        /// ColorFormatが420のYUVインスタンスを受け取り、ColorFormatが444のYUVインスタンスを作成します。
        /// </summary>
        /// <param name="srcImg">ColorFormatが420のYUVインスタンス</param>
        /// <param name="param">0でY成分のみ、0以外でY,u,v成分すべて含む</param>
        /// <returns></returns>
        public static YUV TransImage420To444(YUV srcImg, int param)
        {
            if (srcImg.ColorFormat==444)
            {
                Console.WriteLine("すでにYUV444のフォーマットが指定されています");
                return srcImg;
            }

            int j, k;
            int width, height;
            int widthc, heightc;

            width = srcImg.Width;
            height = srcImg.Height;
            widthc = width / 2;
            heightc = height / 2;

            YUV dstImg = new YUV(width, height, 444);

            //#pragma omp parallel for
            for (k = 0; k < height; k++)
            {
                for (j = 0; j < width; j++)
                {
                    dstImg.Y[j + k * width] = srcImg.Y[j + k * width];
                }
            }

            if (param == 0)
            {
                //#pragma omp parallel for
                for (k = 0; k < height; k++)
                {
                    for (j = 0; j < width; j++)
                    {
                        dstImg.U[j + k * width] = 128;
                        dstImg.V[j + k * width] = 128;
                    }
                }
            }
            else {

                //#pragma omp parallel for
                for (k = 0; k < heightc - 1; k++)
                {
                    for (j = 0; j < widthc - 1; j++)
                    {
                        dstImg.U[2 * j + 1 + (2 * k + 1) * width] = (byte)((9 * srcImg.U[j + k * widthc] + 4 * srcImg.U[j + 1 + k * widthc] + 4 * srcImg.U[j + (k + 1) * widthc] + 3 * srcImg.U[j + 1 + (k + 1) * widthc] + 10) / 20);
                        dstImg.U[2 * j + 2 + (2 * k + 1) * width] = (byte)((4 * srcImg.U[j + k * widthc] + 9 * srcImg.U[j + 1 + k * widthc] + 3 * srcImg.U[j + (k + 1) * widthc] + 4 * srcImg.U[j + 1 + (k + 1) * widthc] + 10) / 20);
                        dstImg.U[2 * j + 1 + (2 * k + 2) * width] = (byte)((4 * srcImg.U[j + k * widthc] + 3 * srcImg.U[j + 1 + k * widthc] + 9 * srcImg.U[j + (k + 1) * widthc] + 4 * srcImg.U[j + 1 + (k + 1) * widthc] + 10) / 20);
                        dstImg.U[2 * j + 2 + (2 * k + 2) * width] = (byte)((3 * srcImg.U[j + k * widthc] + 4 * srcImg.U[j + 1 + k * widthc] + 4 * srcImg.U[j + (k + 1) * widthc] + 9 * srcImg.U[j + 1 + (k + 1) * widthc] + 10) / 20); ;
                        dstImg.V[2 * j + 1 + (2 * k + 1) * width] = (byte)((9 * srcImg.V[j + k * widthc] + 4 * srcImg.V[j + 1 + k * widthc] + 4 * srcImg.V[j + (k + 1) * widthc] + 3 * srcImg.V[j + 1 + (k + 1) * widthc] + 10) / 20);
                        dstImg.V[2 * j + 2 + (2 * k + 1) * width] = (byte)((4 * srcImg.V[j + k * widthc] + 9 * srcImg.V[j + 1 + k * widthc] + 3 * srcImg.V[j + (k + 1) * widthc] + 4 * srcImg.V[j + 1 + (k + 1) * widthc] + 10) / 20);
                        dstImg.V[2 * j + 1 + (2 * k + 2) * width] = (byte)((4 * srcImg.V[j + k * widthc] + 3 * srcImg.V[j + 1 + k * widthc] + 9 * srcImg.V[j + (k + 1) * widthc] + 4 * srcImg.V[j + 1 + (k + 1) * widthc] + 10) / 20);
                        dstImg.V[2 * j + 2 + (2 * k + 2) * width] = (byte)((3 * srcImg.V[j + k * widthc] + 4 * srcImg.V[j + 1 + k * widthc] + 4 * srcImg.V[j + (k + 1) * widthc] + 9 * srcImg.V[j + 1 + (k + 1) * widthc] + 10) / 20); ;
                    }
                }

                k = 0;
                //#pragma omp parallel for
                for (j = 0; j < widthc - 1; j++)
                {
                    dstImg.U[2 * j + 1 + k * width] = (byte)((18 * srcImg.U[j + k * widthc] + 8 * srcImg.U[j + 1 + k * widthc] + 13) / 26);
                    dstImg.U[2 * j + 2 + k * width] = (byte)((8 * srcImg.U[j + k * widthc] + 18 * srcImg.U[j + 1 + k * widthc] + 13) / 26);
                    dstImg.V[2 * j + 1 + k * width] = (byte)((18 * srcImg.V[j + k * widthc] + 8 * srcImg.V[j + 1 + k * widthc] + 13) / 26);
                    dstImg.V[2 * j + 2 + k * width] = (byte)((8 * srcImg.V[j + k * widthc] + 18 * srcImg.V[j + 1 + k * widthc] + 13) / 26);
                }
                k = heightc - 1;
                //#pragma omp parallel for
                for (j = 0; j < widthc - 1; j++)
                {
                    dstImg.U[2 * j + 1 + (2 * k + 1) * width] = (byte)((18 * srcImg.U[j + k * widthc] + 8 * srcImg.U[j + 1 + k * widthc] + 13) / 26);
                    dstImg.U[2 * j + 2 + (2 * k + 1) * width] = (byte)((8 * srcImg.U[j + k * widthc] + 18 * srcImg.U[j + 1 + k * widthc] + 13) / 26);
                    dstImg.V[2 * j + 1 + (2 * k + 1) * width] = (byte)((18 * srcImg.V[j + k * widthc] + 8 * srcImg.V[j + 1 + k * widthc] + 13) / 26);
                    dstImg.V[2 * j + 2 + (2 * k + 1) * width] = (byte)((8 * srcImg.V[j + k * widthc] + 18 * srcImg.V[j + 1 + k * widthc] + 13) / 26);
                }

                //#pragma omp parallel for
                for (k = 0; k < heightc - 1; k++)
                {
                    j = 0;
                    dstImg.U[j + (2 * k + 1) * width] = (byte)((18 * srcImg.U[j + k * widthc] + 8 * srcImg.U[j + (k + 1) * widthc] + 13) / 26);
                    dstImg.U[j + (2 * k + 2) * width] = (byte)((8 * srcImg.U[j + k * widthc] + 18 * srcImg.U[j + (k + 1) * widthc] + 13) / 26);
                    dstImg.V[j + (2 * k + 1) * width] = (byte)((18 * srcImg.V[j + k * widthc] + 8 * srcImg.V[j + (k + 1) * widthc] + 13) / 26);
                    dstImg.V[j + (2 * k + 2) * width] = (byte)((8 * srcImg.V[j + k * widthc] + 18 * srcImg.V[j + (k + 1) * widthc] + 13) / 26);
                    j = widthc - 1;
                    dstImg.U[2 * j + 1 + (2 * k + 1) * width] = (byte)((18 * srcImg.U[j + k * widthc] + 8 * srcImg.U[j + (k + 1) * widthc] + 13) / 26);
                    dstImg.U[2 * j + 1 + (2 * k + 2) * width] = (byte)((8 * srcImg.U[j + k * widthc] + 18 * srcImg.U[j + (k + 1) * widthc] + 13) / 26);
                    dstImg.V[2 * j + 1 + (2 * k + 1) * width] = (byte)((18 * srcImg.V[j + k * widthc] + 8 * srcImg.V[j + (k + 1) * widthc] + 13) / 26);
                    dstImg.V[2 * j + 1 + (2 * k + 2) * width] = (byte)((8 * srcImg.V[j + k * widthc] + 18 * srcImg.V[j + (k + 1) * widthc] + 13) / 26);
                }
                k = 0; j = 0; dstImg.U[j + k * width] = srcImg.U[j + k * widthc];
                dstImg.V[j + k * width] = srcImg.V[j + k * widthc];
                k = 0; j = widthc - 1; dstImg.U[2 * j + 1 + k * width] = srcImg.U[j + k * widthc];
                dstImg.V[2 * j + 1 + k * width] = srcImg.V[j + k * widthc];
                k = heightc - 1; j = 0; dstImg.U[j + (2 * k + 1) * width] = srcImg.U[j + k * widthc];
                dstImg.V[j + (2 * k + 1) * width] = srcImg.V[j + k * widthc];
                k = heightc - 1; j = widthc - 1; dstImg.U[2 * j + 1 + (2 * k + 1) * width] = srcImg.U[j + k * widthc];
                dstImg.V[2 * j + 1 + (2 * k + 1) * width] = srcImg.V[j + k * widthc];

                

            }
            return dstImg;
        }

        public static YUV TransImage420To444Fast(YUV srcImg, int param)
        {

            if (srcImg.ColorFormat == 444)
            {
                Console.WriteLine("すでにYUV444のフォーマットが指定されています");
                return srcImg;
            }

            int j, k;
            int width, height;
            int widthc, heightc;

            width = srcImg.Width;
            height = srcImg.Height;
            widthc = width / 2;
            heightc = height / 2;

            YUV dstImg = new YUV(width, height, 444);

            for (k = 0; k < height; k++)
            {
                for (j = 0; j < width; j++)
                {
                    dstImg.Y[j + k * width] = srcImg.Y[j + k * width];
                }
            }

            if (param == 0)
            {
                for (k = 0; k < height; k++)
                {
                    for (j = 0; j < width; j++)
                    {
                        dstImg.U[j + k * width] = 128;
                        dstImg.V[j + k * width] = 128;
                    }
                }
            }
            else
            {
                for (k = 0; k < heightc; k++)
                {
                    for (j = 0; j < widthc; j++)
                    {
                        dstImg.U[2 * j + (2 * k) * width] = dstImg.U[2 * j + 1 + (2 * k) * width] = dstImg.U[2 * j + (2 * k + 1) * width] = dstImg.U[2 * j + 1 + (2 * k + 1) * width] = srcImg.U[j + k * widthc];
                        dstImg.V[2 * j + (2 * k) * width] = dstImg.V[2 * j + 1 + (2 * k) * width] = dstImg.V[2 * j + (2 * k + 1) * width] = dstImg.V[2 * j + 1 + (2 * k + 1) * width] = srcImg.V[j + k * widthc];
                    }
                }

            }

            return dstImg;

        }

        /// <summary>
        /// ColorFormatが444のYUVインスタンスを受け取り、ColorFormatが420のYUVインスタンスを作成します。
        /// </summary>
        /// <param name="img">ColorFormatが444のYUVインスタンス</param>
        /// <param name="param">0でY成分のみ、0以外でY,u,v成分すべて含む</param>
        /// <returns></returns>
        public static YUV TransImage444To420(YUV img, int param)
        {
            
            if (img.ColorFormat == 420)
            {
                Console.WriteLine("すでにYUV420のフォーマットが指定されています");
                return img;
            }

            int j, k;
            int width, height;
            int widthc, heightc;

            width = img.Width;
            height = img.Height;
            widthc = width / 2;
            heightc = height / 2;

            YUV outImg = new YUV(width, height, 444);

            //#pragma omp parallel for
            for (k = 0; k < height; k++)
            {
                for (j = 0; j < width; j++)
                {
                    outImg.Y[j + k * width] = img.Y[j + k * width];
                }
            }

            if (param == 0)
            {
                //#pragma omp parallel for
                for (k = 0; k < heightc; k++)
                {
                    for (j = 0; j < widthc; j++)
                    {
                        outImg.U[j + k * widthc] = 128;
                        outImg.V[j + k * widthc] = 128;
                    }
                }
            }
            else {
                //#pragma omp parallel for
                for (k = 0; k < heightc; k++)
                {
                    for (j = 0; j < widthc; j++)
                    {
                        outImg.U[j + k * widthc] = (byte)((img.U[2 * j + 2 * k * width] + img.U[2 * j + 1 + 2 * k * width] + img.U[2 * j + (2 * k + 1) * width] + img.U[2 * j + 1 + (2 * k + 1) * width] + 2) / 4);
                        outImg.V[j + k * widthc] = (byte)((img.V[2 * j + 2 * k * width] + img.V[2 * j + 1 + 2 * k * width] + img.V[2 * j + (2 * k + 1) * width] + img.V[2 * j + 1 + (2 * k + 1) * width] + 2) / 4);
                    }
                }
            }

            return outImg;
        }

        /// <summary>
        /// 引数に指定したYUVインスタンスのディープコピーを作成するメソッド
        /// </summary>
        /// <param name="target">ディープコピーを行うイメージ</param>
        /// <returns>ディープコピーされたイメージ</returns>
        public static YUV DeepCopy(YUV target)
        {

            YUV result;
            BinaryFormatter b = new BinaryFormatter();

            MemoryStream mem = new MemoryStream();

            try
            {
                b.Serialize(mem, target);
                mem.Position = 0;
                result = (YUV)b.Deserialize(mem);
            }
            finally
            {
                mem.Close();
            }

            return result;

        }

    }
}
