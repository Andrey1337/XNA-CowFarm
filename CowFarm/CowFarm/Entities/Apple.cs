using System.Collections.Generic;
using CowFarm.DrowingSystem;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Entities
{
    public class Apple : Decoration, IEatable
    {
        public Apple(World world, Rectangle destRect, IDictionary<string, Texture2D> gameTextures) : base(world, destRect, new AnimatedSprites(gameTextures["appleMovement"], 1, 0))
        {

        }

        public override void Load(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override Rectangle GetPosition()
        {
            throw new System.NotImplementedException();
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