﻿using System;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public abstract class Entity
    {
        public Body Body { get; protected set; }

        protected CowGameScreen CowGameScreen;
        protected Entity(CowGameScreen cowGameScreen)
        {
            CowGameScreen = cowGameScreen;
        }
        public int BodyId => Body.BodyId;
        public string BodyTypeName => Body.BodyTypeName;
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract Rectangle GetPosition();
    }
}
