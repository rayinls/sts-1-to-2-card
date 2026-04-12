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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.BlueDefect.cards
{
    public sealed class BlueReprogram : CardModel
    {
        public BlueReprogram()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
        {
        }

        private const string Focus = "Focus";
        private const string Dex = "Dex";
        private const string Str = "Str";


        protected override IEnumerable<DynamicVar> CanonicalVars =>
        
            new List<DynamicVar>
            {
                new DynamicVar(Focus, 1),
                new DynamicVar(Dex, 1),
                new DynamicVar(Str, 1)
            };

        protected override void OnUpgrade()
        {
            base.DynamicVars[Focus].UpgradeValueBy(1m);
            base.DynamicVars[Dex].UpgradeValueBy(1m);
            base.DynamicVars[Str].UpgradeValueBy(1m);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;

            await PowerCmd.Apply<FocusPower>(
                [Owner.Creature],
                -base.DynamicVars[Focus].IntValue,
                Owner.Creature,
                this);

            await PowerCmd.Apply<StrengthPower>(
                [Owner.Creature],
                base.DynamicVars[Str].IntValue,
                Owner.Creature,
                this);

            await PowerCmd.Apply<DexterityPower>(
                [Owner.Creature],
                base.DynamicVars[Dex].IntValue,
                Owner.Creature,
                this);

        }
    }
}