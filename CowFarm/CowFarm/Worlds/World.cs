using System;
using System.Collections.Generic;
using CowFarm.Entities;
using CowFarm.Generators;
using CowFarm.ScreenSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CowFarm.Worlds
{
    public abstract class World : Entity
    {
        public List<Entity>[] StaticEntities;
        protected List<Entity> DynamicEntities;
        protected GraphicsDeviceManager Graphics;
        protected Dictionary<string, Texture2D> GameTextures;
        protected GrassGenerator GrassGenerator;

        public DateTime GameStartedTime { get; set; }
        public TimeSpan PlayTime { get; set; }

        protected ScreenManager ScreenManager;

        protected World(GraphicsDeviceManager graphics, List<Entity> dynamicEntities,
            Dictionary<string, Texture2D> gameTextures,
            ScreenManager screenManager, DateTime gameStartedTime)
        {
            ScreenManager = screenManager;
            Graphics = graphics;
            GameTextures = gameTextures;

            StaticEntities = new List<Entity>[graphics.PreferredBackBufferHeight];
            DynamicEntities = dynamicEntities;


            PlayTime = new TimeSpan(0);
            GameStartedTime = gameStartedTime;
        }   
         
        public override void Update(GameTime gameTime)
        {
            GrassGenerator.Generate(StaticEntities, DateTime.Now);
            foreach (var item in StaticEntities)
            {
                item?.ForEach(entity => entity.Update(gameTime));
            }

            DynamicEntities.ForEach(entity => entity.Update(gameTime));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
