using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenDoppelganger : CardModel
    {
        public GreenDoppelganger()
            : base(-1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
        {
        }
        public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
        {
            CardKeyword.Exhaust
        };
        protected override bool HasEnergyCostX => true;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> { base.EnergyHoverTip };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new EnergyVar(1) // 基础X为1，用于计算X+1
        };

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int x = ResolveEnergyXValue();
            int drawCount = x + 1;
            int energyCount = x + 1;

            // 下回合抽牌
            await PowerCmd.Apply<DrawCardsNextTurnPower>(
                Owner.Creature,
                drawCount,
                Owner.Creature,
                this,
                false
            );

            // 下回合获得能量
            await PowerCmd.Apply<EnergyNextTurnPower>(
                Owner.Creature,
                energyCount,
                Owner.Creature,
                this,
                false
            );
        }

        protected override void OnUpgrade()
        {
            // 升级可以增加X的基础值
            DynamicVars.Energy.UpgradeValueBy(1m);
        }
    }
}