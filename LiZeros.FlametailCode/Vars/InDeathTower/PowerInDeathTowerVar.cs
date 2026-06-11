using LiZeros.FlametailCode.Powers.Tlipoca;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Vars.InDeathTower
{
    public class PowerInDeathTowerVar<T> : PowerVar<T>
        where T : PowerModel
    {
        public decimal InDeathTowerValue { get; }

        public PowerInDeathTowerVar(decimal powerAmount, decimal powerInDeathTowerAmount) : base(powerAmount)
        {
            InDeathTowerValue = powerInDeathTowerAmount;
        }

        public PowerInDeathTowerVar(string name, decimal powerAmount, decimal powerInDeathTowerAmount) : base(name, powerAmount)
        {
            InDeathTowerValue = powerInDeathTowerAmount;
        }

        private Creature? GetOwnCreature()
        {
            if (_owner != null && _owner.IsMutable)
            {
                if (_owner is CardModel card)
                    return card.Owner.Creature;
                if (_owner is PowerModel power)
                    return power.Owner;
                if (_owner is RelicModel relic)
                    return relic.Owner.Creature;
            }
            return null;
        }

        public decimal GetAmount()
        {
            Creature? creature = GetOwnCreature();
            if (creature != null && creature.HasPower<DeathTowerPower>())
                return InDeathTowerValue;
            return BaseValue;
        }

        protected override decimal GetBaseValueForIConvertible()
        {
            return GetAmount();
        }

        public override string ToString()
        {
            return GetAmount().ToString();
        }

        public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
        {
            if (runGlobalHooks)
                PreviewValue = Hook.ModifyPowerAmountGiven(card.CombatState!, ModelDb.Power<T>(), card.Owner.Creature, GetAmount(), target, card, out IEnumerable<AbstractModel> _);
        }
    }
}
