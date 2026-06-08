using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Vars
{
    public class DefendVar : DynamicVar
    {
        public const string NAME = "Defend";

        public DefendVar(decimal baseValue) : base(NAME, baseValue)
        {
        }

        public DefendVar(string name, decimal baseValue) : base(name, baseValue)
        {
        }
    }
}
