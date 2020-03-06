using HostileEnvironment.Core.Colors;
using HostileEnvironment.Core.Entities;
using Microsoft.Xna.Framework;
using SadConsole.Components;

namespace HostileEnvironment.Core.Map
{
    public class World
    {
        private int _mapWidth = 100;
        private int _mapHeight = 100;
        private int _maxRooms = 500;
        private int _minRoomSize = 4;
        private int _maxRoomSize = 15;

        public Map CurrentMap { get; set; }
        public Player Player { get; set; }

        public World()
        {
            CreateMap();
            CreatePlayer();
        }

        private void CreateMap()
        {
            CurrentMap = new Map(_mapWidth, _mapHeight);
            MapGenerator mapGen = new MapGenerator();
            CurrentMap = mapGen.GenerateMap(_mapWidth, _mapHeight, _maxRooms, _minRoomSize, _maxRoomSize);
        }

        private void CreatePlayer()
        {
            Player = new Player(Palette.Player, Palette.PlayerBG);
            Player.Components.Add(new EntityViewSyncComponent());
            Player.Position = new Point(20, 10);
        }
    }
}
