using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using BaseLib.Abstracts;

namespace sts1to2card.src.GreenSilentAwakened
{
    public class GreenSilentAwakenedCardPool : CustomCardPoolModel
    {
        // 卡池标识（非显示名）
        public override string Title => "GreenSilentAwakened";

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
            // 尝试加载 GreenSilentAwakened/images/cards/frame.png
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
               	ModelDb.Card<Abrasive>(),
				ModelDb.Card<Accelerant>(),
				ModelDb.Card<Accuracy>(),
				ModelDb.Card<Acrobatics>(),
				ModelDb.Card<Adrenaline>(),
				ModelDb.Card<Afterimage>(),
				ModelDb.Card<Anticipate>(),
				ModelDb.Card<Assassinate>(),
				ModelDb.Card<Backflip>(),
				ModelDb.Card<Backstab>(),
				ModelDb.Card<BladeOfInk>(),
				ModelDb.Card<BladeDance>(),
				ModelDb.Card<Blur>(),
				ModelDb.Card<BouncingFlask>(),
				ModelDb.Card<BubbleBubble>(),
				ModelDb.Card<BulletTime>(),
				ModelDb.Card<Burst>(),
				ModelDb.Card<CalculatedGamble>(),
				ModelDb.Card<CloakAndDagger>(),
				ModelDb.Card<CorrosiveWave>(),
				ModelDb.Card<DaggerSpray>(),
				ModelDb.Card<DaggerThrow>(),
				ModelDb.Card<Dash>(),
				ModelDb.Card<DeadlyPoison>(),
				ModelDb.Card<DefendSilent>(),
				ModelDb.Card<Deflect>(),
				ModelDb.Card<DodgeAndRoll>(),
				ModelDb.Card<EchoingSlash>(),
				//ModelDb.Card<Envenom>(), 采用1代涂毒
				ModelDb.Card<EscapePlan>(),
				ModelDb.Card<Expertise>(),
				ModelDb.Card<Expose>(),
				ModelDb.Card<FanOfKnives>(),
				ModelDb.Card<Finisher>(),
				ModelDb.Card<Flanking>(),
				ModelDb.Card<Flechettes>(),
				ModelDb.Card<FlickFlack>(),
				ModelDb.Card<FollowThrough>(),
				ModelDb.Card<Footwork>(),
				ModelDb.Card<GrandFinale>(),
				ModelDb.Card<HandTrick>(),
				ModelDb.Card<Haze>(),
				ModelDb.Card<HiddenDaggers>(),
				ModelDb.Card<InfiniteBlades>(),
				ModelDb.Card<KnifeTrap>(),
				ModelDb.Card<LeadingStrike>(),
				ModelDb.Card<LegSweep>(),
				ModelDb.Card<Malaise>(),
				ModelDb.Card<MasterPlanner>(),
				ModelDb.Card<MementoMori>(),
				ModelDb.Card<Mirage>(),
				ModelDb.Card<Murder>(),
				ModelDb.Card<Neutralize>(),
				ModelDb.Card<Nightmare>(),
				ModelDb.Card<NoxiousFumes>(),
				ModelDb.Card<Outbreak>(),
				ModelDb.Card<PhantomBlades>(),
				ModelDb.Card<PiercingWail>(),
				ModelDb.Card<Pinpoint>(),
				ModelDb.Card<PoisonedStab>(),
				ModelDb.Card<Pounce>(),
				ModelDb.Card<PreciseCut>(),
				ModelDb.Card<Predator>(),
				ModelDb.Card<Prepared>(),
				ModelDb.Card<Reflex>(),
				ModelDb.Card<Ricochet>(),
				ModelDb.Card<SerpentForm>(),
				ModelDb.Card<ShadowStep>(),
				ModelDb.Card<Shadowmeld>(),
				ModelDb.Card<Skewer>(),
				ModelDb.Card<Slice>(),
				ModelDb.Card<Snakebite>(),
				ModelDb.Card<Sneaky>(),
				ModelDb.Card<Speedster>(),
				ModelDb.Card<StormOfSteel>(),
				ModelDb.Card<Strangle>(),
				ModelDb.Card<StrikeSilent>(),
				ModelDb.Card<SuckerPunch>(),
				ModelDb.Card<Suppress>(),
				ModelDb.Card<Survivor>(),
				ModelDb.Card<Tactician>(),
				ModelDb.Card<TheHunt>(),
				ModelDb.Card<ToolsOfTheTrade>(),
				ModelDb.Card<Tracking>(),
				ModelDb.Card<Untouchable>(),
				ModelDb.Card<UpMySleeve>(),
				ModelDb.Card<WellLaidPlans>(),
				ModelDb.Card<WraithForm>(),
            };
        }
    }
}