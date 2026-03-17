
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenEnvenom : CardModel
    {
        public GreenEnvenom()
            : base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                yield return new PowerVar<EnvenomPower>(1m);
            }
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                yield return HoverTipFactory.FromPower<PoisonPower>();
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            await PowerCmd.Apply<EnvenomPower>(base.Owner.Creature, base.DynamicVars["EnvenomPower"].BaseValue, base.Owner.Creature, this, false);
        }

        protected override void OnUpgrade()
        {
        base.EnergyCost.UpgradeBy(-1);
        }
    }
}