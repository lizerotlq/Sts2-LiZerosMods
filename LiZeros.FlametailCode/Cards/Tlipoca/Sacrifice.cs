using BaseLib.Extensions;
using LiZeros.FlametailCode.Powers.Tlipoca;
using LiZeros.FlametailCode.Relics.Tlipoca;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class Sacrifice() : BasicTlipocaCard(0, CardType.Status, CardRarity.Basic, TargetType.Self)
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new HpLossVar(75),
            new PowerVar<SacrificePower>(5)
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Innate
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<SacrificePower>(),
            HoverTipFactory.FromPower<NightLordPower>()
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            // 缺失夜之石自杀
            if (Owner.GetRelic<NightStoneRelic>() == null)
            {
                await CreatureCmd.Kill(Owner.Creature);
                return;
            }

            decimal loseHp = Owner.Creature.CurrentHp * DynamicVars.HpLoss.BaseValue / 100;

            // 失去当前生命。
            await CreatureCmd.Damage(choiceContext, Owner.Creature, loseHp, ValueProp.Unblockable, this);

            // 献祭
            await PowerCmd.Apply<SacrificePower>(choiceContext, Owner.Creature, DynamicVars.Power<SacrificePower>().BaseValue, Owner.Creature, this);

            // 结束当前回合。
            PlayerCmd.EndTurn(Owner, canBackOut: false);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.HpLoss.UpgradeValueBy(-25);
            DynamicVars.Power<SacrificePower>().UpgradeValueBy(-2);
        }
    }
}
