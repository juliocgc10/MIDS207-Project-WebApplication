using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDS207_Project.BusinessLogic
{
    public static class DataURI
    {

        public static byte[] GetFile(string dataURI)
        {

            List<string> scheme = dataURI.Split(new char[] { ';' }).ToList();
            string codeFile = scheme.Where(part => part.Contains("base64")).FirstOrDefault();

            if (!string.IsNullOrEmpty(codeFile))
            {

                string dataFile = codeFile.Split(new char[] { ',' }).ElementAt(1);
                return Convert.FromBase64String(dataFile);

            }

            return new byte[] { };

        }

        public static string GetMimeType(string dataURI)
        {

            List<string> scheme = dataURI.Split(new char[] { ';' }).ToList();
            string mimeType = scheme.Where(part => part.Contains("data")).FirstOrDefault();

            return !string.IsNullOrEmpty(mimeType) ? mimeType.Split(new char[] { ':' }).ElementAt(1) : null;

        }
    }
}
