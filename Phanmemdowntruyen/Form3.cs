using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phanmemdowntruyen
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private string _tentruyen;
        private string _tap;
        List<string> dstruyen = new List<string>();
        int demhienthi = 0;
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
        public Form3(string _tentruyen, string _tap)
        {
            InitializeComponent();
            this.Tentruyen = _tentruyen;
            this.Tap = _tap;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string tam = _tentruyen;
            textBox1.Text = _tentruyen;
            textBox2.Text = _tap;
            _tentruyen = _tentruyen.Trim().Replace(" ", "").ToLower();
            using (StreamReader sr = new StreamReader("danhsachvuilen.txt"))
            {
                String line = sr.ReadToEnd();
                line = line.Replace("\r\n", ";");
                dstruyen = line.Split(';').ToList();
                comboBox1.DataSource = dstruyen;
                comboBox1.SelectedItem = tam;
            }
            PictureBox pb = new PictureBox();
            pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + demhienthi + ".jpg";
            pb.Width = 480;
            pb.Height = 800;
            pb.MouseClick += new MouseEventHandler(pb_click);
            flowLayoutPanel1.Controls.Add(pb);
            PictureBox pb2 = new PictureBox();
            pb2.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + (demhienthi+1) + ".jpg";
            pb2.Width = 480;
            pb2.Height = 800;
            pb2.MouseClick += new MouseEventHandler(pb_click);
            flowLayoutPanel1.Controls.Add(pb2);
            label3.Text = "Trang: " + demhienthi + ", " + (demhienthi + 1);
            //demhienthi = demhienthi + 2;
        }
        void pb_click(object sender, EventArgs e)
        {
            try
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Right)
                {
                    if (demhienthi >= 2)
                    {
                        flowLayoutPanel1.Controls.Clear();
                        demhienthi = demhienthi - 2;
                        PictureBox pb = new PictureBox();
                        pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + demhienthi + ".jpg";
                        pb.Width = 480;
                        pb.Height = 800;
                        pb.MouseClick += new MouseEventHandler(pb_click);
                        flowLayoutPanel1.Controls.Add(pb);
                        PictureBox pb2 = new PictureBox();
                        pb2.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + (demhienthi + 1) + ".jpg";
                        pb2.Width = 480;
                        pb2.Height = 800;
                        pb2.MouseClick += new MouseEventHandler(pb_click);
                        flowLayoutPanel1.Controls.Add(pb2);
                        label3.Text = "Trang: " + demhienthi + ", " + (demhienthi + 1);
                        
                    }
                }
                if (me.Button == MouseButtons.Left)
                {
                    flowLayoutPanel1.Controls.Clear();
                    demhienthi = demhienthi + 2;
                    PictureBox pb = new PictureBox();
                    pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + demhienthi + ".jpg";
                    pb.Width = 480;
                    pb.Height = 800;
                    pb.MouseClick += new MouseEventHandler(pb_click);
                    flowLayoutPanel1.Controls.Add(pb);
                    PictureBox pb2 = new PictureBox();
                    pb2.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + (demhienthi + 1) + ".jpg";
                    pb2.Width = 480;
                    pb2.Height = 800;
                    pb2.MouseClick += new MouseEventHandler(pb_click);
                    flowLayoutPanel1.Controls.Add(pb2);
                    label3.Text = "Trang: " + demhienthi + ", " + (demhienthi + 1);
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            demhienthi = 0;
            flowLayoutPanel1.Controls.Clear();
            _tap = textBox2.Text;
            _tentruyen = textBox1.Text;
            PictureBox pb = new PictureBox();
            pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + demhienthi + ".jpg";
            pb.Width = 480;
            pb.Height = 800;
            pb.MouseClick += new MouseEventHandler(pb_click);
            flowLayoutPanel1.Controls.Add(pb);
            PictureBox pb2 = new PictureBox();
            pb2.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + (demhienthi + 1) + ".jpg";
            pb2.Width = 480;
            pb2.Height = 800;
            pb2.MouseClick += new MouseEventHandler(pb_click);
            flowLayoutPanel1.Controls.Add(pb2);
            label3.Text = "Trang: " + demhienthi + ", " + (demhienthi + 1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedItem.ToString().Trim().Replace(" ", "").ToLower();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(comboBox1.SelectedItem.ToString(), textBox2.Text);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                demhienthi = Convert.ToInt16(txtTrang.Text);
                if (demhienthi % 2 != 0)
                {
                    demhienthi = demhienthi - 1;
                }
                flowLayoutPanel1.Controls.Clear();
                string tam = _tentruyen;
                textBox1.Text = _tentruyen;
                textBox2.Text = _tap;
                _tentruyen = _tentruyen.Trim().Replace(" ", "").ToLower();
                //using (StreamReader sr = new StreamReader("danhsachvuilen.txt"))
                //{
                //    String line = sr.ReadToEnd();
                //    line = line.Replace("\r\n", ";");
                //    dstruyen = line.Split(';').ToList();
                //    comboBox1.DataSource = dstruyen;
                //    comboBox1.SelectedItem = tam;
                //}
                PictureBox pb = new PictureBox();
                pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + demhienthi + ".jpg";
                pb.Width = 480;
                pb.Height = 800;
                pb.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb);
                PictureBox pb2 = new PictureBox();
                pb2.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + (demhienthi + 1) + ".jpg";
                pb2.Width = 480;
                pb2.Height = 800;
                pb2.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb2);
                label3.Text = "Trang: " + demhienthi + ", " + (demhienthi + 1);
                txtTrang.Text = "";
                //demhienthi = demhienthi + 2;
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (demhienthi >= 2)
            {
                flowLayoutPanel1.Controls.Clear();
                demhienthi = demhienthi - 2;
                PictureBox pb = new PictureBox();
                pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + demhienthi + ".jpg";
                pb.Width = 480;
                pb.Height = 800;
                pb.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb);
                PictureBox pb2 = new PictureBox();
                pb2.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + (demhienthi + 1) + ".jpg";
                pb2.Width = 480;
                pb2.Height = 800;
                pb2.MouseClick += new MouseEventHandler(pb_click);
                flowLayoutPanel1.Controls.Add(pb2);
                label3.Text = "Trang: " + demhienthi + ", " + (demhienthi + 1);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            demhienthi = demhienthi + 2;
            PictureBox pb = new PictureBox();
            pb.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + demhienthi + ".jpg";
            pb.Width = 480;
            pb.Height = 800;
            pb.MouseClick += new MouseEventHandler(pb_click);
            flowLayoutPanel1.Controls.Add(pb);
            PictureBox pb2 = new PictureBox();
            pb2.ImageLocation = "http://comicserver.vuilen.com/imagecache/w480/" + _tentruyen + "/tap" + _tap + "/img/Untitled-" + (demhienthi + 1) + ".jpg";
            pb2.Width = 480;
            pb2.Height = 800;
            pb2.MouseClick += new MouseEventHandler(pb_click);
            flowLayoutPanel1.Controls.Add(pb2);
            label3.Text = "Trang: " + demhienthi + ", " + (demhienthi + 1);
        }
    }
}
