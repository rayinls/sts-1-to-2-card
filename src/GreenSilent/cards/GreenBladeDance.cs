
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace sts1to2card.src.GreenSilent.cards
{
    public sealed class GreenBladeDance : CardModel
    {
        public GreenBladeDance()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new CardsVar(3)
                };
            }
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                return new List<IHoverTip>
                {
                    HoverTipFactory.FromCard<Shiv>(false)
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;

            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

            for (int i = 0; i < DynamicVars.Cards.IntValue; i++)
            {
                await Shiv.CreateInHand(Owner, CombatState);
                await Cmd.Wait(0.1f, false);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1m);
        }
    }
}