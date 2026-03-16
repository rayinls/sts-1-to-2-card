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

namespace sts1to2card.src.GreenSilentAwakened;

public class GreenSilentAwakened : PlaceholderCharacterModel
{
	public static readonly Color Color = new Color("5EBD00");

	public override string PlaceholderID => "silent";

	public override Color NameColor => Color;

	public override int StartingHp => 70;

	public override int StartingGold => 99;

	public override CharacterGender Gender => (CharacterGender)2;

	public override CardPoolModel CardPool => (CardPoolModel)(object)ModelDb.CardPool<GreenSilentAwakenedCardPool>();

	public override RelicPoolModel RelicPool => (RelicPoolModel)(object)ModelDb.RelicPool<SilentRelicPool>();

	public override PotionPoolModel PotionPool => (PotionPoolModel)(object)ModelDb.PotionPool<SilentPotionPool>();

	public override IEnumerable<CardModel> StartingDeck => new List<CardModel>
	{
		(CardModel)(object)ModelDb.Card<StrikeSilent>(),
		(CardModel)(object)ModelDb.Card<StrikeSilent>(),
		(CardModel)(object)ModelDb.Card<StrikeSilent>(),
		(CardModel)(object)ModelDb.Card<StrikeSilent>(),
		(CardModel)(object)ModelDb.Card<StrikeSilent>(),
		(CardModel)(object)ModelDb.Card<DefendSilent>(),
		(CardModel)(object)ModelDb.Card<DefendSilent>(),
		(CardModel)(object)ModelDb.Card<DefendSilent>(),
		(CardModel)(object)ModelDb.Card<DefendSilent>(),
		(CardModel)(object)ModelDb.Card<DefendSilent>(),
		(CardModel)(object)ModelDb.Card<Survivor>(),
		(CardModel)(object)ModelDb.Card<Neutralize>()
	};

	public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel> { (RelicModel)(object)ModelDb.Relic<BurningBlood>() };

}
