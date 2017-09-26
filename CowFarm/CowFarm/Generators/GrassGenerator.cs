using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;

namespace CowFarm.Generators
{
    public class GrassGenerator : Generator
    {
        private DateTime _grassSpawnTime;
        public GrassGenerator(World world, GraphicsDeviceManager graphics, AnimatedSprites animatedSprites, int objectsMaxCounter, DateTime gameStartedTime) :
            base(world, graphics, animatedSprites, objectsMaxCounter)
        {
            _grassSpawnTime = gameStartedTime.AddSeconds(2);
        }

        public void Generate(List<Entity>[] statiEntities, DateTime currentTime)
        {
            if (_grassSpawnTime - currentTime >= TimeSpan.FromMilliseconds(0) ||
                ObjectsCounter > ObjectsMaxCounter) return;

            var x = Random.Next(0,
                (int)(((double)Graphics.PreferredBackBufferWidth - AnimatedSpriteMovement.SpriteWidth) * 0.9));
            var y = Random.Next(Graphics.PreferredBackBufferHeight / 6,
                Graphics.PreferredBackBufferHeight - AnimatedSpriteMovement.Animation.Height);

            //var grass = new Grass(Graphics,
            //    new Rectangle(x, y, AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.Animation.Height),
            //    new AnimatedSprites(AnimatedSpriteMovement.Animation, AnimatedSpriteMovement.Frames,
            //        AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.SpaceFromSprites));

            //World.AddStaticEntity(grass);

            ObjectsCounter++;
            _grassSpawnTime = _grassSpawnTime.AddSeconds(Random.Next(10, 15));
        }
    }
}