using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.green.cards
{

	public sealed class GreenTerror : CardModel
	{
		private const string _powerKey = "Power";

		public GreenTerror()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
		{
			new DynamicVar(_powerKey, 99m)
		};

		public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
		{
			CardKeyword.Exhaust
		};

		protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
		{
			HoverTipFactory.FromPower<VulnerablePower>()
		};

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int amount = base.DynamicVars[_powerKey].IntValue;
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, amount, base.Owner.Creature, this, false);
		}

		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}