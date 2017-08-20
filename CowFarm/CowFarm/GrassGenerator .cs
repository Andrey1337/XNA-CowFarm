using System;
using System.Collections.Generic;
using CowFarm.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    public class GrassGenerator : IGenerator
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly Texture2D _grassMovement;
        private DateTime _grassSpawnTime;
        private readonly int _grassWidth;
        private readonly Random _random;

        public GrassGenerator(GraphicsDeviceManager graphics, Texture2D grassMovement, int spriteWidth)
        {
            this._graphics = graphics;
            this._random = new Random();
            this._grassSpawnTime = DateTime.Now.AddSeconds(1);
            this._grassMovement = grassMovement;
            this._grassWidth = spriteWidth;
        }


        public void Generate(World world)
        {
            if (DateTime.Now - _grassSpawnTime < TimeSpan.FromMilliseconds(0)) return;

            var x = _random.Next(0, (int)(((double)_graphics.PreferredBackBufferWidth - _grassWidth) * 0.9));
            var y = _random.Next(_graphics.PreferredBackBufferHeight / 6, _graphics.PreferredBackBufferHeight - _grassMovement.Height);

            var grass = new Grass(_graphics, new Rectangle(x, y, _grassWidth, _grassMovement.Height), new AnimatedSprites(_grassMovement, 2, _grassWidth, 15));

            if (world.StaticEntities[grass.GetPosition().Y + grass.GetPosition().Height] != null)
                world.StaticEntities[grass.GetPosition().Y + grass.GetPosition().Height].Add(grass);
            else
                world.StaticEntities[grass.GetPosition().Y + grass.GetPosition().Height] = new List<Entity>() { grass };

            _grassSpawnTime = _grassSpawnTime.AddSeconds(_random.Next(10, 15));
        }
    }
}