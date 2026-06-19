using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Powers.Flametail;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Cards.Flametail
{
    public class DefendFlametail() : BasicFlametailCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DefendVar(5)
        ];

        protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return PowerCmd.Apply<DefendPower>(choiceContext, Owner.Creature, DynamicVars.GetDefend().BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.GetDefend().UpgradeValueBy(3);
        }
    }
}
