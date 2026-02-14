using ST.Library.UI.NodeEditor;

namespace HardLife_Options.Nodes.Clothes.Components
{
    [STNode("/Instance/Clothes/Components", "Mask")]
    internal class Mask : STNode
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
