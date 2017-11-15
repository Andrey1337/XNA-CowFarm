using CowFarm.DrowingSystem;
using CowFarm.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Decorations
{
    public abstract class Decoration : Entity
    {
        protected StaticAnimatedSprites DecorationMovement;
        protected Rectangle DestRect;
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;

        protected Decoration(CowGameScreen cowGameScreen, World world, Rectangle destRect, StaticAnimatedSprites decorationMovement) : base(cowGameScreen, world)
        {
            this.DestRect = destRect;
            this.DecorationMovement = decorationMovement;
            this.ObjectMovingType = ObjectMovingType.Static;
        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = new Rectangle(0, 0, DecorationMovement.SpriteWidth, DecorationMovement.SpriteHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DecorationMovement.Animation, DestRect, Color.White);
        }
    }
}