using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.cards;

public sealed class RedClash : CardModel
{

    protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar> { new DamageVar(14m, ValueProp.Move) };

    // 手牌中只有攻击牌时可打
    protected override bool IsPlayable => CardPile.GetCards(base.Owner, PileType.Hand).All(c => c.Type == CardType.Attack);

    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public RedClash()
        : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) // 改为 Uncommon
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target == null) throw new ArgumentNullException(nameof(cardPlay.Target));
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(4m);
    }
}