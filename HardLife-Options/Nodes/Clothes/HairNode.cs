using HardLife_Options.Core;
using HardLife_Options.Extensions;
using HardLife_Options.Nodes.Clothes.Components;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HardLife_Options.Nodes.Clothes
{
	[STNode("Instance/Clothes", "Узел одежды")]
	public class HairNode : STNodeEx
	{
		[STNodeProperty("Имя", "Название прически")]
		public string Name { get; set; }

		[STNodeProperty("Описание", "Описание прически")]
		public string Description { get; set; }

		[STNodeProperty("Пол", "Для какого пола предназначена прическа")]
		public Sex Sex { get; set; } = Sex.MALE;

		private STNodeOption _inName;
		private STNodeOption _inComponentHair;
		private STNodeOption _outComponentHair;

		protected override void OnCreate()
		{
			Title = "Прическа";
			TitleColor = Color.MediumPurple;
			BackColor = Color.FromArgb(200, 54, 54, 60);
			ForeColor = Color.White;
			AutoSize = false;
			Width = 150;
			Height = 60;

			_inName = InputOptions.Add("Название", typeof(string), true);

			_inComponentHair = InputOptions.Add("Компонент", typeof(Hair), true);
			_outComponentHair = OutputOptions.Add("Прическа", typeof(HairNode), false);

			base.OnCreate();
		}
		public override object GetBuildObject()
		{
			return new Dictionary<string, object>()
			{
				{ "Name", Name },
				{ "Description", Description },
				{ "Sex", Sex },
			};
		}
	}
}
