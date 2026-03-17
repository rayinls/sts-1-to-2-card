using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using BaseLib.Abstracts;

namespace sts1to2card.src.GreenSilent
{
    public class GreenSilentCardPool : CustomCardPoolModel
    {
        // 卡池标识（非显示名）
        public override string Title => "greensilent";

        // 能量颜色名（对应原版 silent）
        public override string EnergyColorName => "silent";

        /* 卡背颜色 HSV 设置
           H = Hue 色相
           S = Saturation 饱和度
           V = Brightness 亮度
           范围通常 0~1，可用来给卡背着色 */
        public override float H => 0.3f;   // 红色偏向
        public override float S => 0.8f; // 饱和度
        public override float V => 0.8f; // 亮度

        // 可选自定义卡框（如果不想用 HSV 着色）
        /*public override Texture2D CustomFrame(CustomCardModel card)
        {
            // 尝试加载 GreenSilent/images/cards/frame.png
            return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
        }*/

        // 卡池列表里小图标颜色
        public override Color DeckEntryCardColor => new Color(0.8f, 0.2f, 0.2f);

        // 是否无色卡
        public override bool IsColorless => false;

        // 生成所有卡牌
        protected override CardModel[] GenerateAllCards()
        {
            return new CardModel[]
            {
				ModelDb.Card<Accuracy>(),            // 精准
				ModelDb.Card<Acrobatics>(),          // 杂技
				ModelDb.Card<Adrenaline>(),          // 肾上腺素
				ModelDb.Card<Afterimage>(),          // 残影
				ModelDb.Card<Backflip>(),            // 后空翻
				ModelDb.Card<Backstab>(),            // 背刺
				ModelDb.Card<Blur>(),                // 模糊
				ModelDb.Card<BouncingFlask>(),       // 弹跳药瓶
				ModelDb.Card<BulletTime>(),          // 子弹时间
				ModelDb.Card<Burst>(),               // 爆发
				ModelDb.Card<CloakAndDagger>(),      // 斗篷与匕首
				ModelDb.Card<DaggerSpray>(),         // 匕首风暴
				ModelDb.Card<DaggerThrow>(),         // 投掷匕首
				ModelDb.Card<Dash>(),                // 冲刺
				ModelDb.Card<DeadlyPoison>(),        // 致命毒药
				ModelDb.Card<DefendSilent>(),        // 防御
				ModelDb.Card<Deflect>(),             // 偏斜
				ModelDb.Card<DodgeAndRoll>(),        // 闪避翻滚
				ModelDb.Card<EscapePlan>(),          // 逃脱计划
				ModelDb.Card<Expertise>(),           // 专家技艺
				ModelDb.Card<Finisher>(),            // 终结
				ModelDb.Card<Flechettes>(),          // 飞镖
				ModelDb.Card<Footwork>(),            // 步法
				ModelDb.Card<GrandFinale>(),         // 盛大终幕
				ModelDb.Card<InfiniteBlades>(),      // 无限刀刃
				ModelDb.Card<LegSweep>(),            // 扫腿
				ModelDb.Card<Malaise>(),             // 衰弱
				ModelDb.Card<Neutralize>(),          // 中和
				ModelDb.Card<Nightmare>(),           // 噩梦
				ModelDb.Card<NoxiousFumes>(),        // 毒烟
				ModelDb.Card<PiercingWail>(),        // 穿刺哀嚎
				ModelDb.Card<PoisonedStab>(),        // 毒刺
				ModelDb.Card<Predator>(),            // 捕食者
				ModelDb.Card<Prepared>(),            // 准备
				ModelDb.Card<Reflex>(),              // 反射
				ModelDb.Card<Skewer>(),              // 穿刺
				ModelDb.Card<Slice>(),               // 切割
				ModelDb.Card<StormOfSteel>(),        // 钢铁风暴
				ModelDb.Card<StrikeSilent>(),        // 攻击
				ModelDb.Card<SuckerPunch>(),         // 阴险一击
				ModelDb.Card<Survivor>(),            // 幸存者
				ModelDb.Card<Tactician>(),           // 战术家
				ModelDb.Card<ToolsOfTheTrade>(),     // 交易工具
				ModelDb.Card<WellLaidPlans>(),       // 周密计划
				ModelDb.Card<Suppress>(),            // 压制
				ModelDb.Card<Sneaky>(),              // 鬼祟
				ModelDb.Card<Flanking>(),            // 夹击
            };
        }
    }
}