using LiZeros.FlametailCode.Core.Commands;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Powers.Common;
using LiZeros.FlametailCode.Powers.Flametail;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Cards.Flametail
{
    public class RecastDefense() : BasicFlametailCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DefendVar(2),
            new EncourageVar(2)
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<CoattackPower>(),
            HoverTipFactory.FromPower<EncouragePower>()
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            IEnumerable<CardModel> cards = PileType.Hand.GetPile(Owner).Cards;
            int cardsCount = cards.Count();
            await CardCmd.Discard(choiceContext, cards);
            for (int i = 0; i < cardsCount; i++)
                await LzmCmd.Defend(choiceContext, this, Owner.Creature, cardPlay);
            await LzmCmd.Encourage(choiceContext, this, Owner.Creature, cardPlay);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.GetEncourage().UpgradeValueBy(1);
        }
    }
}
