using NewMinemosyne.Classes;
using NewMinemosyne.Forms;
using Newtonsoft.Json;
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
using Wissen.SC501.Common;

namespace NewMinemosyne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        KategoriContainer container;
        string path = string.Empty;

        public string Anahtar { get; set; }
        public AnahtarForm AnahtarFormu { get; set; }



        private void Form1_Load(object sender, EventArgs e)
        {
            lblDosyaAdi.Text = string.Empty;
            lblDosyaAdiStatus.Text = string.Empty;
        }

        private void LoadKategorilerSifreler(KategoriContainer container)
        {
            trvKategoriler.Nodes.Clear();

            // node oluştu..
            TreeNode rootNode = new TreeNode(container.ToString());
            rootNode.Tag = container; // Tag özelliğine container nesnesi atıldı.
            rootNode.ContextMenuStrip = cmnuContainer;

            // Node treeview'a eklendi.
            trvKategoriler.Nodes.Add(rootNode);

            foreach (Kategori kate in container.Kategoriler)
            {
                // Kategori için node oluşturulur..
                TreeNode katNode = new TreeNode(kate.ToString());
                katNode.Tag = kate;
                katNode.ContextMenuStrip = cmnuKategori;

                // Kategori node'ı rootnode'a eklenir.
                rootNode.Nodes.Add(katNode);

                foreach (TheSifre sif in kate.TheSifres)
                {
                    // Sifre için node oluşturulur.
                    TreeNode sifNode = new TreeNode(sif.ToString());
                    sifNode.Tag = sif;
                    sifNode.ContextMenuStrip = cmnuTheSifre;

                    // Kategori node'ına eklenir.
                    katNode.Nodes.Add(sifNode);
                }

            }

            trvKategoriler.ExpandAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Json formatında container nesnesi serialize edilir.
            string json = JsonConvert.SerializeObject(container);

            // şifreleme yapılır.
            json = new Sifreleyici().Sifrele(json, this.Anahtar);

            //txtOzet.Text = json;

            File.WriteAllText(path, json);
        }

        private void btnKategorilerDuzenle_Click(object sender, EventArgs e)
        {
            TreeNode selected = trvKategoriler.SelectedNode;
            KategoriContainer nesne = (KategoriContainer)selected.Tag;

            ContainerDuzenleForm frm = new ContainerDuzenleForm();
            frm.Title = nesne.Title;
            //frm.Title = container.Title;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                container.Title = frm.Title;
                LoadKategorilerSifreler(container);
            }
        }

        private void btnKategoriEkle_Click(object sender, EventArgs e)
        {
            KategoriEkleForm frm = new KategoriEkleForm();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                container.Kategoriler.Add(frm.KategoriNesnesi);
                LoadKategorilerSifreler(container);
            }
        }

        private void btnKategoriDuzenle_Click(object sender, EventArgs e)
        {
            TreeNode selected = trvKategoriler.SelectedNode;
            Kategori kategori = (Kategori)selected.Tag;

            KategoriDuzenleForm frm = new KategoriDuzenleForm();
            frm.KategoriNesnesi = kategori;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadKategorilerSifreler(container);
            }
        }

        private void btnKategoriSil_Click(object sender, EventArgs e)
        {
            TreeNode selected = trvKategoriler.SelectedNode;
            Kategori kategori = (Kategori)selected.Tag;

            DialogResult result =
                MessageBox.Show(kategori.Name + " isimli kategori silinsin mi?", "Kategori Silme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                container.Kategoriler.Remove(kategori);
                LoadKategorilerSifreler(container);
            }

        }

        private void btnTheSifreEkle_Click(object sender, EventArgs e)
        {
            TreeNode selected = trvKategoriler.SelectedNode;
            Kategori kategori = (Kategori)selected.Tag;

            TheSfireEkleForm frm = new TheSfireEkleForm();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                kategori.TheSifres.Add(frm.Sifre);
                LoadKategorilerSifreler(container);
            }
        }

        private void btnTheSifreDuzenle_Click(object sender, EventArgs e)
        {
            TreeNode selected = trvKategoriler.SelectedNode;
            TheSifre sifre = (TheSifre)selected.Tag;

            TheSifreDuzenleForm frm = new TheSifreDuzenleForm();
            frm.Sifre = sifre;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadKategorilerSifreler(container);
            }
        }

        private void btnTtheSifreSil_Click(object sender, EventArgs e)
        {
            TreeNode selected = trvKategoriler.SelectedNode;
            TheSifre sifre = (TheSifre)selected.Tag;

            TreeNode selectedParent = selected.Parent;
            Kategori kategori = (Kategori)selectedParent.Tag;

            DialogResult result =
                MessageBox.Show(sifre.Title + " isimli şifre silinsin mi?", "Şifre Sil", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                kategori.TheSifres.Remove(sifre);
                LoadKategorilerSifreler(container);
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Minemosyne Files(*.acma)|*.acma";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = Application.StartupPath;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;

                lblDosyaAdi.Text = ofd.SafeFileName.Replace(".acma", "");
                lblDosyaAdiStatus.Text = ofd.SafeFileName.Replace(".acma", "");

                if (File.Exists(path) == false)
                {
                    return;
                }

                try
                {
                    string json = File.ReadAllText(path);
                    json = new Sifreleyici().SifreCoz(json, this.Anahtar);

                    container = JsonConvert.DeserializeObject<KategoriContainer>(json);

                    LoadKategorilerSifreler(container);
                }
                catch (Exception)
                {
                    MessageBox.Show("Anahtar değer hatası","Hatalı Anahtar", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    AnahtarFormu.Show();
                    this.Close();
                }
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Minemosyne Files(*.acma)|*.acma";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = Application.StartupPath;
            sfd.FileName = "dosya_adı";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                path = sfd.FileName;

                container = new KategoriContainer();
                container.Title = "Kategoriler";

                btnSave_Click(btnSave, EventArgs.Empty);

                FileInfo fi = new FileInfo(path);
                lblDosyaAdi.Text = fi.Name.Replace(".acma", "");
                lblDosyaAdiStatus.Text = fi.Name.Replace(".acma", "");
                fi = null;

                LoadKategorilerSifreler(container);
            }
        }

        private void trvKategoriler_MouseUp(object sender, MouseEventArgs e)
        {
            TreeNode selected = trvKategoriler.GetNodeAt(e.X, e.Y);
            object tagObject = selected.Tag; // KategoriContainer, Kategori, TheSifre..

            IBase obj = (IBase)tagObject;
            txtOzet.Text = obj.GetDataString();

            string[] keywords = new string[5]
                { "Adı :", "Açıklama :", "Başlık :", "Kullanıcı Adı :", "Şifre :" };

            foreach (string word in keywords)
            {
                int start = 0;

                do
                {
                    // Kelimeyi arar, bulamazsa -1 döner.
                    start = txtOzet.Find(word, start, RichTextBoxFinds.MatchCase);

                    if (start > -1)
                    {
                        // bulunan kelimeyi seçer.
                        txtOzet.Select(start, word.Length);
                        // kelimeyi bold yapar.
                        txtOzet.SelectionFont = new Font(txtOzet.SelectionFont, FontStyle.Bold);

                        // aramaya başlama index i değiştirilir.
                        start += word.Length;
                    }

                } while (start > -1);

            }

            //if(tagObject is KategoriContainer)
            //{
            //    KategoriContainer obj = (KategoriContainer)tagObject;
            //    txtOzet.Text = obj.Title;
            //}

            //if (tagObject is Kategori)
            //{
            //    Kategori obj = (Kategori)tagObject;
            //    txtOzet.Text = obj.Name + "\n" + obj.Description;
            //}

            //if (tagObject is TheSifre)
            //{
            //    TheSifre obj = (TheSifre)tagObject;
            //    txtOzet.Text = obj.Title + "\n" + obj.Username + "\n" + obj.Password + "\n" + obj.Description;
            //}

        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOzet.SelectionFont = fontDialog1.Font;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOzet.SelectionColor = colorDialog1.Color;
            }
        }

        private void btnBackcolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOzet.SelectionBackColor = colorDialog1.Color;
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOzet.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            txtOzet.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            txtOzet.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            txtOzet.Paste();
        }

        private void btnKes_Click(object sender, EventArgs e)
        {
            txtOzet.Cut();
        }

        private void btnKopyala_Click(object sender, EventArgs e)
        {
            txtOzet.Copy();
        }

        private void btnYapistir_Click(object sender, EventArgs e)
        {
            txtOzet.Paste();
        }

        private void btnTumunuSec_Click(object sender, EventArgs e)
        {
            txtOzet.SelectAll();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            string helpFilePath = Application.StartupPath + "//Files//yardim.docx";
            System.Diagnostics.Process.Start(helpFilePath);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            HakkimdaForm frm = new HakkimdaForm();
            frm.ShowDialog();
        }
    }
}
