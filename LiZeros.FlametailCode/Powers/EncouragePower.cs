using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LiZeros.FlametailCode.Powers
{
    public class EncouragePower : BasicPower
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player == Owner.Player && AmountOnTurnStart != 0)
            {
                await CardPileCmd.Draw(choiceContext, Amount, player);
                await PlayerCmd.GainEnergy(Amount, player);
                await PowerCmd.Remove(this);
            }
        }
    }
}
