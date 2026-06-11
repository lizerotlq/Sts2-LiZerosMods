using LiZeros.FlametailCode.Powers.Tlipoca;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Vars.InDeathTower
{
    public class DynamicInDeathTowerVar : DynamicVar
    {
        public decimal InDeathTowerValue { get; }

        public DynamicInDeathTowerVar(string name, decimal baseValue, decimal inDeathTowerValue) : base(name, baseValue)
        {
            InDeathTowerValue = inDeathTowerValue;
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
            PreviewValue = GetAmount();
        }
    }
}
