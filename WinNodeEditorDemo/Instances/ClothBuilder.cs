using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using WinNodeEditorDemo.Core.Extensions;
using System.Linq;
using WinNodeEditorDemo.Clothes;
using Newtonsoft.Json;

namespace WinNodeEditorDemo.Instances
{
    internal class ClothBuilder : Script
    {
        #region Singleton
        private static ClothBuilder _instance;
        public static ClothBuilder Instance { get => _instance; }
        #endregion
        private readonly HardLifeEditor _form = null;
        public ClothBuilder(HardLifeEditor form)
        {
            _form = form;
            _instance = this;
            form.clothToolStripMenuItem.Click += PressCompile;
        }

        private void PressCompile(object sender, EventArgs e)
        {
            STNodeEditorEx editor = TabEditorInstance.Instance.GetSelectedNodeEditor();
            List<STNode> clothNodes = editor.Nodes.Cast<STNode>().ToList().Where(t => t.GetType() == typeof(Cloth)).ToList();
            List<object> resultList = new();

            foreach (var clothNode in clothNodes)
                resultList.Add(clothNode.GetBuildObject());

            string json = JsonConvert.SerializeObject(resultList);
        }
        

    }
}