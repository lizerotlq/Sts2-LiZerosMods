using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Core.Commands
{
    public static class LiZerosActions
    {
        public static Task CardDefend(PlayerChoiceContext choiceContext, CardModel card, CardPlay? play)
        {
            if (card.DynamicVars.TryGetValue("CalculatedDefend", out DynamicVar? dynamicVar))
                return CardDefend(choiceContext, card, dynamicVar, play);

            if (card.DynamicVars.TryGetValue("Defend", out dynamicVar))
                return CardDefend(choiceContext, card, dynamicVar, play);

            throw new Exception("Card " + card.Title + " does not have a defend variable supported by LiZerosActions.CardDefend");
        }

        public static Task CardDefend(PlayerChoiceContext choiceContext, CardModel card, DynamicVar dynamicVar, CardPlay? cardPlay, bool silent = false)
        {
            if (dynamicVar is CalculatedDefendVar calculatedDefendVar)
                return DefendCmd.Gain(choiceContext, card, calculatedDefendVar.Calculate(cardPlay?.Target), cardPlay, silent);
            return DefendCmd.Gain(choiceContext, card, dynamicVar.BaseValue, cardPlay, silent);
        }

        public static Task<decimal> CardCollectSoul(CardModel card, CardPlay? cardPlay)
        {
            if (card.DynamicVars.TryGetValue("CalculatedSoul", out DynamicVar? dynamicVar))
                return CardCollectSoul(card, dynamicVar, cardPlay);

            if (card.DynamicVars.TryGetValue("Soul", out dynamicVar))
                return CardCollectSoul(card, dynamicVar, cardPlay);

            throw new Exception("Card " + card.Title + " does not have a soul variable supported by LiZerosActions.CardCollectSoul");
        }

        public static Task<decimal> CardCollectSoul(CardModel card, DynamicVar dynamicVar, CardPlay? cardPlay)
        {
            if (dynamicVar is CalculatedSoulVar calculatedSoulVar)
                return SoulCmd.GainSoul(card.Owner.Creature, calculatedSoulVar.Calculate(cardPlay?.Target), cardPlay);
            return SoulCmd.GainSoul(card.Owner.Creature, dynamicVar.BaseValue, cardPlay);
        }
    }
}
