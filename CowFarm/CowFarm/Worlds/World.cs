using System;
using System.Collections.Generic;
using CowFarm.Entities;
using CowFarm.Generators;
using CowFarm.ScreenSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CowFarm.Worlds
{
    public abstract class World : FarseerPhysics.Dynamics.World
    {
        protected GraphicsDeviceManager Graphics;
        protected ScreenManager ScreenManager;

        protected List<Entity>[] StaticEntities;
        protected List<Entity> DynamicEntities;

        protected GrassGenerator GrassGenerator;

        protected Dictionary<string, Texture2D> GameTextures;

        public DateTime GameStartedTime { get; set; }
        public TimeSpan TimeInTheGame { get; set; }

        protected World(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures, ScreenManager screenManager, DateTime gameStartedTime)
               : base(Vector2.Zero)
        {
            ScreenManager = screenManager;
            Graphics = graphics;
            GameTextures = gameTextures;

            StaticEntities = new List<Entity>[graphics.PreferredBackBufferHeight];
            DynamicEntities = new List<Entity>();

            TimeInTheGame = new TimeSpan(0);
            GameStartedTime = gameStartedTime;
        }

        public virtual void Load(ContentManager content)
        {

        }

        public void AddDynamicEntity(Entity dynamicEntity)
        {
            DynamicEntities.Add(dynamicEntity);
        }

        public void AddStaticEntity(Entity staticEntity)
        {
            int yPos = staticEntity.GetPosition().Y + staticEntity.GetPosition().Height;

            if (StaticEntities[yPos] == null)
                StaticEntities[yPos] = new List<Entity>() { staticEntity };
            else
                StaticEntities[yPos].Add(staticEntity);
        }


        public virtual void Update(GameTime gameTime)
        {
            GrassGenerator.Generate(StaticEntities, DateTime.Now);
            foreach (var item in StaticEntities)
            {
                item?.ForEach(entity => entity.Update(gameTime));

            }
            DynamicEntities.ForEach(entity => entity.Update(gameTime));

            this.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var dynamicYposition = int.MaxValue;
            Entity dynamicEntity = null;
            var dynamicCount = 0;

            if (DynamicEntities.Count >= 1)
            {
                dynamicEntity = DynamicEntities[dynamicCount];
                dynamicYposition = dynamicEntity.GetPosition().Y + dynamicEntity.GetPosition().Height;
                dynamicCount++;
                if (dynamicYposition >= StaticEntities.Length)
                    dynamicYposition = Graphics.PreferredBackBufferHeight - 1;
            }

            for (var i = 0; i < StaticEntities.Length; i++)
            {
                if (i == dynamicYposition)
                {
                    dynamicEntity?.Draw(gameTime, spriteBatch);
                    dynamicCount++;
                    if (dynamicCount <= DynamicEntities.Count - 1)
                    {
                        dynamicEntity = DynamicEntities[dynamicCount];
                        dynamicYposition = dynamicEntity.GetPosition().Y + dynamicEntity.GetPosition().Height;
                    }

                }
                if (StaticEntities[i] != null)
                {
                    StaticEntities[i].ForEach(entity => entity.Draw(gameTime, spriteBatch));
                }
            }
        }
    }
}
