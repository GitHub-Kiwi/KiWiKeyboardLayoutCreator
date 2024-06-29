namespace KiWi_Keyboard_Layout_Creator
{
    partial class CtlLayerBitmodSelection
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GrpLayerSelection = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ChkCTRL = new CheckBox();
            ChkShift = new CheckBox();
            ChkAlt = new CheckBox();
            ChkRoya = new CheckBox();
            ChkKana = new CheckBox();
            ChkLoya = new CheckBox();
            ChkCustom = new CheckBox();
            ChkGrpSelTap = new CheckBox();
            GrpLayerSelection.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // GrpLayerSelection
            // 
            GrpLayerSelection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GrpLayerSelection.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GrpLayerSelection.Controls.Add(flowLayoutPanel1);
            GrpLayerSelection.Location = new Point(3, 3);
            GrpLayerSelection.Name = "GrpLayerSelection";
            GrpLayerSelection.Padding = new Padding(3, 3, 3, 0);
            GrpLayerSelection.Size = new Size(425, 85);
            GrpLayerSelection.TabIndex = 3;
            GrpLayerSelection.TabStop = false;
            GrpLayerSelection.Text = "Layer Selection";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(ChkCTRL);
            flowLayoutPanel1.Controls.Add(ChkShift);
            flowLayoutPanel1.Controls.Add(ChkAlt);
            flowLayoutPanel1.Controls.Add(ChkRoya);
            flowLayoutPanel1.Controls.Add(ChkKana);
            flowLayoutPanel1.Controls.Add(ChkLoya);
            flowLayoutPanel1.Controls.Add(ChkCustom);
            flowLayoutPanel1.Controls.Add(ChkGrpSelTap);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 19);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(419, 66);
            flowLayoutPanel1.TabIndex = 8;
            // 
            // ChkCTRL
            // 
            ChkCTRL.AutoSize = true;
            ChkCTRL.Location = new Point(3, 3);
            ChkCTRL.Name = "ChkCTRL";
            ChkCTRL.Size = new Size(45, 19);
            ChkCTRL.TabIndex = 1;
            ChkCTRL.Text = "Ctrl";
            ChkCTRL.UseVisualStyleBackColor = true;
            ChkCTRL.CheckedChanged += Chk_CheckedChanged;
            // 
            // ChkShift
            // 
            ChkShift.AutoSize = true;
            ChkShift.Location = new Point(54, 3);
            ChkShift.Name = "ChkShift";
            ChkShift.Size = new Size(50, 19);
            ChkShift.TabIndex = 0;
            ChkShift.Text = "Shift";
            ChkShift.UseVisualStyleBackColor = true;
            ChkShift.CheckedChanged += Chk_CheckedChanged;
            // 
            // ChkAlt
            // 
            ChkAlt.AutoSize = true;
            ChkAlt.Location = new Point(110, 3);
            ChkAlt.Name = "ChkAlt";
            ChkAlt.Size = new Size(53, 19);
            ChkAlt.TabIndex = 2;
            ChkAlt.Text = "AltGr";
            ChkAlt.UseVisualStyleBackColor = true;
            ChkAlt.CheckedChanged += Chk_CheckedChanged;
            // 
            // ChkRoya
            // 
            ChkRoya.AutoSize = true;
            ChkRoya.Location = new Point(169, 3);
            ChkRoya.Name = "ChkRoya";
            ChkRoya.Size = new Size(52, 19);
            ChkRoya.TabIndex = 4;
            ChkRoya.Text = "Roya";
            ChkRoya.UseVisualStyleBackColor = true;
            ChkRoya.CheckedChanged += Chk_CheckedChanged;
            // 
            // ChkKana
            // 
            ChkKana.AutoSize = true;
            ChkKana.Location = new Point(227, 3);
            ChkKana.Name = "ChkKana";
            ChkKana.Size = new Size(52, 19);
            ChkKana.TabIndex = 3;
            ChkKana.Text = "Kana";
            ChkKana.UseVisualStyleBackColor = true;
            ChkKana.CheckedChanged += Chk_CheckedChanged;
            // 
            // ChkLoya
            // 
            ChkLoya.AutoSize = true;
            ChkLoya.Location = new Point(285, 3);
            ChkLoya.Name = "ChkLoya";
            ChkLoya.Size = new Size(51, 19);
            ChkLoya.TabIndex = 5;
            ChkLoya.Text = "Loya";
            ChkLoya.UseVisualStyleBackColor = true;
            ChkLoya.CheckedChanged += Chk_CheckedChanged;
            // 
            // ChkCustom
            // 
            ChkCustom.AutoSize = true;
            ChkCustom.Location = new Point(342, 3);
            ChkCustom.Name = "ChkCustom";
            ChkCustom.Size = new Size(68, 19);
            ChkCustom.TabIndex = 6;
            ChkCustom.Text = "Custom";
            ChkCustom.UseVisualStyleBackColor = true;
            ChkCustom.CheckedChanged += Chk_CheckedChanged;
            // 
            // ChkGrpSelTap
            // 
            ChkGrpSelTap.AutoSize = true;
            ChkGrpSelTap.Enabled = false;
            ChkGrpSelTap.Location = new Point(3, 28);
            ChkGrpSelTap.Name = "ChkGrpSelTap";
            ChkGrpSelTap.Size = new Size(139, 19);
            ChkGrpSelTap.TabIndex = 7;
            ChkGrpSelTap.Text = "Group Selector Aping";
            ChkGrpSelTap.UseVisualStyleBackColor = true;
            ChkGrpSelTap.CheckedChanged += Chk_CheckedChanged;
            // 
            // CtlLayerSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(GrpLayerSelection);
            Name = "CtlLayerSelection";
            Size = new Size(431, 91);
            GrpLayerSelection.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GrpLayerSelection;
        internal CheckBox ChkGrpSelTap;
        internal CheckBox ChkCustom;
        internal CheckBox ChkLoya;
        internal CheckBox ChkKana;
        internal CheckBox ChkRoya;
        internal CheckBox ChkAlt;
        internal CheckBox ChkCTRL;
        internal CheckBox ChkShift;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
