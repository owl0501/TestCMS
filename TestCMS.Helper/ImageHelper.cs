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
