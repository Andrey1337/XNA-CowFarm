using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;

namespace CowFarm.Generators
{
    public class TreeGenerator : Generator
    {
        private readonly int _obectsMaxCounter;

        public TreeGenerator(World world, GraphicsDeviceManager graphics, AnimatedSprites textureMovement,
            int objectsMaxCounter) : base(world, graphics, textureMovement, objectsMaxCounter)
        {
            _obectsMaxCounter = objectsMaxCounter;
        }

        public override void Generate(List<Entity>[] staticEntities)
        {
            for (var i = ObjectsCounter; i < Random.Next(1, _obectsMaxCounter); i++)
            {
                var x = Random.Next(Graphics.PreferredBackBufferWidth / 6, (int)(((double)Graphics.PreferredBackBufferWidth - AnimatedSpriteMovement.SpriteWidth) * 0.9));
                var y = Random.Next(Graphics.PreferredBackBufferHeight / 6, Graphics.PreferredBackBufferHeight - AnimatedSpriteMovement.Animation.Height - 50);

                var tree = new Tree(World, Graphics,
                    new Rectangle(x, y, AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.Animation.Height),
                    new AnimatedSprites(AnimatedSpriteMovement.Animation, AnimatedSpriteMovement.Frames,
                        AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.SpaceFromSprites));
                World.AddStaticEntity(tree);
            }
        }
    }
}