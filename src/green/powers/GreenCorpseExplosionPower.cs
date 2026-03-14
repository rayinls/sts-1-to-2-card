using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.green.powers
{
	public sealed class GreenCorpseExplosionPower : PowerModel
	{
		public override PowerType Type => PowerType.Debuff;

		public override PowerStackType StackType => PowerStackType.Counter;

		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (wasRemovalPrevented || creature != base.Owner)
			{
				return;
			}

			decimal damage = base.Owner.MaxHp * base.Amount;
			if (damage <= 0m)
			{
				return;
			}

			List<Creature> targets = base.CombatState
				.GetCreaturesOnSide(base.Owner.Side)
				.Where(c => c != base.Owner && c.IsAlive)
				.ToList();

			if (targets.Count == 0)
			{
				return;
			}

			NCombatRoom instance = NCombatRoom.Instance;
			foreach (Creature target in targets)
			{
				NCreature node = instance?.GetCreatureNode(target);
				if (node == null)
				{
					continue;
				}

				NGaseousImpactVfx vfx = NGaseousImpactVfx.Create(node.VfxSpawnPosition, new Color("83eb85"));
				instance.CombatVfxContainer.AddChildSafely(vfx);
			}

			await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));

			await CreatureCmd.Damage(
				choiceContext,
				targets,
				damage,
				ValueProp.Unpowered,
				base.Applier ?? base.Owner,
				null
			);
		}
	}
}