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
    public class ChickenLeg : Item, IEatable, IDynamic
    {
        public ChickenLeg(CowGameScreen cowGameScreen) : base(cowGameScreen, new StaticAnimatedSprites(cowGameScreen.GameTextures["chickenLegMovement"], 1, 0), cowGameScreen.GameTextures["chickenLegIcon"])
        {
            ItemId = 5;
            Satiety = 20f;
            StackCount = 6;
        }

        public override void Update(GameTime gameTime)
        {
            Body.Hikuah(12);
            SourceRect = ItemMovement.Animate(gameTime);
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

        public override void Drop(World world, Vector2 position)
        {
            Body = BodyFactory.CreateCircle(world, (float)4 / 100, 1f, new Vector2(position.X, position.Y) / 100);
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
            Body.BodyTypeName = "chickenLeg";
            CanInteract = true;
            DestRect = new Rectangle((int)position.X, (int)position.Y, 33, 29);
            world.AddDynamicEntity(this);
            CurrentWorld = world;
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
                    Body = BodyFactory.CreateCircle(world, (float)4 / 100, 1f, new Vector2(0.25f, Body.Position.Y));
                    break;

                case Direction.Left:
                    Body = BodyFactory.CreateCircle(world, (float)4 / 100, 1f, new Vector2((float)(CowGameScreen.Graphics.PreferredBackBufferWidth - 25) / 100, Body.Position.Y));
                    break;
            }

            Body.BodyTypeName = "chickenLeg";
            CurrentWorld = world;
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
        }

    }
}