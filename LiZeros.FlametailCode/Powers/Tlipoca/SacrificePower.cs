using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace LiZeros.FlametailCode.Powers.Tlipoca
{
    public class SacrificePower : BasicPower
    {
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<NightLordPower>()
        ];

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            // 眩晕
            PlayerCmd.EndTurn(player, canBackOut: false);
            await PowerCmd.Decrement(this);

            // 下一回合获得夜之主
            if (Amount == 0)
                await PowerCmd.Apply<GainNightLordPower>(choiceContext, player.Creature, 1, player.Creature, null);
        }
    }
}
