using LiZeros.FlametailCode.Powers.Tlipoca;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class ISummon() : BasicTlipocaCard(-1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        private static decimal CalculateCount(CardModel card, Creature? creature)
        {
            PlayerCombatState? state = card.Owner.PlayerCombatState;
            if (state != null)
                return state.Energy;
            return 0;
        }

        protected override bool HasEnergyCostX => true;

        protected override bool IsPlayable => Owner.Creature.HasPower<DeathTowerPower>();

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(0),
            new CalculationExtraVar(1),
            new CalculatedVar("CalculatedCount").WithMultiplier(CalculateCount),
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            Core.HoverTips.HoverTipFactory.Static(Core.HoverTips.StaticHoverTip.LittleDeath)
        ];

        protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return Task.CompletedTask;
        }
    }
}
