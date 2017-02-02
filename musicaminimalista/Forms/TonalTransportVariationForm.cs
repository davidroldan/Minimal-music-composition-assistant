using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicaMinimalista.Forms
{
    public partial class TonalTransportVariationForm : Form
    {
        public TonalTransportVariationForm()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.orig = this.comboBox1.SelectedIndex;
            this.dest = this.comboBox2.SelectedIndex;
            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public int orig;
        public int dest;

        private void txtTransport_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
