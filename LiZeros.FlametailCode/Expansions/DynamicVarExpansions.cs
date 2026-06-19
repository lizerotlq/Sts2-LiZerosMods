using LiZeros.FlametailCode.Powers.Common;
using LiZeros.FlametailCode.Vars;
using LiZeros.FlametailCode.Vars.Coattack;
using LiZeros.FlametailCode.Vars.InDeathTower;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Expansions
{
    public static class DynamicVarSetExpansions
    {
        public static DefendVar GetDefend(this DynamicVarSet set)
        {
            return (DefendVar)set[DefendVar.NAME];
        }

        public static PowerVar<EncouragePower> GetEncourage(this DynamicVarSet set)
        {
            return (PowerVar<EncouragePower>)set[nameof(EncouragePower)];
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

        public static SoulVar GetSoul(this DynamicVarSet set)
        {
            return (SoulVar)set[SoulVar.NAME];
        }

        public static CalculatedSoulVar GetCalculatedSoul(this DynamicVarSet set)
        {
            return (CalculatedSoulVar)set[CalculatedSoulVar.NAME];
        }

        public static InDeathTowerVar GetInDeathTower(this DynamicVarSet set)
        {
            return (InDeathTowerVar)set[InDeathTowerVar.NAME];
        }

        public static decimal GetAmount<T>(this PowerVar<T> powerVar) where T : PowerModel
        {
            if (powerVar is PowerInDeathTowerVar<T> powerInDeathTowerVar)
                return powerInDeathTowerVar.GetAmount();
            return powerVar.BaseValue;
        }

        public static decimal GetAmount(this DynamicVar dynamicVar)
        {
            if (dynamicVar is DynamicInDeathTowerVar dynamicInDeathTower)
                return dynamicInDeathTower.GetAmount();
            return dynamicVar.BaseValue;
        }
    }
}
