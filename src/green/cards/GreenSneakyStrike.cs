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
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenSneakyStrike : CardModel
    {
        public GreenSneakyStrike()
            : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new DamageVar(12m, ValueProp.Move),
                    new EnergyVar(2)
                };
            }
        }

        // --- 高亮逻辑 ---
        protected override bool ShouldGlowGoldInternal
        {
            get
            {
                bool discardedThisTurn =
                    CombatManager.Instance.History.Entries
                    .OfType<CardDiscardedEntry>()
                    .Any(e => e.HappenedThisTurn(CombatState) && e.Card.Owner == Owner);

                return discardedThisTurn;
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", null, null)
                .Execute(choiceContext);

            bool discardedThisTurn =
                CombatManager.Instance.History.Entries
                .OfType<CardDiscardedEntry>()
                .Any(e => e.HappenedThisTurn(CombatState) && e.Card.Owner == Owner);

            if (discardedThisTurn)
            {
                await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(4m);
        }
    }
}