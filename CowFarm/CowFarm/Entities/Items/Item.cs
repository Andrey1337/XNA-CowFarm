using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Enums;
using CowFarm.Interfaces;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public abstract class Item : Entity, IDynamic
    {
        protected AnimatedSprites ItemMovement;
        public int ItemId { get; protected set; }
        public Texture2D IconTexture { get; }
        protected Rectangle DestRect;
        protected GraphicsDeviceManager Graphics => CurrentWorld.Graphics;
        protected Rectangle SourceRect;
        public World CurrentWorld { get; set; }
        protected ObjectMovingType ObjectMovingType;

        public int StackCount { get; protected set ;}

        protected Item(World world, Rectangle destRect, AnimatedSprites itemMovement, Texture2D iconTexture)
        {
            IconTexture = iconTexture;
            CurrentWorld = world;
            DestRect = destRect;
            ItemMovement = itemMovement;
            ObjectMovingType = ObjectMovingType.Static;

        }

        public virtual void Pick()
        {
            CurrentWorld.RemoveDynamicEntity(this);
        }


        public void ChangeWorld(World world, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    Body = BodyFactory.CreateCircle(world, (float)1 / 100, 0.2f, new Vector2(0.25f, Body.Position.Y));
                    break;

                case Direction.Left:
                    Body = BodyFactory.CreateCircle(world, (float)1 / 100, 0.2f, new Vector2((float)(Graphics.PreferredBackBufferWidth - 25) / 100, Body.Position.Y));
                    break;
            }

            Body.BodyTypeName = "apple";
            CurrentWorld = world;
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
        }
    }
}