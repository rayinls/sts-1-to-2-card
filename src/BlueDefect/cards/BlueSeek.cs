using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.BlueDefect.cards
{
    public sealed class BlueSeek : CardModel
    {
        public BlueSeek()
            : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>    
        new List<DynamicVar>
        {
            new CardsVar(1),
        };
    
        protected override void OnUpgrade()
        {
            base.DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;
            
            var cards = Owner.Piles.Where(pile => pile.Type == PileType.Draw).FirstOrDefault()?.Cards;
            if (cards == null || cards.Count == 0)
                return;

            CardModel? selection = (await CardSelectCmd.FromChooseACardScreen(
                context: choiceContext,
                cards: cards,
                player: base.Owner
            ));

            if (selection != null)
            {
                await CardPileCmd.Add(selection, PileType.Hand, CardPilePosition.Top);
            }
        }
    }
}