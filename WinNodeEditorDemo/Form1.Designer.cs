namespace WinNodeEditorDemo
{
    partial class HardLifeEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lockLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lockConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabNodeEditors = new System.Windows.Forms.TabControl();
			this.tabStartup = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabEditors = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tabEditorAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tabEditorRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.stNodePropertyGrid = new ST.Library.UI.NodeEditor.STNodePropertyGrid();
			this.stNodeTreeView = new ST.Library.UI.NodeEditor.STNodeTreeView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadAndAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ConsoleOutputTextBox = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1.SuspendLayout();
			this.tabNodeEditors.SuspendLayout();
			this.tabStartup.SuspendLayout();
			this.tabEditors.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.lockLocationToolStripMenuItem,
            this.lockConnectionToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(178, 70);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.removeToolStripMenuItem.Text = "&Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// lockLocationToolStripMenuItem
			// 
			this.lockLocationToolStripMenuItem.Name = "lockLocationToolStripMenuItem";
			this.lockLocationToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.lockLocationToolStripMenuItem.Text = "U/Lock &Location";
			// 
			// lockConnectionToolStripMenuItem
			// 
			this.lockConnectionToolStripMenuItem.Name = "lockConnectionToolStripMenuItem";
			this.lockConnectionToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.lockConnectionToolStripMenuItem.Text = "U/Lock &Connection";
			this.lockConnectionToolStripMenuItem.Click += new System.EventHandler(this.lockConnectionToolStripMenuItem_Click);
			// 
			// tabNodeEditors
			// 
			this.tabNodeEditors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabNodeEditors.Controls.Add(this.tabStartup);
			this.tabNodeEditors.Location = new System.Drawing.Point(218, 27);
			this.tabNodeEditors.Name = "tabNodeEditors";
			this.tabNodeEditors.SelectedIndex = 0;
			this.tabNodeEditors.Size = new System.Drawing.Size(1034, 639);
			this.tabNodeEditors.TabIndex = 5;
			this.tabNodeEditors.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabNodeEditors_MouseClick);
			// 
			// tabStartup
			// 
			this.tabStartup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.tabStartup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tabStartup.Controls.Add(this.label2);
			this.tabStartup.Controls.Add(this.label1);
			this.tabStartup.Location = new System.Drawing.Point(4, 22);
			this.tabStartup.Name = "tabStartup";
			this.tabStartup.Padding = new System.Windows.Forms.Padding(3);
			this.tabStartup.Size = new System.Drawing.Size(1026, 613);
			this.tabStartup.TabIndex = 0;
			this.tabStartup.Text = "Startup";
			this.tabStartup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabStartup_MouseClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.Brown;
			this.label2.Location = new System.Drawing.Point(580, 288);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 31);
			this.label2.TabIndex = 1;
			this.label2.Text = "RP";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.LightCoral;
			this.label1.Location = new System.Drawing.Point(455, 288);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 31);
			this.label1.TabIndex = 0;
			this.label1.Text = "Five City";
			// 
			// tabEditors
			// 
			this.tabEditors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabEditorAdd,
            this.tabEditorRemove});
			this.tabEditors.Name = "contextMenuStrip1";
			this.tabEditors.Size = new System.Drawing.Size(104, 48);
			this.tabEditors.Opening += new System.ComponentModel.CancelEventHandler(this.tabEditors_Opening);
			// 
			// tabEditorAdd
			// 
			this.tabEditorAdd.Name = "tabEditorAdd";
			this.tabEditorAdd.Size = new System.Drawing.Size(103, 22);
			this.tabEditorAdd.Text = "&Add";
			// 
			// tabEditorRemove
			// 
			this.tabEditorRemove.Name = "tabEditorRemove";
			this.tabEditorRemove.Size = new System.Drawing.Size(103, 22);
			this.tabEditorRemove.Text = "&Close";
			// 
			// stNodePropertyGrid
			// 
			this.stNodePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.stNodePropertyGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.stNodePropertyGrid.DescriptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(184)))), ((int)(((byte)(134)))), ((int)(((byte)(11)))));
			this.stNodePropertyGrid.ErrorColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(165)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
			this.stNodePropertyGrid.ForeColor = System.Drawing.Color.White;
			this.stNodePropertyGrid.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.stNodePropertyGrid.ItemValueBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.stNodePropertyGrid.Location = new System.Drawing.Point(12, 481);
			this.stNodePropertyGrid.MinimumSize = new System.Drawing.Size(120, 50);
			this.stNodePropertyGrid.Name = "stNodePropertyGrid";
			this.stNodePropertyGrid.ShowTitle = true;
			this.stNodePropertyGrid.Size = new System.Drawing.Size(200, 249);
			this.stNodePropertyGrid.TabIndex = 2;
			this.stNodePropertyGrid.Text = "stNodePropertyGrid1";
			this.stNodePropertyGrid.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// stNodeTreeView
			// 
			this.stNodeTreeView.AllowDrop = true;
			this.stNodeTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.stNodeTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.stNodeTreeView.FolderCountColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.stNodeTreeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.stNodeTreeView.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
			this.stNodeTreeView.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.stNodeTreeView.Location = new System.Drawing.Point(12, 27);
			this.stNodeTreeView.MinimumSize = new System.Drawing.Size(100, 60);
			this.stNodeTreeView.Name = "stNodeTreeView";
			this.stNodeTreeView.ShowFolderCount = true;
			this.stNodeTreeView.Size = new System.Drawing.Size(200, 448);
			this.stNodeTreeView.TabIndex = 0;
			this.stNodeTreeView.Text = "stNodeTreeView1";
			this.stNodeTreeView.TextBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.stNodeTreeView.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.compileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.loadAndAddToolStripMenuItem});
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(48, 20);
			this.toolStripComboBox1.Text = "&Файл";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.saveToolStripMenuItem.Text = "&Сохранить";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.saveAsToolStripMenuItem.Text = "&Сохранить как...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.loadToolStripMenuItem.Text = "&Загрузить";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
			// 
			// loadAndAddToolStripMenuItem
			// 
			this.loadAndAddToolStripMenuItem.Name = "loadAndAddToolStripMenuItem";
			this.loadAndAddToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.loadAndAddToolStripMenuItem.Text = "&Загрузить и добавить";
			this.loadAndAddToolStripMenuItem.Click += new System.EventHandler(this.loadAndAddToolStripMenuItem_Click);
			// 
			// compileToolStripMenuItem
			// 
			this.compileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clothToolStripMenuItem});
			this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
			this.compileToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
			this.compileToolStripMenuItem.Text = "&Компиляция";
			// 
			// clothToolStripMenuItem
			// 
			this.clothToolStripMenuItem.Name = "clothToolStripMenuItem";
			this.clothToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.clothToolStripMenuItem.Text = "&Одежда";
			// 
			// ConsoleOutputTextBox
			// 
			this.ConsoleOutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ConsoleOutputTextBox.Location = new System.Drawing.Point(218, 672);
			this.ConsoleOutputTextBox.Multiline = true;
			this.ConsoleOutputTextBox.Name = "ConsoleOutputTextBox";
			this.ConsoleOutputTextBox.ReadOnly = true;
			this.ConsoleOutputTextBox.Size = new System.Drawing.Size(1030, 58);
			this.ConsoleOutputTextBox.TabIndex = 7;
			// 
			// HardLifeEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 742);
			this.Controls.Add(this.ConsoleOutputTextBox);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.tabNodeEditors);
			this.Controls.Add(this.stNodePropertyGrid);
			this.Controls.Add(this.stNodeTreeView);
			this.Name = "HardLifeEditor";
			this.Text = "HardLifeEditor";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.tabNodeEditors.ResumeLayout(false);
			this.tabStartup.ResumeLayout(false);
			this.tabStartup.PerformLayout();
			this.tabEditors.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private ST.Library.UI.NodeEditor.STNodePropertyGrid stNodePropertyGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockConnectionToolStripMenuItem;
        internal ST.Library.UI.NodeEditor.STNodeTreeView stNodeTreeView;
        private System.Windows.Forms.TabControl tabNodeEditors;
        private System.Windows.Forms.TabPage tabStartup;
        private System.Windows.Forms.ContextMenuStrip tabEditors;
        private System.Windows.Forms.ToolStripMenuItem tabEditorAdd;
        private System.Windows.Forms.ToolStripMenuItem tabEditorRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ConsoleOutputTextBox;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem clothToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAndAddToolStripMenuItem;
    }
}

