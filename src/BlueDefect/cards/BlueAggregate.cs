using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.BlueDefect.cards
{
    public sealed class BlueAggregate : CardModel
    {
        public BlueAggregate()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
        {
        }


        protected override IEnumerable<DynamicVar> CanonicalVars =>
        
            new List<DynamicVar>
            {
                new EnergyVar(1),
                new CardsVar(4)
            };

        protected override void OnUpgrade()
        {
            base.DynamicVars.Cards.UpgradeValueBy(-1m);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;
            
            int cardsInDrawPile = CardPile.Get(PileType.Draw, Owner)?.Cards.Count ?? 0;
            int energy = cardsInDrawPile % DynamicVars.Cards.IntValue;

            await PlayerCmd.GainEnergy(energy, Owner);
        }
    }
}