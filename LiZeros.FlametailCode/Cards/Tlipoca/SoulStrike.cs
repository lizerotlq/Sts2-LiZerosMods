using BaseLib.Utils;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class SoulStrike() : SoulCostCard(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            Core.HoverTips.HoverTipFactory.Static(Core.HoverTips.StaticHoverTip.Soul)
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(9, ValueProp.Move),
            new SoulVar(5)
        ];

        protected override decimal SoulCost => DynamicVars.GetSoul().BaseValue;

        public override TargetType TargetType
        {
            get
            {
                VerifyPlayable(out bool soulUsable);
                return soulUsable ? TargetType.AllEnemies : base.TargetType;
            }
        }

        protected override Task OnPlayWithSoul(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        }

        protected override Task OnPlayWithoutSoul(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3m);
        }
    }
}
