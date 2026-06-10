using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Powers.Tlipoca;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class DeathTower() : SoulCostCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            Core.HoverTips.HoverTipFactory.Static(Core.HoverTips.StaticHoverTip.Soul),
            HoverTipFactory.FromPower<DeathTowerPower>()
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new SoulVar(100)
        ];

        protected override bool RequiredSoul => true;

        protected override decimal SoulCost => DynamicVars.GetSoul().BaseValue;

        protected override Task OnPlayWithSoul(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return PowerCmd.Apply<DeathTowerPower>(Owner.Creature, 1, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
            AddKeyword(CardKeyword.Innate);
        }
    }
}
