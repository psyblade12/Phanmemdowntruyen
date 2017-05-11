using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phanmemdowntruyen
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private string _tentruyen;
        private string _tap;
        private string _dai;
        private string _cao;
        private string[] sochapter;
        List<string> dsdownload;
        int demhienthi;
        bool lanhienthidau = true;
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

        public string Tap
        {
            get
            {
                return _tap;
            }

            set
            {
                _tap = value;
            }
        }

        public string Dai
        {
            get
            {
                return _dai;
            }

            set
            {
                _dai = value;
            }
        }

        public string Cao1
        {
            get
            {
                return _cao;
            }

            set
            {
                _cao = value;
            }
        }

        public Form4(string _tentruyen, string _tap, string _dai, string _cao)
        {
            InitializeComponent();
            this.Tentruyen = _tentruyen;
            this.Tap = _tap;
            this.Dai = _dai;
            this.Cao1 = _cao;
        }
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

        private string xoakituthua(string chuoi)
        {
            string ketqua = chuoi;
            int tam1 = ketqua.IndexOf("?");
            if (tam1 > -1)
            {
                int tam2 = ketqua.IndexOf("/>");
                ketqua = ketqua.Substring(0, tam1) + ketqua.Substring(tam2 + 2);
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

        

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                string tentruyen = txtTenTruyenMGK.Text;
                //MessageBox.Show(_tentruyen);
                //MessageBox.Show(_tap);
                //MessageBox.Show(_dai);
                //MessageBox.Show(_cao);
                //string tentruyen = txtTenTruyenMGK.Text;
                _tentruyen = _tentruyen.ToLower();
                _tentruyen = _tentruyen.Trim();
                _tentruyen = _tentruyen.Replace(" ", "-");
                txtChapter.Text = _tap;
                txtTenTruyenMGK.Text = _tentruyen;
                using (var client = new WebClient())
                {
                    string url = "http://mangak.info/" + _tentruyen + "-chap-" + _tap.Replace(".", "-") + "/";
                    if (RemoteFileExists(url) == true)
                    {
                        client.DownloadFile(url, "file.txt");
                        using (StreamReader sr = new StreamReader("file.txt"))
                        {
                            String line = sr.ReadToEnd();
                            string tam = "<div class=\"vung_doc\">";
                            int vitridau = line.IndexOf(tam);
                            line = line.Substring(vitridau);
                            int vitridivgannhat = line.IndexOf("</div>");
                            line = line.Substring(0, vitridivgannhat);
                            line = line.Replace(tam, "");
                            line = line.Replace("</div>", "");
                            line = line.Trim();
                            line = line.Replace("\r\n", ";");
                            line = line.Replace("<img src=\"", "");
                            line = xoakituthua(line);
                            line = xoakituthua2(line);
                            line = line.Replace("jpg", "jpg?imgmax=" + txtDoLon.Text);
                            line = line.Replace("jpeg", "jpeg?imgmax=" + txtDoLon.Text);
                            line = line.Replace("png", "png?imgmax=" + txtDoLon.Text);
                            //string[] links = line.Split(';');
                            dsdownload = line.Split(';').ToList();
                            if (radioButton1.Checked == true)
                            {
                                button6.Visible = true;
                                button5.Visible = true;
                                lblTrang.Visible = true;
                                txtTrang.Visible = true;
                                btnChonTrang.Visible = true;
                                lblTrang.Text = "Trang: " + (demhienthi + 1).ToString();
                                flowLayoutPanel1.Controls.Clear();
                                PictureBox pb = new PictureBox();
                                pb.ImageLocation = dsdownload[demhienthi];
                                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                pb.MouseClick += new MouseEventHandler(pb_click);
                                flowLayoutPanel1.Controls.Add(pb);
                                Label lb = new Label();
                                lb.Text = " ";
                                flowLayoutPanel1.Controls.Add(lb);
                                lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
                            }
                            else
                            {
                                button6.Visible = false;
                                button5.Visible = false;
                                lblTrang.Visible = false;
                                txtTrang.Visible = false;
                                btnChonTrang.Visible = false;
                                flowLayoutPanel1.Controls.Clear();
                                foreach (string s in dsdownload)
                                {
                                    PictureBox pb = new PictureBox();
                                    pb.ImageLocation = s;
                                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                    flowLayoutPanel1.Controls.Add(pb);
                                }
                            }
                        }
                    }

                }
                using (var client = new WebClient())
                {
                    string url = "http://mangak.info/" + txtTenTruyenMGK.Text + "-chap-" + txtChapter.Text + "/";
                    client.DownloadFile(url, "file.txt");
                    using (StreamReader sr = new StreamReader("file.txt"))
                    {
                        String line = sr.ReadToEnd();
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
                        if (line.IndexOf(" - Tập") > -1)
                        {
                            line = line + ";((((";
                            line = XulyHTML.XoaChuTap(line);
                        }
                        sochapter = line.Split(';');
                        comboBox1.DataSource = sochapter;
                        comboBox1.SelectedItem = txtChapter.Text;
                    }
                }
                lanhienthidau = false;
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _tentruyen = _tentruyen.ToLower();
                _tentruyen = _tentruyen.Trim();
                _tentruyen = _tentruyen.Replace(" ", "-");
                txtChapter.Text = comboBox1.SelectedItem.ToString();
                txtTenTruyenMGK.Text = _tentruyen;
                using (var client = new WebClient())
                {
                    string url = "http://mangak.info/" + _tentruyen + "-chap-" + comboBox1.SelectedItem.ToString().Replace(".", "-") + "/";
                    if (RemoteFileExists(url) == true)
                    {
                        client.DownloadFile(url, "file.txt");
                        using (StreamReader sr = new StreamReader("file.txt"))
                        {
                            String line = sr.ReadToEnd();
                            string tam = "<div class=\"vung_doc\">";
                            int vitridau = line.IndexOf(tam);
                            line = line.Substring(vitridau);
                            int vitridivgannhat = line.IndexOf("</div>");
                            line = line.Substring(0, vitridivgannhat);
                            line = line.Replace(tam, "");
                            line = line.Replace("</div>", "");
                            line = line.Trim();
                            line = line.Replace("\r\n", ";");
                            line = line.Replace("<img src=\"", "");
                            line = xoakituthua(line);
                            line = xoakituthua2(line);
                            line = line.Replace("jpg", "jpg?imgmax=" + txtDoLon.Text);
                            line = line.Replace("jpeg", "jpeg?imgmax=" + txtDoLon.Text);
                            line = line.Replace("png", "png?imgmax=" + txtDoLon.Text);
                            dsdownload = line.Split(';').ToList();
                            if (radioButton1.Checked == true)
                            {
                                button6.Visible = true;
                                button5.Visible = true;
                                flowLayoutPanel1.Controls.Clear();
                                PictureBox pb = new PictureBox();
                                pb.ImageLocation = dsdownload[demhienthi];
                                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                pb.MouseClick += new MouseEventHandler(pb_click);
                                flowLayoutPanel1.Controls.Add(pb);
                                Label lb = new Label();
                                lb.Text = " ";
                                flowLayoutPanel1.Controls.Add(lb);
                            }
                            else
                            {
                                button6.Visible = false;
                                button5.Visible = false;
                                flowLayoutPanel1.Controls.Clear();
                                foreach (string s in dsdownload)
                                {
                                    PictureBox pb = new PictureBox();
                                    pb.ImageLocation = s;
                                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                    flowLayoutPanel1.Controls.Add(pb);
                                }
                            }
                            //MessageBox.Show(line.IndexOf("\n").ToString());
                            //richTextBox1.Text = line;
                            //MessageBox.Show(vitridau.ToString());
                        }
                        lanhienthidau = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lanhienthidau == false)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        string url = "http://mangak.info/" + _tentruyen + "-chap-" + comboBox1.SelectedItem.ToString().Replace(".", "-") + "/";
                        if (RemoteFileExists(url) == true)
                        {
                            client.DownloadFile(url, "file.txt");
                            using (StreamReader sr = new StreamReader("file.txt"))
                            {
                                String line = sr.ReadToEnd();
                                string tam = "<div class=\"vung_doc\">";
                                int vitridau = line.IndexOf(tam);
                                line = line.Substring(vitridau);
                                int vitridivgannhat = line.IndexOf("</div>");
                                line = line.Substring(0, vitridivgannhat);
                                line = line.Replace(tam, "");
                                line = line.Replace("</div>", "");
                                line = line.Trim();
                                line = line.Replace("\r\n", ";");
                                line = line.Replace("<img src=\"", "");
                                line = xoakituthua(line);
                                line = xoakituthua2(line);
                                line = line.Replace("jpg", "jpg?imgmax=" + txtDoLon.Text);
                                line = line.Replace("jpeg", "jpeg?imgmax=" + txtDoLon.Text);
                                line = line.Replace("png", "png?imgmax=" + txtDoLon.Text);
                                dsdownload = line.Split(';').ToList();
                                demhienthi = 0;
                                if (radioButton1.Checked == true)
                                {
                                    button6.Visible = true;
                                    button5.Visible = true;
                                    txtTrang.Visible = true;
                                    btnChonTrang.Visible = true;
                                    flowLayoutPanel1.Controls.Clear();
                                    PictureBox pb = new PictureBox();
                                    pb.ImageLocation = dsdownload[demhienthi];
                                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                    pb.MouseClick += new MouseEventHandler(pb_click);
                                    flowLayoutPanel1.Controls.Add(pb);
                                }
                                else
                                {
                                    button6.Visible = false;
                                    button5.Visible = false;
                                    txtTrang.Visible = false;
                                    btnChonTrang.Visible = false;
                                    flowLayoutPanel1.Controls.Clear();
                                    foreach (string s in dsdownload)
                                    {
                                        PictureBox pb = new PictureBox();
                                        pb.ImageLocation = s;
                                        pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                        flowLayoutPanel1.Controls.Add(pb);
                                    }
                                }
                                //MessageBox.Show(line.IndexOf("\n").ToString());
                                //richTextBox1.Text = line;
                                //MessageBox.Show(vitridau.ToString());
                            }
                        }
                    }
                    lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
                    txtChapter.Text = comboBox1.SelectedItem.ToString();
                }
                catch
                {
                    MessageBox.Show("Có lổi xảy ra");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                demhienthi = 0;
                for (int i = 0; i < sochapter.Length; i++)
                {
                    if (sochapter[i] == txtChapter.Text)
                    {
                        index = i;
                        break;
                    }
                }
                if (index < sochapter.Length - 1)
                {
                    comboBox1.SelectedItem = sochapter[index + 1];
                    txtChapter.Text = sochapter[index + 1];


                    using (var client = new WebClient())
                    {
                        string url = "http://mangak.info/" + _tentruyen + "-chap-" + comboBox1.SelectedItem.ToString() + "/";
                        if (RemoteFileExists(url) == true)
                        {
                            client.DownloadFile(url, "file.txt");
                            using (StreamReader sr = new StreamReader("file.txt"))
                            {
                                String line = sr.ReadToEnd();
                                string tam = "<div class=\"vung_doc\">";
                                int vitridau = line.IndexOf(tam);
                                line = line.Substring(vitridau);
                                int vitridivgannhat = line.IndexOf("</div>");
                                line = line.Substring(0, vitridivgannhat);
                                line = line.Replace(tam, "");
                                line = line.Replace("</div>", "");
                                line = line.Trim();
                                line = line.Replace("\r\n", ";");
                                line = line.Replace("<img src=\"", "");
                                line = xoakituthua(line);
                                line = xoakituthua2(line);
                                line = line.Replace("jpg", "jpg?imgmax=" + txtDoLon.Text);
                                line = line.Replace("jpeg", "jpeg?imgmax=" + txtDoLon.Text);
                                line = line.Replace("png", "png?imgmax=" + txtDoLon.Text);
                                dsdownload = line.Split(';').ToList();
                                if (radioButton1.Checked == true)
                                {
                                    button6.Visible = true;
                                    button5.Visible = true;
                                    txtTrang.Visible = true;
                                    btnChonTrang.Visible = true;
                                    flowLayoutPanel1.Controls.Clear();
                                    PictureBox pb = new PictureBox();
                                    pb.ImageLocation = dsdownload[demhienthi];
                                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                    pb.MouseClick += new MouseEventHandler(pb_click);
                                    flowLayoutPanel1.Controls.Add(pb);
                                }
                                else
                                {
                                    button6.Visible = false;
                                    button5.Visible = false;
                                    txtTrang.Visible = false;
                                    btnChonTrang.Visible = false;
                                    flowLayoutPanel1.Controls.Clear();
                                    foreach (string s in dsdownload)
                                    {
                                        PictureBox pb = new PictureBox();
                                        pb.ImageLocation = s;
                                        pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                        flowLayoutPanel1.Controls.Add(pb);
                                        Label lb = new Label();
                                        lb.Text = " ";
                                        flowLayoutPanel1.Controls.Add(lb);
                                    }
                                }
                                //MessageBox.Show(line.IndexOf("\n").ToString());
                                //richTextBox1.Text = line;
                                //MessageBox.Show(vitridau.ToString());
                            }
                            lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }
        void pb_click(object sender, EventArgs e)
        {
            try
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Right)
                {
                    if (demhienthi > 0)
                    {
                        flowLayoutPanel1.Controls.Clear();
                        demhienthi = demhienthi - 1;
                        PictureBox pb = new PictureBox();
                        pb.ImageLocation = dsdownload[demhienthi];
                        pb.SizeMode = PictureBoxSizeMode.AutoSize;
                        pb.MouseClick += new MouseEventHandler(pb_click);
                        flowLayoutPanel1.Controls.Add(pb);
                        Label lb = new Label();
                        lb.Text = "      ";
                        flowLayoutPanel1.Controls.Add(lb);
                        lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
                    }
                }
                if (me.Button == MouseButtons.Left)
                {
                    if (demhienthi < dsdownload.Count() - 1)
                    {
                        flowLayoutPanel1.Controls.Clear();
                        demhienthi = demhienthi + 1;
                        PictureBox pb = new PictureBox();
                        pb.ImageLocation = dsdownload[demhienthi];
                        pb.SizeMode = PictureBoxSizeMode.AutoSize;
                        pb.MouseClick += new MouseEventHandler(pb_click);
                        flowLayoutPanel1.Controls.Add(pb);
                        Label lb = new Label();
                        lb.Text = "      ";
                        flowLayoutPanel1.Controls.Add(lb);
                        lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                demhienthi = 0;
                for (int i = 0; i < sochapter.Length; i++)
                {
                    if (sochapter[i] == txtChapter.Text)
                    {
                        index = i;
                        break;
                    }
                }
                if (index > 0)
                {
                    comboBox1.SelectedItem = sochapter[index - 1];
                    txtChapter.Text = sochapter[index - 1];

                    using (var client = new WebClient())
                    {
                        string url = "http://mangak.info/" + _tentruyen + "-chap-" + comboBox1.SelectedItem.ToString() + "/";
                        if (RemoteFileExists(url) == true)
                        {
                            client.DownloadFile(url, "file.txt");
                            using (StreamReader sr = new StreamReader("file.txt"))
                            {
                                String line = sr.ReadToEnd();
                                string tam = "<div class=\"vung_doc\">";
                                int vitridau = line.IndexOf(tam);
                                line = line.Substring(vitridau);
                                int vitridivgannhat = line.IndexOf("</div>");
                                line = line.Substring(0, vitridivgannhat);
                                line = line.Replace(tam, "");
                                line = line.Replace("</div>", "");
                                line = line.Trim();
                                line = line.Replace("\r\n", ";");
                                line = line.Replace("<img src=\"", "");
                                line = xoakituthua(line);
                                line = xoakituthua2(line);
                                line = line.Replace("jpg", "jpg?imgmax=" + txtDoLon.Text);
                                line = line.Replace("jpeg", "jpeg?imgmax=" + txtDoLon.Text);
                                line = line.Replace("png", "png?imgmax=" + txtDoLon.Text);
                                dsdownload = line.Split(';').ToList();
                                if (radioButton1.Checked == true)
                                {
                                    button6.Visible = true;
                                    button5.Visible = true;
                                    txtTrang.Visible = true;
                                    btnChonTrang.Visible = true;
                                    flowLayoutPanel1.Controls.Clear();
                                    PictureBox pb = new PictureBox();
                                    pb.ImageLocation = dsdownload[demhienthi];
                                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                    pb.MouseClick += new MouseEventHandler(pb_click);
                                    flowLayoutPanel1.Controls.Add(pb);
                                    Label lb = new Label();
                                    lb.Text = " ";
                                    flowLayoutPanel1.Controls.Add(lb);
                                }
                                else
                                {
                                    button6.Visible = false;
                                    button5.Visible = false;
                                    flowLayoutPanel1.Controls.Clear();
                                    foreach (string s in dsdownload)
                                    {
                                        PictureBox pb = new PictureBox();
                                        pb.ImageLocation = s;
                                        pb.SizeMode = PictureBoxSizeMode.AutoSize;
                                        flowLayoutPanel1.Controls.Add(pb);
                                    }
                                }
                                lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
                                //MessageBox.Show(line.IndexOf("\n").ToString());
                                //richTextBox1.Text = line;
                                //MessageBox.Show(vitridau.ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void Pb_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            

        }

        private void Form4_SizeChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            { 
                Form5 form = new Form5(_tentruyen, comboBox1.SelectedItem.ToString(),dsdownload);
                form.Show();
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (demhienthi<dsdownload.Count()-1)
            {
                flowLayoutPanel1.Controls.Clear();
                demhienthi = demhienthi + 1;
                PictureBox pb = new PictureBox();
                pb.ImageLocation = dsdownload[demhienthi];
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb);
                Label lb = new Label();
                lb.Text = "      ";
                flowLayoutPanel1.Controls.Add(lb);
                lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (demhienthi >0)
            {
                flowLayoutPanel1.Controls.Clear();
                demhienthi = demhienthi - 1;
                PictureBox pb = new PictureBox();
                pb.ImageLocation = dsdownload[demhienthi];
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb);
                Label lb = new Label();
                lb.Text = "      ";
                flowLayoutPanel1.Controls.Add(lb);
                lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                demhienthi = 0;
                button6.Visible = true;
                button5.Visible = true;
                lblTrang.Visible = true;
                txtTrang.Visible = true;
                btnChonTrang.Visible = true;
                lblTrang.Text = "Trang: " + (demhienthi + 1).ToString();
                flowLayoutPanel1.Controls.Clear();
                PictureBox pb = new PictureBox();
                pb.ImageLocation = dsdownload[demhienthi];
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb);
                Label lb = new Label();
                lb.Text = " ";
                flowLayoutPanel1.Controls.Add(lb);
                lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                demhienthi = 0;
                button6.Visible = false;
                button5.Visible = false;
                lblTrang.Visible = false;
                txtTrang.Visible = false;
                btnChonTrang.Visible = false;
                flowLayoutPanel1.Controls.Clear();
                foreach (string s in dsdownload)
                {
                    PictureBox pb = new PictureBox();
                    pb.ImageLocation = s;
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    flowLayoutPanel1.Controls.Add(pb);
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void txtTenTruyenMGK_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTrang_Click(object sender, EventArgs e)
        {

        }

        private void txtDoLon_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            demhienthi = Convert.ToInt16(txtTrang.Text)-1;
            if (demhienthi >= 0 && demhienthi < dsdownload.Count)
            {
                PictureBox pb = new PictureBox();
                pb.ImageLocation = dsdownload[demhienthi];
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb);
                Label lb = new Label();
                lb.Text = "      ";
                flowLayoutPanel1.Controls.Add(lb);
                lblTrang.Text = "Trang: " + (demhienthi + 1) + "/" + (dsdownload.Count());
            }
        }
    }
}
