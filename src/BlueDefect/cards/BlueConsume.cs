using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.BlueDefect.cards
{
    public sealed class BlueConsume : CardModel
    {
        public BlueConsume()
            : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
        {
        }

        private const string Focus = "Focus";

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        
            new List<DynamicVar>
            {
                new DynamicVar(Focus, 2)
            };

        protected override void OnUpgrade()
        {
            base.DynamicVars[Focus].UpgradeValueBy(1m);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;

            await PowerCmd.Apply<FocusPower>(
                [Owner.Creature],
                base.DynamicVars[Focus].IntValue,
                Owner.Creature,
                this);

            OrbCmd.RemoveSlots(Owner, 1);
        }
    }
}