using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.Comparables;
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


            var interactable = staticEntity as IInteractable;
            if (interactable != null)
            {
                var interactablePosition = interactable.GetInteractablePosition();
                if (InteractableEntities[(int)interactablePosition.X, (int)interactablePosition.Y] == null)
                {
                    InteractableEntities[(int)interactablePosition.X, (int)interactablePosition.Y] =
                        new HashSet<IInteractable>() { interactable };
                }
                else
                {
                    InteractableEntities[(int)interactablePosition.X, (int)interactablePosition.Y].Add(interactable);
                }
            }
        }


        public virtual void Update(GameTime gameTime)
        {
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
            if (DynamicEntities.Count > 1)
            {
                DynamicEntities.Sort(new PositionYComparer());
                Debug.WriteLine("SORT");
            }

            int j = 0;

            var dynamicYposition = int.MaxValue;

            if (DynamicEntities.Count > 0)
            {
                dynamicYposition = DynamicEntities[j].GetPosition().Y + DynamicEntities[j].GetPosition().Height;
            }

            for (var i = 0; i < StaticEntities.Length; i++)
            {
                while (i == dynamicYposition)
                {

                    if (i != dynamicYposition) break;
                    DynamicEntities[j].Draw(spriteBatch);
                    j++;
                    if (j < DynamicEntities.Count)
                        dynamicYposition = DynamicEntities[j].GetPosition().Y + DynamicEntities[j].GetPosition().Height;
                    else
                    {
                        dynamicYposition = int.MaxValue;
                    }
                }


                if (StaticEntities[i] != null)
                {
                    StaticEntities[i].ForEach(entity => entity.Draw(spriteBatch));
                }
            }

            while (j < DynamicEntities.Count)
            {
                DynamicEntities[j].Draw(spriteBatch);
                j++;
            }
        }
    }
}
