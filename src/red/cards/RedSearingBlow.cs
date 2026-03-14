using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	public sealed class RedSearingBlow : CardModel
	{
		public RedSearingBlow()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		public override int MaxUpgradeLevel => 99;

		protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
		{
			new DamageVar(12m, ValueProp.Move)
		};

		protected override IEnumerable<string> ExtraRunAssetPaths => NGroundFireVfx.AssetPaths;

		public override async Task OnEnqueuePlayVfx(Creature target)
		{
			if (target != null)
			{
				int flameStacks = Math.Clamp(1 + base.CurrentUpgradeLevel / 8, 1, 12);
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					for (int i = 0; i < flameStacks; i++)
					{
						instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(target, VfxColor.Red));
					}
				}
			}

			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			float flameScale = Math.Clamp(0.75f + (float)base.CurrentUpgradeLevel * 0.05f, 0.75f, 2.25f);
			int stompPulses = Math.Clamp(1 + base.CurrentUpgradeLevel / 15, 1, 6);
			ShakeStrength shakeStrength = (base.CurrentUpgradeLevel >= 45) ? ShakeStrength.Strong : ((base.CurrentUpgradeLevel >= 20) ? ShakeStrength.Medium : ShakeStrength.Weak);
			float shakeAngle = Math.Clamp(100f + (float)base.CurrentUpgradeLevel * 6f, 100f, 220f);

			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
				.FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.WithHitVfxNode((Creature t) => NFireBurstVfx.Create(t, flameScale))
				.Execute(choiceContext);

			for (int i = 0; i < stompPulses; i++)
			{
				await Cmd.Wait(0.03f, false);
				VfxCmd.PlayOnCreature(cardPlay.Target, "vfx/vfx_attack_blunt");
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NFireBurstVfx.Create(cardPlay.Target, flameScale));
				}
				NGame instance2 = NGame.Instance;
				if (instance2 != null)
				{
					instance2.ScreenShake(shakeStrength, ShakeDuration.Short, shakeAngle);
				}
			}
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m + (decimal)base.CurrentUpgradeLevel);
		}
	}
}