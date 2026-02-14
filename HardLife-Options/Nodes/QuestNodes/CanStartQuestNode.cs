using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace HardLife_Options.Nodes.QuestNodes
{
	[STNode("Instance/Quests/CanStart", "Условия для взятия квеста")]
	public class CanStartQuestNode : STNode
	{
		[STNodeProperty("", "Сколько монет выдать")]
		public int NeedFractionId { get; set; } = -1;

		private STNodeOption _outQuest;

		protected override void OnCreate()
		{
			base.OnCreate();

			Title = "Can Start";
			TitleColor = Color.MediumPurple;
			BackColor = Color.FromArgb(200, 54, 54, 60);
			ForeColor = Color.White;
			AutoSize = true;
			_outQuest = OutputOptions.Add("Quest", typeof(CanStartQuestNode), false);

		}
		public override object GetBuildObject()
		{
			return new CanStartQuestData
			{
				NeedFractionId = 0,
			};
		}
		public class CanStartQuestData
		{
			public int NeedFractionId { get; set; } = -1;

		}
	}
}
