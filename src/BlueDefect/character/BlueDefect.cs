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

namespace sts1to2card.src.BlueDefect.character;

public class BlueDefect : PlaceholderCharacterModel
{
	public static readonly Color Color = new Color("54A5FF");

	public override string PlaceholderID => "defect";

	public override string CustomCharacterSelectBg => "res://images/scenes/blue_defect_portrait.tscn";

	public override string CustomCharacterSelectIconPath => "res://images/scenes/blue_defect_button.png";

    public override string CustomIconTexturePath => "res://images/scenes/blue_defect_button.png";
    
	public override Color NameColor => Color;

	public override int StartingHp => 75;
	public override int BaseOrbSlotCount => 3;
	
	public override CharacterGender Gender => CharacterGender.Neutral;

	public override CardPoolModel CardPool => ModelDb.CardPool<BlueDefectCardPool>();

	public override RelicPoolModel RelicPool => ModelDb.RelicPool<BlueDefectRelicPool>();

	public override PotionPoolModel PotionPool => ModelDb.PotionPool<BlueDefectPotionPool>();

	public override IEnumerable<CardModel> StartingDeck => new List<CardModel>
	{
		ModelDb.Card<StrikeDefect>(),
		ModelDb.Card<StrikeDefect>(),
		ModelDb.Card<StrikeDefect>(),
		ModelDb.Card<StrikeDefect>(),
		ModelDb.Card<DefendDefect>(),
		ModelDb.Card<DefendDefect>(),
		ModelDb.Card<DefendDefect>(),
		ModelDb.Card<DefendDefect>(),
		ModelDb.Card<Dualcast>(),
		ModelDb.Card<Zap>()
	};

	public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel> { ModelDb.Relic<CrackedCore>() };

	//TODO
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_silent";
}
