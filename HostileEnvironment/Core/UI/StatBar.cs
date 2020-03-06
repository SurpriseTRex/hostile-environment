using System;
using SadConsole;
using Console = SadConsole.Console;
using Palette = HostileEnvironment.Core.Colors.Palette;

namespace HostileEnvironment.Core.UI
{
    class StatBar : Console
    {
        public StatBar(string name, float percent, int w = 20, int h = 2)
            : base(w, h)
        {
            Print(0, 0, name.Align(HorizontalAlignment.Center, Width), Palette.Text, Palette.TextBG);

            Fill(0, 1, w, Palette.BarBG, Palette.BarBG, ' ');
            Fill(0, 1, (int)(w * percent), Palette.BarFill, Palette.BarFill, ' ');
        }
    }
}
