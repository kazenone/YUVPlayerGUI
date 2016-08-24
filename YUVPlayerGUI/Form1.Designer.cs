namespace YUVPlayerGUI
{
    partial class Form1
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fPathTextBox = new System.Windows.Forms.TextBox();
            this.CFComboBox = new System.Windows.Forms.ComboBox();
            this.loopFlagCBOX = new System.Windows.Forms.CheckBox();
            this.frameLockCBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.fpsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "実行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "width※";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "height※";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "fps※";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "カラーフォーマット※";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "ファイルのパス※";
            // 
            // fPathTextBox
            // 
            this.fPathTextBox.Location = new System.Drawing.Point(27, 25);
            this.fPathTextBox.Name = "fPathTextBox";
            this.fPathTextBox.Size = new System.Drawing.Size(224, 19);
            this.fPathTextBox.TabIndex = 0;
            // 
            // CFComboBox
            // 
            this.CFComboBox.AllowDrop = true;
            this.CFComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CFComboBox.FormattingEnabled = true;
            this.CFComboBox.Items.AddRange(new object[] {
            "420",
            "444"});
            this.CFComboBox.Location = new System.Drawing.Point(27, 109);
            this.CFComboBox.Name = "CFComboBox";
            this.CFComboBox.Size = new System.Drawing.Size(94, 20);
            this.CFComboBox.TabIndex = 3;
            // 
            // loopFlagCBOX
            // 
            this.loopFlagCBOX.AutoSize = true;
            this.loopFlagCBOX.Location = new System.Drawing.Point(27, 135);
            this.loopFlagCBOX.Name = "loopFlagCBOX";
            this.loopFlagCBOX.Size = new System.Drawing.Size(129, 16);
            this.loopFlagCBOX.TabIndex = 6;
            this.loopFlagCBOX.Text = "動画をループ再生する";
            this.loopFlagCBOX.UseVisualStyleBackColor = true;
            // 
            // frameLockCBox
            // 
            this.frameLockCBox.AutoSize = true;
            this.frameLockCBox.Location = new System.Drawing.Point(27, 157);
            this.frameLockCBox.Name = "frameLockCBox";
            this.frameLockCBox.Size = new System.Drawing.Size(205, 16);
            this.frameLockCBox.TabIndex = 7;
            this.frameLockCBox.Text = "静止画として0フレーム目のみ表示する";
            this.frameLockCBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Copyright By 2016 kazenone";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(12, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(177, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "※は必須項目です";
            // 
            // heightNumericUpDown
            // 
            this.heightNumericUpDown.Location = new System.Drawing.Point(157, 68);
            this.heightNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.heightNumericUpDown.Name = "heightNumericUpDown";
            this.heightNumericUpDown.Size = new System.Drawing.Size(94, 19);
            this.heightNumericUpDown.TabIndex = 2;
            this.heightNumericUpDown.Click += new System.EventHandler(this.heightNumericUpDown_Click);
            this.heightNumericUpDown.Enter += new System.EventHandler(this.heightNumericUpDown_Enter);
            // 
            // widthNumericUpDown
            // 
            this.widthNumericUpDown.Location = new System.Drawing.Point(25, 68);
            this.widthNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.widthNumericUpDown.Name = "widthNumericUpDown";
            this.widthNumericUpDown.Size = new System.Drawing.Size(94, 19);
            this.widthNumericUpDown.TabIndex = 1;
            this.widthNumericUpDown.Click += new System.EventHandler(this.widthNumericUpDown_Click);
            this.widthNumericUpDown.Enter += new System.EventHandler(this.widthNumericUpDown_Enter);
            // 
            // fpsNumericUpDown
            // 
            this.fpsNumericUpDown.BackColor = System.Drawing.Color.White;
            this.fpsNumericUpDown.Cursor = System.Windows.Forms.Cursors.Default;
            this.fpsNumericUpDown.Location = new System.Drawing.Point(157, 110);
            this.fpsNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.fpsNumericUpDown.Name = "fpsNumericUpDown";
            this.fpsNumericUpDown.Size = new System.Drawing.Size(94, 19);
            this.fpsNumericUpDown.TabIndex = 4;
            this.fpsNumericUpDown.Click += new System.EventHandler(this.fpsNumericUpDown_Click);
            this.fpsNumericUpDown.Enter += new System.EventHandler(this.fpsNumericUpDown_Enter);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.widthNumericUpDown);
            this.Controls.Add(this.fpsNumericUpDown);
            this.Controls.Add(this.heightNumericUpDown);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.frameLockCBox);
            this.Controls.Add(this.loopFlagCBOX);
            this.Controls.Add(this.CFComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fPathTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "YUVPlayerGUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.heightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fPathTextBox;
        private System.Windows.Forms.ComboBox CFComboBox;
        private System.Windows.Forms.CheckBox loopFlagCBOX;
        private System.Windows.Forms.CheckBox frameLockCBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown heightNumericUpDown;
        private System.Windows.Forms.NumericUpDown widthNumericUpDown;
        private System.Windows.Forms.NumericUpDown fpsNumericUpDown;
    }
}

