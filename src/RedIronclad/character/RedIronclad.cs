using System.Collections.Generic;
using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using sts1to2card.src.RedIronclad.character;

namespace sts1to2card.src.RedIronclad.character;

public class RedIronclad : PlaceholderCharacterModel
{
	public static readonly Color Color = new Color("D62000");

	public override string PlaceholderID => "ironclad";

    //选人背景
    public override string CustomCharacterSelectBg => "res://images/scenes/red_ironclad_portrait.tscn";
	
    //选人图标
	public override string CustomCharacterSelectIconPath => "res://images/scenes/red_ironclad_button.png";
	
	//百科小图标
	public override string CustomIconTexturePath => "res://images/scenes/red_ironclad_button.png";

	public override Color NameColor => Color;

	public override int StartingHp => 80;

	public override CharacterGender Gender => CharacterGender.Masculine;

	public override CardPoolModel CardPool => ModelDb.CardPool<RedIroncladCardPool>();

	public override RelicPoolModel RelicPool => ModelDb.RelicPool<IroncladRelicPool>();

	public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

	public override IEnumerable<CardModel> StartingDeck => new List<CardModel>
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
		ModelDb.Card<Bash>()
	};

	public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel> { ModelDb.Relic<BurningBlood>() };

	// 攻击建筑师的攻击特效列表
	// 过渡音效。这个不能删。
	public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";
}
