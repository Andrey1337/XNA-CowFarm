using CowFarm.DrowingSystem;
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
    public class CutGrass : Item, IInteractable
    {
        public CutGrass(CowGameScreen cowGameScreen, World world, Vector2 position)
            : base(cowGameScreen, world, new Rectangle((int)position.X, (int)position.Y, 32, 32), new AnimatedSprites(cowGameScreen.GameTextures["cutGrassMovement"], 1, 0), cowGameScreen.GameTextures["cutGrassIcon"])
        {
            Body = BodyFactory.CreateCircle(world, (float)2 / 100, 1f, position / 100);
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
            Body.BodyTypeName = "rocks";
            ItemId = 2;
            world.AddDynamicEntity(this);

            StackCount = 9;

            CanInteract = true;
        }

        public CutGrass(CowGameScreen cowGameScreen) : base(cowGameScreen, null, Rectangle.Empty, new AnimatedSprites(cowGameScreen.GameTextures["cutGrassMovement"], 1, 0), cowGameScreen.GameTextures["cutGrassIcon"])
        {
            StackCount = 9;
            ItemId = 2;
            CanInteract = true;
        }

        public override void Update(GameTime gameTime)
        {
            Body.Hikuah(12);
            SourceRect = ItemMovement.Animate(gameTime, ObjectMovingType);
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
    }
}