using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using sts1to2card.src.GreenSilent.powers;

namespace sts1to2card.src.GreenSilent.cards;

public sealed class GreenCorpseExplosion : CardModel
{
	public GreenCorpseExplosion()
		: base(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, true)
	{
	}

	protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new PowerVar<PoisonPower>(6m) };

	protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
	{
		HoverTipFactory.FromPower<PoisonPower>(),
		HoverTipFactory.FromPower<GreenCorpseExplosionPower>()
	};

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

		NCombatRoom? instance = NCombatRoom.Instance;
		NCreature? ncreature = instance?.GetCreatureNode(cardPlay.Target);
		if (ncreature != null)
		{
			NGaseousImpactVfx? vfx = NGaseousImpactVfx.Create(ncreature.VfxSpawnPosition, new Color("83eb85"));
			instance?.CombatVfxContainer.AddChildSafely(vfx);
		}

		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

		await PowerCmd.Apply<PoisonPower>(
			cardPlay.Target,
			base.DynamicVars.Poison.BaseValue,
			base.Owner.Creature,
			this,
			false
		);

		await PowerCmd.Apply<GreenCorpseExplosionPower>(
			cardPlay.Target,
			1m,
			base.Owner.Creature,
			this,
			false
		);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Poison.UpgradeValueBy(3m);
	}
}