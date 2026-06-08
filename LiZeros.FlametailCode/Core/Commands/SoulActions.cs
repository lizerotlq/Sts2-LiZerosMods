using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Core.Commands
{
    public static class SoulActions
    {
        public static Task CardCollectSoul(CardModel card, CardPlay play)
        {
            if (card.DynamicVars.TryGetValue("CalculatedSoul", out DynamicVar? calculatedSoul))
            {
                return SoulCmd.GainSoul(card.Owner.Creature, calculatedSoul.BaseValue, play);
            }

            if (card.DynamicVars.TryGetValue("Soul", out DynamicVar? soul))
            {
                return SoulCmd.GainSoul(card.Owner.Creature, soul.BaseValue, play);
            }

            throw new Exception("Card " + card.Title + " does not have a soul variable supported by SoulActions.CardCollectSoul");
        }
    }
}
