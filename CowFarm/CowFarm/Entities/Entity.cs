using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Entity
    {
        public Body Body { get; protected set; }
        public int BodyId => Body.BodyId;
        public string BodyTypeName => Body.BodyTypeName;
        public abstract void Load(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract Rectangle GetPosition();       
    }
}
