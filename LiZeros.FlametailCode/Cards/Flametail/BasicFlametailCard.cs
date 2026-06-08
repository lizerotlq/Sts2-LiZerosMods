using BaseLib.Utils;
using LiZeros.FlametailCode.Characters.Flametail;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LiZeros.FlametailCode.Cards.Flametail
{
    [Pool(typeof(FlametailCardPool))]
    public abstract class BasicFlametailCard(int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true)
        : BasicCard(baseCost, type, rarity, target, showInCardLibrary, autoAdd)
    {
    }
}
