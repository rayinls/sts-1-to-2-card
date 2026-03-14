using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models;
using sts1to2card.src.red.powers;

namespace sts1to2card.src.red.cards;

public sealed class RedMetallicize : CardModel
{
	public RedMetallicize()
		: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
	{
	}

	protected override IEnumerable<IHoverTip> ExtraHoverTips
	{
		get
		{
			return new IHoverTip[] { HoverTipFactory.FromPower<RedMetallicizePower>() };
		}
	}

	protected override IEnumerable<DynamicVar> CanonicalVars
	{
		get
		{
			return new DynamicVar[] { new PowerVar<RedMetallicizePower>(3m) };
		}
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		
		await PowerCmd.Apply<RedMetallicizePower>(base.Owner.Creature, base.DynamicVars["RedMetallicizePower"].BaseValue, base.Owner.Creature, this, false);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["RedMetallicizePower"].UpgradeValueBy(1m);
	}

}
