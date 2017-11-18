using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Utility
{
    public static class TextureHelper
    {
        public static Texture2D CopyTexture(Texture2D texture, GraphicsDeviceManager graphics)
        {
            Texture2D copyTexture = new Texture2D(graphics.GraphicsDevice, texture.Width, texture.Height);
            Color[] oldC = new Color[texture.Width * texture.Height];

            texture.GetData(oldC);
            copyTexture.SetData(oldC);

            return copyTexture;
        }

        public static Texture2D RepaintRectangle(Texture2D texture)
        {
            Color[] color = new Color[texture.Width * texture.Height];
            texture.GetData(color);

            for (int i = 0; i < color.Length; i++)
            {
                if (color[i].A > 225)
                {
                    color[i] = Color.White;
                }
                else
                {
                    color[i].A = 0;
                }
            }

            texture.SetData(color);
            return texture;
        }

        public static Texture2D CreateBorderTexture(GraphicsDevice graphicsDevice, int width, int height)
        {
            Texture2D borderTexture = new Texture2D(graphicsDevice, 1, 1);
            Color[,] colors = new Color[1, 1];
            colors[0, 0] = Color.White;
            borderTexture.SetData(colors.Cast<Color>().ToArray());

            return borderTexture;
        }

        public static Texture2D CreateTexture(GraphicsDevice graphicsDevice, int width, int height)
        {
            Texture2D borderTexture = new Texture2D(graphicsDevice, width, height);

            Color[,] colors = new Color[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    colors[i, j] = Color.White;
                }
            }
            borderTexture.SetData(colors.Cast<Color>().ToArray());

            return borderTexture;
        }
    }
}