using LiZeros.Flametail.FlametailCode;
using LiZeros.FlametailCode.Core.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace LiZeros.FlametailCode.Relics.Tlipoca
{
    public abstract class SoulRelic : BasicTlipocaRelic
    {
        private int _soul;
        private bool _isActivating;

        [SavedProperty]
        public int Soul
        {
            get { return _soul; }
            set
            {
                AssertMutable();
                _soul = value;
                InvokeDisplayAmountChanged();
            }
        }

        private bool IsActivating
        {
            get
            {
                return _isActivating;
            }
            set
            {
                AssertMutable();
                _isActivating = value;
                InvokeDisplayAmountChanged();
            }
        }

        public override int DisplayAmount => Soul;

        public override RelicRarity Rarity => RelicRarity.Starter;

        public override bool ShowCounter => true;

        public void GainSoulInternal(decimal amount)
        {
            MainFile.Logger.Info($"Gain soul: {amount}");
            TaskHelper.RunSafely(DoActivateVisuals());
            Soul = (int)(Soul + amount);
        }

        public void LoseSoulInternal(decimal amount)
        {
            MainFile.Logger.Info($"Lose soul: {amount}");
            Soul = (int)(Soul - amount);
        }

        public override Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
        {
            return SoulCmd.GainSoul(Owner.Creature, creature.MaxHp / 4, null);
        }

        private async Task DoActivateVisuals()
        {
            IsActivating = true;
            Flash();
            await Cmd.Wait(1f);
            IsActivating = false;
        }
    }
}
