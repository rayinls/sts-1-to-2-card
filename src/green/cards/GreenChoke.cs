using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using sts1to2card.src.green.powers;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenChoke : CardModel
    {
        public GreenChoke()
            : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new DamageVar(12m, ValueProp.Move),      // 基础伤害12
                    new PowerVar<GreenChokePower>(3m)        // 每打出一张牌造成3伤害
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target);

            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", null, null)
                .Execute(choiceContext);

            await PowerCmd.Apply<GreenChokePower>(
                cardPlay.Target,
                DynamicVars["GreenChokePower"].BaseValue,
                Owner.Creature,
                this,
                false
            );
        }

        protected override void OnUpgrade()
        {
            DynamicVars["GreenChokePower"].UpgradeValueBy(2); // 3 -> 5
        }
    }
}