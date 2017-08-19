using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{

    public abstract class World : Entity
    {
        public List<Entity>[] StaticEntities;
        protected List<Entity> DynamicEntities;
        protected GraphicsDeviceManager Graphics;
        protected Dictionary<string, Texture2D> GameTextures;

        protected GrassGenerator GrassGenerator;

        public override void Update(GameTime gameTime)
        {
            foreach (var item in StaticEntities)
            {
                item?.ForEach(entity => entity.Update(gameTime));
            }
            GrassGenerator.Generate(this);

            DynamicEntities.ForEach(entity => entity.Update(gameTime));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Entity cow = DynamicEntities[0];

            int dynamicYposition = cow.GetPosition().Y + cow.GetPosition().Height;

            if (dynamicYposition >= StaticEntities.Length)
                dynamicYposition = Graphics.PreferredBackBufferHeight - 1;

            for (var i = 0; i < StaticEntities.Length; i++)
            {
                if (i == dynamicYposition)
                {
                    cow.Draw(gameTime, spriteBatch);
                }
                if (StaticEntities[i] != null)
                {
                    StaticEntities[i].ForEach(entity => entity.Draw(gameTime, spriteBatch));
                }
            }
        }



    }
}
