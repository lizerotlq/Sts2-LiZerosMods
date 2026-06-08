using BaseLib.Utils;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class SoulShield() : SoulCostCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(20, ValueProp.Move),
            new SoulVar(5)
        ];

        protected override bool RequiredSoul => true;

        protected override decimal SoulCost => DynamicVars.GetSoul().BaseValue;

        protected override Task OnPlayWithSoul(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return CommonActions.CardBlock(this, cardPlay);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(10);
        }
    }
}
