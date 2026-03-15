using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.cards
{
	public sealed class RedHeavyBlade : CardModel
	{
		public RedHeavyBlade()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new CalculationBaseVar(14m),
					new ExtraDamageVar(1m),
					new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, Creature _) =>
						(card != null && card.IsMutable && card.Owner != null)
							? card.Owner.Creature.GetPowerAmount<StrengthPower>() * (card.DynamicVars["StrengthMultiplier"].BaseValue - 1m)
							: 0m),
					new DynamicVar("StrengthMultiplier", 3m)
				};
			}
		}

		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromPower<StrengthPower>() };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

			// 攻击动画
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Attack", base.Owner.Character.AttackAnimDelay);

			// 地刺特效
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(
					NSpikeSplashVfx.Create(cardPlay.Target, VfxColor.Red)
				);
			}

			// 伤害 + 命中特效 + 音效
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
				.FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(3m);
			base.DynamicVars["StrengthMultiplier"].UpgradeValueBy(2m);
		}
	}
}