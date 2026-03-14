using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.powers
{

	public sealed class RedFireBreathingPower : PowerModel
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

		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner.Creature == base.Owner && (card.Type == CardType.Status || card.Type == CardType.Curse))
			{
				base.Flash();
				await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
			}
		}
	}
}
