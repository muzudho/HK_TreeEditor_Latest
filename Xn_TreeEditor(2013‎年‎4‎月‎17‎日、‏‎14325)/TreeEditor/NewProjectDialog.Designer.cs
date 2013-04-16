namespace TreeEditor
{
    partial class NewProjectDialog
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
            this.uiNewProject1 = new TreeEditor.UiNewProject();
            this.SuspendLayout();
            // 
            // uiNewProject1
            // 
            this.uiNewProject1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uiNewProject1.Location = new System.Drawing.Point(12, 12);
            this.uiNewProject1.Name = "uiNewProject1";
            this.uiNewProject1.Size = new System.Drawing.Size(505, 440);
            this.uiNewProject1.TabIndex = 0;
            // 
            // NewProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 464);
            this.Controls.Add(this.uiNewProject1);
            this.Name = "NewProjectDialog";
            this.Text = "新規プロジェクトの作成";
            this.Load += new System.EventHandler(this.NewProjectDialog_Load);
            this.Resize += new System.EventHandler(this.NewProjectDialog_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private UiNewProject uiNewProject1;
    }
}