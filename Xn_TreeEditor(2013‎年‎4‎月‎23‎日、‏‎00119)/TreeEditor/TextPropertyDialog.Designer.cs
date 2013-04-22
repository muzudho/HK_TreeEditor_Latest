namespace TreeEditor
{
    partial class TextPropertyDialog
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
            this.uiTextProperty1 = new TreeEditor.UiTextProperty();
            this.SuspendLayout();
            // 
            // uiTextProperty1
            // 
            this.uiTextProperty1.Location = new System.Drawing.Point(0, 0);
            this.uiTextProperty1.Name = "uiTextProperty1";
            this.uiTextProperty1.Size = new System.Drawing.Size(379, 433);
            this.uiTextProperty1.TabIndex = 0;
            // 
            // TextPropertyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 434);
            this.Controls.Add(this.uiTextProperty1);
            this.Name = "TextPropertyDialog";
            this.Text = "テキスト装飾";
            this.ResumeLayout(false);

        }

        #endregion

        private UiTextProperty uiTextProperty1;
    }
}