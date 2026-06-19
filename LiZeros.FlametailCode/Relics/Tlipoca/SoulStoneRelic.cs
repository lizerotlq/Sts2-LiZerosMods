using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Relics.Tlipoca
{
    /// <summary>
    /// 灵魂之石头：升级成为夜之石
    /// </summary>
    public class SoulStoneRelic : SoulRelic
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new SoulVar(100)
        ];

        public int UpgradeRequiredSouls => DynamicVars.GetSoul().IntValue;

        public Task Upgrade()
        {
            NightStoneRelic nightStone = (NightStoneRelic)ModelDb.Relic<NightStoneRelic>().ToMutable();
            nightStone.Soul = Soul;
            return RelicCmd.Replace(this, nightStone);
        }

        public override Task AfterSoulGained(Creature creature, decimal amount, Creature? cardSource)
        {
            if (Soul > UpgradeRequiredSouls)
                return Upgrade();
            return Task.CompletedTask;
        }
    }
}
