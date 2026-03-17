using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.red.cards
{
    public sealed class RedBloodletting : CardModel
    {
        public RedBloodletting()
            : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new HpLossVar(3m),
                    new EnergyVar(2)
                };
            }
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                return new List<IHoverTip>
                {
                    EnergyHoverTip
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

            VfxCmd.PlayOnCreatureCenter(Owner.Creature, "vfx/vfx_bloody_impact");

            await CreatureCmd.Damage(
                choiceContext,
                Owner.Creature,
                DynamicVars.HpLoss.BaseValue,
                ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move,
                this
            );

            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Energy.UpgradeValueBy(1m);
        }
    }
}