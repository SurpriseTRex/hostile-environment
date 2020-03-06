using System;
using HostileEnvironment.Core.Colors;
using HostileEnvironment.Core.Map;
using HostileEnvironment.Core.UI;
using Microsoft.Xna.Framework;
using SadConsole.Themes;
using Colors = SadConsole.Themes.Colors;
using Game = SadConsole.Game;

namespace HostileEnvironment
{
    public static class Program
    {
        public const int GAME_WIDTH = 80, GAME_HEIGHT = 25;

        public static UIManager UIManager { get; set; }
        public static World World { get; set; }

        [STAThread]
        static void Main()
        {
            SetupTheme();

            Game.Create(GAME_WIDTH, GAME_HEIGHT);

            Game.OnInitialize = Init;
            Game.OnUpdate = Update;

            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void SetupTheme()
        {
            WindowTheme windowTheme = new WindowTheme(new Colors());
            Library.Default.WindowTheme = windowTheme;

            Library.Default.Colors.ControlHostBack = Palette.Background;
            Library.Default.Colors.ControlHostFore = Palette.Text;
            Library.Default.Colors.TitleText = Palette.Text;
        }

        private static void Init()
        {
            UIManager = new UIManager();
            World = new World();

            UIManager.Init();
        }

        private static void Update(GameTime time)
        {

        }
    }
}
