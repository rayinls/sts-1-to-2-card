using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.powers
{

	public sealed class RedMetallicizePower : PowerModel
	{
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()) };
			}
		}

		public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
			}
		}
	}
}
