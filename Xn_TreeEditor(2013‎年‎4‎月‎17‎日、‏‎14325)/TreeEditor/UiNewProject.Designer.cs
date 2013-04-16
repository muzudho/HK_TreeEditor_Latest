namespace TreeEditor
{
    partial class UiNewProject
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.newProjectFolderTxt1 = new System.Windows.Forms.TextBox();
            this.createDammyBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.currentTxt = new System.Windows.Forms.TextBox();
            this.openBtn = new System.Windows.Forms.Button();
            this.defaultPrjBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(17, 82);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 112);
            this.listBox1.TabIndex = 0;
            // 
            // newProjectFolderTxt1
            // 
            this.newProjectFolderTxt1.Enabled = false;
            this.newProjectFolderTxt1.Location = new System.Drawing.Point(17, 235);
            this.newProjectFolderTxt1.Name = "newProjectFolderTxt1";
            this.newProjectFolderTxt1.Size = new System.Drawing.Size(463, 19);
            this.newProjectFolderTxt1.TabIndex = 1;
            this.newProjectFolderTxt1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // createDammyBtn
            // 
            this.createDammyBtn.Enabled = false;
            this.createDammyBtn.Location = new System.Drawing.Point(262, 260);
            this.createDammyBtn.Name = "createDammyBtn";
            this.createDammyBtn.Size = new System.Drawing.Size(218, 23);
            this.createDammyBtn.TabIndex = 2;
            this.createDammyBtn.Text = "ダミー・プロジェクトをここに作成";
            this.createDammyBtn.UseVisualStyleBackColor = true;
            this.createDammyBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "新しいフォルダー";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "プロジェクト・フォルダーの中身";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "今、使用中のフォルダー";
            // 
            // currentTxt
            // 
            this.currentTxt.Location = new System.Drawing.Point(17, 34);
            this.currentTxt.Name = "currentTxt";
            this.currentTxt.ReadOnly = true;
            this.currentTxt.Size = new System.Drawing.Size(463, 19);
            this.currentTxt.TabIndex = 6;
            // 
            // openBtn
            // 
            this.openBtn.Location = new System.Drawing.Point(57, 260);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(199, 23);
            this.openBtn.TabIndex = 7;
            this.openBtn.Text = "探す";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // defaultPrjBtn
            // 
            this.defaultPrjBtn.Location = new System.Drawing.Point(322, 289);
            this.defaultPrjBtn.Name = "defaultPrjBtn";
            this.defaultPrjBtn.Size = new System.Drawing.Size(158, 23);
            this.defaultPrjBtn.TabIndex = 8;
            this.defaultPrjBtn.Text = "デフォルト・プロジェクトを開く";
            this.defaultPrjBtn.UseVisualStyleBackColor = true;
            this.defaultPrjBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // UiNewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.defaultPrjBtn);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.currentTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createDammyBtn);
            this.Controls.Add(this.newProjectFolderTxt1);
            this.Controls.Add(this.listBox1);
            this.Name = "UiNewProject";
            this.Size = new System.Drawing.Size(498, 450);
            this.Load += new System.EventHandler(this.UiNewProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox newProjectFolderTxt1;
        private System.Windows.Forms.Button createDammyBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox currentTxt;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Button defaultPrjBtn;
    }
}
