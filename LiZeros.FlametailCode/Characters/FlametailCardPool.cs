using BaseLib.Abstracts;
using Godot;
using LiZeros.FlametailCode.Expansions;

namespace LiZeros.FlametailCode.Characters
{
    public class FlametailCardPool : CustomCardPoolModel
    {
        public override string Title => FlametailCharacter.CHARACTER_ID;

        public override string BigEnergyIconPath => "big_energy.png".UiImagePath();
        public override string TextEnergyIconPath => "text_energy.png".UiImagePath();

        public override float H => 0.0278f;
        public override float S => 0.8627f;
        public override float V => 1.0000f;

        public override Color DeckEntryCardColor => new Color("ff4923");

        public override bool IsColorless => false;
    }
}
