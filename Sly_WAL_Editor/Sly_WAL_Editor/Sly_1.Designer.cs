namespace Sly_WAL_Editor
{
    partial class Sly_1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sly_1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.addAmbientFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.addLevelFileWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebuildArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceFileRequiresRebuildingWALToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceAmbientAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceDialogueDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceEffectsEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceMusicMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceWorldWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFileRequiresRebuildingWALToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 354);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(530, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(530, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openFile_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAmbientFileToolStripMenuItem,
            this.addToolStripMenuItem,
            this.addToolStripMenuItem1,
            this.addToolStripMenuItem2,
            this.addLevelFileWToolStripMenuItem,
            this.rebuildArchiveToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(40, 22);
            this.toolStripDropDownButton2.Text = "Edit";
            // 
            // addAmbientFileToolStripMenuItem
            // 
            this.addAmbientFileToolStripMenuItem.Enabled = false;
            this.addAmbientFileToolStripMenuItem.Name = "addAmbientFileToolStripMenuItem";
            this.addAmbientFileToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addAmbientFileToolStripMenuItem.Text = "Add Ambient File (A)";
            this.addAmbientFileToolStripMenuItem.Click += new System.EventHandler(this.addAFile);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Enabled = false;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addToolStripMenuItem.Text = "Add Dialogue (D)";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addAFile);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Enabled = false;
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.addToolStripMenuItem1.Text = "Add Sound Effects (E)";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addAFile);
            // 
            // addToolStripMenuItem2
            // 
            this.addToolStripMenuItem2.Enabled = false;
            this.addToolStripMenuItem2.Name = "addToolStripMenuItem2";
            this.addToolStripMenuItem2.Size = new System.Drawing.Size(188, 22);
            this.addToolStripMenuItem2.Text = "Add Music (M)";
            this.addToolStripMenuItem2.Click += new System.EventHandler(this.addAFile);
            // 
            // addLevelFileWToolStripMenuItem
            // 
            this.addLevelFileWToolStripMenuItem.Enabled = false;
            this.addLevelFileWToolStripMenuItem.Name = "addLevelFileWToolStripMenuItem";
            this.addLevelFileWToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.addLevelFileWToolStripMenuItem.Text = "Add Level File (W)";
            this.addLevelFileWToolStripMenuItem.Click += new System.EventHandler(this.addAFile);
            // 
            // rebuildArchiveToolStripMenuItem
            // 
            this.rebuildArchiveToolStripMenuItem.Enabled = false;
            this.rebuildArchiveToolStripMenuItem.Name = "rebuildArchiveToolStripMenuItem";
            this.rebuildArchiveToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.rebuildArchiveToolStripMenuItem.Text = "Rebuild Archive";
            this.rebuildArchiveToolStripMenuItem.Click += new System.EventHandler(this.rebuildWAL);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(44, 22);
            this.toolStripButton1.Text = "About";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(530, 329);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 109;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 121;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Offset";
            this.columnHeader4.Width = 119;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "file-icon-16.ico");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "No Files Loaded";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToolStripMenuItem,
            this.replaceFileRequiresRebuildingWALToolStripMenuItem,
            this.deleteFileRequiresRebuildingWALToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(281, 92);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "exportFile";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.extractToolStripMenuItem.Text = "Extract";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.exportFile);
            // 
            // replaceFileRequiresRebuildingWALToolStripMenuItem
            // 
            this.replaceFileRequiresRebuildingWALToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceAmbientAToolStripMenuItem,
            this.replaceDialogueDToolStripMenuItem,
            this.replaceEffectsEToolStripMenuItem,
            this.replaceMusicMToolStripMenuItem,
            this.replaceWorldWToolStripMenuItem});
            this.replaceFileRequiresRebuildingWALToolStripMenuItem.Name = "replaceFileRequiresRebuildingWALToolStripMenuItem";
            this.replaceFileRequiresRebuildingWALToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.replaceFileRequiresRebuildingWALToolStripMenuItem.Text = "Replace File (Requires Rebuilding WAL)";
            // 
            // replaceAmbientAToolStripMenuItem
            // 
            this.replaceAmbientAToolStripMenuItem.Name = "replaceAmbientAToolStripMenuItem";
            this.replaceAmbientAToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.replaceAmbientAToolStripMenuItem.Text = "Replace Ambient (A)";
            this.replaceAmbientAToolStripMenuItem.Click += new System.EventHandler(this.replaceFile);
            // 
            // replaceDialogueDToolStripMenuItem
            // 
            this.replaceDialogueDToolStripMenuItem.Name = "replaceDialogueDToolStripMenuItem";
            this.replaceDialogueDToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.replaceDialogueDToolStripMenuItem.Text = "Replace Dialogue (D)";
            this.replaceDialogueDToolStripMenuItem.Click += new System.EventHandler(this.replaceFile);
            // 
            // replaceEffectsEToolStripMenuItem
            // 
            this.replaceEffectsEToolStripMenuItem.Name = "replaceEffectsEToolStripMenuItem";
            this.replaceEffectsEToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.replaceEffectsEToolStripMenuItem.Text = "Replace Effects (E)";
            this.replaceEffectsEToolStripMenuItem.Click += new System.EventHandler(this.replaceFile);
            // 
            // replaceMusicMToolStripMenuItem
            // 
            this.replaceMusicMToolStripMenuItem.Name = "replaceMusicMToolStripMenuItem";
            this.replaceMusicMToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.replaceMusicMToolStripMenuItem.Text = "Replace Music (M)";
            this.replaceMusicMToolStripMenuItem.Click += new System.EventHandler(this.replaceFile);
            // 
            // replaceWorldWToolStripMenuItem
            // 
            this.replaceWorldWToolStripMenuItem.Name = "replaceWorldWToolStripMenuItem";
            this.replaceWorldWToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.replaceWorldWToolStripMenuItem.Text = "Replace World (W)";
            this.replaceWorldWToolStripMenuItem.Click += new System.EventHandler(this.replaceFile);
            // 
            // deleteFileRequiresRebuildingWALToolStripMenuItem
            // 
            this.deleteFileRequiresRebuildingWALToolStripMenuItem.Name = "deleteFileRequiresRebuildingWALToolStripMenuItem";
            this.deleteFileRequiresRebuildingWALToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.deleteFileRequiresRebuildingWALToolStripMenuItem.Text = "Delete File (Requires Rebuilding WAL)";
            this.deleteFileRequiresRebuildingWALToolStripMenuItem.Click += new System.EventHandler(this.deleteFile);
            // 
            // Sly_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 376);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Sly_1";
            this.Text = "Sly 1 WAL Editor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem addAmbientFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem addLevelFileWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebuildArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceFileRequiresRebuildingWALToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceAmbientAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceDialogueDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceEffectsEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceMusicMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceWorldWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFileRequiresRebuildingWALToolStripMenuItem;
    }
}