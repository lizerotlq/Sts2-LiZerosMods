using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Vars
{
    public class SoulVar : DynamicVar
    {
        public const string NAME = "Soul";

        public SoulVar(decimal baseValue) : base(NAME, baseValue)
        {
        }

        public SoulVar(string name, decimal baseValue) : base(name, baseValue)
        {
        }
    }
}
