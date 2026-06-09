using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace LiZeros.Flametail.FlametailCode
{
    [ModInitializer(nameof(Initialize))]
    public partial class MainFile : Node
    {
        public const string MOD_ID = "LiZeros.Flametail";
        public const string RES_PATH = $"res://{MOD_ID}";

        public static Logger Logger { get; } = new Logger(MOD_ID, LogType.Generic);

        public static void Initialize()
        {
            Harmony harmony = new Harmony(MOD_ID);
            harmony.PatchAll();
        }
    }
}
