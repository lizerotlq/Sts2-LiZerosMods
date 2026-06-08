using BaseLib.Abstracts;
using Godot;
using LiZeros.FlametailCode.Expansions;

namespace LiZeros.FlametailCode.Characters
{
    public class FlametailPotionPool : CustomPotionPoolModel
    {
        public override Color LabOutlineColor => FlametailCharacter.CHARACTER_COLOR;

        public override string BigEnergyIconPath => "big_energy.png".UiImagePath();
        public override string TextEnergyIconPath => "text_energy.png".UiImagePath();
    }
}
