using BaseLib.Utils;
using LiZeros.FlametailCode.Core.Commands;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Vars.InDeathTower;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class Cutting() : BasicDeathTowerCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        private static decimal CalculateLoseMaxHp(CardModel card, Creature? creature)
        {
            if (card is Cutting c && c.IsMutable && creature != null)
            {
                // 存在死神塔时倍率增加到 5%
                decimal ratio = card.DynamicVars.GetValueOrDefault("LoseMaxHpRatio")!.GetAmount();
                decimal maxHp = creature.MaxHp;
                return maxHp * ratio / 100;
            }
            return 0;
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(10, ValueProp.Move),
            new DynamicInDeathTowerVar("LoseMaxHpRatio", 2, 5),
            new InDeathTowerVar(),
            new CalculationBaseVar(0),
            new CalculationExtraVar(1),
            new CalculatedVar("CalculatedLoseMaxHp").WithMultiplier(CalculateLoseMaxHp)
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            .. base.ExtraHoverTips,
            Core.HoverTips.HoverTipFactory.Static(Core.HoverTips.StaticHoverTip.Soul),
        ];

        public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
        {
            if (card == this && VerifyPlayable(out bool hasDeathTower) && hasDeathTower)
            {
                modifiedCost = 0;
                return true;
            }
            modifiedCost = originalCost;
            return false;
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CalculatedVar? calculatedLoseMaxHp = (CalculatedVar?)DynamicVars.GetValueOrDefault("CalculatedLoseMaxHp");
            ArgumentNullException.ThrowIfNull(cardPlay.Target);
            ArgumentNullException.ThrowIfNull(calculatedLoseMaxHp);

            // 造成伤害
            AttackCommand command = await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);

            // 扣除最大生命值上限
            int loseMaxHp = (int)calculatedLoseMaxHp.Calculate(cardPlay.Target);
            await CreatureCmd.LoseMaxHp(choiceContext, cardPlay.Target, loseMaxHp, true);

            // 获得灵魂
            decimal soul = command.Results.SelectMany(r => r).Sum(r => r.UnblockedDamage) + loseMaxHp;
            await SoulCmd.GainSoul(Owner.Creature, soul, cardPlay);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(5);
        }
    }
}
