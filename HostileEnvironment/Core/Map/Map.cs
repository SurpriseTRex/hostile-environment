using HostileEnvironment.Core.Tiles;
using Microsoft.Xna.Framework;

namespace HostileEnvironment.Core.Map
{
    public class Map
    {
        public Tile[] Tiles { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width * height];
        }

        public bool IsTileWalkable(Point location)
        {
            if (location.X < 0 || location.Y < 0 || location.X >= Width || location.Y >= Height)
                return false;

            return Tiles[location.Y * Width + location.X].IsWalkable;
        }
    }
}
