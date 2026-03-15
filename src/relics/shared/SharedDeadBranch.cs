using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.relics.shared
{
    public sealed class SharedDeadBranch : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
        {
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
        };

        public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool _)
        {
            if (card.Owner == base.Owner)
            {
                base.Flash();

                CardModel cardModel = CardFactory
                    .GetDistinctForCombat(
                        base.Owner,
                        base.Owner.Character.CardPool.GetUnlockedCards(
                            base.Owner.UnlockState,
                            base.Owner.RunState.CardMultiplayerConstraint
                        ),
                        1,
                        base.Owner.RunState.Rng.CombatCardGeneration
                    )
                    .First();

                await CardPileCmd.AddGeneratedCardToCombat(
                    cardModel,
                    PileType.Hand,
                    true,
                    CardPilePosition.Bottom
                );
            }
        }
    }
}