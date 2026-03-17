using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.red.cards
{
    public sealed class RedHemokinesis : CardModel
    {
        public RedHemokinesis()
            : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new HpLossVar(2m),
                    new DamageVar(15m, ValueProp.Move) //  基础伤害 +1（14 → 15）
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target);

            // 先掉血
            await CreatureCmd.Damage(
                choiceContext,
                Owner.Creature,
                DynamicVars.HpLoss.BaseValue,
                ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move,
                this
            );

            // 再攻击
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_bloody_impact", null, null)
                .Execute(choiceContext);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(5m);
        }
    }
}