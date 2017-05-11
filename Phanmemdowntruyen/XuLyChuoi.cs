using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Phanmemdowntruyen
{
    class XuLyChuoi
    {
        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string KiemTraDuoiAnh (string link)
        {
            if(link.IndexOf(".jpg")>-1)
            {
                return "jpg";
            }
            if (link.IndexOf(".jpeg") > -1)
            {
                return "jpeg";
            }
            if (link.IndexOf(".png") > -1)
            {
                return "png";
            }
            else
            {
                return ".ttf";
            }
        }
        public static string ToFirstUpper(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }
    }
}
