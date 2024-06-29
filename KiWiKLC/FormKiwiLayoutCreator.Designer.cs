namespace KiWi_Keyboard_Layout_Creator
{
    partial class FormKiwiLayoutCreator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            layoutProToolStripMenuItem = new ToolStripMenuItem();
            saveProjectToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            loadProjectToolStripMenuItem = new ToolStripMenuItem();
            newProjectToolStripMenuItem = new ToolStripMenuItem();
            buildKbdFileToolStripMenuItem = new ToolStripMenuItem();
            buildKbdFileWithCommentsToolStripMenuItem = new ToolStripMenuItem();
            physicalLayoutToolStripMenuItem = new ToolStripMenuItem();
            addButtonToolStripMenuItem = new ToolStripMenuItem();
            moveButtonToolStripMenuItem = new ToolStripMenuItem();
            addSpacebarToolStripMenuItem = new ToolStripMenuItem();
            AddCtrlButtonToolStripMenuItem = new ToolStripMenuItem();
            tabButtonToolStripMenuItem = new ToolStripMenuItem();
            cabsButtonToolStripMenuItem = new ToolStripMenuItem();
            BackButtonToolStripMenuItem = new ToolStripMenuItem();
            longEnterSizedButtonToolStripMenuItem = new ToolStripMenuItem();
            shiftSizedButtonToolStripMenuItem = new ToolStripMenuItem();
            spacebarToolStripMenuItem = new ToolStripMenuItem();
            numpadEnterHighButtonToolStripMenuItem = new ToolStripMenuItem();
            removeButtonToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            virtualKeysToolStripMenuItem = new ToolStripMenuItem();
            tsmSetupScancode = new ToolStripMenuItem();
            tsmSetupVK = new ToolStripMenuItem();
            setFlagsToolStripMenuItem = new ToolStripMenuItem();
            extensionToolStripMenuItem = new ToolStripMenuItem();
            multiToolStripMenuItem = new ToolStripMenuItem();
            specialToolStripMenuItem = new ToolStripMenuItem();
            numpadToolStripMenuItem = new ToolStripMenuItem();
            setLockToolStripMenuItem = new ToolStripMenuItem();
            capslockToolStripMenuItem = new ToolStripMenuItem();
            sGCapsToolStripMenuItem = new ToolStripMenuItem();
            capsAltGrToolStripMenuItem = new ToolStripMenuItem();
            kanalockToolStripMenuItem = new ToolStripMenuItem();
            groupselectorToolStripMenuItem = new ToolStripMenuItem();
            layerSetupToolStripMenuItem = new ToolStripMenuItem();
            tsmSetupLayers = new ToolStripMenuItem();
            tsmShiftPressed = new ToolStripMenuItem();
            tsmCtrlPressed = new ToolStripMenuItem();
            tsmAltPressed = new ToolStripMenuItem();
            tsmKanaPressed = new ToolStripMenuItem();
            tsmRoyaPressed = new ToolStripMenuItem();
            tsmLoyaPressed = new ToolStripMenuItem();
            tsmCustomPressed = new ToolStripMenuItem();
            tsmGRouPSELecTorAPingPressed = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            numpadSetupToolStripMenuItem = new ToolStripMenuItem();
            saveProjektDialog = new SaveFileDialog();
            openProjektDialog = new OpenFileDialog();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, physicalLayoutToolStripMenuItem, virtualKeysToolStripMenuItem, layerSetupToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { layoutProToolStripMenuItem, saveProjectToolStripMenuItem, toolStripMenuItem3, loadProjectToolStripMenuItem, newProjectToolStripMenuItem, buildKbdFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // layoutProToolStripMenuItem
            // 
            layoutProToolStripMenuItem.Name = "layoutProToolStripMenuItem";
            layoutProToolStripMenuItem.Size = new Size(166, 22);
            layoutProToolStripMenuItem.Text = "Layout Properties";
            layoutProToolStripMenuItem.Click += LayoutProToolStripMenuItem_Click;
            // 
            // saveProjectToolStripMenuItem
            // 
            saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            saveProjectToolStripMenuItem.Size = new Size(166, 22);
            saveProjectToolStripMenuItem.Text = "Save Project";
            saveProjectToolStripMenuItem.Click += SaveProjectToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(166, 22);
            toolStripMenuItem3.Text = "----------";
            // 
            // loadProjectToolStripMenuItem
            // 
            loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            loadProjectToolStripMenuItem.Size = new Size(166, 22);
            loadProjectToolStripMenuItem.Text = "Open Project";
            loadProjectToolStripMenuItem.Click += LoadProjectToolStripMenuItem_Click;
            // 
            // newProjectToolStripMenuItem
            // 
            newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            newProjectToolStripMenuItem.Size = new Size(166, 22);
            newProjectToolStripMenuItem.Text = "New Project";
            newProjectToolStripMenuItem.Click += NewProjectToolStripMenuItem_Click;
            // 
            // buildKbdFileToolStripMenuItem
            // 
            buildKbdFileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { buildKbdFileWithCommentsToolStripMenuItem });
            buildKbdFileToolStripMenuItem.Name = "buildKbdFileToolStripMenuItem";
            buildKbdFileToolStripMenuItem.Size = new Size(166, 22);
            buildKbdFileToolStripMenuItem.Text = "BuildKbdFile";
            buildKbdFileToolStripMenuItem.Click += BuildKbdFileToolStripMenuItem_Click;
            // 
            // buildKbdFileWithCommentsToolStripMenuItem
            // 
            buildKbdFileWithCommentsToolStripMenuItem.Name = "buildKbdFileWithCommentsToolStripMenuItem";
            buildKbdFileWithCommentsToolStripMenuItem.Size = new Size(228, 22);
            buildKbdFileWithCommentsToolStripMenuItem.Text = "BuildKbdFile with Comments";
            buildKbdFileWithCommentsToolStripMenuItem.Click += BuildKbdFileWithCommentsToolStripMenuItem_Click;
            // 
            // physicalLayoutToolStripMenuItem
            // 
            physicalLayoutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addButtonToolStripMenuItem, moveButtonToolStripMenuItem, addSpacebarToolStripMenuItem, removeButtonToolStripMenuItem, toolStripMenuItem2 });
            physicalLayoutToolStripMenuItem.Name = "physicalLayoutToolStripMenuItem";
            physicalLayoutToolStripMenuItem.Size = new Size(101, 20);
            physicalLayoutToolStripMenuItem.Text = "Physical Layout";
            // 
            // addButtonToolStripMenuItem
            // 
            addButtonToolStripMenuItem.Name = "addButtonToolStripMenuItem";
            addButtonToolStripMenuItem.Size = new Size(180, 22);
            addButtonToolStripMenuItem.Text = "Add Button";
            addButtonToolStripMenuItem.Click += AddButtonToolStripMenuItem_Click;
            // 
            // moveButtonToolStripMenuItem
            // 
            moveButtonToolStripMenuItem.Name = "moveButtonToolStripMenuItem";
            moveButtonToolStripMenuItem.Size = new Size(180, 22);
            moveButtonToolStripMenuItem.Text = "Move Button";
            moveButtonToolStripMenuItem.Click += MoveButtonToolStripMenuItem_Click;
            // 
            // addSpacebarToolStripMenuItem
            // 
            addSpacebarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AddCtrlButtonToolStripMenuItem, tabButtonToolStripMenuItem, cabsButtonToolStripMenuItem, BackButtonToolStripMenuItem, longEnterSizedButtonToolStripMenuItem, shiftSizedButtonToolStripMenuItem, spacebarToolStripMenuItem, numpadEnterHighButtonToolStripMenuItem });
            addSpacebarToolStripMenuItem.Name = "addSpacebarToolStripMenuItem";
            addSpacebarToolStripMenuItem.Size = new Size(180, 22);
            addSpacebarToolStripMenuItem.Text = "Add other buttons";
            // 
            // AddCtrlButtonToolStripMenuItem
            // 
            AddCtrlButtonToolStripMenuItem.Name = "AddCtrlButtonToolStripMenuItem";
            AddCtrlButtonToolStripMenuItem.Size = new Size(215, 22);
            AddCtrlButtonToolStripMenuItem.Text = "Ctrl / Win / Alt / Menu";
            AddCtrlButtonToolStripMenuItem.Click += CtrlButtonToolStripMenuItem_Click;
            // 
            // tabButtonToolStripMenuItem
            // 
            tabButtonToolStripMenuItem.Name = "tabButtonToolStripMenuItem";
            tabButtonToolStripMenuItem.Size = new Size(215, 22);
            tabButtonToolStripMenuItem.Text = "Tab";
            tabButtonToolStripMenuItem.Click += TabButtonToolStripMenuItem_Click;
            // 
            // cabsButtonToolStripMenuItem
            // 
            cabsButtonToolStripMenuItem.Name = "cabsButtonToolStripMenuItem";
            cabsButtonToolStripMenuItem.Size = new Size(215, 22);
            cabsButtonToolStripMenuItem.Text = "Capital";
            cabsButtonToolStripMenuItem.Click += CapsButtonToolStripMenuItem_Click;
            // 
            // BackButtonToolStripMenuItem
            // 
            BackButtonToolStripMenuItem.Name = "BackButtonToolStripMenuItem";
            BackButtonToolStripMenuItem.Size = new Size(215, 22);
            BackButtonToolStripMenuItem.Text = "Backspace";
            BackButtonToolStripMenuItem.Click += ReturnButtonToolStripMenuItem_Click;
            // 
            // longEnterSizedButtonToolStripMenuItem
            // 
            longEnterSizedButtonToolStripMenuItem.Name = "longEnterSizedButtonToolStripMenuItem";
            longEnterSizedButtonToolStripMenuItem.Size = new Size(215, 22);
            longEnterSizedButtonToolStripMenuItem.Text = "Left shift / Enter";
            longEnterSizedButtonToolStripMenuItem.Click += LongEnterSizedButtonToolStripMenuItem_Click;
            // 
            // shiftSizedButtonToolStripMenuItem
            // 
            shiftSizedButtonToolStripMenuItem.Name = "shiftSizedButtonToolStripMenuItem";
            shiftSizedButtonToolStripMenuItem.Size = new Size(215, 22);
            shiftSizedButtonToolStripMenuItem.Text = "Right shift";
            shiftSizedButtonToolStripMenuItem.Click += RShiftSizedButtonToolStripMenuItem_Click;
            // 
            // spacebarToolStripMenuItem
            // 
            spacebarToolStripMenuItem.Name = "spacebarToolStripMenuItem";
            spacebarToolStripMenuItem.Size = new Size(238, 22);
            spacebarToolStripMenuItem.Text = "Spacebar";
            spacebarToolStripMenuItem.Click += SpacebarToolStripMenuItem_Click;
            // 
            // numpadEnterHighButtonToolStripMenuItem
            // 
            numpadEnterHighButtonToolStripMenuItem.Name = "numpadEnterHighButtonToolStripMenuItem";
            numpadEnterHighButtonToolStripMenuItem.Size = new Size(193, 22);
            numpadEnterHighButtonToolStripMenuItem.Text = "Numpad enter";
            numpadEnterHighButtonToolStripMenuItem.Click += NumpadEnterHighButtonToolStripMenuItem_Click;
            // 
            // removeButtonToolStripMenuItem
            // 
            removeButtonToolStripMenuItem.Name = "removeButtonToolStripMenuItem";
            removeButtonToolStripMenuItem.Size = new Size(180, 22);
            removeButtonToolStripMenuItem.Text = "Remove Button";
            removeButtonToolStripMenuItem.Click += RemoveButtonToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(180, 22);
            toolStripMenuItem2.Text = "-------";
            // 
            // virtualKeysToolStripMenuItem
            // 
            virtualKeysToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmSetupScancode, tsmSetupVK, setFlagsToolStripMenuItem, setLockToolStripMenuItem });
            virtualKeysToolStripMenuItem.Name = "virtualKeysToolStripMenuItem";
            virtualKeysToolStripMenuItem.Size = new Size(76, 20);
            virtualKeysToolStripMenuItem.Text = "Setup Keys";
            // 
            // tsmSetupScancode
            // 
            tsmSetupScancode.CheckOnClick = true;
            tsmSetupScancode.Name = "tsmSetupScancode";
            tsmSetupScancode.Size = new Size(192, 22);
            tsmSetupScancode.Text = "Scancodes Setup";
            tsmSetupScancode.Click += TsmSetupScancode_Click;
            // 
            // tsmSetupVK
            // 
            tsmSetupVK.CheckOnClick = true;
            tsmSetupVK.Name = "tsmSetupVK";
            tsmSetupVK.Size = new Size(192, 22);
            tsmSetupVK.Text = "Show Virtual Keys (VK)";
            tsmSetupVK.Click += TsmEditVirtualKeys_Click;
            // 
            // setFlagsToolStripMenuItem
            // 
            setFlagsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { extensionToolStripMenuItem, multiToolStripMenuItem, specialToolStripMenuItem, numpadToolStripMenuItem });
            setFlagsToolStripMenuItem.Name = "setFlagsToolStripMenuItem";
            setFlagsToolStripMenuItem.Size = new Size(192, 22);
            setFlagsToolStripMenuItem.Text = "Set VK handling flags";
            // 
            // extensionToolStripMenuItem
            // 
            extensionToolStripMenuItem.CheckOnClick = true;
            extensionToolStripMenuItem.Name = "extensionToolStripMenuItem";
            extensionToolStripMenuItem.Size = new Size(125, 22);
            extensionToolStripMenuItem.Text = "Extension";
            extensionToolStripMenuItem.Click += ExtensionToolStripMenuItem_Click;
            // 
            // multiToolStripMenuItem
            // 
            multiToolStripMenuItem.CheckOnClick = true;
            multiToolStripMenuItem.Name = "multiToolStripMenuItem";
            multiToolStripMenuItem.Size = new Size(125, 22);
            multiToolStripMenuItem.Text = "Multi";
            multiToolStripMenuItem.Click += MultiToolStripMenuItem_Click;
            // 
            // specialToolStripMenuItem
            // 
            specialToolStripMenuItem.CheckOnClick = true;
            specialToolStripMenuItem.Name = "specialToolStripMenuItem";
            specialToolStripMenuItem.Size = new Size(125, 22);
            specialToolStripMenuItem.Text = "Special";
            specialToolStripMenuItem.Click += SpecialToolStripMenuItem_Click;
            // 
            // numpadToolStripMenuItem
            // 
            numpadToolStripMenuItem.CheckOnClick = true;
            numpadToolStripMenuItem.Name = "numpadToolStripMenuItem";
            numpadToolStripMenuItem.Size = new Size(125, 22);
            numpadToolStripMenuItem.Text = "Numpad";
            numpadToolStripMenuItem.Click += NumpadToolStripMenuItem_Click;
            // 
            // setLockToolStripMenuItem
            // 
            setLockToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { capslockToolStripMenuItem, sGCapsToolStripMenuItem, capsAltGrToolStripMenuItem, kanalockToolStripMenuItem, groupselectorToolStripMenuItem });
            setLockToolStripMenuItem.Name = "setLockToolStripMenuItem";
            setLockToolStripMenuItem.Size = new Size(192, 22);
            setLockToolStripMenuItem.Text = "Set lock behaviour";
            // 
            // capslockToolStripMenuItem
            // 
            capslockToolStripMenuItem.Name = "capslockToolStripMenuItem";
            capslockToolStripMenuItem.Size = new Size(148, 22);
            capslockToolStripMenuItem.Text = "Capslock";
            capslockToolStripMenuItem.Click += CapslockToolStripMenuItem_Click;
            // 
            // sGCapsToolStripMenuItem
            // 
            sGCapsToolStripMenuItem.Name = "sGCapsToolStripMenuItem";
            sGCapsToolStripMenuItem.Size = new Size(148, 22);
            sGCapsToolStripMenuItem.Text = "SGCaps";
            sGCapsToolStripMenuItem.Click += SGCapsToolStripMenuItem_Click;
            // 
            // capsAltGrToolStripMenuItem
            // 
            capsAltGrToolStripMenuItem.Name = "capsAltGrToolStripMenuItem";
            capsAltGrToolStripMenuItem.Size = new Size(148, 22);
            capsAltGrToolStripMenuItem.Text = "CapsAltGr";
            capsAltGrToolStripMenuItem.Click += CapsAltGrToolStripMenuItem_Click;
            // 
            // kanalockToolStripMenuItem
            // 
            kanalockToolStripMenuItem.Name = "kanalockToolStripMenuItem";
            kanalockToolStripMenuItem.Size = new Size(148, 22);
            kanalockToolStripMenuItem.Text = "Kanalock";
            kanalockToolStripMenuItem.Click += KanalockToolStripMenuItem_Click;
            // 
            // groupselectorToolStripMenuItem
            // 
            groupselectorToolStripMenuItem.Name = "groupselectorToolStripMenuItem";
            groupselectorToolStripMenuItem.Size = new Size(148, 22);
            groupselectorToolStripMenuItem.Text = "Groupselector";
            groupselectorToolStripMenuItem.Click += GroupselectorToolStripMenuItem_Click;
            // 
            // layerSetupToolStripMenuItem
            // 
            layerSetupToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmSetupLayers, tsmShiftPressed, tsmCtrlPressed, tsmAltPressed, tsmKanaPressed, tsmRoyaPressed, tsmLoyaPressed, tsmCustomPressed, tsmGRouPSELecTorAPingPressed, toolStripMenuItem4, numpadSetupToolStripMenuItem });
            layerSetupToolStripMenuItem.Name = "layerSetupToolStripMenuItem";
            layerSetupToolStripMenuItem.Size = new Size(80, 20);
            layerSetupToolStripMenuItem.Text = "Layer Setup";
            // 
            // tsmSetupLayers
            // 
            tsmSetupLayers.CheckOnClick = true;
            tsmSetupLayers.Name = "tsmSetupLayers";
            tsmSetupLayers.Size = new Size(204, 22);
            tsmSetupLayers.Text = "---Layer Setup---";
            tsmSetupLayers.Click += EditToolStripPress;
            // 
            // tsmShiftPressed
            // 
            tsmShiftPressed.CheckOnClick = true;
            tsmShiftPressed.Name = "tsmShiftPressed";
            tsmShiftPressed.Size = new Size(204, 22);
            tsmShiftPressed.Text = "Shift";
            tsmShiftPressed.Click += EditToolStripPress;
            // 
            // tsmCtrlPressed
            // 
            tsmCtrlPressed.CheckOnClick = true;
            tsmCtrlPressed.Name = "tsmCtrlPressed";
            tsmCtrlPressed.Size = new Size(204, 22);
            tsmCtrlPressed.Text = "Ctrl";
            tsmCtrlPressed.Click += EditToolStripPress;
            // 
            // tsmAltPressed
            // 
            tsmAltPressed.CheckOnClick = true;
            tsmAltPressed.Name = "tsmAltPressed";
            tsmAltPressed.Size = new Size(204, 22);
            tsmAltPressed.Text = "Alt";
            tsmAltPressed.Click += EditToolStripPress;
            // 
            // tsmKanaPressed
            // 
            tsmKanaPressed.CheckOnClick = true;
            tsmKanaPressed.Name = "tsmKanaPressed";
            tsmKanaPressed.Size = new Size(204, 22);
            tsmKanaPressed.Text = "Kana";
            tsmKanaPressed.Click += EditToolStripPress;
            // 
            // tsmRoyaPressed
            // 
            tsmRoyaPressed.CheckOnClick = true;
            tsmRoyaPressed.Name = "tsmRoyaPressed";
            tsmRoyaPressed.Size = new Size(204, 22);
            tsmRoyaPressed.Text = "Roya";
            tsmRoyaPressed.Click += EditToolStripPress;
            // 
            // tsmLoyaPressed
            // 
            tsmLoyaPressed.CheckOnClick = true;
            tsmLoyaPressed.Name = "tsmLoyaPressed";
            tsmLoyaPressed.Size = new Size(204, 22);
            tsmLoyaPressed.Text = "Loya";
            tsmLoyaPressed.Click += EditToolStripPress;
            // 
            // tsmCustomPressed
            // 
            tsmCustomPressed.CheckOnClick = true;
            tsmCustomPressed.Name = "tsmCustomPressed";
            tsmCustomPressed.Size = new Size(204, 22);
            tsmCustomPressed.Text = "Custom";
            tsmCustomPressed.Click += EditToolStripPress;
            // 
            // tsmGRouPSELecTorAPingPressed
            // 
            tsmGRouPSELecTorAPingPressed.CheckOnClick = true;
            tsmGRouPSELecTorAPingPressed.Name = "tsmGRouPSELecTorAPingPressed";
            tsmGRouPSELecTorAPingPressed.Size = new Size(204, 22);
            tsmGRouPSELecTorAPingPressed.Text = "GRouP SELecTor APing";
            tsmGRouPSELecTorAPingPressed.Click += EditToolStripPress;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(204, 22);
            toolStripMenuItem4.Text = "--------------------------";
            // 
            // numpadSetupToolStripMenuItem
            // 
            numpadSetupToolStripMenuItem.Name = "numpadSetupToolStripMenuItem";
            numpadSetupToolStripMenuItem.Size = new Size(204, 22);
            numpadSetupToolStripMenuItem.Text = "Numpad Setup";
            numpadSetupToolStripMenuItem.Click += NumpadSetupToolStripMenuItem_Click;
            // 
            // saveProjektDialog
            // 
            saveProjektDialog.FileName = "MyKeyboardLayout.json";
            saveProjektDialog.Filter = "Json|*.json|All files|*.*";
            // 
            // openProjektDialog
            // 
            openProjektDialog.Filter = "Json|*.json|All files|*.*";
            // 
            // FormKiwiLayoutCreator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            Name = "FormKiwiLayoutCreator";
            Text = "Kiwi Keyboard Layout Creator";
            MouseDown += FormKiwiLayoutCreator_MouseDown;
            MouseMove += FormKiwiLayoutCreator_MouseMove;
            MouseUp += FormKiwiLayoutCreator_MouseUp;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveProjectToolStripMenuItem;
        private ToolStripMenuItem loadProjectToolStripMenuItem;
        private ToolStripMenuItem physicalLayoutToolStripMenuItem;
        private ToolStripMenuItem virtualKeysToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem addButtonToolStripMenuItem;
        private ToolStripMenuItem moveButtonToolStripMenuItem;
        private ToolStripMenuItem addSpacebarToolStripMenuItem;
        private ToolStripMenuItem spacebarToolStripMenuItem;
        private ToolStripMenuItem AddCtrlButtonToolStripMenuItem;
        private ToolStripMenuItem tabButtonToolStripMenuItem;
        private ToolStripMenuItem cabsButtonToolStripMenuItem;
        private ToolStripMenuItem BackButtonToolStripMenuItem;
        private ToolStripMenuItem longEnterSizedButtonToolStripMenuItem;
        private ToolStripMenuItem shiftSizedButtonToolStripMenuItem;
        private ToolStripMenuItem numpadEnterHighButtonToolStripMenuItem;
        private ToolStripMenuItem layoutProToolStripMenuItem;
        private SaveFileDialog saveProjektDialog;
        private ToolStripMenuItem tsmSetupVK;
        private ToolStripMenuItem removeButtonToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem newProjectToolStripMenuItem;
        private OpenFileDialog openProjektDialog;
        private ToolStripMenuItem buildKbdFileToolStripMenuItem;
        private ToolStripMenuItem tsmSetupScancode;
        private ToolStripMenuItem layerSetupToolStripMenuItem;
        private ToolStripMenuItem tsmCtrlPressed;
        private ToolStripMenuItem tsmAltPressed;
        private ToolStripMenuItem tsmKanaPressed;
        private ToolStripMenuItem tsmRoyaPressed;
        private ToolStripMenuItem tsmLoyaPressed;
        private ToolStripMenuItem tsmCustomPressed;
        private ToolStripMenuItem tsmGRouPSELecTorAPingPressed;
        private ToolStripMenuItem tsmSetupLayers;
        private ToolStripMenuItem tsmShiftPressed;
        private ToolStripMenuItem setFlagsToolStripMenuItem;
        private ToolStripMenuItem setLockToolStripMenuItem;
        private ToolStripMenuItem extensionToolStripMenuItem;
        private ToolStripMenuItem multiToolStripMenuItem;
        private ToolStripMenuItem specialToolStripMenuItem;
        private ToolStripMenuItem numpadToolStripMenuItem;
        private ToolStripMenuItem capslockToolStripMenuItem;
        private ToolStripMenuItem sGCapsToolStripMenuItem;
        private ToolStripMenuItem capsAltGrToolStripMenuItem;
        private ToolStripMenuItem kanalockToolStripMenuItem;
        private ToolStripMenuItem groupselectorToolStripMenuItem;
        private ToolStripMenuItem buildKbdFileWithCommentsToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem numpadSetupToolStripMenuItem;
    }
}
