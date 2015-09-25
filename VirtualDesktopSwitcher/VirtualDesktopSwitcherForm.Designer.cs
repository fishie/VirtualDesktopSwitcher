namespace VirtualDesktopSwitcher
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
            this.taskViewScrollCheckbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trayIconRightClickMenu.SuspendLayout();
            this.treeViewRightClickMenuAdd.SuspendLayout();
            this.treeViewRightClickMenuRemove.SuspendLayout();
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
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // desktopScrollCheckbox
            // 
            this.desktopScrollCheckbox.AutoSize = true;
            this.desktopScrollCheckbox.Location = new System.Drawing.Point(34, 12);
            this.desktopScrollCheckbox.Name = "desktopScrollCheckbox";
            this.desktopScrollCheckbox.Size = new System.Drawing.Size(93, 17);
            this.desktopScrollCheckbox.TabIndex = 1;
            this.desktopScrollCheckbox.Text = "Desktop scroll";
            this.desktopScrollCheckbox.UseVisualStyleBackColor = true;
            this.desktopScrollCheckbox.CheckedChanged += new System.EventHandler(this.desktopScrollCheckbox_CheckedChanged);
            // 
            // hideOnStartupCheckbox
            // 
            this.hideOnStartupCheckbox.AutoSize = true;
            this.hideOnStartupCheckbox.Location = new System.Drawing.Point(34, 81);
            this.hideOnStartupCheckbox.Name = "hideOnStartupCheckbox";
            this.hideOnStartupCheckbox.Size = new System.Drawing.Size(83, 17);
            this.hideOnStartupCheckbox.TabIndex = 2;
            this.hideOnStartupCheckbox.Text = "Start hidden";
            this.hideOnStartupCheckbox.UseVisualStyleBackColor = true;
            this.hideOnStartupCheckbox.CheckedChanged += new System.EventHandler(this.hideOnStartupCheckbox_CheckedChanged);
            // 
            // loadOnWindowsStartupCheckbox
            // 
            this.loadOnWindowsStartupCheckbox.AutoSize = true;
            this.loadOnWindowsStartupCheckbox.Location = new System.Drawing.Point(34, 58);
            this.loadOnWindowsStartupCheckbox.Name = "loadOnWindowsStartupCheckbox";
            this.loadOnWindowsStartupCheckbox.Size = new System.Drawing.Size(147, 17);
            this.loadOnWindowsStartupCheckbox.TabIndex = 3;
            this.loadOnWindowsStartupCheckbox.Text = "Load on Windows startup";
            this.loadOnWindowsStartupCheckbox.UseVisualStyleBackColor = true;
            this.loadOnWindowsStartupCheckbox.CheckedChanged += new System.EventHandler(this.loadOnWindowsStartupCheckbox_CheckedChanged);
            // 
            // rectanglesTreeView
            // 
            this.rectanglesTreeView.ContextMenuStrip = this.treeViewRightClickMenuAdd;
            this.rectanglesTreeView.Location = new System.Drawing.Point(34, 127);
            this.rectanglesTreeView.Name = "rectanglesTreeView";
            this.rectanglesTreeView.Size = new System.Drawing.Size(217, 122);
            this.rectanglesTreeView.TabIndex = 4;
            this.rectanglesTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.rectanglesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.rectanglesTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
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
            this.addRectangleToolStripMenuItem.Click += new System.EventHandler(this.addRectangleToolStripMenuItem_Click);
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
            this.removeRectangleToolStripMenuItem.Click += new System.EventHandler(this.removeRectangleToolStripMenuItem_Click);
            // 
            // taskViewScrollCheckbox
            // 
            this.taskViewScrollCheckbox.AutoSize = true;
            this.taskViewScrollCheckbox.Location = new System.Drawing.Point(34, 35);
            this.taskViewScrollCheckbox.Name = "taskViewScrollCheckbox";
            this.taskViewScrollCheckbox.Size = new System.Drawing.Size(103, 17);
            this.taskViewScrollCheckbox.TabIndex = 5;
            this.taskViewScrollCheckbox.Text = "Task View scroll";
            this.taskViewScrollCheckbox.UseVisualStyleBackColor = true;
            this.taskViewScrollCheckbox.CheckedChanged += new System.EventHandler(this.taskViewScrollCheckbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "+ Rectangles";
            this.label1.Click += new System.EventHandler(this.ToggleRectangles);
            this.label1.DoubleClick += new System.EventHandler(this.ToggleRectangles);
            // 
            // VirtualDesktopSwitcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.taskViewScrollCheckbox);
            this.Controls.Add(this.rectanglesTreeView);
            this.Controls.Add(this.loadOnWindowsStartupCheckbox);
            this.Controls.Add(this.hideOnStartupCheckbox);
            this.Controls.Add(this.desktopScrollCheckbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.Name = "VirtualDesktopSwitcherForm";
            this.ShowInTaskbar = false;
            this.Text = "VirtualDesktopSwitcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VirtualDesktopSwitcherForm_FormClosed);
            this.Shown += new System.EventHandler(this.VirtualDesktopSwitcherForm_Shown);
            this.VisibleChanged += new System.EventHandler(this.VirtualDesktopSwitcherForm_VisibleChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToggleVisibilityWithMouseClick);
            this.trayIconRightClickMenu.ResumeLayout(false);
            this.treeViewRightClickMenuAdd.ResumeLayout(false);
            this.treeViewRightClickMenuRemove.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox taskViewScrollCheckbox;
        private System.Windows.Forms.Label label1;
    }
}

