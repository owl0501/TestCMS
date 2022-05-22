using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace TestCMS.Helper
{
    public class ImageHelper
    {
        public static string SaveImagePath(string fileName)
        {
            return $@"\UploadFolder\{fileName}";
        }

        public static Bitmap ResizeImage(string imgPath, EResizeMode cutType, int width = 0, int height = 0)
        {
            using (var imgBmp = new Bitmap(imgPath))
            {
                var oWidth = imgBmp.Width;
                var oHeight = imgBmp.Height;

                int oh, ow, x, y;
                if (cutType == EResizeMode.自動)
                {
                    if (width == 0 && height >= 0)
                        cutType = EResizeMode.固定高等比例縮放;
                    else if (height == 0 && width >= 0)
                        cutType = EResizeMode.固定寬等比例縮放;
                    else if (width >= 0 && height >= 0)
                        cutType = EResizeMode.固定寬高切中間;
                    else
                        throw new Exception("請指定正確的寬高");
                }
                switch (cutType)
                {
                    case EResizeMode.自動:
                        break;
                    case EResizeMode.固定寬等比例縮放:
                        if (width <= 0)
                            throw new Exception("寬度指定錯誤");
                        height = width * oHeight / oWidth;
                        break;
                    case EResizeMode.固定高等比例縮放:
                        if (height <= 0)
                            throw new Exception("高度指定錯誤");
                        width = height * oWidth / oHeight;
                        break;
                    case EResizeMode.固定寬高切中間:
                        if ((double)oWidth / (double)oHeight > (double)width / (double)height)
                        {
                            oh = oHeight;
                            ow = oHeight * width / height;
                            y = 0;
                            x = (oWidth - ow) / 2;
                        }
                        else
                        {
                            ow = oWidth;
                            oh = oWidth * height / width;
                            x = 0;
                            y = (oHeight - oh) / 2;
                        }
                        var cutBitmap = new Bitmap(width, height);
                        var graphics = Graphics.FromImage(cutBitmap);
                        graphics.DrawImage(imgBmp, new System.Drawing.Rectangle(0, 0, width, height),
                        new System.Drawing.Rectangle(x, y, ow, oh),
                        System.Drawing.GraphicsUnit.Pixel);
                        cutBitmap.SetResolution(72, 72);
                        return cutBitmap;
                }
                var newImg = new Bitmap(imgBmp, width, height);
                newImg.SetResolution(72, 72);
                return newImg;
            }
        }
        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                var bytes = ms.GetBuffer();
                return bytes;
            }
        }
    }
}
