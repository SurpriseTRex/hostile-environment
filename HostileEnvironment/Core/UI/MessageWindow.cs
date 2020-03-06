using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;
using Palette = HostileEnvironment.Core.Colors.Palette;

namespace HostileEnvironment.Core.UI
{
    public class MessageWindow : Window
    {
        private static readonly int _maxLines = 100;
        private readonly Queue<string> _lines;

        private ScrollingConsole _messageConsole;

        public MessageWindow(int width, int height, string title) : base(width, height)
        {
            _lines = new Queue<string>();
            Title = title.Align(HorizontalAlignment.Center, Width);

            _messageConsole = new ScrollingConsole(width, _maxLines);

            _messageConsole.Position = new Point(0, 0);
            _messageConsole.ViewPort = new Rectangle(0, 0, width, height);
            
            Children.Add(_messageConsole);
            
            Show();
        }

        public void NewMessage(string message)
        {
            _lines.Enqueue(message);
            
            if (_lines.Count > _maxLines)
            {
                _lines.Dequeue();
            }
            
            _messageConsole.Cursor.Position = new Point(1, _lines.Count);
            _messageConsole.Cursor.PrintAppearance = new Cell(Palette.Text, Palette.TextBG);
            _messageConsole.Cursor.Print(message + "\n");
        }

        public override void Update(TimeSpan time)
        {
            base.Update(time);
        }

        public override void Draw(TimeSpan drawTime)
        {
            base.Draw(drawTime);
        }
    }
}
