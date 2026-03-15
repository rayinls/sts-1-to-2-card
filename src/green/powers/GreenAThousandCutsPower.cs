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

namespace sts1to2card.src.green.powers
{
    public sealed class GreenAThousandCutsPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override object InitInternalData()
        {
            return new Data();
        }

        public override Task BeforeCardPlayed(CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner != base.Owner.Player)
                return Task.CompletedTask;

            base.GetInternalData<Data>()
                .amountsForPlayedCards
                .Add(cardPlay.Card, base.Amount);

            return Task.CompletedTask;
        }

        public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner != base.Owner.Player)
                return;

            int damage;

            if (base.GetInternalData<Data>()
                .amountsForPlayedCards
                .Remove(cardPlay.Card, out damage))
            {
                if (damage > 0)
                {
                    await Cmd.CustomScaledWait(0.1f, 0.2f, false, default(CancellationToken));

                    foreach (Creature creature in base.Owner.CombatState.HittableEnemies)
                    {
                        VfxCmd.PlayOnCreatureCenter(creature, "vfx/vfx_attack_blunt");

                        await CreatureCmd.Damage(
                            context,
                            creature,
                            damage,
                            ValueProp.Unpowered,
                            base.Owner
                        );
                    }
                }
            }
        }

        private class Data
        {
            public readonly Dictionary<CardModel, int> amountsForPlayedCards =
                new Dictionary<CardModel, int>();
        }
    }
}