using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Combat;

namespace sts1to2card.src.red.powers;

public sealed class RedCombustPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar("SelfDamage", 1m, ValueProp.Unblockable | ValueProp.Unpowered)
    };

    public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side)
            return;

        base.Flash();

        // 自身燃烧特效
        NCombatRoom.Instance?.CombatVfxContainer
            ?.AddChildSafely(NFireSmokePuffVfx.Create(base.Owner));

        // 自伤
        DamageVar selfDamage = (DamageVar)base.DynamicVars["SelfDamage"];

        await CreatureCmd.Damage(
            choiceContext,
            base.Owner,
            selfDamage.BaseValue,
            ValueProp.Unblockable | ValueProp.Unpowered,
            base.Owner,
            null
        );

        // 敌人火焰爆炸特效
        foreach (Creature enemy in base.CombatState.HittableEnemies)
        {
            NCombatRoom.Instance?.CombatVfxContainer
                ?.AddChildSafely(NFireBurstVfx.Create(enemy, 0.75f));
        }

        // 对所有敌人造成伤害
        await CreatureCmd.Damage(
            choiceContext,
            base.CombatState.HittableEnemies,
            base.Amount,
            ValueProp.Unpowered,
            base.Owner,
            null
        );
    }
}