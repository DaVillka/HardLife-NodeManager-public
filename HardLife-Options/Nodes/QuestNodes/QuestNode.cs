using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardLife_Options.Nodes.QuestNodes
{
	public enum QuestType
	{
		Red, Green, Yellow
	}
	[STNodeAttribute("Quests/Quest", "Квестовый узел")] // попадёт в дерево по этому пути
	public class QuestNode : STNode
	{
		// Специальный тип для «потоковых» соединений
		public sealed class Flow { }

		// Порты, если нужно к ним обращаться из кода
		private STNodeOption _inPrev, _inCanStart, _outNext, _outRewards;

		// ---- Свойства квеста (редактируются в STNodePropertyGrid) ----
		[STNodeProperty("ID", "Уникальный идентификатор квеста")]
		public int Id { get; set; } = 1;

		[STNodeProperty("Заголовок", "Отображаемое название")]
		public string QuestTitle { get; set; } = "Новый квест";

		[STNodeProperty("Описание", "Краткое описание/цель")]
		public string Description { get; set; } = "";

		[STNodeProperty("Повторяемый", "Можно ли проходить повторно")]
		public bool Repeatable { get; set; } = false;

		[STNodeProperty("Мин. уровень", "Минимальный уровень игрока")]
		public int RequiredLevel { get; set; } = 1;

		[STNodeProperty("Тип", "Тип квеста (цвет)")]
		public QuestType Type { get; set; } = QuestType.Yellow;


		// ---- Инициализация узла и портов ----
		protected override void OnCreate()
		{
			base.OnCreate();

			Title = "Quest";
			TitleColor = Color.MediumPurple;
			BackColor = Color.FromArgb(200, 54, 54, 60);
			ForeColor = Color.White;
			AutoSize = true; // движок сам посчитает размеры

			// Входы
			_inPrev = InputOptions.Add("Prev", typeof(Flow), true);  // «подключись после»
			_inCanStart = InputOptions.Add("CanStart", typeof(CanStartQuestNode), true);  // условие старта
			

			// Выходы
			_outNext = OutputOptions.Add("Next", typeof(Flow), false);
			OutputOptions.Add("OnStarted", typeof(Flow), false);
			OutputOptions.Add("OnCompleted", typeof(Flow), false);

			// Полезные данные наружу
			OutputOptions.Add("QuestId", typeof(int), true);
			OutputOptions.Add("Title", typeof(string), true);

			_outRewards = OutputOptions.Add("Rewards", typeof(RewardBase), false);
		}

		// Удобный способ получить DTO при «сборке» графа
		public override object GetBuildObject()
		{
			return new QuestData
			{
				Id = Id,
				Title = QuestTitle,
				Description = Description,
				Repeatable = Repeatable,
				RequiredLevel = RequiredLevel,
			};
		}

		public class QuestData
		{
			public int Id;
			public string Title;
			public string Description;
			public bool Repeatable;
			public int RequiredLevel;
		}

		// При желании можно переопределить OnDrawBody(DrawingTools dt) и
		// нарисовать превью (например, ID и заголовок) в теле узла.
	}
}
