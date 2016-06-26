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
    public partial class TheSfireEkleForm : Form
    {
        public TheSfireEkleForm()
        {
            InitializeComponent();
        }

        public TheSifre Sifre { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Sifre = new TheSifre();
            Sifre.ID = Guid.NewGuid();
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
    }
}
