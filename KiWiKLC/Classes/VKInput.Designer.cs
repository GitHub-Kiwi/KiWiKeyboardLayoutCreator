namespace KiWi_Keyboard_Layout_Creator.Classes
{
    partial class VKInput
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
            TxtVk = new TextBox();
            SuspendLayout();
            // 
            // TxtVk
            // 
            TxtVk.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtVk.Location = new Point(0, 0);
            TxtVk.Margin = new Padding(0);
            TxtVk.Name = "TxtVk";
            TxtVk.Size = new Size(100, 23);
            TxtVk.TabIndex = 0;
            TxtVk.TextChanged += TxtVk_TextChanged;
            // 
            // VKInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TxtVk);
            Name = "VKInput";
            Size = new Size(100, 23);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TxtVk;
    }
}
