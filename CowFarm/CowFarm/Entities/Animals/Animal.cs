using CowFarm.DrowingSystem;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Animals
{
    public abstract class Animal : Entity
    {
        protected DynamicAnimatedSprites RightWalk;
        protected DynamicAnimatedSprites LeftWalk;
        protected DynamicAnimatedSprites DownWalk;
        protected DynamicAnimatedSprites UpWalk;
        protected DynamicAnimatedSprites CurrentAnim;

        protected Rectangle DestRect;
        protected Rectangle SourceRect;

        protected ObjectMovingType ObjectMovingType;
        protected float Delay;

        public float HealthPoint { get; protected set; }

        protected Animal(CowGameScreen cowGameScreen, World world, Rectangle destRect, DynamicAnimatedSprites rightWalk,
            DynamicAnimatedSprites leftWalk, DynamicAnimatedSprites upWalk, DynamicAnimatedSprites downWalk) : base(cowGameScreen)
        {
            DestRect = destRect;
            RightWalk = rightWalk;
            LeftWalk = leftWalk;
            UpWalk = upWalk;
            DownWalk = downWalk;
            ObjectMovingType = ObjectMovingType.Dynamic;
        }

        public abstract void Eat(IEatable food);


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), SourceRect, Color.White);
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)CurrentAnim.SpriteWidth / 2;
            vector.Y -= (float)CurrentAnim.SpriteHeight / 2;

            return new Rectangle((int)vector.X, (int)vector.Y, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }
        public virtual Vector2 GetAttackPosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + (float)(GetPosition().Height / 2));
        }


    }
}
