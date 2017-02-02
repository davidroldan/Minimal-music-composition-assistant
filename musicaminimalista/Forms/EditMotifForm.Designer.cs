namespace MusicaMinimalista.Forms
{
    partial class EditMotifForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.scrollBarPanel = new System.Windows.Forms.Panel();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.scrollBarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox.Location = new System.Drawing.Point(12, 209);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(535, 188);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(530, 184);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // scrollBarPanel
            // 
            this.scrollBarPanel.AutoScroll = true;
            this.scrollBarPanel.BackColor = System.Drawing.Color.White;
            this.scrollBarPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scrollBarPanel.Controls.Add(this.pictureBox);
            this.scrollBarPanel.Location = new System.Drawing.Point(12, 12);
            this.scrollBarPanel.Name = "scrollBarPanel";
            this.scrollBarPanel.Size = new System.Drawing.Size(535, 191);
            this.scrollBarPanel.TabIndex = 2;
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(164, 406);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(118, 30);
            this.acceptButton.TabIndex = 3;
            this.acceptButton.Text = "Aceptar";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(288, 406);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(119, 30);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditMotifForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 444);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.scrollBarPanel);
            this.Controls.Add(this.richTextBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(575, 483);
            this.MinimumSize = new System.Drawing.Size(575, 483);
            this.Name = "EditMotifForm";
            this.Text = "Editar Motivo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.scrollBarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel scrollBarPanel;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
    }
}