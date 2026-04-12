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
    public sealed class BlueRecursion : CardModel
    {
        public BlueRecursion()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
        {
        }

        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;

            OrbQueue? orbQueue = Owner.PlayerCombatState?.OrbQueue;

            if (orbQueue == null)
                return;

            OrbModel? orb = orbQueue.Orbs.FirstOrDefault();

            if (orb == null)
                return;
                
            await OrbCmd.EvokeNext(choiceContext, Owner);
            await OrbCmd.Channel(choiceContext, orb, Owner);
        }
    }
}