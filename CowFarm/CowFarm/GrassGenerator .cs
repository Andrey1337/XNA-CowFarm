using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
{
    public class GrassGenerator : IGenerator
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly Texture2D _grassMovement;
        private DateTime _grassSpawnTime;

        private readonly Random _random;

        public GrassGenerator(GraphicsDeviceManager graphics, Texture2D grassMovement)
        {
            this._graphics = graphics;
            this._random = new Random();
            this._grassSpawnTime = DateTime.Now.AddSeconds(_random.Next(2, 4));
            this._grassMovement = grassMovement;
        }

        public Entity Generate()
        {
            if (_grassSpawnTime - DateTime.Now >= TimeSpan.FromMilliseconds(2)) return null;

            int x = _random.Next(0, _graphics.PreferredBackBufferWidth - 24);
            int y = _random.Next((int)(_graphics.PreferredBackBufferHeight / 9), _graphics.PreferredBackBufferHeight - _grassMovement.Height);


            _grassSpawnTime = DateTime.Now.AddSeconds(_random.Next(2, 4));
            return new Grass(new Rectangle(x, y, 24, _grassMovement.Height), _grassMovement);
        }

    }
}