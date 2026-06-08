using LiZeros.FlametailCode.Expansions;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Powers.Flametail
{
    /// <summary>
    /// 使反击攻击所有敌人 并且 额外攻击一次。
    /// </summary>
    public class ForwardSwordsmanshipPower : BasicPower
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<CoattackPower>()
        ];

        public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if (power is ForwardSwordsmanshipPower && power.Owner == Owner)
            {
                if (Amount == 0)
                    RemoveInternal();

                CoattackPower? coattackPower = Owner.GetPower<CoattackPower>();
                if (coattackPower != null)
                    coattackPower.DynamicVars.GetCoattackTime().BaseValue += amount;
            }
            return Task.CompletedTask;
        }

        public override Task AfterApplied(Creature? applier, CardModel? cardSource)
        {
            CoattackPower? coattackPower = Owner.GetPower<CoattackPower>();
            if (coattackPower != null)
                coattackPower.DynamicVars.GetCoattackAll().BoolVal = true;
            return Task.CompletedTask;
        }

        public override Task AfterRemoved(Creature oldOwner)
        {
            CoattackPower? counterattackPower = Owner.GetPower<CoattackPower>();
            if (counterattackPower != null)
                counterattackPower.DynamicVars.GetCoattackAll().BoolVal = false;
            return Task.CompletedTask;
        }
    }
}
