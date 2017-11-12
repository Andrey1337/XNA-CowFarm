using System;
using CowFarm.DrowingSystem;
using CowFarm.Entities.Items;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Plants
{
    public class Grass : Plant, IInteractable
    {
        private const float Delay = 5000f;

        private AnimatedSprites _currentAnim;

        private readonly AnimatedSprites _eatenGrassMovement;

        public Grass(CowGameScreen cowGameScreen, World world, Vector2 position)
            : base(cowGameScreen, new Rectangle((int)position.X, (int)position.Y, 25, 51), new AnimatedSprites(cowGameScreen.GameTextures["grassMovement"], 1, 0))
        {
            _currentAnim = PlantMovement;
            _eatenGrassMovement = new AnimatedSprites(cowGameScreen.GameTextures["eatenGrassMovement"], 1, 0);

            CurrentWorld = world;

            Body = BodyFactory.CreateRectangle(world, (float)DestRect.Width / 100, (float)DestRect.Height / 200, 0, new Vector2((float)(DestRect.X + DestRect.Width / 2) / 100, (float)(DestRect.Y + 30) / 100));

            Body.BodyTypeName = "grass";
            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.Cat10;
            Body.CollidesWith = Category.Cat10;

            ReapaintTexture = TextureHelper.RepaintRectangle(TextureHelper.CopyTexture(PlantMovement.Animation, Graphics));
            CanInteract = true;
            world.AddStaticEntity(this);
            _eatedTime = TimeSpan.Zero;
        }


        public override void Update(GameTime gameTime)
        {
            if (!CanInteract)
            {
                _eatedTime += gameTime.ElapsedGameTime;
                if (_eatedTime > TimeSpan.FromSeconds(new Random().Next(40, 50)))
                {
                    CanInteract = true;
                    _currentAnim = PlantMovement;
                    _eatedTime = TimeSpan.Zero;
                }
            }
            if (!CanInteract)
                _currentAnim = _eatenGrassMovement;

            SourceRect = _currentAnim.Animate(gameTime, ObjectMovingType, Delay);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (OnFocus)
            {
                //spriteBatch.Draw(ReapaintTexture, new Rectangle(DestRect.X - 3, DestRect.Y - 4, DestRect.Width + 6, DestRect.Height + 6), SourceRect, Color.White);
                spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, new Color(209, 209, 224));
            }
            else
            {
                spriteBatch.Draw(_currentAnim.Animation, DestRect, SourceRect, Color.White);
            }
        }

        public override Rectangle GetPosition()
        {
            return DestRect;
        }

        public Vector2 GetInteractablePosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height);
        }

        private TimeSpan _eatedTime;

        public void Interact()
        {
            CanInteract = false;
            new CutGrass(CowGameScreen).Drop(CurrentWorld, new Vector2(GetPosition().X + GetPosition().Width / 2f, GetPosition().Y + GetPosition().Height / 1.6f));
        }
        public Texture2D ReapaintTexture { get; set; }
        public bool OnFocus { get; set; }
        public bool CanInteract { get; set; }
    }
}
