using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using sts1to2card.src.green.powers;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenAThousandCuts : CardModel
    {
        public GreenAThousandCuts()
            : base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new PowerVar<GreenAThousandCutsPower>(1m)
        };

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

            await PowerCmd.Apply<GreenAThousandCutsPower>(
                base.Owner.Creature,
                base.DynamicVars["GreenAThousandCutsPower"].BaseValue,
                base.Owner.Creature,
                this,
                false
            );
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["GreenAThousandCutsPower"].UpgradeValueBy(1m);
        }
    }
}