using System.Collections.Generic; 
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace sts1to2card.src.red.cards
{
    public sealed class RedImmolate : CardModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[]
        {
            HoverTipFactory.FromCard<Burn>()
        };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new DamageVar(21m, ValueProp.Move)
        };

        public RedImmolate()
            : base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (CombatState == null)
                return;

             // 播放玩家施法动画延迟
            DamageVar damage = base.DynamicVars.Damage;
            var container = NCombatRoom.Instance?.CombatVfxContainer;

            // --- 玩家火焰 ---
            container?.AddChildSafely(NGroundFireVfx.Create(base.Owner.Creature));
    
            // --- 敌人火焰 ---
            foreach (Creature enemy in base.CombatState.Enemies)
            {
                container?.AddChildSafely(NGroundFireVfx.Create(enemy));
            }

            // 播放玩家施法动画延迟
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

            // --- 群体攻击伤害和打击特效 ---
            await DamageCmd.Attack(damage.BaseValue)
                .FromCard(this)
                .TargetingAllOpponents(base.CombatState)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);

            // 加入1张 Burn 卡到弃牌堆
            CardModel card = base.CombatState.CreateCard<Burn>(base.Owner);
            CardCmd.PreviewCardPileAdd(
                await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Discard, addedByPlayer: true)
            );

            await Cmd.Wait(0.5f);
        }

        // 升级伤害
        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(7m); // 21 -> 28
        }

        // 保留空实现，VFX逻辑已经在 OnPlay 处理
        public override async Task OnEnqueuePlayVfx(Creature? target)
        {
            await Task.CompletedTask;
        }
    }
}