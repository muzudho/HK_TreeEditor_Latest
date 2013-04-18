namespace TreeEditor
{
    partial class PicturePropertyDlg
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
            this.uiPictureProperty1 = new TreeEditor.UiPictureProperty();
            this.SuspendLayout();
            // 
            // uiPictureProperty1
            // 
            this.uiPictureProperty1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiPictureProperty1.DialogResult = "";
            this.uiPictureProperty1.File = "";
            this.uiPictureProperty1.Location = new System.Drawing.Point(0, 0);
            this.uiPictureProperty1.Name = "uiPictureProperty1";
            this.uiPictureProperty1.Size = new System.Drawing.Size(816, 300);
            this.uiPictureProperty1.TabIndex = 0;
            // 
            // PicturePropertyDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 330);
            this.Controls.Add(this.uiPictureProperty1);
            this.Name = "PicturePropertyDlg";
            this.Text = "画像プロパティー";
            this.ResumeLayout(false);

        }

        #endregion

        private UiPictureProperty uiPictureProperty1;
    }
}