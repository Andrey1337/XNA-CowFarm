using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Utility
{
    public static class TextDrawHeleper
    {
        public static void DrawText(SpriteBatch spritebatch, SpriteFont font, string text, Color backColor, Color frontColor, float scale, Vector2 position)
        {
            Vector2 origin = Vector2.Zero;

            spritebatch.DrawString(font, text, position + new Vector2(1.5f * scale, 1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 0f);
            spritebatch.DrawString(font, text, position + new Vector2(-1.5f * scale, -1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 0f);
            spritebatch.DrawString(font, text, position + new Vector2(1 * scale, -1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 0f);
            spritebatch.DrawString(font, text, position + new Vector2(-1.5f * scale, 1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 0f);

            spritebatch.DrawString(font, text, position, frontColor, 0, origin, scale, SpriteEffects.None, 0f);
        }
    }
}