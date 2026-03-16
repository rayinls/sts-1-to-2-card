using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace sts1to2card.src.red.cards
{
    public sealed class RedDarkEmbrace : CardModel
    {
        // 构造函数
        public RedDarkEmbrace()
            : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
        {
        }

        // 卡牌悬浮提示
        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                // 从关键字生成悬浮提示
                return new List<IHoverTip> { HoverTipFactory.FromKeyword(CardKeyword.Exhaust) };
            }
        }

        // 卡牌使用效果
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<DarkEmbracePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
        }

        // 卡牌升级效果
        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}