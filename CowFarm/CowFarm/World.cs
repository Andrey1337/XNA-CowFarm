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
        protected List<Entity> _staticEntities;
        protected List<Entity> _dynamicEntities;

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            _staticEntities.ForEach(entity => entity.Update(gameTime, graphics));
            _dynamicEntities.ForEach(entity => entity.Update(gameTime, graphics));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var allEntities = _staticEntities.Union(_dynamicEntities).ToList();
            allEntities.Sort(new EntityYPositionComparer());
            allEntities.ForEach(entity => entity.Draw(gameTime, spriteBatch));

            //_staticEntities.ForEach(entity => entity.Draw(gameTime,spriteBatch));
            //_dynamicEntities.ForEach(entity => entity.Draw(gameTime,spriteBatch));        
        }



    }
}
