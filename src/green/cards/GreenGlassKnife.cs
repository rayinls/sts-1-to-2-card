using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenGlassKnife : CardModel
    {
        private decimal DamageLossFromPlays;

        public GreenGlassKnife()
            : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new DamageVar(8m, ValueProp.Move),
                    new DynamicVar("Loss", 2m)
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .WithHitCount(2)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash", null, null)
                .Execute(choiceContext);

            decimal loss = DynamicVars["Loss"].BaseValue;

            DynamicVars.Damage.BaseValue -= loss;
            DamageLossFromPlays += loss;
        }

        protected override void AfterDowngraded()
        {
            base.AfterDowngraded();

            DynamicVars.Damage.BaseValue -= DamageLossFromPlays;
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(4m);
        }
    }
}