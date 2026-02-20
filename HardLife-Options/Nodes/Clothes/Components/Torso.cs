using HardLife_Options.Enums;
using ST.Library.UI.NodeEditor;

namespace HardLife_Options.Nodes.Clothes.Components
{

	[STNode("/Instance/Clothes/Components", "Torso")]
    internal class Torso : STNode
    {
		[STNodeProperty("Variant", "Variant count")]
		public TorsoVariant Variant { get; set; } = TorsoVariant.TORSO_8;
		private STNodeOption _out = null;

		protected override void OnCreate()
		{
			base.OnCreate();
			Title = GetType().Name;
			AutoSize = false;
			Width = 120;
			Height = 40;

			_out = OutputOptions.Add("Выход", GetType(), false);
			_out.TransferData(this);
		}

		protected override void OnDrawTitle(DrawingTools dt)
		{
			base.OnDrawTitle(dt);
		}
	}
}
