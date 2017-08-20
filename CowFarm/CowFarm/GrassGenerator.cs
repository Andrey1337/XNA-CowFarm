using System;
using System.Collections.Generic;
using CowFarm.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    public class GrassGenerator : Generator
    {
        private DateTime _grassSpawnTime;
        public GrassGenerator(GraphicsDeviceManager graphics, AnimatedSprites animatedSprites, int objectsMaxCounter) :
            base(graphics, animatedSprites, objectsMaxCounter)
        {
            this._grassSpawnTime = DateTime.Now.AddSeconds(1);
        }

        public override void Generate(List<Entity>[] statiEntities)
        {
            if (DateTime.Now - _grassSpawnTime < TimeSpan.FromMilliseconds(0) || ObjectsCounter > ObjectsMaxCounter) return;

            var x = Random.Next(0, (int)(((double)Graphics.PreferredBackBufferWidth - AnimatedSpriteMovement.SpriteWidth) * 0.9));
            var y = Random.Next(Graphics.PreferredBackBufferHeight / 6, Graphics.PreferredBackBufferHeight - AnimatedSpriteMovement.Animation.Height);

            var grass = new Grass(Graphics, new Rectangle(x, y, AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.Animation.Height),
                new AnimatedSprites(AnimatedSpriteMovement.Animation, AnimatedSpriteMovement.Frames, AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.SpaceFromSprites));

            if (statiEntities[grass.GetPosition().Y + grass.GetPosition().Height] != null)
                statiEntities[grass.GetPosition().Y + grass.GetPosition().Height].Add(grass);
            else
                statiEntities[grass.GetPosition().Y + grass.GetPosition().Height] = new List<Entity>() { grass };

            ObjectsCounter++;
            _grassSpawnTime = _grassSpawnTime.AddSeconds(Random.Next(10, 15));
        }
    }
}