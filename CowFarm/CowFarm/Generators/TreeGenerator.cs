using System;
using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Entities;
using Microsoft.Xna.Framework;

namespace CowFarm.Generators
{
    public class TreeGenerator : Generator
    {
        public TreeGenerator(GraphicsDeviceManager graphics, AnimatedSprites textureMovement,
            int objectsMaxCounter) : base(graphics, textureMovement, objectsMaxCounter) { }

        public override void Generate(List<Entity>[] statiEntities)
        {
            for (var i = ObjectsCounter; i < Random.Next(1, 3); i++)
            {
                var x = Random.Next(Graphics.PreferredBackBufferWidth / 6, (int)(((double)Graphics.PreferredBackBufferWidth - AnimatedSpriteMovement.SpriteWidth) * 0.9));
                var y = Random.Next(Graphics.PreferredBackBufferHeight / 6, Graphics.PreferredBackBufferHeight - AnimatedSpriteMovement.Animation.Height);

                var tree = new Tree(Graphics,
                    new Rectangle(x, y, AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.Animation.Height),
                    new AnimatedSprites(AnimatedSpriteMovement.Animation, AnimatedSpriteMovement.Frames,
                        AnimatedSpriteMovement.SpriteWidth, AnimatedSpriteMovement.SpaceFromSprites));
                if (statiEntities[tree.GetPosition().Y + tree.GetPosition().Height] == null)
                {
                    statiEntities[tree.GetPosition().Y + tree.GetPosition().Height] = new List<Entity>() { tree };
                }
                else
                {
                    statiEntities[tree.GetPosition().Y + tree.GetPosition().Height].Add(tree);
                }
            }
        }
    }
}