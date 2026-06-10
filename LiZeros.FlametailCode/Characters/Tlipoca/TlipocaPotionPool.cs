using BaseLib.Abstracts;
using Godot;
using LiZeros.FlametailCode.Expansions;

namespace LiZeros.FlametailCode.Characters.Tlipoca
{
    public class TlipocaPotionPool : CustomPotionPoolModel
    {
        public override Color LabOutlineColor => TlipocaCharacter.CHARACTER_COLOR;

        public override string BigEnergyIconPath => "big_energy.png".UiImagePath();
        public override string TextEnergyIconPath => "text_energy.png".UiImagePath();
    }
}
