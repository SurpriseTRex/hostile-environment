using Microsoft.Xna.Framework;
using SadConsole;
using Palette = HostileEnvironment.Core.Colors.Palette;

namespace HostileEnvironment.Core.Tiles
{
    public class Tile : Cell
    {
        private Tile(int glyph,
            bool isWalkable = true, 
            bool isTransparent = false, 
            string name = "")
            : base(Palette.Tile, Palette.TileBG, glyph)
        {
            Name = name;
            IsWalkable = isWalkable;
            IsTransparent = isTransparent;
        }

        public Tile(int glyph,
            Color foreground,
            Color background,
            bool isWalkable = true,
            bool isTransparent = false,
            string name = "")
            : this(glyph, isWalkable, isTransparent, name)
        {
            Foreground = foreground;
            Background = background;
        }

        public string Name { get; }
        public bool IsWalkable { get; }
        public bool IsTransparent { get; }

        public static Tile Wall()
        {
            return new Tile('#', isWalkable: false, isTransparent: false, name: "Wall");
        }

        public static Tile Floor()
        {
            return new Tile(' ', isWalkable: true, isTransparent: true, name: "Floor");
        }
    }
}
