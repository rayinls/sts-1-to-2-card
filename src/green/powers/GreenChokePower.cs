using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.green.powers
{

    public sealed class GreenChokePower : PowerModel
    {
        public override PowerType Type => PowerType.Debuff;
        public override PowerStackType StackType => PowerStackType.Counter;

        protected override object InitInternalData()
        {
            return new Data();
        }

        public override Task BeforeCardPlayed(CardPlay cardPlay)
        {
            base.GetInternalData<Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
            return Task.CompletedTask;
        }

        public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {
            int num;
            if (base.GetInternalData<Data>().amountsForPlayedCards.Remove(cardPlay.Card, out num))
            {
                base.Flash();
                await CreatureCmd.Damage(context, base.Owner, num, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
            }
        }

        public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            await PowerCmd.Remove(this);
        }

        private class Data
        {
            public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
        }
    }
}