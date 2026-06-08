using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Vars
{
    public class CoattackAmountVar : DynamicVar
    {
        public const string NAME = "CoattackAmount";

        public CoattackAmountVar(decimal baseValue) : base(NAME, baseValue)
        {
        }

        public CoattackAmountVar(string name, decimal baseValue) : base(name, baseValue)
        {
        }
    }
}
