using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models;
using sts1to2card.src.red.powers;

namespace sts1to2card.src.red.cards;

public sealed class RedFireBreathing : CardModel
{
    public RedFireBreathing()
        : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
    {
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            return new IHoverTip[] { HoverTipFactory.FromPower<RedFireBreathingPower>() };
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return new DynamicVar[] { new PowerVar<RedFireBreathingPower>(6m) };
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

        await PowerCmd.Apply<RedFireBreathingPower>(
            base.Owner.Creature,
            base.DynamicVars["RedFireBreathingPower"].BaseValue,
            base.Owner.Creature,
            this,
            false
        );
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["RedFireBreathingPower"].UpgradeValueBy(2m);
    }
}