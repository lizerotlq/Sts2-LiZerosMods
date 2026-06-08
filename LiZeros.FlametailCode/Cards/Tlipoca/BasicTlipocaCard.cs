using BaseLib.Utils;
using LiZeros.FlametailCode.Characters.Tlipoca;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    [Pool(typeof(TlipocaCardPool))]
    public abstract class BasicTlipocaCard(int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true)
        : BasicCard(baseCost, type, rarity, target, showInCardLibrary, autoAdd)
    {
    }
}
