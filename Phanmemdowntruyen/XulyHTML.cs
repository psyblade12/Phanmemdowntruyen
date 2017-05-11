using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phanmemdowntruyen
{
    class XulyHTML
    {
        public static string PreXulyTimKiemMGK(string textnhapvao)
        {
            string ketqua = textnhapvao;
            int indexof1 = ketqua.IndexOf("<div class=\"update_item\">");
            ketqua = ketqua.Substring(indexof1);
            indexof1 = ketqua.IndexOf("<div class=\"clearfix\">");
            ketqua = ketqua.Substring(0, indexof1);
            ketqua = ketqua.Replace("&#8211;","");
            ketqua = ketqua.Replace("title=\"\"","");
            indexof1 = ketqua.IndexOf("title=\"");
            ketqua = ketqua.Substring(indexof1+7);
            return ketqua;
        }
        public static string XuLyTimKiemMGK(string textnhapvao)
        {
            string ketqua = textnhapvao;
            int indexof1 = ketqua.IndexOf("title=\"");
            if (indexof1 > -1)
            {
                int indexof2 = ketqua.IndexOf("\">");
                ketqua = ketqua.Substring(0, indexof2)+";" + ketqua.Substring(indexof1 + 7);
                ketqua = XuLyTimKiemMGK(ketqua);
            }
            return ketqua;
        }
        public static string XulyHauTimKiemMGK(string textnhapvao)
        {
            string ketqua = textnhapvao;
            int indexof1 = ketqua.IndexOf("\">");
            ketqua = ketqua.Substring(0, indexof1);
            List<string> stringtam = new List<string>();
            stringtam = ketqua.Split(';').ToList();
            stringtam = stringtam.Distinct().ToList();
            ketqua = "";
            foreach (string s in stringtam)
            {
                ketqua = ketqua + s + ";";
            }
            indexof1 = ketqua.LastIndexOf(";");
            ketqua = ketqua.Remove(indexof1, 1);
            ketqua = ketqua.Replace("(","");
            ketqua = ketqua.Replace(")", "");
            return ketqua;
        }
        public static string XoaChuTap(string textnhapvao)
        {
            string ketqua = textnhapvao;
            int index = ketqua.IndexOf(" - Tập");
            if (index > -1)
            {
                string tam = ketqua.Substring(index + 9);
                int index2 = tam.IndexOf(";");
                if (index2 > -1)
                {
                    ketqua = ketqua.Substring(0, index) + tam.Substring(index2);
                    ketqua = XoaChuTap(ketqua);
                }
            }
            ketqua = ketqua.Replace(" - Tập 1;((((", "");
            return ketqua;
        }
    }
}
