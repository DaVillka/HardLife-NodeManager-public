using HardLife_Options.Enums;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardLife_Options.Nodes.Clothes
{
	[STNode("Instance/Clothes", "Компоненты одежды")]
	public class ClothComponentNode : STNode
	{
		// 0 — Head
		[STNodeProperty("Имя(дебаг)", "Для отображения в прогремме, не более")]
		public string Name { get => GetName(); set { SetName(value); } }

		// 0 — Head
		[STNodeProperty("Голова (0)", "Компонент головы")]
		public int Head { get; set; } = -1;

		// 1 — Masks
		[STNodeProperty("Маска (1)", "Аксессуар на лицо")]
		public int Masks { get; set; } = -1;

		// 2 — Hair
		[STNodeProperty("Прическа (2)", "Стиль волос")]
		public int Hair { get; set; } = -1;

		// 3 — Torso
		[STNodeProperty("Торс (3)", "Тип торса персонажа")]
		public int Torso { get; set; } = -1;

		// 4 — Legs
		[STNodeProperty("Ноги (4)", "Штаны / низ одежды")]
		public int Legs { get; set; } = -1;

		// 5 — Bags & Parachutes
		[STNodeProperty("Рюкзак (5)", "Сумки и парашюты")]
		public int Bags { get; set; } = -1;

		// 6 — Shoes
		[STNodeProperty("Обувь (6)", "Ботинки, кроссовки")]
		public int Shoes { get; set; } = -1;

		// 7 — Accessories
		[STNodeProperty("Аксессуары (7)", "Часы, браслеты и прочее")]
		public int Accessories { get; set; } = -1;

		// 8 — Undershirts
		[STNodeProperty("Подмайка (8)", "Футболки / нижний слой под верхом")]
		public int Undershirt { get; set; } = -1;

		// 9 — Armor
		[STNodeProperty("Броня (9)", "Бронежилеты / нагрудники")]
		public int Armor { get; set; } = -1;

		// 10 — Decals
		[STNodeProperty("Декали (10)", "Нашивки, стикеры, логотипы")]
		public int Decals { get; set; } = -1;

		// 11 — Tops
		[STNodeProperty("Верх (11)", "Куртки, свитеры, толстовки")]
		public int Tops { get; set; } = -1;


		private STNodeOption _inputComponent;
		private STNodeOption _outComponent;
		private string _name = "Component";
		protected override void OnCreate()
		{
			Title = _name;
			_inputComponent = InputOptions.Add("Component", typeof(ClothComponentNode), true);
			_outComponent = OutputOptions.Add("Component", typeof(ClothComponentNode), false);
		}
		public void SetName(string name)
		{
			_name = name;
			Title = _name;
		}
		public string GetName()
		{
			return _name;
		}
	}

}
