using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewMinemosyne.Forms
{
    public partial class AnahtarForm : Form
    {
        public AnahtarForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 frm = new Form1();
            frm.Anahtar = txtAnahtar.Text;
            frm.AnahtarFormu = this;

            frm.FormClosed += Frm_FormClosed;

            frm.Show();
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
