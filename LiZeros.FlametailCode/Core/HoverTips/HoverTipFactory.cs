using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Core.HoverTips
{
    public static class HoverTipFactory
    {
        public static IHoverTip Static(StaticHoverTip tip, params DynamicVar[] vars)
        {
            Type type = tip.GetType();
            string text = type.GetPrefix() + StringHelper.Slugify(tip.ToString());
            LocString locString = L10NStatic(text + ".title");
            LocString locString2 = L10NStatic(text + ".description");
            foreach (DynamicVar dynamicVar in vars)
            {
                locString.Add(dynamicVar);
                locString2.Add(dynamicVar);
            }
            return new HoverTip(locString, locString2);
        }

        private static LocString L10NStatic(string entry)
        {
            return new LocString("static_hover_tips", entry);
        }
    }
}
