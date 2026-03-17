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

     	public override string CustomCharacterSelectIconPath => "res://images/scenes/red_ironclad_awakened_button.png";

        public override Color NameColor => Color;

        public override int StartingHp => 80;

        public override int StartingGold => 99;

        public override CharacterGender Gender => CharacterGender.Feminine;
        // 卡池
        public override CardPoolModel CardPool => ModelDb.CardPool<RedIroncladAwakenedCardPool>();

        // 遗物池
        public override RelicPoolModel RelicPool => ModelDb.RelicPool<IroncladRelicPool>();

        // 药水池
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

        // 攻击建筑师的攻击特效列表
        public override List<string> GetArchitectAttackVfx() => [
            "vfx/vfx_attack_slash",
            "vfx/vfx_heavy_slash",
            "vfx/vfx_attack_blunt"
        ];
        // 过渡音效。这个不能删。
        public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";
    }
}