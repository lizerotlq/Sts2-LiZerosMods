using BaseLib.Utils;
using LiZeros.FlametailCode.Powers.Flametail;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Flametail
{
    public class ChangeStance() : BasicFlametailCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        private static decimal CalculateDefendAmount(CardModel card, Creature? creature)
        {
            DefendPower? defendPower = card.Owner.Creature.GetPower<DefendPower>();
            if (defendPower != null)
                return defendPower.GetDefendAmount();
            return 0;
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(0),
            new CalculationExtraVar(1),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(CalculateDefendAmount)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            DefendPower? defendPower = Owner.Creature.GetPower<DefendPower>();
            if (defendPower != null)
            {
                await CommonActions.CardBlock(this, cardPlay);
                defendPower.ClearDefends();
            }
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
    }
}
