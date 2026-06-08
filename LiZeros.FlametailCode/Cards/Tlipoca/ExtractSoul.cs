using BaseLib.Utils;
using LiZeros.FlametailCode.Core.Commands;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class ExtractSoul() : BasicTlipocaCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        private static decimal CalculateSoulAmount(CardModel card, Creature? creature)
        {
            if (creature != null)
                return 10 - Math.Min(card.DynamicVars.GetSoul().BaseValue, creature.CurrentHp);
            return 0;
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new SoulVar(10),
            new CalculationBaseVar(10),
            new CalculationExtraVar(-1),
            new CalculatedVar("CalculatedSoul").WithMultiplier(CalculateSoulAmount)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            Creature? target = cardPlay.Target;
            if (target != null && DynamicVars.TryGetValue("CalculatedSoul", out DynamicVar? calculatedSoul))
            {
                await CommonActions.CardAttack(this, target, calculatedSoul.BaseValue).Execute(choiceContext);
                await SoulActions.CardCollectSoul(this, cardPlay);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.GetSoul().UpgradeValueBy(5);
        }
    }
}
