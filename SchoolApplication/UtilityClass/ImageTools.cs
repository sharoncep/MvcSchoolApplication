using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace SchoolApplication.UtilityClass
{
    public static class ImageTools
    {

        public static bool IsImage(object value)
        {
            int maxContent = 1024 * 1024; //1 MB
            string[] sAllowedExt = new string[] { ".jpg", ".gif", ".png", ".jpeg" };


            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;
            if (!sAllowedExt.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                return false;
            }
            if (file.ContentLength > maxContent)
            {
                return false;
            }
            return true;
        }
    }
}