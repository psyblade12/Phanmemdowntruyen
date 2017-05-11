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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.MdiParent = this;
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.MdiParent = this;
            form.Show();
        }

        private void thoátToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GioiThieuTacGia form = new GioiThieuTacGia();
            form.Show();
        }

        private void phầnMềmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPhienBan form = new FormPhienBan();
            form.Show();
        }

        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHuongDanSuDungcs form = new FormHuongDanSuDungcs();
            form.Show();
        }
    }
}
