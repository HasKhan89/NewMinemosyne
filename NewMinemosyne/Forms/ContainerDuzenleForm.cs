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
    public partial class ContainerDuzenleForm : Form
    {
        public ContainerDuzenleForm()
        {
            InitializeComponent();
        }

        public string Title { get; set; }

        private void ContainerDuzenleForm_Load(object sender, EventArgs e)
        {
            txtTitle.Text = Title;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Title = txtTitle.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}
