using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenAllOutAttack : CardModel
    {
        public GreenAllOutAttack()
            : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
        {
        }

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new DamageVar(10m, ValueProp.Move)
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Attack", Owner.Character.AttackAnimDelay);

            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_attack_slash", null, null)
                .Execute(choiceContext);

            CardPile pile = PileType.Hand.GetPile(Owner);
            CardModel cardModel = Owner.RunState.Rng.CombatCardSelection.NextItem(pile.Cards);

            if (cardModel != null)
            {
                await CardCmd.Discard(choiceContext, cardModel);
            }
        }
    }
}