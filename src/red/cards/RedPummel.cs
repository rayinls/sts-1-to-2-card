using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	public sealed class RedPummel : CardModel
	{
		public RedPummel()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromKeyword(CardKeyword.Exhaust) };
			}
		}

		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new CardKeyword[] { CardKeyword.Exhaust };
			}
		}

		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new DamageVar(2m, ValueProp.Move),
					new DynamicVar("Hits", 4m)
				};
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
				.WithHitCount(base.DynamicVars["Hits"].IntValue)
				.FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_thrash", null, null)
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars["Hits"].UpgradeValueBy(1m);
		}
	}
}