using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.DrowingSystem;
using CowFarm.Enums;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
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
        private float _rotationAngle;

        private readonly World _world;
        private Body _floor;
        private readonly GreenTree _tree;
        private readonly CowGameScreen _cowGameScreen;
        private Texture2D _eatenAppleMovement;

        private readonly Vector2 _origin;

        public Apple(CowGameScreen cowGameScreen, World world, GreenTree tree, Rectangle destRect, IDictionary<string, Texture2D> gameTextures) : base(world, destRect, new AnimatedSprites(gameTextures["appleMovement"], 1, 0))
        {
            _origin.X = DecorationMovement.SpriteWidth / 2;
            _origin.Y = DecorationMovement.SpriteHeight / 2;

            ReapaintTexture = TextureHelper.RepaintRectangle(TextureHelper.CopyTexture(DecorationMovement.Animation, Graphics));
            _eatenAppleMovement = gameTextures["eatenAppleMovement"];
            _cowGameScreen = cowGameScreen;
            _world = world;
            _tree = tree;
            Body = BodyFactory.CreateCircle(world, (float)1 / 100, 0.2f, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Body.BodyTypeName = "apple";
            Body.Restitution = 0.26f;
            Body.CollisionCategories = Category.Cat10;
            Body.CollidesWith = Category.Cat10;

            _world.ContactManager.Contacted += AppleFloorContacted;
        }

        private void AppleFloorContacted(object sender, CollideEventArg collide)
        {
            if (collide.Dictionary.ContainsKey(BodyId) && collide.Dictionary[BodyId].Contains(_floor))
            {
                Body.Restitution = 0f;
                _isFalling = false;
                _world.RemoveBody(_floor);
                Body.CollisionCategories = Category.All & ~Category.Cat10;
                Body.CollidesWith = Category.All & ~Category.Cat10;
                _tree.Apple = null;
                _world.AddDynamicEntity(this);
                CanInteract = true;
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
        }

        public override void Update(GameTime gameTime)
        {
            if (_isFalling)
            {
                Body.ApplyForce(new Vector2(0, 0.001f));
                return;
            }
            if (IsEaten)
            {
                Body.Stop();
                return;
            }
            if (Body.GetVelocity().X != 0)
            {
                _rotationAngle += Body.GetVelocity().X / 10;
                float circle = MathHelper.Pi * 2;
                _rotationAngle %= circle;
            }

            Body.Hikuah(0.08f);

            //if (GetCenterPosition().X > Graphics.PreferredBackBufferWidth && _cowGameScreen.WorldOnFocus.RightWorld != null)
            //{
            //    _cowGameScreen.ChangeWorld(this, Direction.Right);
               
            //    Body = BodyFactory.CreateRectangle(_cowGameScreen.WorldOnFocus, 0.54f, 0.15f, 0, new Vector2((float)GetCenterPosition().X / 100, (float)(GetPosition().Y + GetPosition().Height) / 100));
            //    Body.BodyType = BodyType.Dynamic;
            //    Body.CollisionCategories = Category.All & ~Category.Cat10;
            //    Body.CollidesWith = Category.All & ~Category.Cat10;
            //}

            //if (GetCenterPosition().X < 0 && _cowGameScreen.WorldOnFocus.LeftWorld != null)
            //{
            //    _cowGameScreen.ChangeWorld(this, Direction.Left);
                
            //    Body = BodyFactory.CreateRectangle(_cowGameScreen.WorldOnFocus, 0.54f, 0.15f, 0, new Vector2((float)Graphics.PreferredBackBufferWidth / 100, (float)(GetPosition().Y + GetPosition().Height) / 100));
            //    Body.BodyType = BodyType.Dynamic;
            //    Body.CollisionCategories = Category.All & ~Category.Cat10;
            //    Body.CollidesWith = Category.All & ~Category.Cat10;
            //}
        }

        private Vector2 GetCenterPosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsEaten)
                spriteBatch.Draw(_eatenAppleMovement, GetPosition(), Color.White);
            else
            {
                if (OnFocus)
                {
                    spriteBatch.Draw(DecorationMovement.Animation,
                        new Vector2(GetPosition().X + GetPosition().Width / 2,
                            GetPosition().Y + GetPosition().Height / 2), null, new Color(209, 209, 224), _rotationAngle,
                        _origin, 0.34f, SpriteEffects.None, 0f);
                    //spriteBatch.Draw(DecorationMovement.Animation, GetPosition(), new Color(209, 209, 224));
                }
                else
                {
                    spriteBatch.Draw(DecorationMovement.Animation,
                        new Vector2(GetPosition().X + GetPosition().Width / 2,
                            GetPosition().Y + GetPosition().Height / 2), null, Color.White, _rotationAngle, _origin,
                        0.34f, SpriteEffects.None, 0f);
                    //spriteBatch.Draw(DecorationMovement.Animation, GetPosition(), Color.White);
                }
            }

        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)DestRect.Width / 2;

            return new Rectangle((int)vector.X, (int)vector.Y, DestRect.Height, DestRect.Width);
        }

        public Texture2D ReapaintTexture { get; set; }
        public bool OnFocus { get; set; }
        public bool CanInteract { get; set; }
        public void Interact()
        {
            IsEaten = true;
            CanInteract = false;

        }

        public bool IsEaten { get; set; }
    }
}