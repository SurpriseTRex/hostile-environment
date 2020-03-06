using System;
using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;

namespace HostileEnvironment.Core.UI
{
    public class StatsWindow : Window
    {
        private Console _console;

        public StatsWindow(int width, int height, string title) : base(width, height)
        {
            _console = new Console(width, height, Global.FontDefault);

            int _consoleWidth = width - 1;

            CanDrag = false;
            Title = title.Align(HorizontalAlignment.Center, _consoleWidth);
            _console.Position = new Point(0, 1);

            Children.Add(_console);

            var healthBar = new StatBar("Health", .5f);
            healthBar.Position = new Point(2, 1);
            _console.Children.Add(healthBar);

            var waterBar = new StatBar("Water", .75f);
            waterBar.Position = new Point(2, 4);
            _console.Children.Add(waterBar);

            var foodBar = new StatBar("Food", .15f);
            foodBar.Position = new Point(2, 7);
            _console.Children.Add(foodBar);

            var exertionBar = new StatBar("Exertion", .25f);
            exertionBar.Position = new Point(2, 10);
            _console.Children.Add(exertionBar);

            Show();
        }
    }
}
