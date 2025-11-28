using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WinNodeEditorDemo.Core.Extensions;
using WinNodeEditorDemo.Instances;

namespace WinNodeEditorDemo
{

    public partial class HardLifeEditor : Form
    {
        public static HardLifeEditor Instance { get; private set; }
        private static readonly string dataNodes = "./Data/";
        public HardLifeEditor()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            Instance = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Console.SetOut(new ControlWriter(ConsoleOutputTextBox));

            stNodePropertyGrid.Text = "Node_Property";
            _ = stNodeTreeView.LoadAssembly(Application.ExecutablePath);


            stNodePropertyGrid.SetInfoKey("Author", "Mail", "Link", "Show Help");
            stNodeTreeView.PropertyGrid.SetInfoKey("Author", "Mail", "Link", "Show Help");



            contextMenuStrip1.ShowImageMargin = false;
            contextMenuStrip1.Renderer = new ToolStripRendererEx();

            tabEditors.ShowImageMargin = false;
            tabEditors.Renderer = new ToolStripRendererEx();

            ScriptInstance.Instance.Initialize();
            _ = Activator.CreateInstance(typeof(TabEditorInstance), this, tabNodeEditors, stNodePropertyGrid, contextMenuStrip1, tabEditors);
            //stNodePropertyGrid.SetNode(new WinNodeEditorDemo.Utils.TabInfoNode());
            Reflection.CreateInstances<Script>(Reflection.GetTypesImplementingClass(typeof(Script)), this);

        }
        // Класс для перенаправления потока вывода в текстовое поле

        private void lockConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StNodeEditor.ActiveNode.LockOption = !StNodeEditor.ActiveNode.LockOption;
        }
        private void lockLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (StNodeEditor.ActiveNode == null) return;
            //StNodeEditor.ActiveNode.LockLocation = !StNodeEditor.ActiveNode.LockLocation;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (StNodeEditor.ActiveNode == null) return;
            //StNodeEditor.Nodes.Remove(StNodeEditor.ActiveNode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(dataNodes))
            {
                _ = Directory.CreateDirectory(dataNodes);
            }

            _ = Directory.GetFiles(dataNodes, "*.stn", SearchOption.AllDirectories);
        }

        //private void btn_open_Click(object sender, EventArgs e) {
        //    //OpenFileDialog ofd = new OpenFileDialog();
        //    //ofd.Filter = "*.stn|*.stn";
        //    //if (ofd.ShowDialog() != DialogResult.OK) return;
        //    //stNodeEditor.Nodes.Clear();
        //    stNodeEditor.LoadCanvas(dataNodes + "test.stn");
        //}
        //
        //private void btn_save_Click(object sender, EventArgs e) {
        //    //SaveFileDialog sfd = new SaveFileDialog();
        //    //sfd.Filter = "*.stn|*.stn";
        //    //if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        //    stNodeEditor.SaveCanvas(dataNodes + "test.stn");
        //}


        #region TabNodeEditors
        private void tabNodeEditors_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tabEditors.Show(MousePosition);
            }
        }
        private void tabStartup_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tabEditors.Show(MousePosition);
            }
        }
        private void tabEditors_Opening(object sender, CancelEventArgs e)
        {
            tabEditorRemove.Enabled = tabNodeEditors.SelectedIndex != 0 && tabNodeEditors.TabCount != 1;

            for (int i = 0; i < tabNodeEditors.TabCount; i++)
            {
                if (tabNodeEditors.GetTabRect(i).Contains(MousePosition))
                {
                    TabPage selectedTabPage = tabNodeEditors.TabPages[i];
                    if (selectedTabPage == tabNodeEditors.TabPages[0])
                    {
                        
                    }

                    break;
                }
            }
        }

        #endregion

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabEditorInstance.Instance.HasNeedPath())
            {
                TabEditorInstance.Instance.Save(TabEditorInstance.Instance.GetSavePath());
                return;
            }
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Node Files (*.stn)|*.stn|All Files (*.*)|*.*",
                InitialDirectory = Application.StartupPath + "\\Data\\"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                TabEditorInstance.Instance.Save(saveFileDialog.FileName);
            }
        }
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog saveFileDialog = new()
            {
                Filter = "Node Files (*.stn)|*.stn|All Files (*.*)|*.*",
                InitialDirectory = Application.StartupPath + "\\Data\\"
            };
            //saveFileDialog.CheckFileExists = false;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                TabEditorInstance.Instance.Load(saveFileDialog.FileName);
            }
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Node Files (*.stn)|*.stn|All Files (*.*)|*.*",
                InitialDirectory = Application.StartupPath + "\\Data\\"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                TabEditorInstance.Instance.Save(path);
            }
        }
        private void loadAndAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog saveFileDialog = new()
            {
                Filter = "Node Files (*.stn)|*.stn|All Files (*.*)|*.*",
                InitialDirectory = Application.StartupPath + "\\Data\\"
            };
            //saveFileDialog.CheckFileExists = false;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                TabEditorInstance.Instance.LoadAndAdd(saveFileDialog.FileName);
            }
        }

	}
	public class ControlWriter : TextWriter
    {
        private readonly TextBox _output;

        public ControlWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            _output.AppendText(value.ToString());
        }

        public override void Write(string value)
        {
            _output.AppendText(value);
        }

        public override Encoding Encoding => Encoding.Default;
    }
}
