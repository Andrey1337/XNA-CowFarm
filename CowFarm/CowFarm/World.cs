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

        

        public override void Update(GameTime gameTime)
        {
            
            foreach (var item in StaticEntities)
            {
                item?.ForEach(entity => entity.Update(gameTime));
            }

            DynamicEntities.ForEach(entity => entity.Update(gameTime));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Entity cow = DynamicEntities[0];

            int dynamicYposition = cow.GetPosition().Y + cow.GetPosition().Height;

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



            //var allEntities = _staticEntities.Union(_dynamicEntities).ToList();
            //allEntities.Sort(new EntityYPositionComparer());
            //allEntities.ForEach(entity => entity.Draw(gameTime, spriteBatch));                
        }



    }
}
