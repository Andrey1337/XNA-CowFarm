using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Item : Entity
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


       
    }
}