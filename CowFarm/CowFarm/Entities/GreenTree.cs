using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public GreenTree(CowGameScreen cowGameScreen, World world, GraphicsDeviceManager graphics, Rectangle destRect, Dictionary<string, Texture2D> gameTextures)
            : base(graphics, destRect, new AnimatedSprites(gameTextures["greenTreeMovement"], 1, 0))
        {
            _world = world;
            _cowGameScreen = cowGameScreen;
            float width = (float)14 / 100;
            float height = (float)1 / 100;

            float x = (float)(destRect.X + destRect.Width - 80) / 100;
            float y = (float)(destRect.Y + destRect.Height - 22) / 100;

            Body = BodyFactory.CreateRectangle(world, width, height, 0f, new Vector2(x, y));
            Body.BodyType = BodyType.Static;
            Body.BodyTypeName = "tree";
            world.ContactManager.Contacted += TreeCollides;

            world.AddStaticEntity(this);
        }


        public void CreateApple()
        {
            Apple = new Apple(_cowGameScreen, _world, this, new Rectangle(DestRect.X + 35, DestRect.Y + 100, 20, 20), _cowGameScreen.GameTextures);
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
        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {

            SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
            Apple?.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if (OnFocus)
            //{
            //    spriteBatch.Draw(_reapaintTexture, new Rectangle(DestRect.X - 3, DestRect.Y - 3, DestRect.Width + 6, DestRect.Height + 5), SourceRect, Color.White);
            //}
            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);

            Apple?.Draw(spriteBatch);
        }

        private Texture2D CopyTexture(Texture2D texture)
        {
            Texture2D copyTexture = new Texture2D(Graphics.GraphicsDevice, 155, PlantMovement.SpriteHeight);
            Color[] oldC = new Color[texture.Width * texture.Height];

            texture.GetData(oldC);
            copyTexture.SetData<Color>(oldC);

            return copyTexture;
        }


        private Texture2D RepaintRectangle(Texture2D texture)
        {
            Color[] color = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(color);

            for (int i = 0; i < color.Length; i++)
            {
                if (color[i].A > 170)
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
            //return new Rectangle(DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height);
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

        public bool OnFocus { get; set; }
    }
}