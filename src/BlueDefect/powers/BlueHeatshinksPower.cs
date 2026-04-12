using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.BlueDefect.powers
{
    public sealed class BlueHeatsinksPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {     
            if (base.Owner.CombatState == null)
                return;

            if (cardPlay.Card.Owner != base.Owner.Player)
                return;

            if (cardPlay.Card.Type == CardType.Power)
            {
                var amount = base.DynamicVars["Amount"].IntValue;
                await CardPileCmd.Draw(context, amount, base.Owner.Player);
            }
        }
    }
}