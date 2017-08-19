using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Animal : Entity
    {
        protected readonly Texture2D RightWalk;
        protected readonly Texture2D LeftWalk;
        protected readonly Texture2D DownWalk;
        protected readonly Texture2D UpWalk;
        protected Texture2D CurrentAnim;
        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics;

        protected Animal(GraphicsDeviceManager graphics, Rectangle destRect, Texture2D currentAnim, Texture2D rightWalk,
            Texture2D leftWalk, Texture2D downWalk, Texture2D upWalk)
        {
            this.CurrentAnim = currentAnim;
            this.DestRect = destRect;
            this.RightWalk = rightWalk;
            this.LeftWalk = leftWalk;
            this.DownWalk = downWalk;
            this.UpWalk = upWalk;
            this.Graphics = graphics;
        }

        public abstract void Eat();
    }
}
