using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Vars.Coattack;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Powers.Flametail
{
    public class CoattackPower : BasicPower
    {
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Single;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CoattackAmountVar(5),
            new CoattackTimeVar(1),
            new CoattackAllVar(false),
            new CoattackInvokeTimeVar(0)
        ];

        public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
        {
            if (dealer != null && dealer.IsEnemy)
            {
                CoattackInvokeTimeVar invokeTimeVar = DynamicVars.GetCoattackInvokeTime();
                CoattackAllVar allEnemies = DynamicVars.GetCoattackAll();
                CoattackTimeVar timeVar = DynamicVars.GetCoattackTime();
                CoattackAmountVar amountVar = DynamicVars.GetCoattackAmount();

                while (invokeTimeVar.BaseValue > 0)
                {
                    for (int i = 0; i < timeVar.BaseValue; i++)
                    {
                        if (allEnemies.BoolVal)
                        {
                            // 对所有敌人造成伤害
                            await CreatureCmd.Damage(choiceContext, CombatState.Enemies, amountVar.BaseValue, ValueProp.Unpowered | ValueProp.SkipHurtAnim, Owner, null);
                        }
                        else
                        {
                            // 对攻击者造成伤害
                            await CreatureCmd.Damage(choiceContext, dealer, amountVar.BaseValue, ValueProp.Unpowered | ValueProp.SkipHurtAnim, Owner, null);
                        }
                    }
                    invokeTimeVar.BaseValue--;
                }
            }
        }
    }
}
