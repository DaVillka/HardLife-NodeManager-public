using HardLife_Options.Extensions;
using HardLife_Options.Nodes.QuestNodes;
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
using HardLife_Options.Nodes.QuestNodes;

namespace HardLife_Options
{
	public partial class GridForm : UserControl
	{
		public static readonly Dictionary<Type, Color> TypeColors = new Dictionary<Type, Color>()
		{
			{ typeof(string), Color.Green },
			{ typeof(int),    Color.Teal  },
			{ typeof(float),  Color.Aqua  },
		
		    // списки базовых типов
		    { typeof(List<int>),    Color.FromArgb(150, Color.Teal)  },
			{ typeof(List<string>), Color.FromArgb(150, Color.Green) },
			{ typeof(List<float>),  Color.FromArgb(150, Color.Aqua)  },
		
		    // --- квестовые типы ---
		    { typeof(bool),                 Color.Gold },              // условия/флаги (CanStart и т.п.)
		    { typeof(QuestNode.Flow),       Color.OrangeRed },         // «поток» между квестами/событиями
		    { typeof(List<QuestNode.Flow>), Color.FromArgb(150, Color.OrangeRed) },
		
		    // если отдаёте DTO наружу узла
		    { typeof(QuestNode.QuestData),       Color.MediumPurple },
			{ typeof(List<QuestNode.QuestData>), Color.FromArgb(150, Color.MediumPurple) },

			{typeof(ObjectiveState), Color.Orange },
			{typeof(RewardBase)   , Color.Gold }
		};

		public GridForm()
		{
			InitializeComponent();
			stNodePropertyGrid.Text = "Node Property";

			stNodeEditor.ActiveChanged += ActiveChanged;
			stNodeEditor.OptionConnected += OptionConnected;
			stNodeEditor.OptionDisConnected += OptionDisConnected;
			stNodeEditor.CanvasScaled += CanvasScaled;
			stNodeEditor.NodeAdded += NodeAdded;
			stNodeEditor.NodeRemoved += NodeRemoved;

			foreach (KeyValuePair<Type, Color> item in TypeColors)
				stNodeEditor.SetTypeColor(item.Key, item.Value);

			contextMenuStrip.ShowImageMargin = false;
			contextMenuStrip.Renderer = new ToolStripRendererEx();

			contextMenuStrip.Items[0].Click += (sender, e) => stNodeEditor.Nodes.Remove(stNodeEditor.ActiveNode); 
			contextMenuStrip.Items[1].Click += (sender, e) => stNodeEditor.ActiveNode.LockLocation = !stNodeEditor.ActiveNode.LockLocation;
			contextMenuStrip.Items[2].Click += (sender, e) => stNodeEditor.ActiveNode.LockOption = !stNodeEditor.ActiveNode.LockOption;

		}
		public void DeleteSelectedNodes()
		{
			stNodeEditor.RemoveSelectedNode(stNodeEditor.ActiveNode);
		}
		private void NodeRemoved(object s, STNodeEditorEventArgs ea)
		{
		}

		private void NodeAdded(object s, STNodeEditorEventArgs ea)
		{
			ea.Node.ContextMenuStrip = contextMenuStrip;
		}

		private void CanvasScaled(object s, EventArgs ea)
		{
			if (s is STNodeEditor stNodeEditor)
				stNodeEditor.ShowAlert(stNodeEditor.CanvasScale.ToString(""), Color.White, Color.FromArgb(125, Color.Yellow));
			
		}

		private void OptionConnected(object s, STNodeEditorOptionEventArgs ea)
		{
			if (s is STNodeEditor stNodeEditor)
				stNodeEditor.ShowAlert(ea.Status.ToString(), Color.White, ea.Status == ConnectionStatus.Connected ? Color.FromArgb(125, Color.Green) : Color.FromArgb(125, Color.Red));
		}
		private void OptionDisConnected(object s, STNodeEditorOptionEventArgs ea)
		{
			//if (ea.CurrentOption.Owner is IServer serverIn) Server.Send(serverIn.GetData(SendType.Update));
			//if (ea.TargetOption.Owner is IServer serverOut) Server.Send(serverOut.GetData(SendType.Update));
		}
		private void ActiveChanged(object s, EventArgs ea)
		{
			if (s is STNodeEditor stNodeEditor)
				stNodePropertyGrid.SetNode(stNodeEditor.ActiveNode);			
		}
	}
}
