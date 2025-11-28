namespace HardLife_Options
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
			this.gridForm1 = new HardLife_Options.GridForm();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// gridForm1
			// 
			this.gridForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridForm1.AutoSize = true;
			this.gridForm1.Location = new System.Drawing.Point(3, 2);
			this.gridForm1.Name = "gridForm1";
			this.gridForm1.Size = new System.Drawing.Size(1810, 550);
			this.gridForm1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoSize = true;
			this.panel1.Location = new System.Drawing.Point(3, 558);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1260, 121);
			this.panel1.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 681);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.gridForm1);
			this.Name = "MainForm";
			this.Text = "Grid";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GridForm gridForm1;
		private System.Windows.Forms.Panel panel1;
	}
}

