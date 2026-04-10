using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.GreenSilent.cards;

public sealed class GreenMasterfulStab : CardModel
{
    private int _costIncreaseAppliedThisCombat;

    public GreenMasterfulStab()
        : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
    {
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        new List<DynamicVar>
        {
            new DamageVar(12m, ValueProp.Move)
        };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash", null, null)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4m);
    }

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this || IsClone)
            return Task.CompletedTask;

        int currentCombatDamageInstances = CombatManager.Instance.History.Entries
            .OfType<DamageReceivedEntry>()
            .Count(e =>
                e.Receiver == Owner.Creature &&
                e.Receiver.CombatState == CombatState &&
                e.Result.UnblockedDamage > 0m);

        ApplyCostIncreaseToMatch(currentCombatDamageInstances);

        return Task.CompletedTask;
    }

    public override Task AfterDamageReceived(
        PlayerChoiceContext choiceContext,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer = null,
        CardModel? cardSource = null)
    {
        if (target != Owner.Creature || result.UnblockedDamage <= 0m)
            return Task.CompletedTask;

        ApplyCostIncreaseToMatch(_costIncreaseAppliedThisCombat + 1);

        return Task.CompletedTask;
    }

    public override Task AfterCombatEnd(CombatRoom room)
    {
        if (_costIncreaseAppliedThisCombat > 0)
        {
            EnergyCost.AddThisCombat(-_costIncreaseAppliedThisCombat, false);
            _costIncreaseAppliedThisCombat = 0;
        }

        return Task.CompletedTask;
    }

    private void ApplyCostIncreaseToMatch(int targetIncrease)
    {
        if (targetIncrease == _costIncreaseAppliedThisCombat)
            return;

        int delta = targetIncrease - _costIncreaseAppliedThisCombat;

        EnergyCost.AddThisCombat(delta, false);
        _costIncreaseAppliedThisCombat = targetIncrease;
    }
}