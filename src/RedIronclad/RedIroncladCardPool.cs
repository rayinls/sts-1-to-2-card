using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using BaseLib.Abstracts;

namespace sts1to2card.src.RedIronclad
{
    public class RedIroncladCardPool : CustomCardPoolModel
    {
        // 卡池标识（非显示名）
        public override string Title => "redironclad";

        // 能量颜色名（对应原版 Ironclad）
        public override string EnergyColorName => "ironclad";

        /* 卡背颜色 HSV 设置
           H = Hue 色相
           S = Saturation 饱和度
           V = Brightness 亮度
           范围通常 0~1，可用来给卡背着色 */
        public override float H => 0f;   // 红色偏向
        public override float S => 0.8f; // 饱和度
        public override float V => 0.8f; // 亮度

        // 可选自定义卡框（如果不想用 HSV 着色）
        /*public override Texture2D CustomFrame(CustomCardModel card)
        {
            // 尝试加载 RedIronclad/images/cards/frame.png
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
                // 1代卡补充
                ModelDb.Card<Anger>(),              // 愤怒
                ModelDb.Card<Armaments>(),          // 装备武器
                ModelDb.Card<Barricade>(),          // 防御壁垒
                ModelDb.Card<Bash>(),               // 重击
                ModelDb.Card<BattleTrance>(),       // 战斗狂热
                ModelDb.Card<BodySlam>(),           // 肉搏冲撞
                ModelDb.Card<Break>(),              // 打破
                ModelDb.Card<BurningPact>(),        // 灼热契约
                ModelDb.Card<DefendIronclad>(),     // 防御
                ModelDb.Card<DemonForm>(),          // 恶魔形态
                ModelDb.Card<DemonicShield>(),      // 恶魔护盾
                ModelDb.Card<Feed>(),               // 牺牲生命
                ModelDb.Card<FeelNoPain>(),         // 无惧疼痛
                ModelDb.Card<FiendFire>(),          // 魔焰
                ModelDb.Card<FlameBarrier>(),       // 火焰屏障
                ModelDb.Card<Havoc>(),              // 混乱
                ModelDb.Card<Headbutt>(),           // 猛撞
                ModelDb.Card<Impervious>(),         // 无坚不摧
                ModelDb.Card<InfernalBlade>(),      // 地狱之刃
                ModelDb.Card<Inflame>(),            // 激怒
                ModelDb.Card<IronWave>(),           // 铁浪
                ModelDb.Card<Juggernaut>(),         // 不可阻挡
                ModelDb.Card<Offering>(),           // 祭献
                ModelDb.Card<OneTwoPunch>(),        // 连环重击
                ModelDb.Card<PerfectedStrike>(),    // 完美打击
                ModelDb.Card<PommelStrike>(),       // 拳击
                ModelDb.Card<Rage>(),               // 狂怒
                ModelDb.Card<Rampage>(),            // 暴走
                ModelDb.Card<Rupture>(),            // 破裂
                ModelDb.Card<SecondWind>(),         // 再次冲锋
                ModelDb.Card<ShrugItOff>(),         // 摆脱束缚
                ModelDb.Card<StrikeIronclad>(),     // 攻击
                ModelDb.Card<SwordBoomerang>(),     // 飞剑回旋
                ModelDb.Card<Tank>(),               // 肉盾
                ModelDb.Card<Thunderclap>(),        // 雷霆一击
                ModelDb.Card<TrueGrit>(),           // 坚毅
                ModelDb.Card<TwinStrike>(),         // 双重打击
                ModelDb.Card<Uppercut>(),           // 上勾拳
                ModelDb.Card<Whirlwind>()           // 龙卷风
            };
        }
    }
}