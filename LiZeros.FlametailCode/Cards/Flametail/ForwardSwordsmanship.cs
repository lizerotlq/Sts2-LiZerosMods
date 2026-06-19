using LiZeros.FlametailCode.Powers.Flametail;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LiZeros.FlametailCode.Cards.Flametail
{
    public class ForwardSwordsmanship() : BasicFlametailCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return PowerCmd.Apply<ForwardSwordsmanshipPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
            EnergyCost.UpgradeBy(-1);
        }
    }
}
