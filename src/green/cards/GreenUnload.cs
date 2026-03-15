using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.green.cards;

public sealed class GreenUnload : CardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
    {
        new DamageVar(14m, ValueProp.Move)
    };

    public GreenUnload()
        : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

        CardPile hand = PileType.Hand.GetPile(base.Owner);

        // 丢弃所有技能牌（官方丢弃逻辑）
        await CardCmd.Discard(
            choiceContext,
            hand.Cards.Where(c => c.Type == CardType.Skill).ToList()
        );

        AttackCommand attack = DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitVfxNode(t => NShivThrowVfx.Create(base.Owner.Creature, t, Colors.Green));

        // 只有 Silent 使用 Shiv 攻击动画
        if (base.Owner.Character is Silent)
        {
            attack.WithAttackerAnim("Shiv", 0.2f, null);
        }

        await attack.Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(4m);
    }
}