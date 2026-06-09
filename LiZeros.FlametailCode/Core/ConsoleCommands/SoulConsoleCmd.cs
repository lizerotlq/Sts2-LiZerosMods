using LiZeros.FlametailCode.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Runs;

namespace LiZeros.FlametailCode.Core.ConsoleCommands
{
    public class SoulConsoleCmd : AbstractConsoleCmd
    {
        public override string CmdName => "soul";

        public override string Args => "<amount:int>";

        public override string Description => "Manipulate player soul(if the player has soulrelic)!";

        public override bool IsNetworked => true;

        public override CmdResult Process(Player? issuingPlayer, string[] args)
        {
            if (args.Length < 1)
            {
                return new CmdResult(success: false, "An amount is required");
            }

            if (!int.TryParse(args[0], out var result))
            {
                return new CmdResult(success: false, "First argument (the soul amount) must be an int.");
            }

            if (issuingPlayer == null || !RunManager.Instance.IsInProgress)
            {
                return new CmdResult(success: false, "A run does not appear to be in progress");
            }

            if (result >= 0)
                return new CmdResult(SoulCmd.GainSoul(issuingPlayer.Creature, result, null, true), success: true, $"'{result}' soul added.");
            return new CmdResult(SoulCmd.LoseSoul(issuingPlayer.Creature, result, null, true), success: true, $"'{result}' soul removed.");
        }
    }
}
