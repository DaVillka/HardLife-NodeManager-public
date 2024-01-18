using ST.Library.UI.NodeEditor;
using System.Drawing;
using WinNodeEditorDemo.Interfaces;

namespace WinNodeEditorDemo.Nodes.Clothes.Components
{
    [STNode("/Nodes/Clothes/Components", "Bags")]
    internal class Bags : STNode
    {
        private STNodeOption idPair = null;
        private STNodeOption _out = null;

        protected override void OnCreate()
        {
            base.OnCreate();
            Title = GetType().Name;
            AutoSize = false;
            Width = 120;
            Height = 60;

            idPair = InputOptions.Add("Id", typeof(IClothIdPair), true);
            _out = OutputOptions.Add("Выход", GetType(), false);
        }
    }
}
