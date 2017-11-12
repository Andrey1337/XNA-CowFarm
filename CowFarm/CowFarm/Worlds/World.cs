using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.Comparables;
using CowFarm.Entities;

using CowFarm.Generators;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CowFarm.Worlds
{
    public abstract class World : FarseerPhysics.Dynamics.World
    {
        public Dictionary<int, Entity> InteractablesDictionary { get; }
        public Dictionary<int, Entity> AttackableDictionary { get; }

        protected List<Entity>[] StaticEntities;
        protected List<Entity> DynamicEntities { get; private set; }

        public World RightWorld { get; set; }
        public World LeftWorld { get; set; }
        public World UpWorld { get; set; }
        public World DownWorld { get; set; }

        protected CowGameScreen CowGameScreen;
        protected World(CowGameScreen cowGameScreen)
               : base(Vector2.Zero)
        {
            InteractablesDictionary = new Dictionary<int, Entity>();
            AttackableDictionary = new Dictionary<int, Entity>();
            CowGameScreen = cowGameScreen;
            StaticEntities = new List<Entity>[cowGameScreen.Graphics.PreferredBackBufferHeight];
            DynamicEntities = new List<Entity>();
        }


        public virtual void Load(ContentManager content)
        {
            //TODO: fix
        }

        public void AddDynamicEntity(Entity dynamicEntity)
        {
            if (DynamicEntities == null)
                DynamicEntities = new List<Entity>() { dynamicEntity };
            else
                DynamicEntities.Add(dynamicEntity);

            if (dynamicEntity is IInteractable)
            {
                InteractablesDictionary.Add(dynamicEntity.BodyId, dynamicEntity);
            }
            if (dynamicEntity is IAttackable)
            {
                AttackableDictionary.Add(dynamicEntity.BodyId, dynamicEntity);
            }
        }

        public void RemoveDynamicEntity(Entity dynamicEntity)
        {
            if (DynamicEntities.Contains(dynamicEntity))
            {
                DynamicEntities.Remove(dynamicEntity);

                RemoveBody(dynamicEntity.Body);

                if (dynamicEntity is IInteractable && InteractablesDictionary.ContainsKey(dynamicEntity.BodyId))
                    InteractablesDictionary.Remove(dynamicEntity.BodyId);
            }
        }

        public void AddStaticEntity(Entity staticEntity)
        {
            int yPos = staticEntity.GetPosition().Y + staticEntity.GetPosition().Height;

            if (StaticEntities[yPos] == null)
                StaticEntities[yPos] = new List<Entity>() { staticEntity };
            else
                StaticEntities[yPos].Add(staticEntity);

            if (staticEntity is IInteractable)
            {
                InteractablesDictionary.Add(staticEntity.BodyId, staticEntity);
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (DynamicEntities.Count > 1)
            {
                DynamicEntities.Sort(new PositionYComparer());
            }

            int j = 0;
            var dynamicYposition = int.MaxValue;

            if (DynamicEntities.Count > 0)
            {
                dynamicYposition = DynamicEntities[j].GetPosition().Y + DynamicEntities[j].GetPosition().Height;
            }

            while (dynamicYposition < 0)
            {
                DynamicEntities[j].Draw(spriteBatch);
                j++;
                if (j < DynamicEntities.Count)
                    dynamicYposition = DynamicEntities[j].GetPosition().Y + DynamicEntities[j].GetPosition().Height;
                else
                    dynamicYposition = int.MaxValue;
            }

            for (var i = 0; i < StaticEntities.Length; i++)
            {
                if (StaticEntities[i] != null)
                    StaticEntities[i].ForEach(entity => entity.Draw(spriteBatch));

                while (i == dynamicYposition)
                {
                    DynamicEntities[j].Draw(spriteBatch);
                    j++;

                    if (j < DynamicEntities.Count)
                        dynamicYposition = DynamicEntities[j].GetPosition().Y + DynamicEntities[j].GetPosition().Height;
                    else
                        dynamicYposition = int.MaxValue;
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
