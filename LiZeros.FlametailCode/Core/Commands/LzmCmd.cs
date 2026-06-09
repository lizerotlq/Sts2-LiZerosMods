using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Powers.Common;
using LiZeros.FlametailCode.Powers.Flametail;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Core.Commands
{
    public static class LzmCmd
    {
        public static Task Encourage(CardModel cardSource, Creature target, CardPlay? cardPlay, bool silent = false)
        {
            return Encourage(target, cardSource.DynamicVars.GetEncourage().BaseValue, cardPlay?.Card.Owner.Creature, cardPlay?.Card, silent);
        }

        public static Task Encourage(Creature target, decimal amount, Creature? applier, CardModel? cardSource, bool silent = false)
        {
            return PowerCmd.Apply<EncouragePower>(target, amount, applier, cardSource, silent);
        }

        public static Task Defend(CardModel cardSource, Creature target, CardPlay? cardPlay, bool silent = false)
        {
            return Defend(target, cardSource.DynamicVars.GetDefend().BaseValue, cardPlay?.Card.Owner.Creature, cardPlay?.Card, silent);
        }

        public static Task Defend(Creature target, decimal amount, Creature? applier, CardModel? cardSource, bool silent = false)
        {
            return PowerCmd.Apply<DefendPower>(target, amount, applier, cardSource, silent);
        }
    }
}
