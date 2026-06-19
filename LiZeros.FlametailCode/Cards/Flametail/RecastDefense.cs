using BaseLib.Utils;
using LiZeros.FlametailCode.Core.Commands;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Powers.Common;
using LiZeros.FlametailCode.Powers.Flametail;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Cards.Flametail
{
    /// <summary>
    /// 重铸防御：消耗所有手牌获得等额招架2，获得振奋2。
    /// </summary>
    public class RecastDefense() : BasicFlametailCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        private static decimal CalculateTime(CardModel card, Creature? creature)
        {
            CardPile pile = PileType.Hand.GetPile(card.Owner);
            return pile.Cards.Count;
        }

        protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(0),
            new CalculationExtraVar(1),
            new CalculatedVar("CalculatedTime").WithMultiplier(CalculateTime),
            new DefendVar(2),
            new PowerVar<EncouragePower>(2),
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DefendPower>(),
            HoverTipFactory.FromPower<CoattackPower>(),
            HoverTipFactory.FromPower<EncouragePower>()
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal time = ((CalculatedVar)DynamicVars["CalculatedTime"]).Calculate(null);
            for (int i = 0; i < time; i++)
                await LiZerosActions.CardDefend(choiceContext, this, cardPlay);
            await CommonActions.Apply<EncouragePower>(choiceContext, this, cardPlay);
            await CardCmd.Discard(choiceContext, PileType.Hand.GetPile(Owner).Cards);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.GetDefend().UpgradeValueBy(1);
            DynamicVars.GetEncourage().UpgradeValueBy(1);
        }
    }
}
