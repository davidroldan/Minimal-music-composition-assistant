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
using MusicaMinimalista.Objects.Utils;
using MusicaMinimalista.Objects;

namespace MusicaMinimalista.Forms
{
    public partial class EditMotifForm : Form
    {
        private enum BwStatus { READY, BUSY, PENDING };

        public Motif editedMotif;
        private BackgroundWorker bw;
        private BwStatus bw_status;
        private Controller controller;

        public EditMotifForm(Motif m, Controller controller)
        {
            this.controller = controller;
            AbcFileWriter.saveToFile(m, StringConstants.TEMP_ABC, false);
            InitializeComponent();
            this.update();
            this.richTextBox.LoadFile(StringConstants.TEMP_ABC, RichTextBoxStreamType.PlainText);
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            bw_status = BwStatus.READY;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(500);
        }
        
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bw_status == BwStatus.BUSY)
            {
                bw_status = BwStatus.READY;
            }
            else if (bw_status == BwStatus.PENDING)
            {
                try
                {
                    bw_status = BwStatus.BUSY;
                    this.richTextBox.SaveFile(StringConstants.TEMP_ABC, RichTextBoxStreamType.PlainText);
                    this.update();
                }
                catch (System.IO.IOException)
                {
                    bw_status = BwStatus.PENDING;
                }
                finally
                {
                    bw.RunWorkerAsync();
                }
            }
        }

        private void setValid(bool valid)
        {
            this.acceptButton.Enabled = valid;
            if (valid)
            {
                richTextBox.ForeColor = Color.Black;
            }
            else
            {
                richTextBox.ForeColor = Color.Red;
            }
        }

        private bool validError(string err_type)
        {
            switch (err_type)
            {
                case "Note too long": return true;
                case "Invalid note duration": return true;
                case "Note too much dotted": return true;
                case "Bad length divisor": return true;
                default: return false;
            }
        }

        private bool executeABCM2PS()
        {
            string errOutput = ConsoleParser.ExecuteCommand(
                "\"" + StringConstants.ABCM2PS + "\"",
                "-c -g -w " + (this.pictureBox.Width - 90) + " -m 0 \"" + StringConstants.TEMP_ABC + "\" -O \"" + StringConstants.TEMP_SVG_WRITE + "\"");
            
            string[] multilineseparator = {"\r\n", "\n"};
            char[] wordseparator = { ' ' };
            string[] lines = errOutput.Split(multilineseparator, StringSplitOptions.RemoveEmptyEntries);

            bool output = false;

            //Search errors
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line_words = lines[i].Split(wordseparator, StringSplitOptions.RemoveEmptyEntries);
                if (line_words.Length > 1 && line_words[1] == "error:")
                {
                    List<string> aux = line_words.ToList();
                    aux.RemoveAt(0); aux.RemoveAt(0);
                    string err_type = string.Join(" ", aux.ToArray());
                    if (!validError(err_type)) return false;
                }
                else if (line_words[0] == "Output")
                {
                    output = true;
                }
            }

            return output;
        }

        private void update()
        {
            bool validSVG = executeABCM2PS();
            Svg.SvgDocument svgDocument;
            if (validSVG)
            {
                svgDocument = Svg.SvgDocument.Open(StringConstants.TEMP_SVG_READ);
                this.pictureBox.Image = svgDocument.Draw();
                this.pictureBox.Size = this.pictureBox.Image.Size;
                this.setValid(true);
            }
            else
            {
                this.setValid(false);
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (bw_status == BwStatus.READY){
                try
                {
                    bw_status = BwStatus.BUSY;
                    this.richTextBox.SaveFile(StringConstants.TEMP_ABC, RichTextBoxStreamType.PlainText);
                    this.update();
                }
                catch (System.IO.IOException)
                {
                    bw_status = BwStatus.PENDING;
                }
                finally
                {
                    bw.RunWorkerAsync();
                }
            }

            else if (bw_status == BwStatus.BUSY)
            {
                bw_status = BwStatus.PENDING;
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.editedMotif = AbcFileReader.readFromFile(StringConstants.TEMP_ABC);
            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
