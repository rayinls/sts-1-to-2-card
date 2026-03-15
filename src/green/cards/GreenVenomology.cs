using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.green.cards
{

	public sealed class GreenVenomology : CardModel
	{
		public GreenVenomology()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		public override bool CanBeGeneratedInCombat => false;

		public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
		{
			CardKeyword.Exhaust
		};

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PotionCmd.TryToProcure(PotionFactory.CreateRandomPotionInCombat(base.Owner, base.Owner.RunState.Rng.CombatPotionGeneration, null).ToMutable(), base.Owner, -1);
		}

		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}