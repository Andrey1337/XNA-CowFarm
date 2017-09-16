using CowFarm.DrowingSystem;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Tree : Plant, IInteractable
    {
        private const float Delay = float.MaxValue;
        private readonly Texture2D _reapaintTexture;

        public Tree(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites treeMovement)
            : base(graphics, destRect, treeMovement)
        {
            float width = (float)14 / 100;
            float height = (float)1 / 100;

            float x = (float)(destRect.X + destRect.Width - 80) / 100;
            float y = (float)(destRect.Y + destRect.Height - 22) / 100;

            Body = BodyFactory.CreateRectangle(world, width, height, 0f, new Vector2(x, y));

            Body.BodyType = BodyType.Static;
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
            _reapaintTexture = RepaintRectangle(CopyTexture(PlantMovement.Animation));
        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            SourceRect = PlantMovement.Animate(gameTime, Delay, ObjectMovingType);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (OnFocus)
            {
                spriteBatch.Draw(_reapaintTexture, new Rectangle(DestRect.X - 3, DestRect.Y - 3, DestRect.Width + 6, DestRect.Height + 5), SourceRect, Color.White);
            }
            spriteBatch.Draw(PlantMovement.Animation, DestRect, SourceRect, Color.White);
        }

        private Texture2D CopyTexture(Texture2D texture)
        {
            Texture2D copyTexture = new Texture2D(Graphics.GraphicsDevice, 155, PlantMovement.SpriteHeight);
            Color[] oldC = new Color[texture.Width * texture.Height];

            texture.GetData(oldC);
            copyTexture.SetData<Color>(oldC);

            return copyTexture;
        }


        private Texture2D RepaintRectangle(Texture2D texture)
        {
            Color[] color = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(color);

            for (int i = 0; i < color.Length; i++)
            {
                if (color[i].A > 170)
                {
                    color[i] = Color.White;
                }
                else
                {
                    color[i].A = 0;
                }
            }

            texture.SetData<Color>(color);
            return texture;
        }

        public override Rectangle GetPosition()
        {
            return new Rectangle(DestRect.X, DestRect.Y, PlantMovement.SpriteWidth, PlantMovement.SpriteHeight);
        }

        public Vector2 GetInteractablePosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height);
        }

        public bool OnFocus { get; set; }
    }
}