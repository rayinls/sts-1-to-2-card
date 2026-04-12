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
    public sealed class BlueMelter : CardModel
    {
        public BlueMelter()
            : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        
            new List<DynamicVar>
            {
                new DamageVar(10m, ValueProp.Move),
            };
        
        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(4m);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;

            if (cardPlay.Target == null)
                return;
                
            await CreatureCmd.LoseBlock(cardPlay.Target, cardPlay.Target.Block);
            await CreatureCmd.Damage(choiceContext, cardPlay.Target, base.DynamicVars.Damage, this);
        }
    }
}