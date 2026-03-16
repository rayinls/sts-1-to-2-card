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
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace sts1to2card.src.red.cards
{
    public sealed class RedCorruption : CardModel
    {
        public RedCorruption()
            : base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
        {
        }

        // 动态变量（Power 数值）
        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                yield return new DynamicVar("Power", 1m);
            }
        }

        // 额外提示（Exhaust）
        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                yield return HoverTipFactory.FromKeyword(CardKeyword.Exhaust);
            }
        }

        // 卡牌使用逻辑
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            // 施加特效
            NPowerUpVfx.CreateNormal(base.Owner.Creature);

            // 播放施法动画
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

            // 施加力量
            await PowerCmd.Apply<CorruptionPower>(
                base.Owner.Creature,
                base.DynamicVars["Power"].BaseValue,
                base.Owner.Creature,
                this,
                false
            );
        }

        // 卡牌升级逻辑
        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }
    }
}