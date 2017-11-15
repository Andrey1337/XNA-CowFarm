using CowFarm.DrowingSystem;
using CowFarm.Enums;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using FarseerPhysics;
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
        protected ObjectMovingType ObjectMovingType;

        public int StackCount { get; protected set; }

        protected Item(CowGameScreen cowGameScreen, AnimatedSprites itemMovement, Texture2D iconTexture) : base(cowGameScreen, null)
        {
            IconTexture = iconTexture;
            ItemMovement = itemMovement;
            ObjectMovingType = ObjectMovingType.Static;
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)DestRect.Width / 2;


            return new Rectangle((int)vector.X, (int)vector.Y, DestRect.Width, DestRect.Height);
        }
        public abstract void Drop(World world, Vector2 position);

        public virtual void Pick()
        {
            CurrentWorld?.RemoveDynamicEntity(this);
        }
    }
}