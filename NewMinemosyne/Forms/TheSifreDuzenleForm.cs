using NewMinemosyne.Classes;
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
    public partial class TheSifreDuzenleForm : Form
    {
        public TheSifreDuzenleForm()
        {
            InitializeComponent();
        }

        public TheSifre Sifre { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Sifre.Title = txtBaslik.Text;
            Sifre.Username = txtKullaniciAdi.Text;
            Sifre.Password = txtSifre.Text;
            Sifre.Description = txtAciklama.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SifreEkleForm_Load(object sender, EventArgs e)
        {
            txtBaslik.Text = Sifre.Title;
            txtKullaniciAdi.Text = Sifre.Username;
            txtSifre.Text = Sifre.Password;
            txtAciklama.Text = Sifre.Description;
        }
    }
}
