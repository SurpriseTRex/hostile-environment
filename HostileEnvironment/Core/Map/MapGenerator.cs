using System;
using System.Collections.Generic;
using System.Linq;
using HostileEnvironment.Core.Tiles;
using Microsoft.Xna.Framework;

namespace HostileEnvironment.Core.Map
{
    public class MapGenerator
    {
        private Map _map;
        private Random _rng;
        private List<Rectangle> _rooms;

        public Map GenerateMap(int mapWidth, int mapHeight, int maxRooms, int minRoomSize, int maxRoomSize)
        {
            _map = new Map(mapWidth, mapHeight);
            _rooms = new List<Rectangle>();

            _rng = new Random();

            for (int i = 0; i < maxRooms; i++)
            {
                TryPlaceRoom(mapWidth, mapHeight, minRoomSize, maxRoomSize);
            }

            FloodWalls();

            foreach (Rectangle room in _rooms)
            {
                CreateRoom(room);
            }

            for (int r = 1; r < _rooms.Count; r++)
            {
                ConnectRoomToPrevious(r);
            }

            return _map;
        }

        private void ConnectRoomToPrevious(int r)
        {
            Point previousRoomCenter = _rooms[r - 1].Center;
            Point currentRoomCenter = _rooms[r].Center;

            if (_rng.Next(1, 2) == 1)
            {
                CreateHorizontalTunnel(previousRoomCenter.X, currentRoomCenter.X, previousRoomCenter.Y);
                CreateVerticalTunnel(previousRoomCenter.Y, currentRoomCenter.Y, currentRoomCenter.X);
            }
            else
            {
                CreateVerticalTunnel(previousRoomCenter.Y, currentRoomCenter.Y, previousRoomCenter.X);
                CreateHorizontalTunnel(previousRoomCenter.X, currentRoomCenter.X, currentRoomCenter.Y);
            }
        }

        private void TryPlaceRoom(int mapWidth, int mapHeight, int minRoomSize, int maxRoomSize)
        {
            int newRoomWidth = _rng.Next(minRoomSize, maxRoomSize);
            int newRoomHeight = _rng.Next(minRoomSize, maxRoomSize);

            int newRoomX = _rng.Next(0, mapWidth - newRoomWidth - 1);
            int newRoomY = _rng.Next(0, mapHeight - newRoomHeight - 1);

            Rectangle newRoom = new Rectangle(newRoomX, newRoomY, newRoomWidth, newRoomHeight);

            bool newRoomIntersects = _rooms.Any(room => newRoom.Intersects(room));

            if (!newRoomIntersects)
            {
                _rooms.Add(newRoom);
            }
        }

        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                CreateFloor(new Point(x, yPosition));
            }
        }

        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                CreateFloor(new Point(xPosition, y));
            }
        }

        private void FloodWalls()
        {
            for (int i = 0; i < _map.Tiles.Length; i++)
            {
                _map.Tiles[i] = Tile.Wall();
            }
        }

        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right - 1; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom - 1; y++)
                {
                    CreateFloor(new Point(x, y));
                }
            }

            List<Point> perimeter = GetBorderCellLocations(room);
            foreach (Point location in perimeter)
            {
                CreateWall(location);
            }
        }

        private void CreateFloor(Point location)
        {
            _map.Tiles[location.ToIndex(_map.Width)] = Tile.Floor();
        }

        private void CreateWall(Point location)
        {
            _map.Tiles[location.ToIndex(_map.Width)] = Tile.Wall();
        }

        private List<Point> GetBorderCellLocations(Rectangle room)
        {
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            List<Point> borderCells = GetTileLocationsAlongLine(xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange(GetTileLocationsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(xMax, yMin, xMax, yMax));

            return borderCells;
        }

        public IEnumerable<Point> GetTileLocationsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            xOrigin = ClampX(xOrigin);
            yOrigin = ClampY(yOrigin);
            xDestination = ClampX(xDestination);
            yDestination = ClampY(yDestination);

            int dx = Math.Abs(xDestination - xOrigin);
            int dy = Math.Abs(yDestination - yOrigin);

            int sx = xOrigin < xDestination ? 1 : -1;
            int sy = yOrigin < yDestination ? 1 : -1;
            int err = dx - dy;

            while (true)
            {

                yield return new Point(xOrigin, yOrigin);
                if (xOrigin == xDestination && yOrigin == yDestination)
                {
                    break;
                }
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    xOrigin += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    yOrigin += sy;
                }
            }
        }

        private int ClampX(int x)
        {
            return (x < 0) ? 0
                : (x > _map.Width - 1) ? _map.Width - 1
                : x;
        }

        private int ClampY(int y)
        {
            return (y < 0) ? 0
                : (y > _map.Height - 1) ? _map.Height - 1
                : y;
        }
    }
}
