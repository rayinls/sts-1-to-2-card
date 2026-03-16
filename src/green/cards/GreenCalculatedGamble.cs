using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.green.cards
{
	public sealed class GreenCalculatedGamble : CardModel
	{
		public GreenCalculatedGamble()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				yield return CardKeyword.Exhaust;
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			var cards = PileType.Hand.GetPile(Owner).Cards;
			int num = cards.Count();
			await CardCmd.DiscardAndDraw(choiceContext, cards, num);
		}

		protected override void OnUpgrade()
		{
			RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}