namespace HardLife_Options
{
	partial class NodesForm
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
			this.stNodeTreeView = new ST.Library.UI.NodeEditor.STNodeTreeView();
			this.SuspendLayout();
			// 
			// stNodeTreeView
			// 
			this.stNodeTreeView.AllowDrop = true;
			this.stNodeTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stNodeTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
			this.stNodeTreeView.FolderCountColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.stNodeTreeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.stNodeTreeView.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
			this.stNodeTreeView.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
			this.stNodeTreeView.Location = new System.Drawing.Point(0, 0);
			this.stNodeTreeView.MinimumSize = new System.Drawing.Size(100, 60);
			this.stNodeTreeView.Name = "stNodeTreeView";
			this.stNodeTreeView.ShowFolderCount = true;
			this.stNodeTreeView.Size = new System.Drawing.Size(285, 681);
			this.stNodeTreeView.TabIndex = 0;
			this.stNodeTreeView.Text = "stNodeTreeView1";
			this.stNodeTreeView.TextBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.stNodeTreeView.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			// 
			// NodesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 681);
			this.Controls.Add(this.stNodeTreeView);
			this.Name = "NodesForm";
			this.Text = "Nodes";
			this.ResumeLayout(false);

		}

		#endregion

		private ST.Library.UI.NodeEditor.STNodeTreeView stNodeTreeView;
	}
}