using HardLife_Options.Enums;
using ST.Library.UI.NodeEditor;
using System.Collections.Generic;


namespace HardLife_Options.Nodes.Clothes.Components
{
    [STNode("/Instance/Clothes/Components", "Undershirt")]
    internal class Undershirt : STNode
    {
		[STNodeProperty("Id", "Drawable Id")]
		public int Id { get; set; } = -1;
		[STNodeProperty("Torso", "Torso Variant")]
		public TorsoVariant Variant { get; set; } = TorsoVariant.TORSO_8;

		private STNodeOption _out = null;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = GetType().Name;
            AutoSize = false;
            Width = 120;
            Height = 60;

            _out = OutputOptions.Add("Выход", GetType(), false);
            _out.TransferData(this);
        }
		protected override void OnDrawTitle(DrawingTools dt)
		{
			Title = $"{GetType().Name}: {Id}";
			base.OnDrawTitle(dt);
		}
		public override object GetBuildObject()
        {
            return new Dictionary<string, object>()
            {
                { "Id", Id },
                { "Torso", Variant },
            };
        }
    }
}
