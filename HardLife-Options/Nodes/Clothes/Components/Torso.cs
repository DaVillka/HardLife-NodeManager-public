using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HardLife_Options.Nodes.Clothes.Components
{
	[Flags]
	internal enum TorsoVariant
	{
		TORSO_0 = 1 << 0,
		TORSO_1 = 1 << 1,
		TORSO_2 = 1 << 2,
		TORSO_3 = 1 << 3,
		TORSO_4 = 1 << 4,
		TORSO_5 = 1 << 5,

		TORSO_6 = 1 << 6,
		TORSO_7 = 1 << 7,
		TORSO_8 = 1 << 8,
		TORSO_9 = 1 << 9,
		TORSO_10 = 1 << 10,

		TORSO_11 = 1 << 11,
		TORSO_12 = 1 << 12,
		TORSO_13 = 1 << 13,
		TORSO_14 = 1 << 14,
		TORSO_15 = 1 << 15,
	}

	[STNode("/Instance/Clothes/Components", "Torso")]
    internal class Torso : STNode
    {
		[STNodeProperty("Id", "Drawable Id")]
		public int Id { get; set; } = -1;
		[STNodeProperty("Textures", "Texture Count")]
		public int Textures { get; set; } = -1;
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
			Title = $"{GetType().Name}: {Id}";
			base.OnDrawTitle(dt);
		}
		public override object GetBuildObject()
		{
			return new Dictionary<string, object>()
			{
				{ "Id", Id },
				{ "Textures", Textures },
				{ "Variant", Variant },
			};
		}
	}
}
