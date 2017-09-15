using System;
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

        private readonly Texture2D _reapaintTexture;
        public Grass(GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites grassMovement)
            : base(graphics, destRect, grassMovement)
        {

            _reapaintTexture = RepaintRectangle(CopyTexture(PlantMovement.Animation));
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (OnFocus)
            {
                spriteBatch.Draw(_reapaintTexture, new Rectangle(DestRect.X - 1, DestRect.Y - 2, DestRect.Width + 2, DestRect.Height + 3), SourceRect, Color.White);
            }

            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);
        }

        private Texture2D CopyTexture(Texture2D texture)
        {
            Texture2D copyTexture = new Texture2D(Graphics.GraphicsDevice, 60, PlantMovement.SpriteHeight);
            Color[] oldC = new Color[texture.Width * texture.Height];

            texture.GetData(oldC);
            copyTexture.SetData<Color>(oldC);

            return copyTexture;
        }

        private Texture2D RepaintRectangle(Texture2D texture)
        {
            Color[] oldC = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(oldC);
            Color[,] colorArray = new Color[texture.Height, texture.Width];

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    colorArray[y, x] = oldC[y * texture.Width + x];
                }
            }

            oldC = new Color[texture.Width * texture.Height];
            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    if (colorArray[y, x] == Color.White * 0)
                        oldC[y * texture.Width + x] = Color.White * 0;
                    else
                        oldC[y * texture.Width + x] = Color.White;
                }
            }


            texture.SetData<Color>(oldC);
            return texture;
        }



        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, PlantMovement.SpriteWidth, PlantMovement.Animation.Height);
        }

        public bool OnFocus { get; set; }
        public bool IsEaten { get; set; }
    }
}
