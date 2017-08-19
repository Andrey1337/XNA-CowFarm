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
        private int _grassWidth = 24;
        private readonly Random _random;

        public GrassGenerator(GraphicsDeviceManager graphics, Texture2D grassMovement)
        {
            this._graphics = graphics;
            this._random = new Random();
            this._grassSpawnTime = DateTime.Now.AddSeconds(_random.Next(2, 4));
            this._grassMovement = grassMovement;
        }
        

        public void Generate(World world)
        {
            if (DateTime.Now - _grassSpawnTime >= TimeSpan.FromMilliseconds(0))
            {
                int x = _random.Next(0, _graphics.PreferredBackBufferWidth - _grassWidth);
                int y = _random.Next(0, _graphics.PreferredBackBufferHeight - _grassMovement.Height);

                Grass grass = new Grass(new Rectangle(x, y, _grassWidth, _grassMovement.Height), _grassMovement);

                if (world.StaticEntities[grass.GetPosition().Y + grass.GetPosition().Height] != null)
                    world.StaticEntities[grass.GetPosition().Y + grass.GetPosition().Height].Add(grass);
                else
                    world.StaticEntities[grass.GetPosition().Y + grass.GetPosition().Height] = new List<Entity>() { grass };
                _grassSpawnTime = _grassSpawnTime.AddSeconds(_random.Next(1, 2));
            }
        }
    }
}