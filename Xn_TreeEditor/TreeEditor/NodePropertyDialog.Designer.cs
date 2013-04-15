namespace TreeEditor
{
    partial class NodePropertyDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.foreRedTxt = new System.Windows.Forms.TextBox();
            this.backRedTxt = new System.Windows.Forms.TextBox();
            this.foreGreenTxt = new System.Windows.Forms.TextBox();
            this.backGreenTxt = new System.Windows.Forms.TextBox();
            this.foreBlueTxt = new System.Windows.Forms.TextBox();
            this.backBlueTxt = new System.Windows.Forms.TextBox();
            this.foreWebTxt = new System.Windows.Forms.TextBox();
            this.backWebTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageIndexLst = new System.Windows.Forms.ListBox();
            this.nodeNameTxt = new System.Windows.Forms.TextBox();
            this.nameCommentLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "赤";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "緑";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "青";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "前景色";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "背景色";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Web";
            // 
            // foreRedTxt
            // 
            this.foreRedTxt.Location = new System.Drawing.Point(46, 104);
            this.foreRedTxt.Name = "foreRedTxt";
            this.foreRedTxt.Size = new System.Drawing.Size(100, 19);
            this.foreRedTxt.TabIndex = 6;
            this.foreRedTxt.TextChanged += new System.EventHandler(this.foreRedTxt_TextChanged);
            // 
            // backRedTxt
            // 
            this.backRedTxt.Location = new System.Drawing.Point(152, 104);
            this.backRedTxt.Name = "backRedTxt";
            this.backRedTxt.Size = new System.Drawing.Size(100, 19);
            this.backRedTxt.TabIndex = 7;
            this.backRedTxt.TextChanged += new System.EventHandler(this.backRedTxt_TextChanged);
            // 
            // foreGreenTxt
            // 
            this.foreGreenTxt.Location = new System.Drawing.Point(46, 129);
            this.foreGreenTxt.Name = "foreGreenTxt";
            this.foreGreenTxt.Size = new System.Drawing.Size(100, 19);
            this.foreGreenTxt.TabIndex = 8;
            this.foreGreenTxt.TextChanged += new System.EventHandler(this.foreGreenTxt_TextChanged);
            // 
            // backGreenTxt
            // 
            this.backGreenTxt.Location = new System.Drawing.Point(152, 129);
            this.backGreenTxt.Name = "backGreenTxt";
            this.backGreenTxt.Size = new System.Drawing.Size(100, 19);
            this.backGreenTxt.TabIndex = 9;
            this.backGreenTxt.TextChanged += new System.EventHandler(this.backGreenTxt_TextChanged);
            // 
            // foreBlueTxt
            // 
            this.foreBlueTxt.Location = new System.Drawing.Point(46, 154);
            this.foreBlueTxt.Name = "foreBlueTxt";
            this.foreBlueTxt.Size = new System.Drawing.Size(100, 19);
            this.foreBlueTxt.TabIndex = 10;
            this.foreBlueTxt.TextChanged += new System.EventHandler(this.foreBlueTxt_TextChanged);
            // 
            // backBlueTxt
            // 
            this.backBlueTxt.Location = new System.Drawing.Point(152, 154);
            this.backBlueTxt.Name = "backBlueTxt";
            this.backBlueTxt.Size = new System.Drawing.Size(100, 19);
            this.backBlueTxt.TabIndex = 11;
            this.backBlueTxt.TextChanged += new System.EventHandler(this.backBlueTxt_TextChanged);
            // 
            // foreWebTxt
            // 
            this.foreWebTxt.Location = new System.Drawing.Point(46, 207);
            this.foreWebTxt.Name = "foreWebTxt";
            this.foreWebTxt.Size = new System.Drawing.Size(100, 19);
            this.foreWebTxt.TabIndex = 12;
            this.foreWebTxt.TextChanged += new System.EventHandler(this.foreWebTxt_TextChanged);
            // 
            // backWebTxt
            // 
            this.backWebTxt.Location = new System.Drawing.Point(152, 207);
            this.backWebTxt.Name = "backWebTxt";
            this.backWebTxt.Size = new System.Drawing.Size(100, 19);
            this.backWebTxt.TabIndex = 13;
            this.backWebTxt.TextChanged += new System.EventHandler(this.backWebTxt_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(129, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "0～255";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(46, 192);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "#000～#FFF または #000000～#FFFFFF";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "元に戻す";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "ノード名";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 290);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "サンプル";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(71, 273);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 29);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // imageIndexLst
            // 
            this.imageIndexLst.FormattingEnabled = true;
            this.imageIndexLst.ItemHeight = 12;
            this.imageIndexLst.Location = new System.Drawing.Point(16, 308);
            this.imageIndexLst.Name = "imageIndexLst";
            this.imageIndexLst.Size = new System.Drawing.Size(120, 112);
            this.imageIndexLst.TabIndex = 22;
            this.imageIndexLst.SelectedIndexChanged += new System.EventHandler(this.imageIndexLst_SelectedIndexChanged);
            // 
            // nodeNameTxt
            // 
            this.nodeNameTxt.Location = new System.Drawing.Point(61, 39);
            this.nodeNameTxt.Name = "nodeNameTxt";
            this.nodeNameTxt.Size = new System.Drawing.Size(191, 19);
            this.nodeNameTxt.TabIndex = 23;
            this.nodeNameTxt.TextChanged += new System.EventHandler(this.nodeNameTxt_TextChanged);
            // 
            // nameCommentLbl
            // 
            this.nameCommentLbl.AutoSize = true;
            this.nameCommentLbl.Location = new System.Drawing.Point(69, 24);
            this.nameCommentLbl.Name = "nameCommentLbl";
            this.nameCommentLbl.Size = new System.Drawing.Size(134, 12);
            this.nameCommentLbl.TabIndex = 24;
            this.nameCommentLbl.Text = "既にあるノードの名前です。";
            // 
            // NodePropertyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 477);
            this.Controls.Add(this.nameCommentLbl);
            this.Controls.Add(this.nodeNameTxt);
            this.Controls.Add(this.imageIndexLst);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.backWebTxt);
            this.Controls.Add(this.foreWebTxt);
            this.Controls.Add(this.backBlueTxt);
            this.Controls.Add(this.foreBlueTxt);
            this.Controls.Add(this.backGreenTxt);
            this.Controls.Add(this.foreGreenTxt);
            this.Controls.Add(this.backRedTxt);
            this.Controls.Add(this.foreRedTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NodePropertyDialog";
            this.Text = "ノードの編集";
            this.Load += new System.EventHandler(this.NodeColorDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox foreRedTxt;
        private System.Windows.Forms.TextBox backRedTxt;
        private System.Windows.Forms.TextBox foreGreenTxt;
        private System.Windows.Forms.TextBox backGreenTxt;
        private System.Windows.Forms.TextBox foreBlueTxt;
        private System.Windows.Forms.TextBox backBlueTxt;
        private System.Windows.Forms.TextBox foreWebTxt;
        private System.Windows.Forms.TextBox backWebTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox imageIndexLst;
        private System.Windows.Forms.TextBox nodeNameTxt;
        private System.Windows.Forms.Label nameCommentLbl;
    }
}