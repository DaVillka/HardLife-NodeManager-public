using ST.Library.UI.NodeEditor;
using System.Drawing;
using WinNodeEditorDemo.Interfaces;

namespace WinNodeEditorDemo.Nodes.Clothes.Components
{
    [STNode("/Nodes/Clothes/Components", "Undershirt")]
    internal class Undershirt : STNode
    {
        private STNodeOption id = null;
        private STNodeOption _torso = null;
        private STNodeOption _out = null;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = GetType().Name;
            AutoSize = false;
            Width = 120;
            Height = 60;

            id = InputOptions.Add("Id", typeof(int), true);
            _torso = InputOptions.Add("Торс", typeof(Torso), true);
            _out = OutputOptions.Add("Выход", GetType(), false);
        }
    }
}
