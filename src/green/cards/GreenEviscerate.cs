using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenEviscerate : CardModel
    {
        public GreenEviscerate()
            : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new DamageVar(7m, ValueProp.Move)
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .WithHitCount(3)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitVfxNode(t => NStabVfx.Create(t, true, VfxColor.Red))
                .Execute(choiceContext);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
        }

        public override Task AfterCardEnteredCombat(CardModel card)
        {
            if (card != this || IsClone)
                return Task.CompletedTask;

            // 本回合触发：统计本回合已弃牌数量
            int discardCount = CombatManager.Instance.History.Entries
                .OfType<CardDiscardedEntry>()
                .Count(e => e.HappenedThisTurn(CombatState) && e.Card.Owner == Owner);

            ReduceCostThisTurn(discardCount);

            return Task.CompletedTask;
        }

        public override Task AfterCardDiscarded(PlayerChoiceContext context, CardModel discarded)
        {
            if (discarded.Owner != Owner)
                return Task.CompletedTask;

            // 每弃一张本回合减1费
            ReduceCostThisTurn(1);

            return Task.CompletedTask;
        }

        private void ReduceCostThisTurn(int amount)
        {
            EnergyCost.AddThisTurn(-amount, false);
        }
    }
}