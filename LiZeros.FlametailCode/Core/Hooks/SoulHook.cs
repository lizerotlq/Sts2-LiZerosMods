using LiZeros.FlametailCode.Models;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Core.Hooks
{
    public static class SoulHook
    {
        public static async Task BeforeSoulGained(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
        {
            foreach (AbstractModel item in combatState.IterateHookListeners())
            {
                if (item is ISoulModel soulItem)
                {
                    await soulItem.BeforeSoulGained(combatState, creature, amount, cardSource);
                    soulItem.InvokeExecutionFinished();
                }
            }
        }

        public static async Task AfterSoulGained(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
        {
            foreach (AbstractModel item in combatState.IterateHookListeners())
            {
                if (item is ISoulModel soulItem)
                {
                    await soulItem.AfterSoulGained(combatState, creature, amount, cardSource);
                    soulItem.InvokeExecutionFinished();
                }
            }
        }
        public static async Task BeforeSoulLost(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
        {
            foreach (AbstractModel item in combatState.IterateHookListeners())
            {
                if (item is ISoulModel soulItem)
                {
                    await soulItem.BeforeSoulLost(combatState, creature, amount, cardSource);
                    soulItem.InvokeExecutionFinished();
                }
            }
        }

        public static async Task AfterSoulLost(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
        {
            foreach (AbstractModel item in combatState.IterateHookListeners())
            {
                if (item is ISoulModel soulItem)
                {
                    await soulItem.AfterSoulLost(combatState, creature, amount, cardSource);
                    soulItem.InvokeExecutionFinished();
                }
            }
        }

        public static decimal ModifySoul(CombatState combatState, Creature target, decimal amount, CardModel? cardSource, CardPlay? cardPlay, out IEnumerable<ISoulModel> modifiers)
        {
            List<ISoulModel> list = [];
            decimal modifiedAmount = amount;

            foreach (AbstractModel item in combatState.IterateHookListeners())
            {
                if (item is ISoulModel soulItem)
                {
                    decimal num = soulItem.ModifySoulAdditive(target, modifiedAmount, cardSource, cardPlay);
                    if (num != 0)
                    {
                        modifiedAmount += num;
                        list.Add(soulItem);
                    }
                }
            }

            foreach (AbstractModel item in combatState.IterateHookListeners())
            {
                if (item is ISoulModel soulItem)
                {
                    decimal num = soulItem.ModifySoulMultiplicative(target, modifiedAmount, cardSource, cardPlay);
                    if (num != 0)
                    {
                        modifiedAmount *= num;
                        list.Add(soulItem);
                    }
                }
            }

            modifiers = list;
            return modifiedAmount;
        }
    }
}
