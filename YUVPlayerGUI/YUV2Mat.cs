using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace YUVImgProc
{
    class YUV2Mat
    {
        /// <summary>
        /// <para>YUVインスタンスをMatインスタンスに変換します</para>
        /// </summary>
        /// <param name="YUVImg">変換を行いたいYUVインスタンス</param>
        /// <param name="fullYUV">YUVの変換形式を選択(ITU-R BT.601 規定YCbCr=0 , 8bitフルスケールYUV=1)</param>
        /// <param name="mode">低速処理＝0,高速処理＝1(YUV420を処理する際の高速化設定)</param>
        /// <returns>変換を行ったインスタンス(CV_8SC3形式,並び順:BGR)</returns>
        public static Mat YUV2MatBGR(YUV YUVImg, int fullYUV, int mode)
        {

            int j, k;
            int width, height;
            int widthc, heightc;
            int r, g, b;

            width = YUVImg.Width;
            height = YUVImg.Height;
            widthc = YUVImg.Widthc;
            heightc = YUVImg.Heightc;

            Mat MatImg = new Mat(new Size(width, height), MatType.CV_8UC3);
            var step = MatImg.Step();

            YUV imgTmp;

            if (YUVImg.ColorFormat == 420)
            {
                if (mode == 0)
                {
                    imgTmp = YUVProc.TransImage420To444(YUVImg, 1);
                }
                else
                {
                    imgTmp = YUVProc.TransImage420To444Fast(YUVImg, 1);
                }
            }
            else
            {
                imgTmp = YUVProc.DeepCopy(YUVImg);
            }

            if (fullYUV != 0)
            {			//8bitフルスケールYUVに変換
                unsafe
                {
                    byte* MatImgData = MatImg.DataPointer;
                    for (k = 0; k < height; k++)
                    {

                        for (j = 0; j < width; j++)
                        {
                            r = YUVProc.RoundingByte(imgTmp.Y[j + (k * width)] + 1.402 * (imgTmp.V[j + (k * width)] - 128));
                            g = YUVProc.RoundingByte(imgTmp.Y[j + (k * width)] - 0.344136 * (imgTmp.U[j + (k * width)] - 128) - 0.714136 * (imgTmp.V[j + (k * width)] - 128));
                            b = YUVProc.RoundingByte(imgTmp.Y[j + (k * width)] + 1.772 * (imgTmp.U[j + (k * width)] - 128));

                            MatImgData[step * k + j * 3 + 0] = (byte)b;
                            MatImgData[step * k + j * 3 + 1] = (byte)g;
                            MatImgData[step * k + j * 3 + 2] = (byte)r;

                        }
                    }
                }

            }
            else
            {						//ITU-R BT.601 規定YCbCrに変換
                unsafe
                {
                    byte* MatImgData = MatImg.DataPointer;
                    for (k = 0; k < height; k++)
                    {

                        for (j = 0; j < width; j++)
                        {
                            r = YUVProc.RoundingByte(1.164 * (imgTmp.Y[j + (k * width)] - 16) + 1.596 * (imgTmp.V[j + (k * width)] - 128));
                            g = YUVProc.RoundingByte(1.164 * (imgTmp.Y[j + (k * width)] - 16) - 0.391 * (imgTmp.U[j + (k * width)] - 128) - 0.813 * (imgTmp.V[j + (k * width)] - 128));
                            b = YUVProc.RoundingByte(1.164 * (imgTmp.Y[j + (k * width)] - 16) + 2.018 * (imgTmp.U[j + (k * width)] - 128));

                            MatImgData[step * k + j * 3 + 0] = (byte)b;
                            MatImgData[step * k + j * 3 + 1] = (byte)g;
                            MatImgData[step * k + j * 3 + 2] = (byte)r;

                        }
                    }
                }
                

            }

            return MatImg;
        }

        /// <summary>
        /// <para>MatインスタンスをYUVインスタンスに変換します</para>
        /// </summary>
        /// <param name="MatImg">変換したいMatインスタンス</param>
        /// <param name="colorSelect">0でYUV420、0以外でYUV444に変換</param>
        /// <returns></returns>
        public static YUV MatBGR2YUV(Mat MatImg, int colorSelect)
        {

            int j, k;
            int width, height;

            width = MatImg.Width;
            height = MatImg.Height;

            var step = MatImg.Step();


            YUV imgTmp = new YUV(width, height, 444);

            Mat distImg = null;

            Cv2.CvtColor(MatImg, distImg, ColorConversionCodes.YUV2BGR);

            unsafe
            {
                byte* datImgData = distImg.DataPointer;
                for (k = 0; k < height; k++)
                {
                    for (j = 0; j < width; j++)
                    {
                        imgTmp.Y[j + k * width] = datImgData[step * k + j * 3];
                        imgTmp.V[j + k * width] = datImgData[step * k + j * 3 + 1];
                        imgTmp.U[j + k * width] = datImgData[step * k + j * 3 + 2];

                    }
                }
            }

            YUV YUVImg;

            if (colorSelect == 0)
            {
                YUVImg = YUVProc.TransImage444To420(imgTmp, 1);
            }
            else
            {
                YUVImg = YUVProc.DeepCopy(imgTmp);
            }

            return YUVImg;
        }
    }
}
