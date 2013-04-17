namespace TreeEditor
{
    partial class ConfigurationDialog
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
            this.uiConfiguration1 = new TreeEditor.UiConfiguration();
            this.SuspendLayout();
            // 
            // uiConfiguration1
            // 
            this.uiConfiguration1.Location = new System.Drawing.Point(12, 12);
            this.uiConfiguration1.Name = "uiConfiguration1";
            this.uiConfiguration1.Size = new System.Drawing.Size(242, 237);
            this.uiConfiguration1.TabIndex = 0;
            // 
            // ConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.uiConfiguration1);
            this.Name = "ConfigurationDialog";
            this.Text = "設定";
            this.ResumeLayout(false);

        }

        #endregion

        private UiConfiguration uiConfiguration1;
    }
}