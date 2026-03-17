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

namespace sts1to2card.src.RedIronclad;

public class RedIronclad : PlaceholderCharacterModel
{
	public static readonly Color Color = new Color("D62000");

	public override string PlaceholderID => "ironclad";

    public override string CustomCharacterSelectBg => "res://images/scenes/red_ironclad_portrait.tscn";

	public override string CustomCharacterSelectIconPath => "res://images/scenes/red_ironclad_button.png";
	public override Color NameColor => Color;

	public override int StartingHp => 80;

	public override int StartingGold => 99;

	public override CharacterGender Gender => (CharacterGender)2;

	public override CardPoolModel CardPool => (CardPoolModel)(object)ModelDb.CardPool<RedIroncladCardPool>();

	public override RelicPoolModel RelicPool => (RelicPoolModel)(object)ModelDb.RelicPool<IroncladRelicPool>();

	public override PotionPoolModel PotionPool => (PotionPoolModel)(object)ModelDb.PotionPool<IroncladPotionPool>();

	public override IEnumerable<CardModel> StartingDeck => new List<CardModel>
	{
		(CardModel)(object)ModelDb.Card<StrikeIronclad>(),
		(CardModel)(object)ModelDb.Card<StrikeIronclad>(),
		(CardModel)(object)ModelDb.Card<StrikeIronclad>(),
		(CardModel)(object)ModelDb.Card<StrikeIronclad>(),
		(CardModel)(object)ModelDb.Card<StrikeIronclad>(),
		(CardModel)(object)ModelDb.Card<DefendIronclad>(),
		(CardModel)(object)ModelDb.Card<DefendIronclad>(),
		(CardModel)(object)ModelDb.Card<DefendIronclad>(),
		(CardModel)(object)ModelDb.Card<DefendIronclad>(),
		(CardModel)(object)ModelDb.Card<Bash>()
	};

	public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel> { (RelicModel)(object)ModelDb.Relic<BurningBlood>() };

	// 攻击建筑师的攻击特效列表
	// 过渡音效。这个不能删。
	public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";
}
