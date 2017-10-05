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
        private GreenTree _tree;

        private Vector2 origin;
        
        public Apple(World world, GreenTree tree, Rectangle destRect, IDictionary<string, Texture2D> gameTextures) : base(world, destRect, new AnimatedSprites(gameTextures["appleMovement"], 1, 0))
        {
            origin.X = DecorationMovement.Animation.Width / 2;
            origin.Y = DecorationMovement.Animation.Height / 2;            

            _world = world;
            _tree = tree;
            Body = BodyFactory.CreateCircle(world, (float)1 / 100, 0.2f, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Body.Restitution = 0.26f;
            Body.CollisionCategories = Category.Cat10;
            Body.CollidesWith = Category.Cat10;
            _world.ContactManager.Contacted += AppleFloorContacted;
        }

        private void AppleFloorContacted(object sender, CollideEventArg contact)
        {
            //Debug.WriteLine(Body.GetVelocity());

            if (Body.BodyId == contact.BodyIdB && _floor.BodyId == contact.BodyIdA || Body.BodyId == contact.BodyIdA && _floor.BodyId == contact.BodyIdB)
            {
                Body.Restitution = 0f;
                _isFalling = false;
                _world.RemoveBody(_floor);
                Body.CollisionCategories = Category.All;
                Body.CollidesWith = Category.All;
                _tree.Apple = null;
                _world.AddDynamicEntity(this);
            }
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
            Debug.WriteLine(_floor.BodyId);
        }

        public override void Update(GameTime gameTime)
        {
            if (_isFalling)
                Body.ApplyForce(new Vector2(0, 0.001f));
            else
            {
                Body.Stop();
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DecorationMovement.Animation, GetPosition(), Color.White);            
            //spriteBatch.Draw(DecorationMovement.Animation, new Vector2(GetPosition().X + GetPosition().Width, GetPosition().Y + GetPosition().Height / 2), null, Color.White, Body.Rotation, origin, 1.0f, SpriteEffects.None, 0f);

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