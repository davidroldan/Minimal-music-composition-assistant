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
    public partial class TransportVariationForm : Form
    {
        public TransportVariationForm()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            try{
                this.transport = Int32.Parse(this.txtTransport.Text);
                if (this.transport <= 0)
                {
                    MessageBox.Show("La distancia debe ser un número entero positivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (rbDescendente.Checked)
                    {
                        this.transport = -this.transport;
                    }
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Formato incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }            

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public int transport;

        private void txtTransport_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
