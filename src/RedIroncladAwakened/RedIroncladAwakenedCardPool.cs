using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using BaseLib.Abstracts;

namespace sts1to2card.src.RedIroncladAwakened
{
    public class RedIroncladAwakenedCardPool : CustomCardPoolModel
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
                // 官方 Ironclad 卡
                ModelDb.Card<Aggression>(),
                ModelDb.Card<Anger>(),
                ModelDb.Card<Armaments>(),
                ModelDb.Card<AshenStrike>(),
                ModelDb.Card<Barricade>(),
                ModelDb.Card<Bash>(),
                ModelDb.Card<BattleTrance>(),
                ModelDb.Card<BloodWall>(),
                ModelDb.Card<Bloodletting>(),
                ModelDb.Card<Bludgeon>(),
                ModelDb.Card<BodySlam>(),
                ModelDb.Card<Brand>(),
                ModelDb.Card<Break>(),
                ModelDb.Card<Breakthrough>(),
                ModelDb.Card<Bully>(),
                ModelDb.Card<BurningPact>(),
                ModelDb.Card<Cascade>(),
                ModelDb.Card<Cinder>(),
                ModelDb.Card<Colossus>(),
                ModelDb.Card<Conflagration>(),
                ModelDb.Card<Corruption>(),
                ModelDb.Card<CrimsonMantle>(),
                ModelDb.Card<Cruelty>(),
                ModelDb.Card<DarkEmbrace>(),
                ModelDb.Card<DefendIronclad>(),
                ModelDb.Card<DemonForm>(),
                ModelDb.Card<DemonicShield>(),
                ModelDb.Card<Dismantle>(),
                ModelDb.Card<Dominate>(),
                ModelDb.Card<DrumOfBattle>(),
                ModelDb.Card<EvilEye>(),
                ModelDb.Card<ExpectAFight>(),
                ModelDb.Card<Feed>(),
                ModelDb.Card<FeelNoPain>(),
                ModelDb.Card<FiendFire>(),
                ModelDb.Card<FightMe>(),
                ModelDb.Card<FlameBarrier>(),
                ModelDb.Card<ForgottenRitual>(),
                ModelDb.Card<Grapple>(),
                ModelDb.Card<Havoc>(),
                ModelDb.Card<Headbutt>(),
                ModelDb.Card<Hellraiser>(),
                ModelDb.Card<Hemokinesis>(),
                ModelDb.Card<HowlFromBeyond>(),
                ModelDb.Card<Impervious>(),
                ModelDb.Card<InfernalBlade>(),
                ModelDb.Card<Inferno>(),
                ModelDb.Card<Inflame>(),
                ModelDb.Card<IronWave>(),
                ModelDb.Card<Juggernaut>(),
                ModelDb.Card<Juggling>(),
                ModelDb.Card<Mangle>(),
                ModelDb.Card<MoltenFist>(),
                ModelDb.Card<Offering>(),
                ModelDb.Card<OneTwoPunch>(),
                ModelDb.Card<PactsEnd>(),
                ModelDb.Card<PerfectedStrike>(),
                ModelDb.Card<Pillage>(),
                ModelDb.Card<PommelStrike>(),
                ModelDb.Card<PrimalForce>(),
                ModelDb.Card<Pyre>(),
                ModelDb.Card<Rage>(),
                ModelDb.Card<Rampage>(),
                ModelDb.Card<Rupture>(),
                ModelDb.Card<SecondWind>(),
                ModelDb.Card<SetupStrike>(),
                ModelDb.Card<ShrugItOff>(),
                ModelDb.Card<Spite>(),
                ModelDb.Card<Stampede>(),
                ModelDb.Card<Stoke>(),
                ModelDb.Card<Stomp>(),
                ModelDb.Card<StoneArmor>(),
                ModelDb.Card<StrikeIronclad>(),
                ModelDb.Card<SwordBoomerang>(),
                ModelDb.Card<Tank>(),
                ModelDb.Card<Taunt>(),
                ModelDb.Card<TearAsunder>(),
                ModelDb.Card<Thrash>(),
                ModelDb.Card<Thunderclap>(),
                ModelDb.Card<Tremble>(),
                ModelDb.Card<TrueGrit>(),
                ModelDb.Card<TwinStrike>(),
                ModelDb.Card<Unmovable>(),
                ModelDb.Card<Unrelenting>(),
                ModelDb.Card<Uppercut>(),
                ModelDb.Card<Vicious>(),
                ModelDb.Card<Whirlwind>()
            };
        }
    }
}