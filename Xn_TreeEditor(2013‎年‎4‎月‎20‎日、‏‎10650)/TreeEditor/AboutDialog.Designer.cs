namespace TreeEditor
{
    partial class AboutDialog
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
            this.uiAbout1 = new TreeEditor.UiAbout();
            this.SuspendLayout();
            // 
            // uiAbout1
            // 
            this.uiAbout1.Location = new System.Drawing.Point(77, 12);
            this.uiAbout1.Name = "uiAbout1";
            this.uiAbout1.Size = new System.Drawing.Size(144, 329);
            this.uiAbout1.TabIndex = 0;
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 345);
            this.Controls.Add(this.uiAbout1);
            this.Name = "AboutDialog";
            this.Text = "バージョン情報";
            this.ResumeLayout(false);

        }

        #endregion

        private UiAbout uiAbout1;
    }
}