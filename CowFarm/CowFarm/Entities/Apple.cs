using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Apple : Decoration, IEatable
    {
        public Apple(World world, Rectangle destRect, IDictionary<string, Texture2D> gameTextures) : base(world, destRect, new AnimatedSprites(gameTextures["appleMovement"], 1, 0))
        {
            Body = BodyFactory.CreateCircle(world, (float)1 / 100, 0.2f, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Debug.WriteLine(Body.Position);
            Body.CollisionCategories = Category.Cat10;
            Body.CollidesWith = Category.Cat10;
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Body.Stop();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DecorationMovement.Animation, GetPosition(), Color.White);
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)DestRect.Width / 2;
            //vector.Y -= (float)DestRect.Width / 2;

            return new Rectangle((int)vector.X, (int)vector.Y, DestRect.Height, DestRect.Width);
        }

        public bool OnFocus { get; set; }
        public bool CanInteract { get; set; }
        public Vector2 GetInteractablePosition()
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {

        }

        public bool IsEaten { get; set; }
    }
}