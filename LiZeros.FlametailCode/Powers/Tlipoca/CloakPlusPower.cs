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
    public class CloakPlusPower() : BasicPower
    {
        private Creature? _dealer;

        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Single;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DynamicVar("ReduceInjury", 18),
            new DynamicVar("ReduceDamage", 50),
            new InDeathTowerVar(),
            new PowerVar<WeakPower>(5),
            new PowerVar<VulnerablePower>(2)
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
                BlockingPlayerChoiceContext context = new BlockingPlayerChoiceContext();
                await PowerCmd.Apply<WeakPower>(context, _dealer, DynamicVars.Weak.BaseValue, Owner, null);
                await PowerCmd.Apply<VulnerablePower>(context, _dealer, DynamicVars.Vulnerable.BaseValue, Owner, null);
                _dealer = null;
            }
        }

        public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, Creature? applier, out decimal modifiedAmount)
        {
            if (canonicalPower is ClarityPower && target == Owner)
            {
                modifiedAmount = 0;
                return true;
            }

            modifiedAmount = amount;
            return false;
        }

        public override Task AfterApplied(Creature? applier, CardModel? cardSource)
        {
            if (Owner.HasPower<CloakPower>())
                return PowerCmd.Remove<CloakPower>(Owner);
            return Task.CompletedTask;
        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Enemy)
                return PowerCmd.Remove(this);
            return Task.CompletedTask;
        }
    }
}
