using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Vars
{
    public class CoattackTimeVar : DynamicVar
    {
        public const string NAME = "CoattackTime";

        public CoattackTimeVar(decimal baseValue) : base(NAME, baseValue)
        {
        }

        public CoattackTimeVar(string name, decimal baseValue) : base(name, baseValue)
        {
        }
    }
}
