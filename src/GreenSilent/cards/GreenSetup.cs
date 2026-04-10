using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.GreenSilent.cards
{
    public sealed class GreenSetup : CardModel
    {
        public GreenSetup()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            // 选择1张手牌
            CardSelectorPrefs prefs = new CardSelectorPrefs(
                SelectionScreenPrompt,
                1
            );

            var selected = await CardSelectCmd.FromHand(choiceContext, Owner, prefs, null, this);
            CardModel? card = selected.FirstOrDefault();

            if (card != null)
            {
                // 费用变为0（直到打出）
                card.EnergyCost.SetUntilPlayed(0, false);

                // 放到抽牌堆顶部
                await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Top, null, false);
            }
        }

        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}