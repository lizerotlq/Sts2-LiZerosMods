using LiZeros.FlametailCode.Expansions;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Powers.Flametail
{
    public class DefendPower : BasicPower
    {
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;

        private readonly Queue<int> _defends = [];

        public IEnumerable<int> Defends => _defends;

        public override int DisplayAmount => GetDefendAmount();

        public int GetDefendAmount()
        {
            return _defends.Sum();
        }

        public void PushDefend(int defendAmount)
        {
            _defends.Enqueue(defendAmount);
            InvokeDisplayAmountChanged();
        }

        public bool TryPopDefend(out int defendAmount)
        {
            defendAmount = default;

            // 弹出顶部招架
            if (_defends.TryDequeue(out defendAmount))
            {
                InvokeDisplayAmountChanged();
                return true;
            }
            return false;
        }

        public int PopDefend()
        {
            if (TryPopDefend(out int defendAmount))
                return defendAmount;
            throw new InvalidOperationException();
        }

        public void ClearDefends()
        {
            _defends.Clear();
            InvokeDisplayAmountChanged();
        }

        public override Task AfterApplied(Creature? applier, CardModel? cardSource)
        {
            ClearDefends();
            return Task.CompletedTask;
        }

        public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            ClearDefends();
            return Task.CompletedTask;
        }

        public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, Creature? applier, out decimal modifiedAmount)
        {
            if (canonicalPower is DefendPower defendPower)
            {
                defendPower.PushDefend((int)amount);
                modifiedAmount = 0;
                return true;
            }
            modifiedAmount = amount;
            return false;
        }

        public override decimal ModifyHpLostAfterOsty(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            // 如果目标是所有者并且攻击者是敌人
            if (target == Owner && !props.HasFlag(ValueProp.Unblockable))
            {
                while (TryPopDefend(out int defendAmount))
                {
                    amount -= defendAmount;
                    if (amount <= 0)
                    {
                        if (dealer != null && dealer.IsEnemy)
                        {
                            CoattackPower? counterattackPower = Owner.GetPower<CoattackPower>();
                            if (counterattackPower != null)
                            {
                                counterattackPower.DynamicVars.GetCoattackInvokeTime().BaseValue++;
                            }
                        }
                        return 0;
                    }
                }
            }
            return amount;
        }
    }
}
