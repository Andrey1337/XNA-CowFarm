using CowFarm.DrowingSystem;
using CowFarm.Enums;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Items
{
    public class CutGrass : Item, IEatable, IDynamic
    {
        public CutGrass(CowGameScreen cowGameScreen) : base(cowGameScreen, new AnimatedSprites(cowGameScreen.GameTextures["cutGrassMovement"], 1, 0), cowGameScreen.GameTextures["cutGrassIcon"])
        {
            ItemId = 2;
            StackCount = 9;
            Satiety = 5f;
        }

        public override void Update(GameTime gameTime)
        {
            Body.Hikuah(12);
            SourceRect = ItemMovement.Animate(gameTime, ObjectMovingType);

            if (GetPosition().X > CowGameScreen.Graphics.PreferredBackBufferWidth && CowGameScreen.WorldOnFocus.RightWorld != null)
            {
                CowGameScreen.ChangeWorld(this, Direction.Right);
            }

            if (GetPosition().X + GetPosition().Width < 0 && CowGameScreen.WorldOnFocus.LeftWorld != null)
            {
                CowGameScreen.ChangeWorld(this, Direction.Left);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ItemMovement.Animation, GetPosition(), SourceRect,
                OnFocus ? new Color(209, 209, 224) : Color.White);
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)DestRect.Width / 2;
            vector.Y -= 10;

            return new Rectangle((int)vector.X, (int)vector.Y, DestRect.Width, DestRect.Height);
        }

        public Vector2 GetInteractablePosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + (float)(GetPosition().Height / 2));
        }

        public bool OnFocus { get; set; }
        public bool CanInteract { get; set; }
        public void Interact()
        {
            Pick();
        }

        public override void Drop(World world, Vector2 position)
        {
            Body = BodyFactory.CreateCircle(world, (float)2 / 100, 1f, new Vector2(position.X, position.Y) / 100);
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
            Body.BodyTypeName = "cutGrass";
            CanInteract = true;
            DestRect = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            world.AddDynamicEntity(this);
            CurrentWorld = world;
        }

        public bool IsEaten { get; set; }
        public void Eat()
        {
            CurrentWorld.RemoveDynamicEntity(this);
        }

        public float Satiety { get; }
        public void ChangeWorld(World world, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    Body = BodyFactory.CreateCircle(world, (float)2 / 100, 1f, new Vector2(0.25f, Body.Position.Y));
                    break;

                case Direction.Left:
                    Body = BodyFactory.CreateCircle(world, (float)2 / 100, 1f, new Vector2((float)(CowGameScreen.Graphics.PreferredBackBufferWidth - 25) / 100, Body.Position.Y));
                    break;
            }

            Body.BodyTypeName = "cutGrass";
            CurrentWorld = world;
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
        }
    }
}