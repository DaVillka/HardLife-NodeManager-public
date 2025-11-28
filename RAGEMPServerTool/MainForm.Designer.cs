namespace RAGEMPServerTool
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabContent = new System.Windows.Forms.TabPage();
            this.panelContent = new System.Windows.Forms.Panel();
            this.contextMenuNew = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.treeContent = new System.Windows.Forms.TreeView();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.tDebug = new System.Windows.Forms.TextBox();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clothesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabContent.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.contextMenuNew.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1240, 480);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabContent);
            this.tabControl1.Controls.Add(this.tabDebug);
            this.tabControl1.Location = new System.Drawing.Point(12, 498);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1240, 183);
            this.tabControl1.TabIndex = 1;
            // 
            // tabContent
            // 
            this.tabContent.Controls.Add(this.panelContent);
            this.tabContent.Controls.Add(this.treeContent);
            this.tabContent.Location = new System.Drawing.Point(4, 22);
            this.tabContent.Name = "tabContent";
            this.tabContent.Padding = new System.Windows.Forms.Padding(3);
            this.tabContent.Size = new System.Drawing.Size(1232, 157);
            this.tabContent.TabIndex = 1;
            this.tabContent.Text = "Content";
            this.tabContent.UseVisualStyleBackColor = true;
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.ContextMenuStrip = this.contextMenuNew;
            this.panelContent.Controls.Add(this.checkBox2);
            this.panelContent.Controls.Add(this.checkBox1);
            this.panelContent.ForeColor = System.Drawing.Color.IndianRed;
            this.panelContent.Location = new System.Drawing.Point(238, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(994, 157);
            this.panelContent.TabIndex = 1;
            // 
            // contextMenuNew
            // 
            this.contextMenuNew.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.contextMenuNew.Name = "contextMenuNew";
            this.contextMenuNew.Size = new System.Drawing.Size(99, 26);
            // 
            // treeContent
            // 
            this.treeContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeContent.Location = new System.Drawing.Point(0, 0);
            this.treeContent.Name = "treeContent";
            this.treeContent.Size = new System.Drawing.Size(232, 157);
            this.treeContent.TabIndex = 0;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.tDebug);
            this.tabDebug.Location = new System.Drawing.Point(4, 22);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(1232, 157);
            this.tabDebug.TabIndex = 0;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // tDebug
            // 
            this.tDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tDebug.Location = new System.Drawing.Point(3, 3);
            this.tDebug.Multiline = true;
            this.tDebug.Name = "tDebug";
            this.tDebug.ReadOnly = true;
            this.tDebug.Size = new System.Drawing.Size(1226, 151);
            this.tDebug.TabIndex = 0;
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderToolStripMenuItem,
            this.clothesToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // clothesToolStripMenuItem
            // 
            this.clothesToolStripMenuItem.Name = "clothesToolStripMenuItem";
            this.clothesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clothesToolStripMenuItem.Text = "Cloth";
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.folderToolStripMenuItem.Text = "Folder";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(54, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(160, 20);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(80, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabContent.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.contextMenuNew.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.tabDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabContent;
        private System.Windows.Forms.TabPage tabDebug;
        protected internal System.Windows.Forms.TextBox tDebug;
        protected internal System.Windows.Forms.TreeView treeContent;
        protected internal System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.ContextMenuStrip contextMenuNew;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clothesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

