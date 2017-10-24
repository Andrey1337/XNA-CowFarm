﻿using System.Collections.Generic;
using CowFarm.DrowingSystem;
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
    public class Rocks : Item, IInteractable
    {

        public Rocks(CowGameScreen cowGameScreen, World world, Vector2 position) : base(world, new Rectangle((int)position.X, (int)position.Y, 30, 23), new AnimatedSprites(cowGameScreen.GameTextures["rocksMovement"], 1, 0), cowGameScreen.GameTextures["rocksIcon"])
        {
            Body = BodyFactory.CreateCircle(world, (float)1 / 100, 0f, position);
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
            Body.BodyTypeName = "rocks";
            ItemId = 1;
            world.AddDynamicEntity(this);

            CanInteract = true;
        }

        public override void Update(GameTime gameTime)
        {
            Body.Stop();
            SourceRect = ItemMovement.Animate(gameTime, ObjectMovingType);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ItemMovement.Animation, GetPosition(), SourceRect,
                OnFocus ? new Color(209, 209, 224) : Color.White);
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)DestRect.Width / 2;

            return new Rectangle((int)vector.X, (int)vector.Y, DestRect.Width, DestRect.Height);
        }

        public Vector2 GetInteractablePosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + (float)(GetPosition().Height / 2));
        }

        public bool OnFocus { get; set; }
        public bool CanInteract { get; set; }
        public void Interact()
        {
            Pick();
        }
    }
}