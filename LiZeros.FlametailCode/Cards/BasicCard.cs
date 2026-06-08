using BaseLib.Abstracts;
using BaseLib.Extensions;
using LiZeros.FlametailCode.Expansions;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LiZeros.FlametailCode.Cards
{
    public abstract class BasicCard(int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true)
        : CustomCardModel(baseCost, type, rarity, target, showInCardLibrary, autoAdd)
    {
        public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
        public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    }
}
