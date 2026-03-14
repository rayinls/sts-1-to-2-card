using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.red.cards;

public sealed class RedWarcry : CardModel
{
    public RedWarcry()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
    {
    }

    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
    {
        CardKeyword.Exhaust
    };

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CardsVar(1)
    };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);

        CardSelectorPrefs prefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);

        IEnumerable<CardModel> selected =
            await CardSelectCmd.FromHand(choiceContext, base.Owner, prefs, null, this);

        CardModel card = selected.FirstOrDefault();

        if (card != null)
        {
            await CardPileCmd.Add(card, PileType.Draw, CardPilePosition.Top, null, false);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}