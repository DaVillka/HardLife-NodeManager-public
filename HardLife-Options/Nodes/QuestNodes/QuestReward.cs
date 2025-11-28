using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardLife_Options.Nodes.QuestNodes
{
	// ====== МОДЕЛИ НАГРАД ======
	public enum RewardKind { Money, Car, Item, Donation }

	[Serializable]
	public abstract class RewardBase
	{
		public RewardKind Kind { get; protected set; }
		public override string ToString() => Kind.ToString();
	}

	[Serializable]
	public sealed class MoneyReward : RewardBase
	{
		public int Amount;
		public string Currency;
		public MoneyReward(int amount, string currency)
		{ Kind = RewardKind.Money; Amount = amount; Currency = currency; }
		public override string ToString() => $"{Amount} {Currency}";
	}

	[Serializable]
	public sealed class CarReward : RewardBase
	{
		public string Model;
		public string ColorName;
		public CarReward(string model, string color)
		{ Kind = RewardKind.Car; Model = model; ColorName = color; }
		public override string ToString() => $"{Model} ({ColorName})";
	}

	[Serializable]
	public sealed class ItemReward : RewardBase
	{
		public string ItemId;
		public int Count;
		public ItemReward(string id, int count)
		{ Kind = RewardKind.Item; ItemId = id; Count = count; }
		public override string ToString() => $"{ItemId} x{Count}";
	}

	[Serializable]
	public sealed class DonationReward : RewardBase
	{
		public int Amount;
		public string ProductId;
		public DonationReward(int amount, string productId)
		{ Kind = RewardKind.Donation; Amount = amount; ProductId = productId; }
		public override string ToString() => $"{Amount} ({ProductId})";
	}

	// ====== БАЗОВАЯ НОДА ДЛЯ ВСЕХ REWARD ======
	public abstract class RewardNodeBase : STNode
	{
		protected STNodeOption _inReward;  // RewardBase наружу

		protected override void OnCreate()
		{
			base.OnCreate();
			AutoSize = false;
			Width = 150;
			Height = 50;

			_inReward = InputOptions.Add("Reward", typeof(RewardBase), true);
		}

		protected abstract RewardBase BuildReward();
	}

	// ====== КОНКРЕТНЫЕ НОДЫ ======

	[STNodeAttribute("Quests/Reward", "Выдать деньги")]
	public class RewardMoneyNode : RewardNodeBase
	{
		[STNodeProperty("Сумма", "Сколько денег выдать")]
		public int Amount { get; set; } = 100;

		protected override void OnCreate()
		{
			base.OnCreate();
			Title = "Reward: Money";
		}

		protected override RewardBase BuildReward() =>
			new MoneyReward(Math.Max(0, Amount), string.Empty);
	}

	[STNodeAttribute("Quests/Reward", "Выдать машину")]
	public class RewardCarNode : RewardNodeBase
	{
		[STNodeProperty("Модель", "ID/название модели")]
		public string Model { get; set; } = "SportsCar";

		[STNodeProperty("Цвет", "Цвет машины")]
		public string ColorName { get; set; } = "Red";

		protected override void OnCreate()
		{
			base.OnCreate();
			Title = "Reward: Car";
		}

		protected override RewardBase BuildReward() =>
			new CarReward(Model ?? string.Empty, ColorName ?? string.Empty);
	}

	[STNodeAttribute("Quests/Reward", "Выдать предмет")]
	public class RewardItemNode : RewardNodeBase
	{
		[STNodeProperty("Предмет ID", "Идентификатор предмета")]
		public string ItemId { get; set; } = "apple";

		[STNodeProperty("Количество", "Сколько предметов выдать")]
		public int Count { get; set; } = 1;

		protected override void OnCreate()
		{
			base.OnCreate();
			Title = "Reward: Item";
		}

		protected override RewardBase BuildReward() =>
			new ItemReward(ItemId ?? string.Empty, Math.Max(1, Count));
	}

	[STNodeAttribute("Quests/Reward", "Выдать донат-покупку")]
	public class RewardDonationNode : RewardNodeBase
	{
		[STNodeProperty("Сумма", "Сумма донат-награды")]
		public int Amount { get; set; } = 5;

		[STNodeProperty("Продукт ID", "ID донат-продукта/пакета")]
		public string ProductId { get; set; } = "donate_small_pack";

		protected override void OnCreate()
		{
			base.OnCreate();
			Title = "Reward: Donation";
		}

		protected override RewardBase BuildReward() =>
			new DonationReward(Math.Max(1, Amount), ProductId ?? string.Empty);
	}
}
