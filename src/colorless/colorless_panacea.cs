using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace sts1to2card.src.colorless
{
    public sealed class ColorlessPanacea : CardModel
    {
        public ColorlessPanacea() 
            : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true) // true = Exhaust
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
        {
            new PowerVar<ArtifactPower>(1m)
        };

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip>
        {
            HoverTipFactory.FromPower<ArtifactPower>()
        };

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

            await PowerCmd.Apply<ArtifactPower>(
                base.Owner.Creature,
                base.DynamicVars["ArtifactPower"].BaseValue,
                base.Owner.Creature,
                this,
                false
            );
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["ArtifactPower"].UpgradeValueBy(1m);
        }
    }
}