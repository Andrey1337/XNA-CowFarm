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
        protected List<Entity>[] _staticEntities;
        protected List<Entity> _dynamicEntities;

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            foreach (var item in _staticEntities)
            {
                item?.ForEach(entity => entity.Update(gameTime, graphics));
            }

            _dynamicEntities.ForEach(entity => entity.Update(gameTime, graphics));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Entity cow = _dynamicEntities[0];

            int dynamicYposition = cow.GetPosition().Y + cow.GetPosition().Height;

            for (var i = 0; i < _staticEntities.Length; i++)
            {
                if (i == dynamicYposition)
                {
                    cow.Draw(gameTime, spriteBatch);
                }
                if (_staticEntities[i] != null)
                {
                    _staticEntities[i].ForEach(entity => entity.Draw(gameTime, spriteBatch));
                }
            }



            //var allEntities = _staticEntities.Union(_dynamicEntities).ToList();
            //allEntities.Sort(new EntityYPositionComparer());
            //allEntities.ForEach(entity => entity.Draw(gameTime, spriteBatch));

            //_staticEntities.ForEach(entity => entity.Draw(gameTime,spriteBatch));
            //_dynamicEntities.ForEach(entity => entity.Draw(gameTime,spriteBatch));        
        }



    }
}
