using LiZeros.FlametailCode.Core.Commands;
using LiZeros.FlametailCode.Models;
using LiZeros.FlametailCode.Relics.Tlipoca;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using System.Diagnostics.CodeAnalysis;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public abstract class BasicSoulCostCard(int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true)
        : BasicTlipocaCard(baseCost, type, rarity, target, showInCardLibrary, autoAdd), ISoulModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            Core.HoverTips.HoverTipFactory.Static(Core.HoverTips.StaticHoverTip.Soul)
        ];

        protected override bool IsPlayable => VerifyPlayable(out _);
        protected override bool ShouldGlowGoldInternal => VerifyPlayable(out bool soulUsable) && soulUsable;

        protected virtual decimal SoulCost { get; }
        protected virtual bool RequiredSoul { get; }

        protected virtual bool VerifyPlayable(out bool soulUsable)
        {
            // 存在灵魂遗物并且灵魂足够
            if (VerifySoulStoneRelic(out SoulRelic? relic) && VerifySoulEnough(relic))
            {
                soulUsable = true;
                return true;
            }
            else
            {
                soulUsable = false;
                return !RequiredSoul;
            }
        }

        protected virtual bool VerifySoulStoneRelic([NotNullWhen(true)] out SoulRelic? relic)
        {
            if (IsMutable)
            {
                relic = Owner.GetRelic<SoulRelic>();
                return relic != null;
            }
            relic = null;
            return false;
        }

        protected virtual bool VerifySoulEnough(SoulRelic relic)
        {
            return relic.Soul >= SoulCost;
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (!VerifyPlayable(out bool soulUsable))
                return;

            if (soulUsable)
            {
                await OnPlayWithSoul(choiceContext, cardPlay);
                await SoulCmd.LoseSoul(Owner.Creature, SoulCost, cardPlay);
            }
            else
            {
                await OnPlayWithoutSoul(choiceContext, cardPlay);
            }
        }

        protected virtual Task OnPlayWithSoul(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnPlayWithoutSoul(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return Task.CompletedTask;
        }

        public virtual Task BeforeSoulGained(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
        {
            return Task.CompletedTask;
        }

        public virtual Task AfterSoulGained(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
        {
            return Task.CompletedTask;
        }

        public virtual Task BeforeSoulLost(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
        {
            return Task.CompletedTask;
        }

        public virtual Task AfterSoulLost(CombatState combatState, Creature creature, decimal amount, Creature? cardSource)
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
