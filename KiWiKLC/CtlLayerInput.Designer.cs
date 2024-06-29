using KiWi_Keyboard_Layout_Creator.Classes;

namespace KiWi_Keyboard_Layout_Creator
{
    partial class CtlLayerInput
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
            label1 = new Label();
            TxtSpecialNls = new VKInput();
            txtNlsUpVk = new VKInput();
            TxtWChar = new TextBox();
            groupBox2 = new GroupBox();
            txtWCharHex = new TextBox();
            label2 = new Label();
            BtnDeleteLayer = new Button();
            groupBox1 = new GroupBox();
            btnNlsEnsureKeyUp = new Button();
            chkToggleKeyUp = new CheckBox();
            label3 = new Label();
            cbbNlsUp = new ComboBox();
            cbbSpecialNls = new ComboBox();
            ctlLayerSelection = new CtlLayerBitmodSelection();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 25);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Character";
            // 
            // TxtSpecialNls
            // 
            TxtSpecialNls.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtSpecialNls.Location = new Point(316, 22);
            TxtSpecialNls.Name = "TxtSpecialNls";
            TxtSpecialNls.Size = new Size(206, 23);
            TxtSpecialNls.TabIndex = 21;
            // 
            // txtNlsUpVk
            // 
            txtNlsUpVk.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNlsUpVk.Location = new Point(316, 120);
            txtNlsUpVk.Name = "txtNlsUpVk";
            txtNlsUpVk.Size = new Size(206, 23);
            txtNlsUpVk.TabIndex = 22;
            // 
            // TxtWChar
            // 
            TxtWChar.Location = new Point(75, 22);
            TxtWChar.MaxLength = 1;
            TxtWChar.Name = "TxtWChar";
            TxtWChar.Size = new Size(97, 23);
            TxtWChar.TabIndex = 1;
            TxtWChar.TextChanged += TxtWChar_TextChanged;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(txtWCharHex);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(TxtWChar);
            groupBox2.Location = new Point(9, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(522, 55);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Normal Character";
            // 
            // txtWCharHex
            // 
            txtWCharHex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtWCharHex.Location = new Point(301, 22);
            txtWCharHex.MaxLength = 6;
            txtWCharHex.Name = "txtWCharHex";
            txtWCharHex.Size = new Size(215, 23);
            txtWCharHex.TabIndex = 3;
            txtWCharHex.TextChanged += TxtWCharHex_TextChanged;
            txtWCharHex.Leave += TxtWCharHex_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(178, 25);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 2;
            label2.Text = "Hex value (Unicode):";
            // 
            // BtnDeleteLayer
            // 
            BtnDeleteLayer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnDeleteLayer.Location = new Point(6, 306);
            BtnDeleteLayer.Name = "BtnDeleteLayer";
            BtnDeleteLayer.Size = new Size(528, 23);
            BtnDeleteLayer.TabIndex = 5;
            BtnDeleteLayer.Text = "Remove Layer";
            BtnDeleteLayer.UseVisualStyleBackColor = true;
            BtnDeleteLayer.Click += BtnDeleteLayer_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnNlsEnsureKeyUp);
            groupBox1.Controls.Add(chkToggleKeyUp);
            groupBox1.Controls.Add(txtNlsUpVk);
            groupBox1.Controls.Add(TxtSpecialNls);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cbbNlsUp);
            groupBox1.Controls.Add(cbbSpecialNls);
            groupBox1.Location = new Point(3, 64);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(528, 155);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Special NLS function (only for baselayer, shift, ctrl, alt and those combined)";
            // 
            // btnNlsEnsureKeyUp
            // 
            btnNlsEnsureKeyUp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnNlsEnsureKeyUp.Location = new Point(6, 51);
            btnNlsEnsureKeyUp.Name = "btnNlsEnsureKeyUp";
            btnNlsEnsureKeyUp.Size = new Size(516, 23);
            btnNlsEnsureKeyUp.TabIndex = 24;
            btnNlsEnsureKeyUp.Text = "Ensure the Nls keydown of this layer is released on all Nls-layers Keyup";
            btnNlsEnsureKeyUp.UseVisualStyleBackColor = true;
            btnNlsEnsureKeyUp.Click += BtnNlsEnsureKeyUp_Click;
            // 
            // chkToggleKeyUp
            // 
            chkToggleKeyUp.AutoSize = true;
            chkToggleKeyUp.Location = new Point(6, 95);
            chkToggleKeyUp.Name = "chkToggleKeyUp";
            chkToggleKeyUp.Size = new Size(324, 19);
            chkToggleKeyUp.TabIndex = 23;
            chkToggleKeyUp.Text = "Keydown on this layer toggles different Keyup behaviour";
            chkToggleKeyUp.UseVisualStyleBackColor = true;
            chkToggleKeyUp.CheckedChanged += ChkToggleKeyUp_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 77);
            label3.Name = "label3";
            label3.Size = new Size(145, 15);
            label3.TabIndex = 20;
            label3.Text = "Different keyUp behaviour";
            // 
            // cbbNlsUp
            // 
            cbbNlsUp.FormattingEnabled = true;
            cbbNlsUp.Items.AddRange(new object[] { "", "drop Key Event", "send base Virtual Key", "send different Virtual Key", "Kanalock (with hardware lock)", "Alphanumeric", "Hiragana", "Katakana", "SBCSChar / DBCSChar", "Roman / No Roman", "Codeinput / No Codeinput", "Help or End [NEC PC-9800 Only]", "Home or Clear [NEC PC-9800 Only]", "Numpad? for Numpad Key [NEC PC-9800 Only]", "Kanaevent [Fujitsu FMV oyayubi Only]", "Convert or Non-Convert [Fujitsu FMV oyayubi Only]" });
            cbbNlsUp.Location = new Point(6, 119);
            cbbNlsUp.Name = "cbbNlsUp";
            cbbNlsUp.Size = new Size(304, 23);
            cbbNlsUp.TabIndex = 18;
            cbbNlsUp.SelectedIndexChanged += CbbNlsUp_SelectedIndexChanged;
            // 
            // cbbSpecialNls
            // 
            cbbSpecialNls.FormattingEnabled = true;
            cbbSpecialNls.Items.AddRange(new object[] { "", "drop Key Event", "send base Virtual Key", "send different Virtual Key", "Kanalock (with hardware lock)", "Alphanumeric", "Hiragana", "Katakana", "SBCSChar / DBCSChar", "Roman / No Roman", "Codeinput / No Codeinput", "Help or End [NEC PC-9800 Only]", "Home or Clear [NEC PC-9800 Only]", "Numpad? for Numpad Key [NEC PC-9800 Only]", "Kanaevent [Fujitsu FMV oyayubi Only]", "Convert or Non-Convert [Fujitsu FMV oyayubi Only]" });
            cbbSpecialNls.Location = new Point(6, 22);
            cbbSpecialNls.Name = "cbbSpecialNls";
            cbbSpecialNls.Size = new Size(304, 23);
            cbbSpecialNls.TabIndex = 16;
            cbbSpecialNls.SelectedIndexChanged += CbbSpecialNls_SelectedIndexChanged;
            // 
            // ctlLayerSelection
            // 
            ctlLayerSelection.LayerMask = 0;
            ctlLayerSelection.Location = new Point(6, 225);
            ctlLayerSelection.Name = "ctlLayerSelection";
            ctlLayerSelection.Size = new Size(528, 75);
            ctlLayerSelection.TabIndex = 7;
            ctlLayerSelection.LayerChange += CtlLayerSelection_LayerChange;
            // 
            // CtlLayerInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ctlLayerSelection);
            Controls.Add(groupBox1);
            Controls.Add(BtnDeleteLayer);
            Controls.Add(groupBox2);
            Name = "CtlLayerInput";
            Size = new Size(534, 332);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

#pragma warning disable CS0169 // The field 'CtlLayerInput.textBox1' is never used
        private Label label1;
        private VKInput TxtSpecialNls;
        private VKInput txtNlsUpVk;
        private GroupBox groupBox2;
        internal TextBox TxtWChar;
        private Button BtnDeleteLayer;
        private GroupBox groupBox1;
        private TextBox txtWCharHex;
        private Label label2;
        private ComboBox cbbSpecialNls;
        private CtlLayerBitmodSelection ctlLayerSelection;
        private Label label3;
        private ComboBox cbbNlsUp;
        private CheckBox chkToggleKeyUp;
        private Button btnNlsEnsureKeyUp;
#pragma warning restore CS0169 // The field 'CtlLayerInput.textBox1' is never used
    }
}
