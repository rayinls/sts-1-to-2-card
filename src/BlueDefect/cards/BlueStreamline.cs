using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.BlueDefect.cards
{
    public sealed class BlueStreamline : CardModel
    {
        public BlueStreamline()
            : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
        {
        }
        protected override IEnumerable<DynamicVar> CanonicalVars =>

            new List<DynamicVar>
            {
                new DamageVar(15m, ValueProp.Move)
            };

        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(5m);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;

            if (cardPlay.Target == null)
                return;

            await CreatureCmd.Damage(choiceContext, cardPlay.Target, base.DynamicVars.Damage, this);
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}