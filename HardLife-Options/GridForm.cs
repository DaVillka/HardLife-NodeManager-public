using HardLife_Options.Extensions;
using HardLife_Options.Nodes.QuestNodes;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

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
		private NodeCopyData _copyData = null;
		public GridForm()
		{
			InitializeComponent();
			stNodeEditor.LoadAssembly(Application.ExecutablePath);
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
			contextMenuStrip.Items[3].Click += (sender, e) => {
				/* stNodeEditor.ActiveNode;*/ 
				_copyData = new NodeCopyData() { GUID = stNodeEditor.ActiveNode.Guid.ToString(), NodeType = stNodeEditor.ActiveNode.GetType() };
			}; 
			contextMenuStrip.Items[4].Click += (sender, e) => {
				/* stNodeEditor.ActiveNode;*/

			};

		}
		public void DeleteSelectedNodes()
		{
			stNodeEditor.RemoveSelectedNode(stNodeEditor.ActiveNode);
		}

		public void DuplicateSelectedNodes()
		{
			var selected = stNodeEditor.GetSelectedNode();
			if (selected == null || selected.Length == 0)
			{
				if (stNodeEditor.ActiveNode == null)
				{
					stNodeEditor.ShowAlert("No node selected", Color.White, Color.FromArgb(125, Color.Orange));
					return;
				}

				selected = new[] { stNodeEditor.ActiveNode };
			}

			const int offsetX = 24;
			const int offsetY = 24;

			foreach (var node in selected)
				node.SetSelected(false, false);

			STNode lastCreated = null;
			foreach (var node in selected)
				lastCreated = DuplicateSingleNode(node, offsetX, offsetY);

			if (lastCreated != null)
				stNodeEditor.SetActiveNode(lastCreated);
		}

		private STNode DuplicateSingleNode(STNode source, int offsetX, int offsetY)
		{
			if (source == null)
				return null;

			byte[] saveData;
			try
			{
				saveData = InvokeNonPublic<byte[]>(source, "GetSaveData");
			}
			catch
			{
				stNodeEditor.ShowAlert("Duplicate failed (save)", Color.White, Color.FromArgb(125, Color.Red));
				return null;
			}

			if (!TryParseNodeSaveData(saveData, out var data))
			{
				stNodeEditor.ShowAlert("Duplicate failed (parse)", Color.White, Color.FromArgb(125, Color.Red));
				return null;
			}

			data["Guid"] = Guid.NewGuid().ToByteArray();
			if (data.TryGetValue("Left", out var leftBytes) && leftBytes.Length >= 4)
			{
				int left = BitConverter.ToInt32(leftBytes, 0);
				data["Left"] = BitConverter.GetBytes(left + offsetX);
			}

			if (data.TryGetValue("Top", out var topBytes) && topBytes.Length >= 4)
			{
				int top = BitConverter.ToInt32(topBytes, 0);
				data["Top"] = BitConverter.GetBytes(top + offsetY);
			}

			STNode clone;
			try
			{
				clone = (STNode)Activator.CreateInstance(source.GetType());
				InvokeNonPublic<object>(clone, "OnLoadNode", data);
			}
			catch
			{
				stNodeEditor.ShowAlert("Duplicate failed (load)", Color.White, Color.FromArgb(125, Color.Red));
				return null;
			}

			try
			{
				_ = stNodeEditor.Nodes.Add(clone);
				clone.SetSelected(true, false);
				return clone;
			}
			catch
			{
				stNodeEditor.ShowAlert("Duplicate failed (add)", Color.White, Color.FromArgb(125, Color.Red));
				return null;
			}
		}

		private static bool TryParseNodeSaveData(byte[] byData, out Dictionary<string, byte[]> dic)
		{
			dic = new Dictionary<string, byte[]>(StringComparer.Ordinal);
			try
			{
				int index = 0;
				if (byData == null || byData.Length < 2)
					return false;

				int modelLen = byData[index];
				index += modelLen + 1;
				if (index >= byData.Length)
					return false;

				int typeGuidLen = byData[index];
				index += typeGuidLen + 1;

				while (index < byData.Length)
				{
					if (index + 4 > byData.Length) return false;
					int keyLen = BitConverter.ToInt32(byData, index);
					index += 4;
					if (keyLen < 0 || index + keyLen > byData.Length) return false;
					string key = Encoding.UTF8.GetString(byData, index, keyLen);
					index += keyLen;

					if (index + 4 > byData.Length) return false;
					int valLen = BitConverter.ToInt32(byData, index);
					index += 4;
					if (valLen < 0 || index + valLen > byData.Length) return false;
					byte[] value = new byte[valLen];
					Buffer.BlockCopy(byData, index, value, 0, valLen);
					index += valLen;

					dic[key] = value;
				}

				return true;
			}
			catch
			{
				dic = new Dictionary<string, byte[]>(StringComparer.Ordinal);
				return false;
			}
		}

		private static T InvokeNonPublic<T>(object target, string methodName, params object[] args)
		{
			var type = target.GetType();
			MethodInfo mi = null;
			for (var t = type; t != null; t = t.BaseType)
			{
				mi = t.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
				if (mi != null)
					break;
			}

			if (mi == null)
				throw new MissingMethodException(type.FullName, methodName);

			return (T)mi.Invoke(target, args);
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
	internal class NodeCopyData
	{
		public string GUID { get; set; }
		public Type NodeType { get; set; }
	}
}
