using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Vars.Coattack
{
    public class CoattackAllVar : BoolVar
    {
        public const string NAME = "CoattackAll";

        public CoattackAllVar(bool baseValue) : base(NAME, baseValue)
        {
        }

        public CoattackAllVar(string name, bool baseValue) : base(name, baseValue)
        {
        }
    }
}
