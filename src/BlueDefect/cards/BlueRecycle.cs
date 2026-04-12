using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Orbs;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.BlueDefect.cards
{
    public sealed class BlueRecycle : CardModel
    {
        public BlueRecycle()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
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

            CardModel? selection = (await CardSelectCmd.FromHand(
                prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 1),
                context: choiceContext,
                player: base.Owner,
                filter: null,
                source: this
            )).FirstOrDefault();

            if (selection != null)
            {
                int energy = selection.EnergyCost.Canonical;
                await PlayerCmd.GainEnergy(energy, base.Owner);
            }
        }
    }
}