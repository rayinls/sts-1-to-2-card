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

namespace sts1to2card.src.GreenSilent.character;

public class GreenSilent : PlaceholderCharacterModel
{
	public static readonly Color Color = new Color("5EBD00");

	public override string PlaceholderID => "silent";

	public override string CustomCharacterSelectBg => "res://images/scenes/green_silent_portrait.tscn";

	public override string CustomCharacterSelectIconPath => "res://images/scenes/green_silent_button.png";

    public override string CustomIconTexturePath => "res://images/scenes/green_silent_button.png";
    
	public override Color NameColor => Color;

	public override int StartingHp => 70;

	public override CharacterGender Gender => CharacterGender.Feminine;

	public override CardPoolModel CardPool => ModelDb.CardPool<GreenSilentCardPool>();

	public override RelicPoolModel RelicPool => ModelDb.RelicPool<SilentRelicPool>();

	public override PotionPoolModel PotionPool => ModelDb.PotionPool<SilentPotionPool>();

	public override IEnumerable<CardModel> StartingDeck => new List<CardModel>
	{
		ModelDb.Card<StrikeSilent>(),
		ModelDb.Card<StrikeSilent>(),
		ModelDb.Card<StrikeSilent>(),
		ModelDb.Card<StrikeSilent>(),
		ModelDb.Card<StrikeSilent>(),
		ModelDb.Card<DefendSilent>(),
		ModelDb.Card<DefendSilent>(),
		ModelDb.Card<DefendSilent>(),
		ModelDb.Card<DefendSilent>(),
		ModelDb.Card<DefendSilent>(),
		ModelDb.Card<Survivor>(),
		ModelDb.Card<Neutralize>()
	};

	public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel> { ModelDb.Relic<RingOfTheSnake>() };

    // 攻击建筑师的攻击特效列表
	// 过渡音效。这个不能删。
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_silent";
}
