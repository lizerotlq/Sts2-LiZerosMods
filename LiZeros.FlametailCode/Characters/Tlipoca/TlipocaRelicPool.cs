using BaseLib.Abstracts;
using Godot;
using LiZeros.FlametailCode.Characters.Flametail;
using LiZeros.FlametailCode.Expansions;

namespace LiZeros.FlametailCode.Characters.Tlipoca
{
    public class TlipocaRelicPool : CustomRelicPoolModel
    {
        public override Color LabOutlineColor => FlametailCharacter.CHARACTER_COLOR;

        public override string BigEnergyIconPath => "big_energy.png".UiImagePath();
        public override string TextEnergyIconPath => "text_energy.png".UiImagePath();
    }
}
