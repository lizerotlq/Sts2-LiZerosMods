using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Powers.Common;
using LiZeros.FlametailCode.Powers.Flametail;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Core.Commands
{
    public static class LzmCmd
    {
        public static Task Encourage(PlayerChoiceContext choiceContext, CardModel cardSource, Creature target, CardPlay? cardPlay, bool silent = false)
        {
            return Encourage(choiceContext, target, cardSource.DynamicVars.GetEncourage().BaseValue, cardPlay?.Card.Owner.Creature, cardPlay?.Card, silent);
        }

        public static Task Encourage(PlayerChoiceContext choiceContext, Creature target, decimal amount, Creature? applier, CardModel? cardSource, bool silent = false)
        {
            return PowerCmd.Apply<EncouragePower>(choiceContext, target, amount, applier, cardSource, silent);
        }

        public static Task Defend(PlayerChoiceContext choiceContext, CardModel cardSource, Creature target, CardPlay? cardPlay, bool silent = false)
        {
            return Defend(choiceContext, target, cardSource.DynamicVars.GetDefend().BaseValue, cardPlay?.Card.Owner.Creature, cardPlay?.Card, silent);
        }

        public static Task Defend(PlayerChoiceContext choiceContext, Creature target, decimal amount, Creature? applier, CardModel? cardSource, bool silent = false)
        {
            return PowerCmd.Apply<DefendPower>(choiceContext, target, amount, applier, cardSource, silent);
        }
    }
}
