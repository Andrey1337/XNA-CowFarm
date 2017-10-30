using CowFarm.DrowingSystem;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using CowFarm.Worlds;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities.Items.Craftables
{
    public class Rope : Item, IInteractable
    {
        public Rope(CowGameScreen cowGameScreen)
            : base(cowGameScreen, new AnimatedSprites(cowGameScreen.GameTextures["ropeMovement"], 1, 0), cowGameScreen.GameTextures["ropeIcon"])
        {
            ItemId = 3;
            StackCount = 3;
        }

        public override void Update(GameTime gameTime)
        {
            Body.Stop();
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

            return new Rectangle((int)vector.X, (int)vector.Y, DestRect.Width, DestRect.Height);
        }

        public override void Drop(World world, Vector2 position)
        {
            throw new System.NotImplementedException();
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