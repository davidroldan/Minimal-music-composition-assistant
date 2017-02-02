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
    public partial class ModulationVariationForm : Form
    {
        public ModulationVariationForm()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.newTonality = calculateTonality();
            this.DialogResult = DialogResult.OK;
        }

        private Tonality calculateTonality()
        {
            int tonality;
            int mode;
            switch (this.comboBox1.SelectedIndex)
            {
                case 0: tonality = 0; break;
                case 1: tonality = 7; break;
                case 2: tonality = 2; break;
                case 3: tonality = -3; break;
                case 4: tonality = 4; break;
                case 5: tonality = -1; break;
                case 6: tonality = 6; break;
                case 7: tonality = 1; break;
                case 8: tonality = -4; break;
                case 9: tonality = 3; break;
                case 10: tonality = -2; break;
                default: tonality = 5; break;
            }
            if (this.comboBox2.SelectedIndex == 0)
            {
                mode = Tonality.MAJOR;
            }
            else
            {
                mode = Tonality.MINOR;
                tonality -= 3;
            }
            return new Tonality(tonality, mode);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public Tonality newTonality;
    }
}
