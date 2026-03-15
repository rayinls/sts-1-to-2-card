using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.relics.shared
{
	public sealed class SharedClockworkSouvenir : RelicModel
	{
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

        protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>
        {
            new PowerVar<ArtifactPower>(1m)
        };

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip>
        {
            HoverTipFactory.FromPower<ArtifactPower>()
        };


		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				base.Flash();
				await PowerCmd.Apply<ArtifactPower>(base.Owner.Creature, base.DynamicVars["ArtifactPower"].BaseValue, base.Owner.Creature, null, false);
			}
		}
	}
}
