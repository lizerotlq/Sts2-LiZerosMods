using BaseLib.Extensions;
using LiZeros.FlametailCode.Powers.Flametail;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Core.Commands
{
    public static class DefendCmd
    {
        public static Task Gain(PlayerChoiceContext choiceContext, CardModel card, decimal amount, CardPlay? cardPlay, bool silent = false)
        {
            if (cardPlay?.Target != null)
                return PowerCmd.Apply<DefendPower>(choiceContext, cardPlay.Target, amount, card.Owner.Creature, card);
            return PowerCmd.Apply<DefendPower>(choiceContext, card.GetTargets(), amount, card.Owner.Creature, card);
        }
    }
}
