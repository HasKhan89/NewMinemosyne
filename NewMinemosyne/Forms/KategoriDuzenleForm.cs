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
    public partial class KategoriDuzenleForm : Form
    {
        public KategoriDuzenleForm()
        {
            InitializeComponent();
        }

        public Kategori KategoriNesnesi { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            KategoriNesnesi.Name = txtKategoriAdi.Text;
            KategoriNesnesi.Description = txtKategoriAciklama.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void KategoriDuzenleForm_Load(object sender, EventArgs e)
        {
            txtKategoriAdi.Text = KategoriNesnesi.Name;
            txtKategoriAciklama.Text = KategoriNesnesi.Description;
        }
    }
}
