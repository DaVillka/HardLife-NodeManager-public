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
	public partial class NodesForm : Form
	{
		public NodesForm()
		{
			InitializeComponent();
			stNodeTreeView.LoadAssembly(Application.ExecutablePath);
			stNodeTreeView.ExpandAll();

		}
	}
}
