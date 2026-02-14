using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardLife_Options
{
	public partial class MainForm : Form
	{
		public MainForm()
		{

			InitializeComponent();
			this.KeyPreview = true;

		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete) gridForm1.DeleteSelectedNodes();
		}

		private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
		{

		}
		
		private void gridForm1_Load(object sender, EventArgs e)
		{
			
		}
		private void Save(string path)
		{
			gridForm1.stNodeEditor.SaveCanvas(path);
			gridForm1.stNodeEditor.ShowAlert("Saved", Color.White, Color.Green);

		}
		// TODO: Implement this method to save grid data
		// This method is called by GridsForm.SaveGridsToFile()
		public object SaveGridData(string path, string name)
		 {
			// Serialize the current grid state (nodes, connections, positions, etc.)
			// Return a serializable object containing the grid data
			string savePath = $"{path}\\{name}.stn";
			Save(savePath);

			 return new { savePath };
		 }

		// TODO: Implement this method to load grid data
		// This method is called by GridsForm.LoadGridsFromFile()
		public void LoadGridData(object gridData)
		{
		    string savePath = ((dynamic)gridData).savePath;
			gridForm1.stNodeEditor.LoadCanvas(savePath);
		}
	}
}
