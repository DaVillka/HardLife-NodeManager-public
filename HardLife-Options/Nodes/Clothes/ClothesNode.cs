using HardLife_Options.Extensions;
using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;

namespace HardLife_Options.Nodes.Clothes
{
	[STNode("Instance/Clothes", "Узел одежды")]
	public class ClothesNode : STNodeEx
	{
		[STNodeProperty("Имя", "Название одежды")]
		public string Name { get; set; }

		[STNodeProperty("Описание", "Описание одежды")]
		public string Description { get; set; }

		private STNodeOption _inName;
		private STNodeOption _inDescription;

		private STNodeOption _inComponentMale;
		private STNodeOption _inComponentFemale;

		private STNodeOption _outComponentFemale;

		protected override void OnCreate()
		{
			Title = "Одежда";
			TitleColor = Color.MediumPurple;
			BackColor = Color.FromArgb(200, 54, 54, 60);
			ForeColor = Color.White;
			AutoSize = false;
			Width = 150;
			Height = 150;

			_inName = InputOptions.Add("Имя", typeof(string), true);
			_inDescription = InputOptions.Add("Описание", typeof(string), true);

			_inComponentMale = InputOptions.Add("Компоненты (М)", typeof(ClothComponentNode), true);
			_inComponentFemale = InputOptions.Add("Компоненты (Ж)", typeof(ClothComponentNode), true);

			_outComponentFemale = OutputOptions.Add("Одежда", typeof(ClothComponentNode), false);

			base.OnCreate();
		}
	}
}
