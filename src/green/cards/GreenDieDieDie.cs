using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.green.cards
{

	public sealed class GreenDieDieDie : CardModel
	{
		public GreenDieDieDie()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
		{
			CardKeyword.Exhaust
		};

		protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
		{
			new DamageVar(13m, ValueProp.Move)
		};

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}