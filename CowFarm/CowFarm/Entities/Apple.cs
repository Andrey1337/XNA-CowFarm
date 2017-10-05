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
        private World _world;
        private Body _floor;
        public Apple(World world, Rectangle destRect, IDictionary<string, Texture2D> gameTextures) : base(world, destRect, new AnimatedSprites(gameTextures["appleMovement"], 1, 0))
        {
            _world = world;
            Body = BodyFactory.CreateCircle(world, (float)6 / 100, 0.2f, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 0.25f;
            Body.CollisionCategories = Category.Cat10;
            Body.CollidesWith = Category.Cat10;
            _world.ContactManager.Contacted += AppleFloorContacted;
        }

        private void AppleFloorContacted(object sender, CollideEventArg contact)
        {
            if ((Body.BodyId != contact.BodyIdA || _floor.BodyId != contact.BodyIdB) &&
                (Body.BodyId != contact.BodyIdB || _floor.BodyId != contact.BodyIdA)) return;

            _isFalling = false;
            _world.RemoveBody(_floor);
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
        }

        public override void Load(ContentManager content)
        {

        }

        private bool _isFalling;
        public void Fall(float height)
        {
            float x1 = (float)(GetPosition().X) / 100;
            float x2 = (float)(GetPosition().X + 10) / 100;
            float y = (height - 10) / 100;
            _floor = BodyFactory.CreateEdge(_world, new Vector2(x1, y), new Vector2(x2, y));
            _floor.CollisionCategories = Category.Cat10;
            _floor.CollidesWith = Category.Cat10;
            _isFalling = true;
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(_isFalling);
            if (_isFalling)
            {
                Body.ApplyForce(new Vector2(0, 0.015f));
            }
            else
            {
                Body.Stop();
            }
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