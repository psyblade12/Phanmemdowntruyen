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

namespace Phanmemdowntruyen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int demhienthi = 0;
        string duongdannhapvao = "D:\\";
        StringBuilder duongdan = new StringBuilder();
        List<string> dstruyen = new List<string>();
        List<string> dstruyentam = new List<string>();
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
            using (var client = new WebClient())
            {
                try
                {
                    for (int i = 1; i <= 210; i++)
                    {
                        string url = "http://comicserver.vuilen.com/imagecache/w480/" + txtTentruyen.Text + "/tap" + txtTentap.Text + "/img/Untitled-" + i + ".jpg";
                        if (RemoteFileExists(url) == true)
                        {
                            duongdan.AppendLine(url);
                            client.DownloadFile(url, duongdannhapvao + i + ".jpg");
                        }
                        else
                        {
                            continue;
                        }
                    }
                    MessageBox.Show("Đã tải xong");
                    richTextBox1.Text = duongdan.ToString();
                }
                catch
                {
                    MessageBox.Show("Có chút lỗi xảy ra");
                }
            }
        }

        
        private void btnChonFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dl = new FolderBrowserDialog();
            dl.ShowDialog();
            duongdannhapvao = dl.SelectedPath;
            label3.Text = duongdannhapvao;
            label3.Text = duongdannhapvao;
            duongdannhapvao = duongdannhapvao.Replace(@"\", @"\\");
            duongdannhapvao = duongdannhapvao + @"\\";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string test = "http://comicserver.vuilen.com/imagecache/w480/conan/tap10/img/Untitled-200.jpg";
            bool a = RemoteFileExists(test);
        }

        private void txtTentruyen_TextChanged(object sender, EventArgs e)
        {
            string url = "http://comicserver.vuilen.com/imagecache/w150/" + txtTentruyen.Text + "/tap" + txtTentap.Text + "/img/bia.jpg";
            if (RemoteFileExists(url) == true)
            {
                pictureBox1.Load(url);
            }
            else
            {
                MessageBox.Show("Không có tập này");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(duongdannhapvao);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //flowLayoutPanel1.Controls.Clear();
            //for (int i = 1; i<200; i++)
            //{
            //    PictureBox pb = new PictureBox();
            //    pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + txtTentruyen.Text + "/tap" + txtTentap.Text + "/img/Untitled-" + i + ".jpg";
            //    pb.Width = 480;
            //    pb.Height = 800;
            //    //pb.MouseClick += new MouseEventHandler(pb_click);
            //    flowLayoutPanel1.Controls.Add(pb);
            //}
            //Form3 form = new Form3();
            Form3 form = new Form3(comboBox1.SelectedItem.ToString(), txtTentap.Text);
            form.Show();
        }
        void pb_click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            string chuoi = XuLyChuoi.ToFirstUpper(pb.Tag.ToString());
            Form3 form = new Form3(chuoi, Convert.ToString(1));
            form.Show();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            StringBuilder strb = new StringBuilder();
            dstruyen.Add("conan");
            dstruyen.Add("dragonball");
            dstruyen.Add("jindo");
            comboBox1.DataSource = dstruyen;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTentruyen.Text = comboBox1.SelectedItem.ToString().Trim().Replace(" ","").ToLower();
            string url = "http://comicserver.vuilen.com/imagecache/w150/" + txtTentruyen.Text + "/tap" + txtTentap.Text + "/img/bia.jpg";
            if (RemoteFileExists(url) == true)
            {
                pictureBox1.Load(url);
            }
            else
            {
                MessageBox.Show("Không có tập này");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder strb = new StringBuilder();
            using (StreamReader sr = new StreamReader("danhsachvuilen.txt"))
            {
                String line = sr.ReadToEnd();
                line = line.Replace("\r\n", ";");
                dstruyen = line.Split(';').ToList();
                comboBox1.DataSource = dstruyen;
                comboBox1.SelectedItem = dstruyen[1];
            }
            for (int i = 0; i < 5; i++)
            {
                PictureBox pb = new PictureBox();
                string url = "http://comicserver.vuilen.com/imagecache/w150/" + dstruyen[i].Trim().Replace(" ", "").ToLower() + "/tap1/img/bia.jpg";
                pb.ImageLocation = url;
                pb.Width = 210;
                pb.Height = 350;
                pb.Tag = dstruyen[i];
                pb.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb);
            }
            string tentruyen = txtTenTruyenMGK.Text;
            tentruyen = tentruyen.ToLower();
            tentruyen = tentruyen.Trim();
            tentruyen = tentruyen.Replace(" ", "-");
            using (var client = new WebClient())
            {
                string url = "http://mangak.info/" + tentruyen + "-chap-" + txtChapter.Text + "/";
                client.DownloadFile(url, "file.txt");
                using (StreamReader sr = new StreamReader("file.txt"))
                {
                    String line = sr.ReadToEnd();
                    richTextBox1.Text = line;
                    string tam = "<select class=\"select-chapter\">";
                    int vitridau = line.IndexOf(tam);
                    line = line.Substring(vitridau);
                    int vitridivgannhat = line.IndexOf("</select>");
                    line = line.Substring(0, vitridivgannhat);
                    //
                    line = line.Replace(tam, "");
                    line = line.Replace("<option value=\"", "");
                    line = line.Trim();
                    line = line.Replace("</option>", "");
                    line = line.Replace("\r\n", ";");
                    line = xoakituthuasochapter(line);
                    richTextBox1.Text = line;
                    string[] sochapter = line.Split(';');
                    comboBox2.DataSource = sochapter;
                    comboBox2.SelectedItem = "1";
                    //tam = "<img src";
                    //vitridau = line.IndexOf(tam);
                    //line = line.Substring(vitridau);
                    //line = line.Replace("<img src=\"", "");
                }
            }
            dstruyentam = dstruyen;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtTentap_TextChanged(object sender, EventArgs e)
        {
            string url = "http://comicserver.vuilen.com/imagecache/w150/" + txtTentruyen.Text + "/tap" + txtTentap.Text + "/img/bia.jpg";
            if (RemoteFileExists(url) == true)
            {
                pictureBox1.Load(url);
            }
            else
            {
                MessageBox.Show("Không có tập này");
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }
        private string xoakituthua(string chuoi)
        {
            string ketqua = chuoi;
            int tam1 = ketqua.IndexOf("?");
            if (tam1 > -1)
            {
                int tam2 = ketqua.IndexOf("/>");
                ketqua = ketqua.Substring(0, tam1) + ketqua.Substring(tam2+2);
                ketqua = xoakituthua(ketqua);
            }
            return ketqua;
        }
        private string xoakituthua2(string chuoi)
        {
            string ketqua = chuoi;
            int tam1 = ketqua.IndexOf("\" alt");
            if (tam1 > -1)
            {
                int tam2 = ketqua.IndexOf("/>");
                ketqua = ketqua.Substring(0, tam1) + ketqua.Substring(tam2 + 2);
                ketqua = xoakituthua2(ketqua);
            }
            return ketqua;
        }
        private string xoakituthuasochapter(string chuoi)
        {
            string ketqua = chuoi;
            int tam1 = ketqua.IndexOf(" chap ");
            if (tam1 > -1)
            {
                int tam2 = ketqua.IndexOf("http");
                ketqua = ketqua.Substring(0, tam2) + ketqua.Substring(tam1 + 6);
                ketqua = xoakituthuasochapter(ketqua);
            }
            return ketqua;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //string tentruyen = txtTenTruyenMGK.Text;
            //tentruyen = tentruyen.ToLower();
            //tentruyen = tentruyen.Trim();
            //tentruyen = tentruyen.Replace(" ","-");
            //using (var client = new WebClient())
            //{
            //    string url = "http://mangak.info/"+tentruyen+"-chap-"+ txtChapter.Text+"/";
            //    client.DownloadFile(url, "file.txt");
            //    using (StreamReader sr = new StreamReader("file.txt"))
            //    {
            //        String line = sr.ReadToEnd();
            //        richTextBox1.Text = line;
            //        string tam = "<div class=\"vung_doc\">";
            //        int vitridau = line.IndexOf(tam);
            //        line = line.Substring(vitridau);
            //        int vitridivgannhat = line.IndexOf("</div>");
            //        line = line.Substring(0,vitridivgannhat);
            //        line = line.Replace(tam, "");
            //        line = line.Replace("</div>", "");
            //        line = line.Trim();
            //        line = line.Replace("\r\n", ";");
            //        line = line.Replace("<img src=\"","");
            //        line = xoakituthua(line);
            //        line = xoakituthua2(line);
            //        line = line.Replace("jpg", "jpg?imgmax=1200");
            //        line = line.Replace("jpeg", "jpeg?imgmax=1200");
            //        string[] links = line.Split(';');
            //        StringBuilder strb = new StringBuilder();
            //        foreach (string s in links)
            //        {
            //            strb.AppendLine(s);
            //        }
            //        richTextBox1.Text = strb.ToString();
            //        flowLayoutPanel1.Controls.Clear();
            //        string linkpreview = links[0].Replace("jpg?imgmax=1200", "jpg?imgmax=230");
            //        pictureBox1.Load(linkpreview);
            //        for (int i = 1; i < links.Length; i++)
            //        {
            //            PictureBox pb = new PictureBox();
            //            pb.ImageLocation = links[i];
            //            pb.Width = Convert.ToInt32(txtDai.Text);
            //            pb.Height = Convert.ToInt32(txtCao.Text);
            //            flowLayoutPanel1.Controls.Add(pb);
            //        }
            //        //MessageBox.Show(line.IndexOf("\n").ToString());
            //        //richTextBox1.Text = line;
            //        //MessageBox.Show(vitridau.ToString());
            //    }
            //}
            Form4 form = new Form4(txtTenTruyenMGK.Text.ToString(), comboBox2.SelectedItem.ToString(),txtDai.Text,txtCao.Text);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string tentruyen = txtTenTruyenMGK.Text;
            tentruyen = tentruyen.ToLower();
            tentruyen = tentruyen.Trim();
            tentruyen = tentruyen.Replace(" ", "-");
            using (var client = new WebClient())
            {
                string url = "http://mangak.info/" + tentruyen + "-chap-" + "1" + "/";
                client.DownloadFile(url, "file.txt");
                using (StreamReader sr = new StreamReader("file.txt"))
                {
                    String line = sr.ReadToEnd();
                    richTextBox1.Text = line;
                    string tam = "<select class=\"select-chapter\">";
                    int vitridau = line.IndexOf(tam);
                    line = line.Substring(vitridau);
                    int vitridivgannhat = line.IndexOf("</select>");
                    line = line.Substring(0, vitridivgannhat);
                    //
                    line = line.Replace(tam,"");
                    line = line.Replace("<option value=\"", "");
                    line = line.Trim();
                    line = line.Replace("</option>", "");
                    line = line.Replace("\r\n", ";");
                    line = xoakituthuasochapter(line);
                    richTextBox1.Text = line;
                    string[] sochapter = line.Split(';');
                    comboBox2.DataSource = sochapter;
                    //tam = "<img src";
                    //vitridau = line.IndexOf(tam);
                    //line = line.Substring(vitridau);
                    //line = line.Replace("<img src=\"", "");
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtChapter.Text = comboBox2.SelectedItem.ToString();
        }
        List<string> arr= new List<string>();
        private void button6_Click(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                //Lấy Danh sách qua internet
                for (int i = 1; i < 1200; i++)
                {
                    string url = "http://comic.vuilen.com/viewbook.php?bookid=" + i;
                    if (RemoteFileExists(url) == true)
                    {
                        client.DownloadFile(url, "file.txt");
                        using (StreamReader sr = new StreamReader("file.txt"))
                        {
                            String line = sr.ReadToEnd();
                            int tam = line.IndexOf("<title>");
                            line = line.Substring(tam);
                            tam = line.IndexOf(" Truyen Tranh");
                            line = line.Substring(7, tam - 7);
                            //line = line.Trim();
                            //line = line.Replace(" ","");
                            //line = line.ToLower();
                            if (line != "")
                            {
                                arr.Add(line);
                                System.IO.File.WriteAllLines("danhsachvuilen.txt", arr);
                            }
                        }
                    }
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("danhsachvuilen.txt"))
            {
                String line = sr.ReadToEnd();
                line = line.Replace("\r\n",";");
                dstruyen = line.Split(';').ToList();
                comboBox1.DataSource = dstruyen;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (demhienthi >= 5)
            {
                flowLayoutPanel1.Controls.Clear();
                demhienthi = demhienthi - 5;
                for (int i = demhienthi; i < demhienthi + 5; i++)
                {
                    PictureBox pb = new PictureBox();
                    string url = "http://comicserver.vuilen.com/imagecache/w150/" + dstruyentam[i].Trim().Replace(" ", "").ToLower() + "/tap1/img/bia.jpg";
                    pb.ImageLocation = url;
                    pb.Width = 210;
                    pb.Height = 350;
                    pb.Tag = dstruyen[i];
                    pb.MouseClick += new MouseEventHandler(pb_click);
                    flowLayoutPanel1.Controls.Add(pb);
                }
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (demhienthi < dstruyentam.Count-5)
            {
                demhienthi = demhienthi + 5;
                flowLayoutPanel1.Controls.Clear();
                for (int i = demhienthi; i < demhienthi + 5; i++)
                {
                    if (i < dstruyentam.Count)
                    {
                        PictureBox pb = new PictureBox();
                        string url = "http://comicserver.vuilen.com/imagecache/w150/" + dstruyentam[i].Trim().Replace(" ", "").ToLower() + "/tap1/img/bia.jpg";
                        pb.ImageLocation = url;
                        pb.Width = 210;
                        pb.Height = 350;
                        pb.Tag = dstruyentam[i];
                        pb.MouseClick += new MouseEventHandler(pb_click);
                        flowLayoutPanel1.Controls.Add(pb);
                    }
                }
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            demhienthi = 0;
            string chuoitimkiem = txtThanhtimkiem.Text.Trim().ToLower();
            dstruyentam = dstruyen.Select(x => x.ToLowerInvariant()).ToList();
            flowLayoutPanel1.Controls.Clear();
            IEnumerable<string> query = dstruyentam.Where(x => x.Contains(chuoitimkiem));
            dstruyentam = query.ToList();
            for (int i = demhienthi; i<demhienthi+5; i++)
            {
                if (i < dstruyentam.Count)
                {
                    PictureBox pb = new PictureBox();
                    string url = "http://comicserver.vuilen.com/imagecache/w150/" + dstruyentam[i].Trim().Replace(" ", "").ToLower() + "/tap1/img/bia.jpg";
                    pb.ImageLocation = url;
                    pb.Width = 210;
                    pb.Height = 350;
                    pb.Tag = dstruyentam[i];
                    pb.MouseClick += new MouseEventHandler(pb_click);
                    flowLayoutPanel1.Controls.Add(pb);
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        void lb_click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            string chuoi = lb.Text.ToLower().Trim().Replace(" ", "-");
            chuoi = XuLyChuoi.convertToUnSign(chuoi);
            Form4 form = new Form4(chuoi, "1", txtDai.Text, txtCao.Text);
            form.Show();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();
            using (var client = new WebClient())
            {
                string chuoitimkiem = txtTenTruyenMGK.Text.Trim().ToLower().Replace(" ", "+");
                string url = "http://mangak.info/?s=" + chuoitimkiem + "&q=" + chuoitimkiem;
                client.DownloadFile(url, "file.txt");
                using (StreamReader sr = new StreamReader("file.txt"))
                {
                    String line = sr.ReadToEnd();
                    line = XulyHTML.PreXulyTimKiemMGK(line);
                    line = XulyHTML.XuLyTimKiemMGK(line);
                    line = XulyHTML.XulyHauTimKiemMGK(line);
                    List<string> dstruyentimthay = line.Split(';').ToList();
                    foreach (string s in dstruyentimthay)
                    {
                        Label lb = new Label();
                        lb.Text = s+"";
                        lb.AutoSize = true;
                        lb.MouseClick += new MouseEventHandler(lb_click);
                        flowLayoutPanel2.Controls.Add(lb);
                        Label lb2 = new Label();
                        lb2.Text = ";";
                        lb2.AutoSize = true;
                        flowLayoutPanel2.Controls.Add(lb2);
                    }
                }
            }
        }
    }
}
