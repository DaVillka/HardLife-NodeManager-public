using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RAGEMPServerTool.Instances
{
    internal class ContentManager : Script
    {
        public override void Start(MainForm mainForm = null)
        {
            LoadTreeView(mainForm.treeContent);
            //mainForm.panelContent

            mainForm.treeContent.SelectedNode = mainForm.treeContent.Nodes[0];
            mainForm.treeContent.Select();

            mainForm.treeContent.NodeMouseClick += (s, e) =>
            {
                Debug.Log($"{e.Node.Text}");
            };
        }
        private void LoadTreeView(System.Windows.Forms.TreeView treeView1)
        {
            treeView1.Nodes.Clear();
            string rootDirectory = $"{ Directory.GetCurrentDirectory()}";
            var rootNode = new TreeNode("Content");
            treeView1.Nodes.Add(rootNode);
            PopulateTreeView(rootDirectory, rootNode);
        }

        private void PopulateTreeView(string directory, TreeNode parentNode)
        {
            try
            {
                string[] subDirectories = Directory.GetDirectories(directory);
                foreach (string subDirectory in subDirectories)
                {
                    var directoryNode = new TreeNode(Path.GetFileName(subDirectory));
                    parentNode.Nodes.Add(directoryNode);
                    PopulateTreeView(subDirectory, directoryNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                parentNode.Nodes.Add("Access Denied");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
