using BaseLib.Utils;
using LiZeros.FlametailCode.Powers.Tlipoca;
using LiZeros.FlametailCode.Vars.InDeathTower;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LiZeros.FlametailCode.Cards.Tlipoca
{
    public class Cloak() : BasicDeathTowerCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(10, ValueProp.Move),
            new InDeathTowerVar()
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            .. base.ExtraHoverTips,
            HoverTipFactory.FromPower<CloakPower>(),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CommonActions.CardBlock(this, cardPlay);
            await PowerCmd.Apply<CloakPower>(Owner.Creature, 1, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(5);
        }
    }
}
