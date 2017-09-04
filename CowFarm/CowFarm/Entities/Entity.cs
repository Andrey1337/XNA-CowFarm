using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Entity : Body
    {
        protected Entity(World world, Vector2? position = null, float rotation = 0, object userdata = null) : base(world, position, rotation, userdata)
        {            
        }
        public abstract void Load(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract Rectangle GetPosition();


    }
}
