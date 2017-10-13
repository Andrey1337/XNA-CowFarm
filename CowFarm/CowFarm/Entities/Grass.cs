using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CowFarm.DrowingSystem;
using CowFarm.Utility;
using CowFarm.Worlds;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Grass : Plant, IEatable
    {
        private const float Delay = 5000f;
        private const float ButtonDelay = 1000f;

        private AnimatedSprites _currentAnim;
        private readonly AnimatedSprites _eBuutonAnim;
        private Rectangle _buttonSourceRectangle;

        private readonly AnimatedSprites _eatenGrassMovement;

        public Grass(GraphicsDeviceManager graphics, World world, Rectangle destRect, Dictionary<string, Texture2D> gameTextures)
            : base(graphics, destRect, new AnimatedSprites(gameTextures["grassMovement"], 1, 0))
        {
            _currentAnim = PlantMovement;
            _eatenGrassMovement = new AnimatedSprites(gameTextures["eatenGrassMovement"], 1, 0);
            _eBuutonAnim = new AnimatedSprites(gameTextures["eButtonMovement"], 2, 0);

            Body = BodyFactory.CreateRectangle(world, (float)destRect.Width / 100, (float)destRect.Height / 200, 0,
                new Vector2((float)(destRect.X + destRect.Width / 2) / 100, (float)(destRect.Y + 30) / 100));
            Body.BodyTypeName = "grass";
            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.Cat10;
            Body.CollidesWith = Category.Cat10;

            ReapaintTexture = TextureHelper.RepaintRectangle(TextureHelper.CopyTexture(PlantMovement.Animation, Graphics));
            CanInteract = true;
            world.AddStaticEntity(this);
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (IsEaten)
                _currentAnim = _eatenGrassMovement;
            if (OnFocus)
            {
                _buttonSourceRectangle = _eBuutonAnim.Animate(gameTime, ButtonDelay, ObjectMovingType.Static);
            }

            SourceRect = _currentAnim.Animate(gameTime, Delay, ObjectMovingType);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (OnFocus)
            {
                spriteBatch.Draw(ReapaintTexture, new Rectangle(DestRect.X - 3, DestRect.Y - 4, DestRect.Width + 6, DestRect.Height + 6), SourceRect, Color.White);
                spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, new Color(209, 209, 224));
            }
            else
            {
                spriteBatch.Draw(_currentAnim.Animation, DestRect, SourceRect, Color.White);
            }

        }


        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height);
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

        public Texture2D ReapaintTexture { get; set; }
        public bool OnFocus { get; set; }
        public bool IsEaten { get; set; }
        public bool CanInteract { get; set; }
    }
}
