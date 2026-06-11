using LiZeros.FlametailCode.Core.Commands;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Vars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class AbsorbSoul() : BasicTlipocaCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            Core.HoverTips.HoverTipFactory.Static(Core.HoverTips.StaticHoverTip.Soul),
            Core.HoverTips.HoverTipFactory.Static(Core.HoverTips.StaticHoverTip.Absorb)
        ];

        private static decimal CalculateSoulAmount(CardModel card, Creature? creature)
        {
            decimal baseValue = card.DynamicVars.CalculationBase.BaseValue;
            if (creature != null)
                return baseValue - Math.Min(baseValue, creature.CurrentHp);
            return 0;
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CalculationBaseVar(10),
            new CalculationExtraVar(-1),
            new CalculatedSoulVar().WithMultiplier(CalculateSoulAmount)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            Creature? target = cardPlay.Target;
            if (target != null)
            {
                IEnumerable<DamageResult> results = await CreatureCmd.Damage(choiceContext, target, DynamicVars.GetCalculatedSoul().Calculate(target), ValueProp.Unblockable, Owner.Creature, this);
                decimal soul = results.Sum(r => r.UnblockedDamage);
                await SoulCmd.GainSoul(Owner.Creature, soul, cardPlay);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(5);
        }
    }
}
