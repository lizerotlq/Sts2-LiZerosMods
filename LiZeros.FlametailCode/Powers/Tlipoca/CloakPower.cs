using LiZeros.FlametailCode.Vars.InDeathTower;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Powers.Tlipoca
{
    public class CloakPower() : BasicPower
    {
        private Creature? _dealer;

        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Single;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DynamicInDeathTowerVar("ReduceInjury", 10, 18),
            new DynamicInDeathTowerVar("ReduceDamage", 20, 50),
            new InDeathTowerVar(),
            new PowerInDeathTowerVar<WeakPower>(2, 5),
            new PowerInDeathTowerVar<VulnerablePower>(0, 2)
        ];

        public override decimal ModifyHpLostAfterOsty(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (target == Owner)
            {
                decimal reduceDamage = DynamicVars.GetValueOrDefault("ReduceDamage")!.BaseValue;
                decimal reduceInjury = DynamicVars.GetValueOrDefault("ReduceInjury")!.BaseValue;
                if (amount <= reduceInjury)
                    return 0;
                if (dealer != null)
                    _dealer = dealer;
                return amount * (100 - reduceDamage) / 100;
            }
            return amount;
        }

        public override async Task AfterModifyingHpLostAfterOsty()
        {
            if (_dealer != null)
            {
                await PowerCmd.Apply<WeakPower>(_dealer, DynamicVars.Weak.BaseValue, Owner, null);
                await PowerCmd.Apply<VulnerablePower>(_dealer, DynamicVars.Vulnerable.BaseValue, Owner, null);
                _dealer = null;
            }
        }

        public override Task AfterApplied(Creature? applier, CardModel? cardSource)
        {
            if (Owner.HasPower<CloakPlusPower>())
                return PowerCmd.Remove(this);
            return Task.CompletedTask;
        }

        public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side == CombatSide.Enemy)
                return PowerCmd.Remove(this);
            return Task.CompletedTask;
        }
    }
}
