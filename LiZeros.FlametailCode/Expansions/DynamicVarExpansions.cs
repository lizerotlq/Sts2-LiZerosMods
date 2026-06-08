using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Expansions
{
    public static class DynamicVarSetExpansions
    {
        public static DefendVar GetDefend(this DynamicVarSet set)
        {
            return (DefendVar)set[DefendVar.NAME];
        }

        public static CoattackAmountVar GetCoattackAmount(this DynamicVarSet set)
        {
            return (CoattackAmountVar)set[CoattackAmountVar.NAME];
        }

        public static CoattackTimeVar GetCoattackTime(this DynamicVarSet set)
        {
            return (CoattackTimeVar)set[CoattackTimeVar.NAME];
        }

        public static CoattackAllVar GetCoattackAll(this DynamicVarSet set)
        {
            return (CoattackAllVar)set[CoattackAllVar.NAME];
        }

        public static CoattackInvokeTimeVar GetCoattackInvokeTime(this DynamicVarSet set)
        {
            return (CoattackInvokeTimeVar)set[CoattackInvokeTimeVar.NAME];
        }

        public static EncourageVar GetEncourage(this DynamicVarSet set)
        {
            return (EncourageVar)set[EncourageVar.NAME];
        }

        public static SoulVar GetSoul(this DynamicVarSet set)
        {
            return (SoulVar)set[SoulVar.NAME];
        }
    }
}
