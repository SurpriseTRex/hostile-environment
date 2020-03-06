using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;

namespace HostileEnvironment.Core.UI
{
    public class UIManager : ContainerConsole
    {
        public MapWindow MapWindow { get; private set; }
        public MessageWindow MessageLog { get; private set; }
        public StatsWindow StatsWindow { get; private set; }

        private int _statsPanelWidth = 24;
        private int _messagePanelHeight = 6;

        public UIManager()
        {
            IsVisible = true;
            IsFocused = true;

            Parent = Global.CurrentScreen;
        }

        public void Init()
        {
            CreateMapWindow(Program.GAME_WIDTH - _statsPanelWidth, Program.GAME_HEIGHT - _messagePanelHeight, "Map");
            CreateMessageWindow(Program.GAME_WIDTH - _statsPanelWidth, _messagePanelHeight, "Messages");
            CreateStatsWindow(_statsPanelWidth, Program.GAME_HEIGHT, "Stats");

            MessageLog.NewMessage("Testing 123");
            MessageLog.NewMessage("Testing 12_statsPanelWidth");
            MessageLog.NewMessage("Testing 123");
            MessageLog.NewMessage("Testing 12543");
            MessageLog.NewMessage("Testing 123");
            MessageLog.NewMessage("Testing 1253");
            MessageLog.NewMessage("Testing 1212");
            MessageLog.NewMessage("Testing 1");
            MessageLog.NewMessage("Testing");
            MessageLog.NewMessage("Testing 122");
            MessageLog.NewMessage("Testing 51");
            MessageLog.NewMessage("Testing");
            MessageLog.NewMessage("Testing 1_messagePanelHeight2");
            MessageLog.NewMessage("Testing 1_messagePanelHeight");
            MessageLog.NewMessage("Testing Last");
        }

        private void CreateStatsWindow(int width, int height, string title)
        {
            StatsWindow = new StatsWindow(width, height, title);
            StatsWindow.Position = new Point(Program.GAME_WIDTH - _statsPanelWidth, 0);
            Children.Add(StatsWindow);
        }

        public void CreateMapWindow(int width, int height, string title)
        {
            MapWindow = new MapWindow(width, height, title);
            Children.Add(MapWindow);
        }

        public void CreateMessageWindow(int width, int height, string title)
        {
            MessageLog = new MessageWindow(width, height, title);
            MessageLog.Position = new Point(0, Program.GAME_HEIGHT - _messagePanelHeight);
            Children.Add(MessageLog);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            CheckKeyboard();
            base.Update(timeElapsed);
        }

        private void CheckKeyboard()
        {
            if (Global.KeyboardState.IsKeyReleased(Keys.F5))
            {
                Settings.ToggleFullScreen();
            }

            if (Global.KeyboardState.IsKeyPressed(Keys.Up))
            {
                Program.World.Player.MoveBy(new Point(0, -1));
                MapWindow.CenterOnActor(Program.World.Player);
            }

            if (Global.KeyboardState.IsKeyPressed(Keys.Down))
            {
                Program.World.Player.MoveBy(new Point(0, 1));
                MapWindow.CenterOnActor(Program.World.Player);
            }

            if (Global.KeyboardState.IsKeyPressed(Keys.Left))
            {
                Program.World.Player.MoveBy(new Point(-1, 0));
                MapWindow.CenterOnActor(Program.World.Player);
            }

            if (Global.KeyboardState.IsKeyPressed(Keys.Right))
            {
                Program.World.Player.MoveBy(new Point(1, 0));
                MapWindow.CenterOnActor(Program.World.Player);
            }
        }
    }
}
