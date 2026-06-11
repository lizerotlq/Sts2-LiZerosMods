using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace LiZeros.FlametailCode.Powers.Tlipoca
{
    public class GainNightLordPower : BasicPower
    {
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Single;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            await PowerCmd.Apply<NightLordPower>(Owner, 1, Owner, null);
            await PowerCmd.Remove(this);
        }
    }
}
