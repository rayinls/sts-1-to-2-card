using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace sts1to2card.src.green.cards
{
	public sealed class GreenPhantasmalKiller : CardModel
	{
		public GreenPhantasmalKiller()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				yield return new CardsVar(3);
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<ShadowStepPower>(Owner.Creature, 1m, Owner.Creature, this, false);
		}

		protected override void OnUpgrade()
		{
			EnergyCost.UpgradeBy(-1);
		}
	}
}