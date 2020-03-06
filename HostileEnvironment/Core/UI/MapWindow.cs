using System;
using HostileEnvironment.Core.Entities;
using Microsoft.Xna.Framework;
using SadConsole;

namespace HostileEnvironment.Core.UI
{
    public class MapWindow : Window
    {
        private ScrollingConsole _console;

        public MapWindow(int width, int height, string title) : base(width, height)
        {
            _console = new ScrollingConsole(Program.World.CurrentMap.Width, Program.World.CurrentMap.Height,
                Global.FontDefault, new Rectangle(0, 0, Program.GAME_WIDTH, Program.GAME_HEIGHT), Program.World.CurrentMap.Tiles);

            int _consoleWidth = width;
            int _consoleHeight = height - 1;

            CanDrag = false;
            Title = title.Align(HorizontalAlignment.Center, _consoleWidth);

            _console.ViewPort = new Rectangle(0, 0, _consoleWidth, _consoleHeight);
            _console.Position = new Point(0, 1);

            Children.Add(_console);
            _console.Children.Add(Program.World.Player);

            CenterOnActor(Program.World.Player);

            Show();
        }

        public void CenterOnActor(Actor actor)
        {
            _console.CenterViewPortOnPoint(actor.Position);
        }
    }
}
