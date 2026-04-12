using System.Collections.Generic;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using BaseLib.Abstracts;
using sts1to2card.src.BlueDefect.cards;
using Buffer = MegaCrit.Sts2.Core.Models.Cards.Buffer;

namespace sts1to2card.src.BlueDefect.character
{
    public class BlueDefectCardPool : CustomCardPoolModel
    {
        public override string Title => "bluedefect";

        public override string EnergyColorName => "defect";

        /* 卡背颜色 HSV 设置
           H = Hue 色相
           S = Saturation 饱和度
           V = Brightness 亮度
           范围通常 0~1，可用来给卡背着色 */
        public override float H => 0.5f;   // 红色偏向
        public override float S => 0.7f; // 饱和度
        public override float V => 1f; // 亮度

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
				ModelDb.Card<BallLightning>(),
				ModelDb.Card<Barrage>(),
				ModelDb.Card<BeamCell>(),          
				ModelDb.Card<ChargeBattery>(),          
				ModelDb.Card<Claw>(),            
				ModelDb.Card<ColdSnap>(),            
				ModelDb.Card<CompileDriver>(),                
				ModelDb.Card<Coolheaded>(),       
				ModelDb.Card<GoForTheEyes>(),          
				ModelDb.Card<Hologram>(),               
				ModelDb.Card<Rebound>(),              
				ModelDb.Card<Stack>(),              
				ModelDb.Card<SweepingBeam>(),        
				ModelDb.Card<Turbo>(),                     
				ModelDb.Card<BootSequence>(),             
				ModelDb.Card<Capacitor>(),            
				ModelDb.Card<Chaos>(),         
				ModelDb.Card<Chill>(),               
				ModelDb.Card<Darkness>(),             
				ModelDb.Card<Defragment>(),                
				ModelDb.Card<DoubleEnergy>(),        
				ModelDb.Card<Equilibrium>(),        
				ModelDb.Card<Ftl>(),          
				ModelDb.Card<Fusion>(),            
				ModelDb.Card<GeneticAlgorithm>(),              
				ModelDb.Card<Glacier>(),          
				ModelDb.Card<HelloWorld>(),        
				ModelDb.Card<Loop>(),      
				ModelDb.Card<Overclock>(),        
				ModelDb.Card<RipAndTear>(),            
				ModelDb.Card<Scrape>(),              
				// ModelDb.Card<SelfRepair>(),            
				ModelDb.Card<Skim>(),               
				// ModelDb.Card<StaticDischarge>(),               
				ModelDb.Card<Storm>(),
				ModelDb.Card<Sunder>(),
				ModelDb.Card<Tempest>(),
				ModelDb.Card<WhiteNoise>(),
				ModelDb.Card<AllForOne>(),
				ModelDb.Card<BiasedCognition>(),
				ModelDb.Card<Buffer>(),
				// ModelDb.Card<CoreSurge>(),
				ModelDb.Card<CreativeAi>(),
				ModelDb.Card<EchoForm>(),
				// ModelDb.Card<Electrodynamics>(),
				// ModelDb.Card<Fission>(),
				ModelDb.Card<Hyperbeam>(),
				ModelDb.Card<MachineLearning>(),
				ModelDb.Card<MeteorStrike>(),
				ModelDb.Card<MultiCast>(),
				ModelDb.Card<Rainbow>(),
				ModelDb.Card<Reboot>(),
				// ModelDb.Card<ThunderStrike>(),
            };
        }
    }
}