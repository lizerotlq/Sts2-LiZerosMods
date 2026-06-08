using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Models
{
    public interface ISoulModel
    {
        public decimal ModifySoulAdditive(Creature target, decimal soul, CardModel? cardSource, CardPlay? cardPlay);
        public decimal ModifySoulMultiplicative(Creature target, decimal soul, CardModel? cardSource, CardPlay? cardPlay);
    }
}
