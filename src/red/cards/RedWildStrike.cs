using System; 
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.cards;

public sealed class RedWildStrike : CardModel
{
    protected override HashSet<CardTag> CanonicalTags =>
        new HashSet<CardTag> { CardTag.Strike };
    // 显示伤害值
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar(12m, ValueProp.Move)
    };

    // 显示 Wound 信息
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
    {
        HoverTipFactory.FromCard<Wound>()
    };

    public RedWildStrike()
        : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null) throw new ArgumentNullException(nameof(cardPlay.Target));

        // 攻击特效
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        // 生成 Wound 卡并加入抽牌堆
        if (base.CombatState != null)
        {
            CardModel wound = base.CombatState.CreateCard<Wound>(base.Owner);
            var addedCards = await CardPileCmd.AddGeneratedCardToCombat(wound, PileType.Draw, addedByPlayer: true);

            // 显示卡牌加入抽牌堆动画
            CardCmd.PreviewCardPileAdd(addedCards);
        }

        await Cmd.Wait(0.5f); // 保持动画停顿
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(5m); // 升级后伤害 12 -> 17
    }
}