using LiZeros.FlametailCode.Powers.Flametail;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LiZeros.FlametailCode.Relics.Flametail
{
    /// <summary>
    /// 焰心：最大能量＋1，招架成功则对敌人造成一次伤害 5
    /// </summary>
    public class FlameCoreRelic : BasicFlametailRelic
    {
        public override RelicRarity Rarity => RelicRarity.Starter;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new EnergyVar(1)
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DefendPower>(),
            HoverTipFactory.FromPower<CoattackPower>(),
        ];

        private List<int> _counterattacks = [];

        public IEnumerable<int> Counterattacks
        {
            get { return _counterattacks; }
            set
            {
                AssertMutable();
                _counterattacks = [.. value];
                InvokeDisplayAmountChanged();
            }
        }

        public override decimal ModifyMaxEnergy(Player player, decimal amount)
        {
            if (player == Owner)
                return amount + DynamicVars.Energy.BaseValue;
            return amount;
        }

        public override async Task BeforeCombatStart()
        {
            BlockingPlayerChoiceContext context = new BlockingPlayerChoiceContext();
            await PowerCmd.Apply<DefendPower>(context, Owner.Creature, 1, Owner.Creature, null);
            await PowerCmd.Apply<CoattackPower>(context, Owner.Creature, 1, Owner.Creature, null);
        }
    }
}
