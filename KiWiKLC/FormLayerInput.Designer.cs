namespace KiWi_Keyboard_Layout_Creator
{
    partial class FormLayerInput
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
            CtlLayerInput = new CtlLayerInput();
            SuspendLayout();
            // 
            // CtlLayerInput
            // 
            CtlLayerInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CtlLayerInput.DeleteLayerBtnVisible = false;
            CtlLayerInput.LayerChkEnabled = false;
            CtlLayerInput.LayerMask = 0;
            CtlLayerInput.Location = new Point(5, 5);
            CtlLayerInput.Name = "CtlLayerInput";
            CtlLayerInput.Nls = NlsType.NULL;
            CtlLayerInput.NlsKeyUpLayerBit = false;
            CtlLayerInput.NlsVk = null;
            CtlLayerInput.NlsVkKeyUp = null;
            CtlLayerInput.Size = new Size(452, 297);
            CtlLayerInput.TabIndex = 0;
            CtlLayerInput.ToggleKeyUp = false;
            CtlLayerInput.WCHAR = null;
            CtlLayerInput.ValueChange += CtlLayerInput_Change;
            CtlLayerInput.LayerChange += CtlLayerInput_LayerChange;
            CtlLayerInput.NlsEnsureKeyUpClick += ctlLayerInput_NlsEnsureKeyUpClick;
            // 
            // FormLayerInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(461, 314);
            Controls.Add(CtlLayerInput);
            MaximizeBox = false;
            Name = "FormLayerInput";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "FormLayerInput";
            FormClosing += FormLayerInput_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private CtlLayerInput CtlLayerInput;
    }
}