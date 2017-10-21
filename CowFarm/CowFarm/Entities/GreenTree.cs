using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class GreenTree : Plant, IInteractable
    {
        private const float Delay = float.MaxValue;
        private readonly CowGameScreen _cowGameScreen;
        public Apple Apple { get; set; }
        private bool _hasApple;
        private readonly World _world;

        public GreenTree(CowGameScreen cowGameScreen, World world, GraphicsDeviceManager graphics, Vector2 position, IDictionary<string, Texture2D> gameTextures)
            : base(graphics, new Rectangle((int)position.X, (int)position.Y, 155, 261), new AnimatedSprites(gameTextures["greenTreeMovement"], 1, 0))
        {
            _world = world;
            _cowGameScreen = cowGameScreen;
            float width = (float)14 / 100;
            float height = (float)1 / 100;

            float x = (float)(DestRect.X + DestRect.Width - 80) / 100;
            float y = (float)(DestRect.Y + DestRect.Height - 22) / 100;

            Body = BodyFactory.CreateRectangle(world, width, height, 0f, new Vector2(x, y));
            Body.BodyType = BodyType.Static;
            Body.BodyTypeName = "tree";
            world.ContactManager.Contacted += TreeCollides;

            world.AddStaticEntity(this);
        }

        public void CreateApple()
        {
            Apple = new Apple(_cowGameScreen, _world, this, new Rectangle(DestRect.X + 35, DestRect.Y + 100, 20, 20));
            _hasApple = true;
        }

        private void TreeCollides(object sender, CollideEventArg contact)
        {
            if (!_hasApple || !contact.Dictionary.ContainsKey(BodyId)) return;
            {
                Apple.Fall(DestRect.Y + DestRect.Height);

                _hasApple = false;
            }
        }
        

        public override void Update(GameTime gameTime)
        {
            SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
            Apple?.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);
            Apple?.Draw(spriteBatch);
        }

        public override Rectangle GetPosition()
        {
            return DestRect;
        }

        public bool CanInteract { get; set; }

        public Vector2 GetInteractablePosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height);
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }

        public Texture2D ReapaintTexture { get; set; }
        public bool OnFocus { get; set; }
    }
}