using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class StrikeTlipoca() : BasicTlipocaCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(6m, ValueProp.Move)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3m);
        }
    }
}
