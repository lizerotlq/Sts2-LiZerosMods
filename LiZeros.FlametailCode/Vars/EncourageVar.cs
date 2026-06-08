using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Vars
{
    public class EncourageVar : DynamicVar
    {
        public const string NAME = "Encourage";

        public EncourageVar(decimal baseValue) : base(NAME, baseValue)
        {
        }

        public EncourageVar(string name, decimal baseValue) : base(name, baseValue)
        {
        }
    }
}
