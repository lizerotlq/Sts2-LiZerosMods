using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Powers.Tlipoca
{
    public class DeathTowerPower() : BasicPower
    {
        private Rng? _rng;

        private Rng Rng
        {
            get { return _rng ??= CreateRng(Owner); }
        }

        private bool IsPreviewMode { get; set; } = true;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new PowerVar<StrengthPower>(1),
            new EnergyVar(1)
        ];

        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Single;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            // 获取力量
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, DynamicVars.Strength.BaseValue, Owner, null);

            // 获得费用
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, player);
        }

        public override Task BeforeAttack(AttackCommand command)
        {
            IsPreviewMode = false;
            return Task.CompletedTask;
        }

        public override Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
        {
            IsPreviewMode = true;
            return Task.CompletedTask;
        }

        public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (target == Owner && props.HasFlag(ValueProp.Move) && dealer != null && !IsPreviewMode)
            {
                // 25% 闪避
                if (Rng.NextInt(100) < 25)
                    return 0;
            }
            return 1;
        }

        private static Rng CreateRng(Creature owner)
        {
            ArgumentNullException.ThrowIfNull(owner.CombatState);
            uint seed = owner.CombatState.RunState.Rng.Seed;
            string name = $"DeathTowerPower{owner.CombatState.RunState.ActFloor}";
            return new Rng(seed, name);
        }
    }
}
