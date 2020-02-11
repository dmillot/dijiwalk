using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace DijiWalk.Common.Clouds
{
    public static class ImageConverter
    {
        public static byte[] Base64ToByte(string base64String)
        {
            // Convert base 64 string to byte[]
            return Convert.FromBase64String(base64String);
        }
    }
}
