using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Helpers;

namespace sts1to2card.src.green.cards
{
    public sealed class GreenEndlessAgony : CardModel
    {
        public GreenEndlessAgony()
            : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true) // 费用0，罕见
        {
        }
		public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
		{
			CardKeyword.Exhaust
		};
        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new DynamicVar[]
                {
                    new DamageVar(4m, ValueProp.Move), // 基础伤害4
                    new DynamicVar("UpgradeDamage", 2m) // 升级增加伤害
                };
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target);

            var room = NCombatRoom.Instance;
            if (room != null)
            {
                room.CombatVfxContainer.AddChildSafely(NThinSliceVfx.Create(cardPlay.Target, VfxColor.Red));
            }

            float animDelay = Owner.Character.AttackAnimDelay;
            if (MegaCrit.Sts2.Core.Saves.SaveManager.Instance.PrefsSave.FastMode == MegaCrit.Sts2.Core.Settings.FastModeType.Normal)
            {
                animDelay += 0.2f;
            }

            await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithAttackerAnim("Attack", animDelay, null)
                .WithHitFx("vfx/vfx_attack_slash", null, "slash_attack.mp3")
                .Execute(choiceContext);
        }

        public override async Task AfterCardDrawn(PlayerChoiceContext context, CardModel card, bool fromHandDraw)
        {
            if (card != this) return;

            // 生成1张复制到手牌
            await CardPileCmd.AddGeneratedCardToCombat(CreateClone(), PileType.Hand, true, CardPilePosition.Bottom);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(DynamicVars["UpgradeDamage"].BaseValue); // 升级后伤害4->6
        }
    }
}