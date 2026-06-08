using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Vars
{
    public class CoattackInvokeTimeVar : DynamicVar
    {
        public const string NAME = "CoattackInvokeTime";

        public CoattackInvokeTimeVar(decimal baseValue) : base(NAME, baseValue)
        {
        }

        public CoattackInvokeTimeVar(string name, decimal baseValue) : base(name, baseValue)
        {
        }
    }
}
