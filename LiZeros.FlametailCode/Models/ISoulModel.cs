using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Models
{
    public interface ISoulModel
    {
        public Task BeforeSoulLost(Creature creature, decimal amount, Creature? cardSource);
        public Task AfterSoulLost(Creature creature, decimal amount, Creature? cardSource);
        public Task BeforeSoulGained(Creature creature, decimal amount, Creature? cardSource);
        public Task AfterSoulGained(Creature creature, decimal amount, Creature? cardSource);
        public decimal ModifySoulAdditive(Creature target, decimal amount, CardModel? cardSource, CardPlay? cardPlay);
        public decimal ModifySoulMultiplicative(Creature target, decimal amount, CardModel? cardSource, CardPlay? cardPlay);
        public void InvokeExecutionFinished();
    }
}
