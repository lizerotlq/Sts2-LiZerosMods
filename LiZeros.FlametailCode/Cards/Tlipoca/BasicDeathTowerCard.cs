using LiZeros.FlametailCode.Models;
using LiZeros.FlametailCode.Powers.Tlipoca;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public abstract class BasicDeathTowerCard(int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true)
        : BasicTlipocaCard(baseCost, type, rarity, target, showInCardLibrary, autoAdd), ISoulModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DeathTowerPower>()
        ];

        protected override bool IsPlayable => VerifyPlayable(out _);
        protected override bool ShouldGlowGoldInternal => VerifyPlayable(out bool hasDeathTower) && hasDeathTower;

        protected virtual bool RequiredDeathTower { get; }

        protected virtual bool VerifyPlayable(out bool hasDeathTower)
        {
            hasDeathTower = false;
            if (IsMutable)
            {
                if (Owner.Creature.HasPower<DeathTowerPower>())
                {
                    hasDeathTower = true;
                    return true;
                }
                return !RequiredDeathTower;
            }
            return false;
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (!VerifyPlayable(out bool soulUsable))
                return;

            if (soulUsable)
            {
                await OnPlayWithDeathTower(choiceContext, cardPlay);
            }
            else
            {
                await OnPlayWithoutDeathTower(choiceContext, cardPlay);
            }
        }

        protected virtual Task OnPlayWithDeathTower(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnPlayWithoutDeathTower(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return Task.CompletedTask;
        }

        public virtual Task BeforeSoulGained(Creature creature, decimal amount, Creature? cardSource)
        {
            return Task.CompletedTask;
        }

        public virtual Task AfterSoulGained(Creature creature, decimal amount, Creature? cardSource)
        {
            return Task.CompletedTask;
        }

        public virtual Task BeforeSoulLost(Creature creature, decimal amount, Creature? cardSource)
        {
            return Task.CompletedTask;
        }

        public virtual Task AfterSoulLost(Creature creature, decimal amount, Creature? cardSource)
        {
            return Task.CompletedTask;
        }

        public virtual decimal ModifySoulAdditive(Creature target, decimal amount, CardModel? cardSource, CardPlay? cardPlay)
        {
            return 0;
        }

        public virtual decimal ModifySoulMultiplicative(Creature target, decimal amount, CardModel? cardSource, CardPlay? cardPlay)
        {
            return 1;
        }
    }
}
