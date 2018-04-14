namespace VirtualDesktopSwitcher.Code
{
    partial class VirtualDesktopSwitcherForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VirtualDesktopSwitcherForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desktopScrollCheckbox = new System.Windows.Forms.CheckBox();
            this.hideOnStartupCheckbox = new System.Windows.Forms.CheckBox();
            this.loadOnWindowsStartupCheckbox = new System.Windows.Forms.CheckBox();
            this.rectanglesTreeView = new System.Windows.Forms.TreeView();
            this.treeViewRightClickMenuAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewRightClickMenuRemove = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskViewButtonScrollCheckbox = new System.Windows.Forms.CheckBox();
            this.advancedLabel = new System.Windows.Forms.Label();
            this.virtualBoxFixCheckbox = new System.Windows.Forms.CheckBox();
            this.versionLabel = new System.Windows.Forms.TextBox();
            this.panel1 = new VirtualDesktopSwitcher.Code.CustomFormControls.TitlePanel();
            this.minimizeButton = new VirtualDesktopSwitcher.Code.CustomFormControls.MinimizeButton();
            this.exitButton = new VirtualDesktopSwitcher.Code.CustomFormControls.ExitButton();
            this.formTitle = new System.Windows.Forms.LinkLabel();
            this.trayIconRightClickMenu.SuspendLayout();
            this.treeViewRightClickMenuAdd.SuspendLayout();
            this.treeViewRightClickMenuRemove.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // notifyIcon
            //
            this.notifyIcon.ContextMenuStrip = this.trayIconRightClickMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "VirtualDesktopSwitcher";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToggleVisibilityWithMouseClick);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ToggleVisibilityWithMouseClick);
            //
            // trayIconRightClickMenu
            //
            this.trayIconRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.trayIconRightClickMenu.Name = "trayIconRightClickMenu";
            this.trayIconRightClickMenu.ShowImageMargin = false;
            this.trayIconRightClickMenu.Size = new System.Drawing.Size(68, 26);
            //
            // exitMenuItem
            //
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(67, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            //
            // desktopScrollCheckbox
            //
            this.desktopScrollCheckbox.AutoSize = true;
            this.desktopScrollCheckbox.Location = new System.Drawing.Point(34, 39);
            this.desktopScrollCheckbox.Name = "desktopScrollCheckbox";
            this.desktopScrollCheckbox.Size = new System.Drawing.Size(93, 17);
            this.desktopScrollCheckbox.TabIndex = 1;
            this.desktopScrollCheckbox.Text = "Desktop scroll";
            this.desktopScrollCheckbox.UseVisualStyleBackColor = true;
            this.desktopScrollCheckbox.CheckedChanged += new System.EventHandler(this.DesktopScrollCheckbox_CheckedChanged);
            //
            // hideOnStartupCheckbox
            //
            this.hideOnStartupCheckbox.AutoSize = true;
            this.hideOnStartupCheckbox.Location = new System.Drawing.Point(34, 108);
            this.hideOnStartupCheckbox.Name = "hideOnStartupCheckbox";
            this.hideOnStartupCheckbox.Size = new System.Drawing.Size(83, 17);
            this.hideOnStartupCheckbox.TabIndex = 4;
            this.hideOnStartupCheckbox.Text = "Start hidden";
            this.hideOnStartupCheckbox.UseVisualStyleBackColor = true;
            this.hideOnStartupCheckbox.CheckedChanged += new System.EventHandler(this.HideOnStartupCheckbox_CheckedChanged);
            //
            // loadOnWindowsStartupCheckbox
            //
            this.loadOnWindowsStartupCheckbox.AutoSize = true;
            this.loadOnWindowsStartupCheckbox.Location = new System.Drawing.Point(34, 85);
            this.loadOnWindowsStartupCheckbox.Name = "loadOnWindowsStartupCheckbox";
            this.loadOnWindowsStartupCheckbox.Size = new System.Drawing.Size(147, 17);
            this.loadOnWindowsStartupCheckbox.TabIndex = 3;
            this.loadOnWindowsStartupCheckbox.Text = "Load on Windows startup";
            this.loadOnWindowsStartupCheckbox.UseVisualStyleBackColor = true;
            this.loadOnWindowsStartupCheckbox.CheckedChanged += new System.EventHandler(this.LoadOnWindowsStartupCheckbox_CheckedChanged);
            //
            // rectanglesTreeView
            //
            this.rectanglesTreeView.ContextMenuStrip = this.treeViewRightClickMenuAdd;
            this.rectanglesTreeView.Location = new System.Drawing.Point(34, 177);
            this.rectanglesTreeView.Name = "rectanglesTreeView";
            this.rectanglesTreeView.Size = new System.Drawing.Size(217, 124);
            this.rectanglesTreeView.TabIndex = 4;
            this.rectanglesTreeView.Visible = false;
            this.rectanglesTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.RectanglesTreeView_AfterLabelEdit);
            this.rectanglesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.RectanglesTreeView_NodeMouseClick);
            this.rectanglesTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RectanglesTreeView_MouseDown);
            //
            // treeViewRightClickMenuAdd
            //
            this.treeViewRightClickMenuAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRectangleToolStripMenuItem});
            this.treeViewRightClickMenuAdd.Name = "treeViewRightClickMenu";
            this.treeViewRightClickMenuAdd.Size = new System.Drawing.Size(149, 26);
            //
            // addRectangleToolStripMenuItem
            //
            this.addRectangleToolStripMenuItem.Name = "addRectangleToolStripMenuItem";
            this.addRectangleToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addRectangleToolStripMenuItem.Text = "Add rectangle";
            this.addRectangleToolStripMenuItem.Click += new System.EventHandler(this.AddRectangleToolStripMenuItem_Click);
            //
            // treeViewRightClickMenuRemove
            //
            this.treeViewRightClickMenuRemove.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeRectangleToolStripMenuItem});
            this.treeViewRightClickMenuRemove.Name = "treeViewRightClickMenuRemove";
            this.treeViewRightClickMenuRemove.Size = new System.Drawing.Size(170, 26);
            //
            // removeRectangleToolStripMenuItem
            //
            this.removeRectangleToolStripMenuItem.Name = "removeRectangleToolStripMenuItem";
            this.removeRectangleToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.removeRectangleToolStripMenuItem.Text = "Remove rectangle";
            this.removeRectangleToolStripMenuItem.Click += new System.EventHandler(this.RemoveRectangleToolStripMenuItem_Click);
            //
            // taskViewButtonScrollCheckbox
            //
            this.taskViewButtonScrollCheckbox.AutoSize = true;
            this.taskViewButtonScrollCheckbox.Location = new System.Drawing.Point(34, 62);
            this.taskViewButtonScrollCheckbox.Name = "taskViewButtonScrollCheckbox";
            this.taskViewButtonScrollCheckbox.Size = new System.Drawing.Size(136, 17);
            this.taskViewButtonScrollCheckbox.TabIndex = 2;
            this.taskViewButtonScrollCheckbox.Text = "Task View button scroll";
            this.taskViewButtonScrollCheckbox.UseVisualStyleBackColor = true;
            this.taskViewButtonScrollCheckbox.CheckedChanged += new System.EventHandler(this.TaskViewButtonScrollCheckbox_CheckedChanged);
            //
            // advancedLabel
            //
            this.advancedLabel.AutoSize = true;
            this.advancedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.advancedLabel.Location = new System.Drawing.Point(31, 161);
            this.advancedLabel.Name = "advancedLabel";
            this.advancedLabel.Size = new System.Drawing.Size(75, 13);
            this.advancedLabel.TabIndex = 6;
            this.advancedLabel.Text = "+ Advanced";
            this.advancedLabel.Click += new System.EventHandler(this.ToggleRectangles);
            this.advancedLabel.DoubleClick += new System.EventHandler(this.ToggleRectangles);
            //
            // virtualBoxFixCheckbox
            //
            this.virtualBoxFixCheckbox.AutoSize = true;
            this.virtualBoxFixCheckbox.Location = new System.Drawing.Point(34, 131);
            this.virtualBoxFixCheckbox.Name = "virtualBoxFixCheckbox";
            this.virtualBoxFixCheckbox.Size = new System.Drawing.Size(86, 17);
            this.virtualBoxFixCheckbox.TabIndex = 5;
            this.virtualBoxFixCheckbox.Text = "VirtualBox fix";
            this.virtualBoxFixCheckbox.UseVisualStyleBackColor = true;
            this.virtualBoxFixCheckbox.CheckedChanged += new System.EventHandler(this.VirtualBoxFixCheckbox_CheckedChanged);
            //
            // versionLabel
            //
            this.versionLabel.BackColor = System.Drawing.SystemColors.Control;
            this.versionLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(133, 28);
            this.versionLabel.Multiline = true;
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(146, 13);
            this.versionLabel.TabIndex = 9;
            this.versionLabel.TabStop = false;
            this.versionLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.minimizeButton);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Controls.Add(this.formTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 25);
            this.panel1.TabIndex = 7;
            //
            // minimizeButton
            //
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Location = new System.Drawing.Point(237, 3);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(19, 19);
            this.minimizeButton.TabIndex = 2;
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToggleVisibilityWithMouseClick);
            //
            // exitButton
            //
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Location = new System.Drawing.Point(262, 3);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(19, 19);
            this.exitButton.TabIndex = 1;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.ExitMenuItem_Click);
            //
            // formTitle
            //
            this.formTitle.Location = new System.Drawing.Point(3, 0);
            this.formTitle.Name = "formTitle";
            this.formTitle.Size = new System.Drawing.Size(207, 25);
            this.formTitle.TabIndex = 0;
            this.formTitle.TabStop = true;
            this.formTitle.Text = "GitHub.com/fishie/VirtualDesktopSwitcher";
            this.formTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FormTitle_LinkClicked);
            //
            // VirtualDesktopSwitcherForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 190);
            this.ControlBox = false;
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.virtualBoxFixCheckbox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.advancedLabel);
            this.Controls.Add(this.taskViewButtonScrollCheckbox);
            this.Controls.Add(this.rectanglesTreeView);
            this.Controls.Add(this.loadOnWindowsStartupCheckbox);
            this.Controls.Add(this.hideOnStartupCheckbox);
            this.Controls.Add(this.desktopScrollCheckbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "VirtualDesktopSwitcherForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VirtualDesktopSwitcherForm_FormClosed);
            this.VisibleChanged += new System.EventHandler(this.VirtualDesktopSwitcherForm_VisibleChanged);
            this.trayIconRightClickMenu.ResumeLayout(false);
            this.treeViewRightClickMenuAdd.ResumeLayout(false);
            this.treeViewRightClickMenuRemove.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.CheckBox desktopScrollCheckbox;
        private System.Windows.Forms.CheckBox hideOnStartupCheckbox;
        private System.Windows.Forms.CheckBox loadOnWindowsStartupCheckbox;
        private System.Windows.Forms.TreeView rectanglesTreeView;
        private System.Windows.Forms.ContextMenuStrip treeViewRightClickMenuAdd;
        private System.Windows.Forms.ToolStripMenuItem addRectangleToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip treeViewRightClickMenuRemove;
        private System.Windows.Forms.ToolStripMenuItem removeRectangleToolStripMenuItem;
        private System.Windows.Forms.CheckBox taskViewButtonScrollCheckbox;
        private System.Windows.Forms.Label advancedLabel;
        private VirtualDesktopSwitcher.Code.CustomFormControls.TitlePanel panel1;
        private System.Windows.Forms.LinkLabel formTitle;
        private VirtualDesktopSwitcher.Code.CustomFormControls.ExitButton exitButton;
        private VirtualDesktopSwitcher.Code.CustomFormControls.MinimizeButton minimizeButton;
        private System.Windows.Forms.CheckBox virtualBoxFixCheckbox;
        private System.Windows.Forms.TextBox versionLabel;
    }
}

