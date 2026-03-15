using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.relics.shared
{
    public sealed class SharedOrangePellets : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Shop;

        private int AttacksPlayedThisTurn { get; set; }
        private int SkillsPlayedThisTurn { get; set; }
        private int PowersPlayedThisTurn { get; set; }
        private int ActivationCountThisTurn { get; set; }

        public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
        {
            if (side != base.Owner.Creature.Side)
                return Task.CompletedTask;

            AttacksPlayedThisTurn = 0;
            SkillsPlayedThisTurn = 0;
            PowersPlayedThisTurn = 0;
            ActivationCountThisTurn = 0;

            return Task.CompletedTask;
        }

        public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner != base.Owner)
                return;

            if (!CombatManager.Instance.IsInProgress)
                return;

            if (ActivationCountThisTurn >= 1)
                return;

            AttacksPlayedThisTurn += cardPlay.Card.Type == CardType.Attack ? 1 : 0;
            SkillsPlayedThisTurn += cardPlay.Card.Type == CardType.Skill ? 1 : 0;
            PowersPlayedThisTurn += cardPlay.Card.Type == CardType.Power ? 1 : 0;

            if (AttacksPlayedThisTurn > 0 && SkillsPlayedThisTurn > 0 && PowersPlayedThisTurn > 0)
            {
                List<PowerModel> debuffs = base.Owner.Creature.Powers
                    .Where(p => p.TypeForCurrentAmount == PowerType.Debuff)
                    .ToList();

                if (debuffs.Count > 0)
                {
                    base.Flash();

                    foreach (var debuff in debuffs)
                    {
                        await PowerCmd.Remove(debuff);
                    }
                }

                ActivationCountThisTurn++;
            }
        }

        public override Task AfterCombatEnd(CombatRoom _)
        {
            AttacksPlayedThisTurn = 0;
            SkillsPlayedThisTurn = 0;
            PowersPlayedThisTurn = 0;
            ActivationCountThisTurn = 0;

            return Task.CompletedTask;
        }
    }
}