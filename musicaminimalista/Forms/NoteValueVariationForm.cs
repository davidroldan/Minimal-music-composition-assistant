using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicaMinimalista.Objects.Music;

namespace MusicaMinimalista.Forms
{
    public partial class NoteValueVariationForm : Form
    {
        public NoteValueVariationForm()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.multiplier = Duration.Parse(this.txtTransport.Text);
                if (this.multiplier <= 0)
                {
                    MessageBox.Show("La fracción debe ser positiva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Formato incorrecto. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public Duration multiplier;

        private void txtTransport_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
