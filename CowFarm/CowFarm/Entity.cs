using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    public abstract class Entity
    {
        public abstract void Load(ContentManager content);
        public abstract void Update(GameTime gameTime, GraphicsDeviceManager graphics);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract Rectangle GetPosition();

        internal void ForEach(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
