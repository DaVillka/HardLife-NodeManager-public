namespace HardLife_Options
{
	partial class GridForm
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

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.stNodeEditor = new ST.Library.UI.NodeEditor.STNodeEditor();
			this.stNodePropertyGrid = new ST.Library.UI.NodeEditor.STNodePropertyGrid();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lockLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lockConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// stNodeEditor
			// 
			this.stNodeEditor.AllowDrop = true;
			this.stNodeEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stNodeEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.stNodeEditor.Curvature = 0.3F;
			this.stNodeEditor.Location = new System.Drawing.Point(201, 0);
			this.stNodeEditor.LocationBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.stNodeEditor.MarkBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.stNodeEditor.MarkForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.stNodeEditor.MinimumSize = new System.Drawing.Size(100, 100);
			this.stNodeEditor.Name = "stNodeEditor";
			this.stNodeEditor.Size = new System.Drawing.Size(1016, 550);
			this.stNodeEditor.TabIndex = 6;
			this.stNodeEditor.Text = "stNodeEditor1";
			// 
			// stNodePropertyGrid
			// 
			this.stNodePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.stNodePropertyGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.stNodePropertyGrid.DescriptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(184)))), ((int)(((byte)(134)))), ((int)(((byte)(11)))));
			this.stNodePropertyGrid.ErrorColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(165)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
			this.stNodePropertyGrid.ForeColor = System.Drawing.Color.White;
			this.stNodePropertyGrid.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.stNodePropertyGrid.ItemValueBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.stNodePropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.stNodePropertyGrid.MinimumSize = new System.Drawing.Size(120, 50);
			this.stNodePropertyGrid.Name = "stNodePropertyGrid";
			this.stNodePropertyGrid.ShowTitle = true;
			this.stNodePropertyGrid.Size = new System.Drawing.Size(200, 550);
			this.stNodePropertyGrid.TabIndex = 5;
			this.stNodePropertyGrid.Text = "stNodePropertyGrid1";
			this.stNodePropertyGrid.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.lockLocationToolStripMenuItem,
            this.lockConnectionToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip1";
			this.contextMenuStrip.Size = new System.Drawing.Size(178, 114);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.removeToolStripMenuItem.Text = "&Remove";
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
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			// 
			// GridForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.stNodeEditor);
			this.Controls.Add(this.stNodePropertyGrid);
			this.Name = "GridForm";
			this.Size = new System.Drawing.Size(1216, 550);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private ST.Library.UI.NodeEditor.STNodePropertyGrid stNodePropertyGrid;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lockLocationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lockConnectionToolStripMenuItem;
		public ST.Library.UI.NodeEditor.STNodeEditor stNodeEditor;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
	}
}
