using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    public interface IEntity
    {
        void Load(ContentManager content);
        void Update(GameTime gameTime, GraphicsDeviceManager graphics);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
