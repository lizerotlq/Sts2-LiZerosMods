using LiZeros.FlametailCode.Core.Hooks;
using LiZeros.FlametailCode.Models;
using LiZeros.FlametailCode.Relics.Tlipoca;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace LiZeros.FlametailCode.Core.Commands
{
    public class SoulCmd
    {
        public static async Task<decimal> GainSoul(Creature creature, decimal amount, CardPlay? cardPlay, bool fast = false)
        {
            if (creature.Player == null ||
                creature.Player.GetRelic<SoulRelic>() is not SoulRelic relic)
                return default;

            CombatState combatState = creature.CombatState!;
            decimal modifiedAmount = amount;

            modifiedAmount = SoulHook.ModifySoul(combatState, creature, modifiedAmount, cardPlay?.Card, cardPlay, out IEnumerable<ISoulModel> modifiers);
            relic.GainSoulInternal(modifiedAmount);

            if (!fast)
                await Cmd.CustomScaledWait(0.1f, 0.25f);
            else
                await Cmd.CustomScaledWait(0f, 0.03f);

            return modifiedAmount;
        }

        public static async Task<decimal> LoseSoul(Creature creature, decimal amount, CardPlay? cardPlay, bool fast = false)
        {
            if (creature.Player == null ||
                creature.Player.GetRelic<SoulRelic>() is not SoulRelic relic)
                return default;

            CombatState combatState = creature.CombatState!;
            decimal modifiedAmount = amount;

            modifiedAmount = SoulHook.ModifySoul(combatState, creature, modifiedAmount, cardPlay?.Card, cardPlay, out IEnumerable<ISoulModel> modifiers);
            relic.LoseSoulInternal(modifiedAmount);

            if (!fast)
                await Cmd.CustomScaledWait(0.1f, 0.25f);
            else
                await Cmd.CustomScaledWait(0f, 0.03f);
            return amount;
        }
    }
}
