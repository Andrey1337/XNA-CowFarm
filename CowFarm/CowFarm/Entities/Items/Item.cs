using CowFarm.DrowingSystem;
using CowFarm.Enums;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Items
{
    public abstract class Item : Entity
    {
        protected AnimatedSprites ItemMovement;
        public int ItemId { get; protected set; }
        public Texture2D IconTexture { get; }
        protected Rectangle DestRect;
        protected Rectangle SourceRect;
        public World CurrentWorld { get; set; }
        protected ObjectMovingType ObjectMovingType;

        public int StackCount { get; protected set; }

        protected Item(CowGameScreen cowGameScreen, AnimatedSprites itemMovement, Texture2D iconTexture) : base(cowGameScreen)
        {
            IconTexture = iconTexture;
            ItemMovement = itemMovement;
            ObjectMovingType = ObjectMovingType.Static;
        }

        public abstract void Drop(World world, Vector2 position);


        public virtual void Pick()
        {
            CurrentWorld?.RemoveDynamicEntity(this);
        }
    }
}