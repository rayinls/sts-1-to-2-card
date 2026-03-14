using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.green.cards;

public sealed class GreenCripplingPoison : CardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            yield return new PowerVar<PoisonPower>(4m);
            yield return new PowerVar<WeakPower>(2m);
        }
    }

    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            yield return CardKeyword.Exhaust;
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            yield return HoverTipFactory.FromPower<PoisonPower>();
            yield return HoverTipFactory.FromPower<WeakPower>();
        }
    }

    public GreenCripplingPoison()
        : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

        SpawnVfx();

        await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));

        await PowerCmd.Apply<PoisonPower>(
            base.CombatState.HittableEnemies,
            base.DynamicVars.Poison.BaseValue,
            base.Owner.Creature,
            this
        );

        await PowerCmd.Apply<WeakPower>(
            base.CombatState.HittableEnemies,
            base.DynamicVars.Weak.BaseValue,
            base.Owner.Creature,
            this
        );
    }

    private void SpawnVfx()
    {
        NCombatRoom instance = NCombatRoom.Instance;
        Node node = instance?.CombatVfxContainer;

        if (node != null)
        {
            NSmokyVignetteVfx vignette =
                NSmokyVignetteVfx.Create(
                    new Color(0.8f, 0.8f, 0.3f, 0.66f),
                    new Color(0f, 4f, 0f, 0.33f)
                );

            node.AddChildSafely(vignette);

            foreach (Creature creature in base.CombatState.HittableEnemies)
            {
                node.AddChildSafely(
                    NSmokePuffVfx.Create(creature, NSmokePuffVfx.SmokePuffColor.Green)
                );
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Poison.UpgradeValueBy(3m);
    }
}