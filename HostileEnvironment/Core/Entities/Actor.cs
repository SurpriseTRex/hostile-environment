using Microsoft.Xna.Framework;
using SadConsole.Entities;

namespace HostileEnvironment.Core.Entities
{
    public abstract class Actor : Entity
    {
        protected Actor(Color foreground, Color background, int glyph, int width = 1, int height = 1) : base(width, height)
        {
            Animation.CurrentFrame[0].Foreground = foreground;
            Animation.CurrentFrame[0].Background = background;
            Animation.CurrentFrame[0].Glyph = glyph;
        }

        public bool MoveBy(Point positionChange)
        {
            if (!Program.World.CurrentMap.IsTileWalkable(Position + positionChange))
                return false;

            Position += positionChange;
            return true;

        }

        public bool MoveTo(Point newPosition)
        {
            Position = newPosition;
            return true;
        }
    }
}
