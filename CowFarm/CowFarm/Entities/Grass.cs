﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using CowFarm.Worlds;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class Grass : Plant, IEatable
    {
        private const float Delay = 5000f;

        private AnimatedSprites _eatenGrassMovement;
        private readonly Texture2D _reapaintTexture;
        public Grass(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites grassMovement, AnimatedSprites eatenGrassMovement)
            : base(graphics, destRect, grassMovement)
        {
            _eatenGrassMovement = eatenGrassMovement;
            _reapaintTexture = RepaintRectangle(CopyTexture(PlantMovement.Animation));
            CanInteract = true;
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
            SourceRect = new Rectangle(0, 0, PlantMovement.SpriteWidth, PlantMovement.SpriteHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsEaten)
            {
                spriteBatch.Draw(_eatenGrassMovement.Animation, DestRect, SourceRect, Color.White);
            }
            else
            {
                if (OnFocus)
                {
                    spriteBatch.Draw(_reapaintTexture,
                        new Rectangle(DestRect.X - 3, DestRect.Y - 3, DestRect.Width + 6, DestRect.Height + 5),
                        SourceRect, Color.White);
                    spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, new Color(209, 209, 224));
                }
                else
                {

                    spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);

                }
            }
        }

        private Texture2D CopyTexture(Texture2D texture)
        {
            Texture2D copyTexture = new Texture2D(Graphics.GraphicsDevice, 60, PlantMovement.SpriteHeight);
            Color[] oldC = new Color[texture.Width * texture.Height];

            texture.GetData(oldC);
            copyTexture.SetData<Color>(oldC);

            return copyTexture;
        }

        public static Texture2D RepaintRectangle(Texture2D texture)
        {
            Color[] color = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(color);

            for (int i = 0; i < color.Length; i++)
            {
                if (color[i].A > 200)
                {
                    color[i] = Color.White;
                }
                else
                {
                    color[i].A = 0;
                }
            }

            texture.SetData<Color>(color);
            return texture;
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, PlantMovement.SpriteWidth, PlantMovement.Animation.Height);
        }

        public Vector2 GetInteractablePosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height);
        }

        public void Interact()
        {
            IsEaten = true;
            CanInteract = false;
        }

        public bool OnFocus { get; set; }
        public bool IsEaten { get; set; }
        public bool CanInteract { get; set; }
    }
}
