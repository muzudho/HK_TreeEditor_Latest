namespace TreeEditor
{
    partial class UiTextProperty
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.foreRedTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.foreGreenTxt = new System.Windows.Forms.TextBox();
            this.foreBlueTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.foreWebTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.backRedTxt = new System.Windows.Forms.TextBox();
            this.backGreenTxt = new System.Windows.Forms.TextBox();
            this.backBlueTxt = new System.Windows.Forms.TextBox();
            this.backWebTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(374, 134);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "赤";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "緑";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "青";
            // 
            // foreRedTxt
            // 
            this.foreRedTxt.Location = new System.Drawing.Point(49, 192);
            this.foreRedTxt.Name = "foreRedTxt";
            this.foreRedTxt.Size = new System.Drawing.Size(62, 19);
            this.foreRedTxt.TabIndex = 4;
            this.foreRedTxt.Text = "0";
            this.foreRedTxt.TextChanged += new System.EventHandler(this.foreRedTxt_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "文字色";
            // 
            // foreGreenTxt
            // 
            this.foreGreenTxt.Location = new System.Drawing.Point(49, 217);
            this.foreGreenTxt.Name = "foreGreenTxt";
            this.foreGreenTxt.Size = new System.Drawing.Size(62, 19);
            this.foreGreenTxt.TabIndex = 7;
            this.foreGreenTxt.Text = "0";
            this.foreGreenTxt.TextChanged += new System.EventHandler(this.foreGreenTxt_TextChanged);
            // 
            // foreBlueTxt
            // 
            this.foreBlueTxt.Location = new System.Drawing.Point(49, 242);
            this.foreBlueTxt.Name = "foreBlueTxt";
            this.foreBlueTxt.Size = new System.Drawing.Size(62, 19);
            this.foreBlueTxt.TabIndex = 8;
            this.foreBlueTxt.Text = "0";
            this.foreBlueTxt.TextChanged += new System.EventHandler(this.foreBlueTxt_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 320);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Web";
            // 
            // foreWebTxt
            // 
            this.foreWebTxt.Location = new System.Drawing.Point(49, 317);
            this.foreWebTxt.Name = "foreWebTxt";
            this.foreWebTxt.Size = new System.Drawing.Size(62, 19);
            this.foreWebTxt.TabIndex = 10;
            this.foreWebTxt.Text = "#000000";
            this.foreWebTxt.TextChanged += new System.EventHandler(this.foreWebTxt_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 36);
            this.label7.TabIndex = 11;
            this.label7.Text = "#000000～#FFFFFF\r\n　　　　または\r\n　　#000～#FFF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "太字";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(246, 275);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "斜体";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(246, 307);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "下線";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 339);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "取り消し線";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(214, 165);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(214, 195);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(61, 20);
            this.comboBox2.TabIndex = 21;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(49, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(130, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "キャンセル";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(302, 389);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "元に戻す";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Image = global::TreeEditor.Properties.Resources.btn_Bold;
            this.checkBox2.Location = new System.Drawing.Point(214, 235);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(26, 26);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Image = global::TreeEditor.Properties.Resources.btn_Italic;
            this.checkBox1.Location = new System.Drawing.Point(214, 268);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(26, 26);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // checkBox3
            // 
            this.checkBox3.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox3.AutoSize = true;
            this.checkBox3.Image = global::TreeEditor.Properties.Resources.btn_Underline;
            this.checkBox3.Location = new System.Drawing.Point(214, 300);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(26, 26);
            this.checkBox3.TabIndex = 27;
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox4.AutoSize = true;
            this.checkBox4.Image = global::TreeEditor.Properties.Resources.btn_Strike;
            this.checkBox4.Location = new System.Drawing.Point(214, 332);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(26, 26);
            this.checkBox4.TabIndex = 28;
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(128, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "背景色";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(93, 177);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 30;
            this.label12.Text = "0～255";
            // 
            // backRedTxt
            // 
            this.backRedTxt.Location = new System.Drawing.Point(117, 192);
            this.backRedTxt.Name = "backRedTxt";
            this.backRedTxt.Size = new System.Drawing.Size(60, 19);
            this.backRedTxt.TabIndex = 31;
            this.backRedTxt.Text = "0";
            this.backRedTxt.TextChanged += new System.EventHandler(this.backRedTxt_TextChanged);
            // 
            // backGreenTxt
            // 
            this.backGreenTxt.Location = new System.Drawing.Point(117, 217);
            this.backGreenTxt.Name = "backGreenTxt";
            this.backGreenTxt.Size = new System.Drawing.Size(60, 19);
            this.backGreenTxt.TabIndex = 32;
            this.backGreenTxt.Text = "0";
            this.backGreenTxt.TextChanged += new System.EventHandler(this.backGreenTxt_TextChanged);
            // 
            // backBlueTxt
            // 
            this.backBlueTxt.Location = new System.Drawing.Point(117, 242);
            this.backBlueTxt.Name = "backBlueTxt";
            this.backBlueTxt.Size = new System.Drawing.Size(60, 19);
            this.backBlueTxt.TabIndex = 33;
            this.backBlueTxt.Text = "0";
            this.backBlueTxt.TextChanged += new System.EventHandler(this.backBlueTxt_TextChanged);
            // 
            // backWebTxt
            // 
            this.backWebTxt.Location = new System.Drawing.Point(117, 317);
            this.backWebTxt.Name = "backWebTxt";
            this.backWebTxt.Size = new System.Drawing.Size(60, 19);
            this.backWebTxt.TabIndex = 34;
            this.backWebTxt.Text = "#000000";
            this.backWebTxt.TextChanged += new System.EventHandler(this.backWebTxt_TextChanged);
            // 
            // UiTextProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backWebTxt);
            this.Controls.Add(this.backBlueTxt);
            this.Controls.Add(this.backGreenTxt);
            this.Controls.Add(this.backRedTxt);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.foreWebTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.foreBlueTxt);
            this.Controls.Add(this.foreGreenTxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.foreRedTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "UiTextProperty";
            this.Size = new System.Drawing.Size(379, 433);
            this.Load += new System.EventHandler(this.UiTextProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox foreRedTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox foreGreenTxt;
        private System.Windows.Forms.TextBox foreBlueTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox foreWebTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox backRedTxt;
        private System.Windows.Forms.TextBox backGreenTxt;
        private System.Windows.Forms.TextBox backBlueTxt;
        private System.Windows.Forms.TextBox backWebTxt;
    }
}
