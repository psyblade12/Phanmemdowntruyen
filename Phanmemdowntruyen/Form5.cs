using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phanmemdowntruyen
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        string _tentruyen;
        string _chap;
        List<string> danhsachdownload;
        

        public string Tentruyen
        {
            get
            {
                return _tentruyen;
            }

            set
            {
                _tentruyen = value;
            }
        }

        public string Chap
        {
            get
            {
                return _chap;
            }

            set
            {
                _chap = value;
            }
        }
        public Form5(string _tentruyen, string _chap)
        {
            InitializeComponent();
            this.Tentruyen = _tentruyen;
            this.Chap = _chap;
        }

        public Form5(string _tentruyen, string _chap, List<string> danhsachdownload)
        {
            InitializeComponent();
            this._tentruyen = _tentruyen;
            this._chap = _chap;
            this.danhsachdownload = danhsachdownload;
        }

        string duongdannhapvao ="D:\\";
        private void btnChonFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dl = new FolderBrowserDialog();
            dl.ShowDialog();
            duongdannhapvao = dl.SelectedPath;
            label3.Text = duongdannhapvao;
            //duongdannhapvao = duongdannhapvao.Replace(@"\", @"\\");
            //duongdannhapvao = duongdannhapvao + @"\\";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Khi tải phần mềm sẽ giống như đang bị treo, nhưng thực ra vẫn đang hoạt động. Mở folder chọn tải sẽ thấy điều đó.");
            duongdannhapvao = duongdannhapvao + "\\"+ _tentruyen + "-" + _chap+"\\";
            int dem = 0;
            string duoifile = XuLyChuoi.KiemTraDuoiAnh(danhsachdownload[0]);
            System.IO.Directory.CreateDirectory(duongdannhapvao);
            using (var client = new System.Net.WebClient())
            {
                try
                {
                    foreach (string s in danhsachdownload)
                    {
                        if(Internet.RemoteFileExists(s) == true)
                        {
                            client.DownloadFile(s, duongdannhapvao+dem.ToString("000")+"."+ duoifile);
                            dem = dem + 1;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra.");
                }
            }
            MessageBox.Show("Đã tải xong");
        }
    }
}
