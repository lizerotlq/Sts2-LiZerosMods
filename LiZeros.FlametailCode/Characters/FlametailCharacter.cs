using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using LiZeros.FlametailCode.Cards.Flametail;
using LiZeros.FlametailCode.Expansions;
using LiZeros.FlametailCode.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace LiZeros.FlametailCode.Characters
{
    public class FlametailCharacter : PlaceholderCharacterModel
    {
        public const string CHARACTER_ID = "Flametail";
        public static readonly Color CHARACTER_COLOR = new Color("ff4923");

        public override Color NameColor => CHARACTER_COLOR;
        public override CharacterGender Gender => CharacterGender.Neutral;
        public override int StartingHp => 75;

        public override CardPoolModel CardPool => ModelDb.CardPool<FlametailCardPool>();

        public override RelicPoolModel RelicPool => ModelDb.RelicPool<FlametailRelicPool>();

        public override PotionPoolModel PotionPool => ModelDb.PotionPool<FlametailPotionPool>();

        public override IEnumerable<CardModel> StartingDeck =>
        [
            ModelDb.Card<StrikeFlametail>(),
            ModelDb.Card<StrikeFlametail>(),
            ModelDb.Card<StrikeFlametail>(),
            ModelDb.Card<DefendFlametail>(),
            ModelDb.Card<DefendFlametail>(),
            ModelDb.Card<DefendFlametail>(),
        ];

        public override IReadOnlyList<RelicModel> StartingRelics =>
        [
            ModelDb.Relic<FlameCoreRelic>()
        ];

        public override Control CustomIcon
        {
            get
            {
                var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
                icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
                return icon;
            }
        }

        public override string CustomIconTexturePath => "character_icon_flametail.png".UiImagePath();
        public override string CustomCharacterSelectIconPath => "char_select_flametail.png".UiImagePath();
        public override string CustomCharacterSelectLockedIconPath => "char_select_flametail_locked.png".UiImagePath();
        public override string CustomMapMarkerPath => "map_marker_flametail.png".UiImagePath();
    }
}
