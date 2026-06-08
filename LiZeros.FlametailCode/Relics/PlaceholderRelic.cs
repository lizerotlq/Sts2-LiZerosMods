using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace LiZeros.FlametailCode.Relics
{
    public class PlaceholderRelic : BasicTlipocaRelic
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

        public override Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
        {
            TaskHelper.RunSafely(DoActivateVisuals());
            Soul++;
            return Task.CompletedTask;
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
