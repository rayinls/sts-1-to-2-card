using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.cards;

public sealed class RedShockwave : CardModel
{
	public RedShockwave()
		: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, true)
	{
	}

	protected override IEnumerable<DynamicVar> CanonicalVars
	{
		get
		{
			return new DynamicVar[] { new DynamicVar("Power", 3m) };
		}
	}

	public override IEnumerable<CardKeyword> CanonicalKeywords
	{
		get
		{
			return new CardKeyword[] { CardKeyword.Exhaust };
		}
	}

	protected override IEnumerable<IHoverTip> ExtraHoverTips
	{
		get
		{
			return new IHoverTip[]
			{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
			};
		}
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_flying_slash");
		int amount = base.DynamicVars["Power"].IntValue;
		foreach (Creature enemy in base.CombatState!.HittableEnemies)
		{
			await PowerCmd.Apply<WeakPower>(enemy, amount, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(enemy, amount, base.Owner.Creature, this, false);
		}
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["Power"].UpgradeValueBy(2m);
	}

}
