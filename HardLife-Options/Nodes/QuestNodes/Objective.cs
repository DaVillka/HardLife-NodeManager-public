using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardLife_Options.Nodes.QuestNodes
{
	// ------- Состояние цели -------
	[Serializable]
	public sealed class ObjectiveState
	{
		public string Id;
		public string Description;
		public int Current;
		public int Target;
		public bool Completed => Target > 0 && Current >= Target;
		public float Progress01 => Target <= 0 ? 0f : Math.Min(1f, (float)Current / Target);
		public override string ToString() => $"{Description} {Current}/{Target}";
	}

	// ================= ObjectiveNode =================
	[STNode("Instance/Quests/Objective")]
	public class ObjectiveNode : STNode
	{
		// Свойства для PropertyGrid
		[STNodeProperty("ID", "Уникальный идентификатор цели")]
		public string ObjectiveId { get; set; } = "objective_01";

		[STNodeProperty("Описание", "Текст цели")]
		public string Description { get; set; } = "Соберите яблоки";

		[STNodeProperty("Нужно", "Сколько требуется для выполнения")]
		public int TargetCount { get; set; } = 10;

		[STNodeProperty("Текущее", "Текущее количество (можно править в рантайме)")]
		public int CurrentCount { get; set; } = 0;

		// Опции
		private STNodeOption _inIncrement;
		private STNodeOption _inSetCount;
		private STNodeOption _inReset;

		private STNodeOption _outProgress;   // float [0..1]
		private STNodeOption _outCompleted;  // bool
		private STNodeOption _outState;      // ObjectiveState

		protected override void OnCreate()
		{
			base.OnCreate();
			Title = "Objective";
			AutoSize = true;

			// Входы
			_inIncrement = InputOptions.Add("Increment (int)", typeof(int), false);
			_inSetCount = InputOptions.Add("Set Count (int)", typeof(int), true);
			_inReset = InputOptions.Add("Reset (bool)", typeof(bool), true);

			_inIncrement.DataTransfer += (s, e) =>
			{
				if (e.TargetOption.Data is int delta)
				{
					CurrentCount = Math.Max(0, CurrentCount + delta);
					Push();
				}
			};
			_inSetCount.DataTransfer += (s, e) =>
			{
				if (e.TargetOption.Data is int v)
				{
					CurrentCount = Math.Max(0, v);
					Push();
				}
			};
			_inReset.DataTransfer += (s, e) =>
			{
				if (e.TargetOption.Data is bool b && b)
				{
					CurrentCount = 0;
					Push();
				}
			};

			// Выходы
			_outProgress = OutputOptions.Add("Progress (float)", typeof(float), false);
			_outCompleted = OutputOptions.Add("Completed (bool)", typeof(bool), false);
			_outState = OutputOptions.Add("Objective (ObjectiveState)", typeof(ObjectiveState), false);

			// Наглядные цвета точек (не обязательно, но удобно)
			_ = SetOptionDotColor(_outCompleted, Color.Lime);   // вернёт false, но цвет применится
			_ = SetOptionDotColor(_outState, Color.Orange);
			Push();
		}
		protected override void OnSaveNode(Dictionary<string, byte[]> dic)
		{
			base.OnSaveNode(dic);
		}
		protected override void OnLoadNode(Dictionary<string, byte[]> dic)
		{
			base.OnLoadNode(dic);
			Push();
		}
		private void Push()
		{
			var st = new ObjectiveState
			{
				Id = ObjectiveId,
				Description = Description,
				Current = CurrentCount,
				Target = TargetCount
			};
			_outState.TransferData(st);                         // отправка данных по соединениям
			_outProgress.TransferData(st.Progress01);
			_outCompleted.TransferData(st.Completed);
		}
	}
}
