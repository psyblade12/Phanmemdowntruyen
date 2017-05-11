using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;

namespace Phanmemdowntruyen
{
    public partial class Form2 : Form
    {
        string _tentruyen;
        string _sotap;
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

        public string Sotap
        {
            get
            {
                return _sotap;
            }

            set
            {
                _sotap = value;
            }
        }
        

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string _tentruyen, string _sotap)
        {
            InitializeComponent();
            this._tentruyen = _tentruyen;
            this._sotap = _sotap;
        }

        string duongdannhapvao = "D:\\";
        StringBuilder duongdan = new StringBuilder();
        List<string> dstruyen = new List<string>();
        int demsolanhong = 0;

        private bool RemoteFileExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Khi tải phần mềm sẽ giống như đang bị treo, nhưng thực ra vẫn đang hoạt động. Mở folder chọn tải sẽ thấy điều đó.");
            using (var client = new WebClient())
            {
                try
                {
                    for (int z = Convert.ToInt16(textBox1.Text); z <= Convert.ToInt16(textBox2.Text); z++)
                    {
                        string tam = duongdannhapvao;
                        duongdannhapvao = duongdannhapvao+"tap"+z.ToString("000");
                        System.IO.Directory.CreateDirectory(duongdannhapvao);
                        for (int i = 1; i <= 210; i++)
                        {
                            string url = "http://comicserver.vuilen.com/imagecache/w480/" + comboBox1.SelectedItem.ToString().ToLower().Replace(" ","") + "/tap" + z + "/img/Untitled-" + i + ".jpg";
                            if (RemoteFileExists(url) == true)
                            {
                                duongdan.AppendLine(url);
                                client.DownloadFile(url, duongdannhapvao + @"\"+i.ToString("000") + ".jpg");
                            }
                            else
                            {
                                demsolanhong = demsolanhong + 1;
                                if(demsolanhong>5)
                                {
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        duongdannhapvao = tam;
                    }
                    MessageBox.Show("Đã tải xong");
                }
                catch
                {
                    MessageBox.Show("Có chút lỗi xảy ra");
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string tam = Tentruyen;
            using (StreamReader sr = new StreamReader("danhsachvuilen.txt"))
            {
                String line = sr.ReadToEnd();
                line = line.Replace("\r\n", ";");
                dstruyen = line.Split(';').ToList();
                comboBox1.DataSource = dstruyen;
                comboBox1.SelectedItem = tam;
            }
        }

        private void btnChonFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dl = new FolderBrowserDialog();
            dl.ShowDialog();
            duongdannhapvao = dl.SelectedPath;
            label3.Text = duongdannhapvao;
            duongdannhapvao = duongdannhapvao.Replace(@"\", @"\\");
            duongdannhapvao = duongdannhapvao + @"\\";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtTentruyen.Text = comboBox1.SelectedItem.ToString();
        }
    }
}
