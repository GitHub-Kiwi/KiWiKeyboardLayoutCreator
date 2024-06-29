using KiWi_Keyboard_Layout_Creator.Classes;

namespace KiWi_Keyboard_Layout_Creator
{
    partial class FormScancodeAndVK
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
            btnScanInput = new Button();
            TxtScancode = new TextBox();
            chkE0 = new CheckBox();
            btnAltGr = new Button();
            btnLWin = new Button();
            btnRWin = new Button();
            label1 = new Label();
            BtnClose = new Button();
            label2 = new Label();
            label3 = new Label();
            TxtKeyName = new TextBox();
            txtVk = new VKInput();
            lblKeypress = new Label();
            chkKeypressStart = new CheckBox();
            groupBox1 = new GroupBox();
            label4 = new Label();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnScanInput
            // 
            btnScanInput.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnScanInput.Location = new Point(6, 90);
            btnScanInput.Name = "btnScanInput";
            btnScanInput.Size = new Size(52, 34);
            btnScanInput.TabIndex = 0;
            btnScanInput.Text = "⌨ input via Keypress";
            btnScanInput.TextAlign = ContentAlignment.TopCenter;
            btnScanInput.UseVisualStyleBackColor = true;
            btnScanInput.Click += BtnScanInput_Click;
            // 
            // TxtScancode
            // 
            TxtScancode.Location = new Point(92, 21);
            TxtScancode.Name = "TxtScancode";
            TxtScancode.Size = new Size(89, 23);
            TxtScancode.TabIndex = 21;
            TxtScancode.KeyUp += TxtScancode_KeyUp;
            TxtScancode.Leave += TxtScancode_Leave;
            // 
            // chkE0
            // 
            chkE0.AutoSize = true;
            chkE0.Location = new Point(187, 23);
            chkE0.Name = "chkE0";
            chkE0.Size = new Size(38, 19);
            chkE0.TabIndex = 22;
            chkE0.Text = "E0";
            chkE0.UseVisualStyleBackColor = true;
            chkE0.CheckedChanged += ChkExt_CheckedChanged;
            // 
            // btnAltGr
            // 
            btnAltGr.BackColor = SystemColors.Control;
            btnAltGr.Location = new Point(6, 50);
            btnAltGr.Name = "btnAltGr";
            btnAltGr.Size = new Size(52, 34);
            btnAltGr.TabIndex = 3;
            btnAltGr.Text = "AltGr";
            btnAltGr.UseVisualStyleBackColor = true;
            btnAltGr.Click += BtnAltGr_Click;
            // 
            // btnLWin
            // 
            btnLWin.Location = new Point(64, 50);
            btnLWin.Name = "btnLWin";
            btnLWin.Size = new Size(75, 34);
            btnLWin.TabIndex = 4;
            btnLWin.Text = "LWin/LSys";
            btnLWin.UseVisualStyleBackColor = true;
            btnLWin.Click += BtnLWin_Click;
            // 
            // btnRWin
            // 
            btnRWin.Location = new Point(145, 50);
            btnRWin.Name = "btnRWin";
            btnRWin.Size = new Size(80, 34);
            btnRWin.TabIndex = 5;
            btnRWin.Text = "RWin/RSys";
            btnRWin.UseVisualStyleBackColor = true;
            btnRWin.Click += BtnRWin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 6;
            label1.Text = "Scancode hex:";
            // 
            // BtnClose
            // 
            BtnClose.Location = new Point(12, 273);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new Size(256, 23);
            BtnClose.TabIndex = 7;
            BtnClose.Text = "Close";
            BtnClose.UseVisualStyleBackColor = true;
            BtnClose.Click += BtnClose_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 24);
            label2.Name = "label2";
            label2.Size = new Size(24, 15);
            label2.TabIndex = 9;
            label2.Text = "VK:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 53);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 11;
            label3.Text = "Key name*:";
            // 
            // TxtKeyName
            // 
            TxtKeyName.Location = new Point(79, 50);
            TxtKeyName.Name = "TxtKeyName";
            TxtKeyName.Size = new Size(171, 23);
            TxtKeyName.TabIndex = 20;
            TxtKeyName.TextChanged += TxtKeyName_TextChanged;
            // 
            // txtVk
            // 
            txtVk.Location = new Point(79, 21);
            txtVk.Name = "txtVk";
            txtVk.Size = new Size(171, 23);
            txtVk.TabIndex = 19;
            txtVk.KeyUp += TxtVk_KeyUp;
            txtVk.Leave += TxtVk_Leave;
            // 
            // lblKeypress
            // 
            lblKeypress.AutoSize = true;
            lblKeypress.Location = new Point(64, 99);
            lblKeypress.Name = "lblKeypress";
            lblKeypress.Size = new Size(110, 15);
            lblKeypress.TabIndex = 12;
            lblKeypress.Text = "Input with Keypress";
            lblKeypress.Visible = false;
            // 
            // chkKeypressStart
            // 
            chkKeypressStart.AutoSize = true;
            chkKeypressStart.Location = new Point(9, 130);
            chkKeypressStart.Name = "chkKeypressStart";
            chkKeypressStart.Size = new Size(216, 19);
            chkKeypressStart.TabIndex = 13;
            chkKeypressStart.Text = "Keypress input acitve when opening";
            chkKeypressStart.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtVk);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(TxtKeyName);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(256, 93);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Virtual Keycode and handling flags";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 76);
            label4.Name = "label4";
            label4.Size = new Size(83, 12);
            label4.TabIndex = 18;
            label4.Text = "*Name is optional";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(TxtScancode);
            groupBox2.Controls.Add(chkKeypressStart);
            groupBox2.Controls.Add(chkE0);
            groupBox2.Controls.Add(lblKeypress);
            groupBox2.Controls.Add(btnAltGr);
            groupBox2.Controls.Add(btnLWin);
            groupBox2.Controls.Add(btnScanInput);
            groupBox2.Controls.Add(btnRWin);
            groupBox2.Location = new Point(12, 111);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(256, 156);
            groupBox2.TabIndex = 19;
            groupBox2.TabStop = false;
            groupBox2.Text = "Scancode";
            // 
            // FormScancodeAndVK
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(281, 308);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(BtnClose);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormScancodeAndVK";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Scancode and VK Input";
            FormClosing += FormScancode_FormClosing;
            PreviewKeyDown += FormScancode_PreviewKeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnScanInput;
        internal TextBox TxtScancode;
        private CheckBox chkE0;
        private Button btnAltGr;
        private Button btnLWin;
        private Button btnRWin;
        private Label label1;
        private Button BtnClose;
        private Label label2;
        private Label label3;
        private TextBox TxtKeyName;
        private Label lblKeypress;
        private CheckBox chkKeypressStart;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label4;
        private VKInput txtVk;
    }
}