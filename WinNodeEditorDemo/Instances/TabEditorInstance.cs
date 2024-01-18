using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinNodeEditorDemo.Core.Extensions;
using WinNodeEditorDemo.Interfaces;

namespace WinNodeEditorDemo.Instances
{
    internal class TabEditorInstance
    {
        #region Singleton
        public static TabEditorInstance Instance { get; private set; }
        #endregion

        public static Action<STNodeEditor> OnCreateEditor = null;
        private readonly Form _form = null;
        private readonly TabControl _tabControl;
        private readonly STNodePropertyGrid _sTNodePropertyGrid;
        private readonly ContextMenuStrip _contextMenuStrip;
        private readonly ContextMenuStrip _tabEditors;

        private readonly List<TabPage> _tabPages = new();
        private readonly Dictionary<TabPage, STNodeEditorEx> _stNodeEditors = new();
        private readonly Dictionary<STNodeEditorEx, string> _stNodeSavePathes = new();

        public static readonly Dictionary<Type, Color> TypeColors = new()
        {
            { typeof(string), Color.Green },
            { typeof(int), Color.Teal },
            { typeof(float), Color.Aqua },
            { typeof(IClothIdPair), Color.BlueViolet },
            { typeof(IArrayInt), Color.FromArgb(200, Color.Teal) },
            { typeof(IArrayString), Color.FromArgb(200,Color.Green) },
            { typeof(IArrayFloat), Color.FromArgb(200,Color.Aqua) },
            { typeof(List<int>), Color.FromArgb(150,Color.Teal) },
            { typeof(List<string>), Color.FromArgb(150,Color.Green) },
            { typeof(List<float>), Color.FromArgb(150,Color.Aqua) },
        };
        public TabEditorInstance(Form form, TabControl tabControl, STNodePropertyGrid sTNodePropertyGrid, ContextMenuStrip contextMenuStrip, ContextMenuStrip tabEditors)
        {
            Instance = this;
            _form = form;
            _tabControl = tabControl;
            _sTNodePropertyGrid = sTNodePropertyGrid;
            _contextMenuStrip = contextMenuStrip;
            _tabEditors = tabEditors;

            contextMenuStrip.Items[0].Click += (sender, e) => { if (_stNodeEditors.ContainsKey(_tabControl.SelectedTab) && _stNodeEditors[_tabControl.SelectedTab].ActiveNode != null) { _stNodeEditors[_tabControl.SelectedTab].Nodes.Remove(_stNodeEditors[_tabControl.SelectedTab].ActiveNode); } };
            contextMenuStrip.Items[1].Click += (sender, e) => { if (_stNodeEditors.ContainsKey(_tabControl.SelectedTab) && _stNodeEditors[_tabControl.SelectedTab].ActiveNode != null) { _stNodeEditors[_tabControl.SelectedTab].ActiveNode.LockLocation = !_stNodeEditors[_tabControl.SelectedTab].ActiveNode.LockLocation; } };
            contextMenuStrip.Items[2].Click += (sender, e) => { if (_stNodeEditors.ContainsKey(_tabControl.SelectedTab) && _stNodeEditors[_tabControl.SelectedTab].ActiveNode != null) { _stNodeEditors[_tabControl.SelectedTab].ActiveNode.LockOption = !_stNodeEditors[_tabControl.SelectedTab].ActiveNode.LockOption; } };


            _tabEditors.Items[0].Click += (sender, e) => Add();
            _tabEditors.Items[1].Click += (sender, e) => Close();
            Add();
        }

        private void Close()
        {
            if (_stNodeEditors.ContainsKey(_tabControl.SelectedTab))
            {
                STNodeEditorEx sTNodeEditor = _stNodeEditors[_tabControl.SelectedTab];
                _tabControl.SelectedTab.Controls.Remove(sTNodeEditor);
                _ = _stNodeEditors.Remove(_tabControl.SelectedTab);
                _ = _tabPages.Remove(_tabControl.SelectedTab);
                _tabControl.Controls.Remove(_tabControl.SelectedTab);
            }
        }
        public void Add(string loadPath = null, bool isHere = false)
        {
            STNodeEditorEx stNodeEditor = null;
            if (!isHere)
            {
                stNodeEditor = new();
                OnCreateEditor?.Invoke(stNodeEditor);
                stNodeEditor.ActiveChanged += ActiveChanged;
                stNodeEditor.OptionConnected += OptionConnected;
                stNodeEditor.OptionDisConnected += OptionDisConnected;
                stNodeEditor.CanvasScaled += CanvasScaled;
                stNodeEditor.NodeAdded += NodeAdded;
                stNodeEditor.NodeRemoved += NodeRemoved;
                stNodeEditor.LoadAssembly(Application.ExecutablePath);
                stNodeEditor.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;


                
                TabPage tabPage = new("Page");
                tabPage.Controls.Add(stNodeEditor);

                _tabPages.Add(tabPage);
                _stNodeEditors.Add(tabPage, stNodeEditor);

                foreach (KeyValuePair<Type, Color> item in TypeColors)
                {
                    _ = stNodeEditor.SetTypeColor(item.Key, item.Value);
                }

                _tabControl.TabPages.Add(tabPage);
                _tabControl.SelectTab(tabPage);
            }
            else
                if (_stNodeEditors.ContainsKey(_tabControl.SelectedTab))
                stNodeEditor = _stNodeEditors[_tabControl.SelectedTab];

            if (stNodeEditor == null) { MessageBox.Show("STNodeEditor not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (loadPath != null)
            {
                if (!isHere) stNodeEditor.Nodes.Clear();
                stNodeEditor.LoadCanvas(loadPath);
            }
        }

        private void NodeRemoved(object s, STNodeEditorEventArgs ea)
        {
            //if (ea.Node is IServer server) Server.Send(server.GetData(SendType.Remove));
        }

        private void NodeAdded(object s, STNodeEditorEventArgs ea)
        {
            ea.Node.ContextMenuStrip = _contextMenuStrip;
            //if (ea.Node is IServer server) Server.Send(server.GetData(SendType.Add));
        }

        private void CanvasScaled(object s, EventArgs ea)
        {
            if (s is STNodeEditorEx stNodeEditor)
            {
                stNodeEditor.ShowAlert(stNodeEditor.CanvasScale.ToString(""), Color.White, Color.FromArgb(125, Color.Yellow));
            }
        }

        private void OptionConnected(object s, STNodeEditorOptionEventArgs ea)
        {
            if (s is STNodeEditorEx stNodeEditor)
            {
                stNodeEditor.ShowAlert(ea.Status.ToString(), Color.White, ea.Status == ConnectionStatus.Connected ? Color.FromArgb(125, Color.Green) : Color.FromArgb(125, Color.Red));
            }
            //if (ea.CurrentOption.Owner is IServer serverIn) Server.Send(serverIn.GetData(SendType.Update));
            //if (ea.TargetOption.Owner is IServer serverOut) Server.Send(serverOut.GetData(SendType.Update));
        }
        private void OptionDisConnected(object s, STNodeEditorOptionEventArgs ea)
        {
            //if (ea.CurrentOption.Owner is IServer serverIn) Server.Send(serverIn.GetData(SendType.Update));
            //if (ea.TargetOption.Owner is IServer serverOut) Server.Send(serverOut.GetData(SendType.Update));
        }
        private void ActiveChanged(object s, EventArgs ea)
        {
            if (s is STNodeEditorEx stNodeEditor)
            {
                _sTNodePropertyGrid.SetNode(stNodeEditor.ActiveNode);
                if (stNodeEditor.ActiveNode == null)
                {
                    _sTNodePropertyGrid.SetNode(stNodeEditor.InfoNode);
                }
            }
        }

        internal bool HasNeedPath()
        {
            return _stNodeEditors.ContainsKey(_tabControl.SelectedTab) && _stNodeSavePathes.ContainsKey(_stNodeEditors[_tabControl.SelectedTab]);
        }
        internal string GetSavePath()
        {
            return HasNeedPath() ? _stNodeSavePathes[_stNodeEditors[_tabControl.SelectedTab]] : null;
        }
        internal void Save(string path)
        {
            if (_stNodeEditors.ContainsKey(_tabControl.SelectedTab))
            {
                STNodeEditorEx sTNodeEditor = _stNodeEditors[_tabControl.SelectedTab];
                sTNodeEditor.SaveCanvas(path);
                if (!_stNodeSavePathes.ContainsKey(sTNodeEditor))
                {
                    _stNodeSavePathes.Add(sTNodeEditor, path);
                }

                sTNodeEditor.ShowAlert("Saved", Color.White, Color.Green);
            }
        }
        internal void Load(string fileName)
        {
            Add(fileName);
        }

        internal STNodeEditorEx GetSelectedNodeEditor()
        {
            if (_stNodeEditors.ContainsKey(_tabControl.SelectedTab))
                return _stNodeEditors[_tabControl.SelectedTab];
            return null;
        }

        internal void LoadAndAdd(string fileName)
        {
            Add(fileName, true);
        }
    }
}
