using System.Collections.Generic;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;

namespace sts1to2card.src.RedIroncladAwakened
{
    public class RedIroncladAwakened : PlaceholderCharacterModel
    {
        public static readonly Color Color = new Color("D62000"); // Ironclad 红色

        public override string PlaceholderID => "ironclad";

        public override Color NameColor => Color;

        public override int StartingHp => 80;

        public override int StartingGold => 99;

        public override CharacterGender Gender => CharacterGender.Feminine;
        // 使用原版铁甲战士卡池
        public override CardPoolModel CardPool => ModelDb.CardPool<RedIroncladAwakenedCardPool>();

        // 使用原版铁甲战士遗物池
        public override RelicPoolModel RelicPool => ModelDb.RelicPool<IroncladRelicPool>();

        // 使用原版铁甲战士药水池
        public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

        // 原版铁甲战士初始卡组
        public override IEnumerable<CardModel> StartingDeck => new List<CardModel>()
        {
            ModelDb.Card<StrikeIronclad>(),
            ModelDb.Card<StrikeIronclad>(),
            ModelDb.Card<StrikeIronclad>(),
            ModelDb.Card<StrikeIronclad>(),
            ModelDb.Card<StrikeIronclad>(),

            ModelDb.Card<DefendIronclad>(),
            ModelDb.Card<DefendIronclad>(),
            ModelDb.Card<DefendIronclad>(),
            ModelDb.Card<DefendIronclad>(),

            ModelDb.Card<Bash>(),
        };

        // 原版铁甲战士初始遗物
        public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel>()
        {
            ModelDb.Relic<BurningBlood>()
        };

        // 直接引用原版铁甲战士视觉
	    public override string CustomVisualPath => SceneHelper.GetScenePath("creature_visuals/ironclad");

        public override string CustomIconTexturePath => ImageHelper.GetImagePath("ui/top_panel/character_icon_ironclad.png");
        public override string CustomCharacterSelectIconPath => ImageHelper.GetImagePath("packed/character_select/char_select_ironclad.png");

        public override string CustomCharacterSelectLockedIconPath => ImageHelper.GetImagePath("packed/character_select/char_select_ironclad_locked.png");

        public override string CustomMapMarkerPath => ImageHelper.GetImagePath("packed/map/icons/map_marker_ironclad.png");

        public override string CustomArmPointingTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_point.png");

        public override string CustomArmRockTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_rock.png");

        public override string CustomArmPaperTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_paper.png");

        public override string CustomArmScissorsTexturePath => ImageHelper.GetImagePath("ui/hands/multiplayer_hand_ironclad_scissors.png");

        public override List<string> GetArchitectAttackVfx()
        {
            return new List<string>()
            {
            "vfx/vfx_attack_blunt",
            "vfx/vfx_heavy_blunt",
            "vfx/vfx_attack_slash",
            "vfx/vfx_bloody_impact",
            "vfx/vfx_rock_shatter"
            };
        }
    }
}