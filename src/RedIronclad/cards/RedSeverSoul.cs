using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.HoverTips;

namespace sts1to2card.src.RedIronclad.cards;

public sealed class RedSeverSoul : CardModel
{
    // 设置伤害动态变量
    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
    {
        new DamageVar(16m, ValueProp.Move)
    };

    // 消耗提示
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip>
    {
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
    };

    public RedSeverSoul()
        : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

        // 消耗手牌中所有非攻击牌
        foreach (CardModel card in GetNonAttackHandCards().ToList())
        {
            await CardCmd.Exhaust(choiceContext, card);
        }

        // 播放施法动画
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);

        // 播放特效
        if (NCombatRoom.Instance != null)
        {
            NSpikeSplashVfx? splash = NSpikeSplashVfx.Create(cardPlay.Target);
            NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(splash);
        }

        // 对目标造成伤害，并使用 Stomp 风格的特效和音效
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(6m); // 升级伤害 16->22
    }

    private IEnumerable<CardModel> GetNonAttackHandCards()
    {
        CardPile hand = PileType.Hand.GetPile(base.Owner);
        return hand.Cards.Where(c => c.Type != CardType.Attack);
    }
}