using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class DefendTlipoca() : BasicTlipocaCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        public override bool GainsBlock => true;

        protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(5m, ValueProp.Move)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CommonActions.CardBlock(this, cardPlay);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
        }
    }
}
