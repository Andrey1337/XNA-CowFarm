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

        public HashSet<IInteractable>[,] InteractableEntities;

        protected Dictionary<string, Texture2D> GameTextures;

        public World RightWorld { get; set; }
        public World LeftWorld { get; set; }
        public World UpWorld { get; set; }
        public World DownWorld { get; set; }

        public DateTime GameStartedTime { get; set; }
        public TimeSpan TimeInTheGame { get; set; }

        protected World(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> gameTextures, ScreenManager screenManager, DateTime gameStartedTime)
               : base(Vector2.Zero)
        {
            ScreenManager = screenManager;
            Graphics = graphics;
            GameTextures = gameTextures;

            InteractableEntities =
                new HashSet<IInteractable>[graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight];

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
            if (DynamicEntities == null)
                DynamicEntities = new List<Entity>() { dynamicEntity };
            else
                DynamicEntities.Add(dynamicEntity);
        }

        public void RemoveDynamicEntity(Entity dynamicEntity)
        {
            if (DynamicEntities.Contains(dynamicEntity))
                DynamicEntities.Remove(dynamicEntity);
            else
            {
                throw new Exception("Entity doesent exist in world");
            }
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
            UpdateInteractable();

            foreach (var item in StaticEntities)
            {
                item?.ForEach(entity => entity.Update(gameTime));

            }
            DynamicEntities.ForEach(entity => entity.Update(gameTime));

            this.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
        }

        private void UpdateInteractable()
        {
            for (var i = 0; i < StaticEntities.Length; i++)
            {
                if (StaticEntities[i] == null) continue;
                foreach (var entity in StaticEntities[i])
                {
                    var interactable = entity as IInteractable;

                    if (interactable == null) continue;

                    var position = interactable.GetInteractablePosition();

                    if (InteractableEntities[(int)position.X, (int)position.Y] == null)
                    {
                        InteractableEntities[(int)position.X, (int)position.Y] =
                            new HashSet<IInteractable>() { interactable };
                    }
                    else
                    {
                        if (!InteractableEntities[(int)position.X, (int)position.Y].Contains(interactable))
                            InteractableEntities[(int)position.X, (int)position.Y].Add(interactable);
                    }
                }
            }
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
                    dynamicEntity?.Draw(spriteBatch);
                    dynamicCount++;
                    if (dynamicCount <= DynamicEntities.Count - 1)
                    {
                        dynamicEntity = DynamicEntities[dynamicCount];
                        dynamicYposition = dynamicEntity.GetPosition().Y + dynamicEntity.GetPosition().Height;
                    }

                }
                if (StaticEntities[i] != null)
                {
                    StaticEntities[i].ForEach(entity => entity.Draw(spriteBatch));
                }
            }
        }
    }
}
