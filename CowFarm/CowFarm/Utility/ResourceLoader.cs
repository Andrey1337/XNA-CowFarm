﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Utility
{
    public static class ResourceLoader
    {
        public static Dictionary<string, Texture2D> GameTextures { get; private set; }
        public static Dictionary<string, SpriteFont> GameFonts { get; private set; }

        public static Dictionary<string, Texture2D> LoadTextures(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            if (GameTextures != null)
            {
                return GameTextures;
            }
            GameTextures = new Dictionary<string, Texture2D>();

            PlantLoad(contentManager);
            LoadMisc(contentManager, graphicsDevice);
            LoadCow(contentManager);
            LoadCat(contentManager);
            LoadFood(contentManager);
            RockLoad(contentManager);
            BackGroundLoad(contentManager);
            LoadButtons(contentManager);
            LoadIcons(contentManager);
            return GameTextures;
        }

        public static Dictionary<string, SpriteFont> LoadFonts(ContentManager contentManager)
        {
            if (GameFonts != null)
            {
                return GameFonts;
            }
            GameFonts = new Dictionary<string, SpriteFont> { { "gameFont", contentManager.Load<SpriteFont>("gameFont") } };

            return GameFonts;
        }

        private static void LoadCow(ContentManager contentManager)
        {
            GameTextures.Add("cowRightWalk", contentManager.Load<Texture2D>("AnimalMovements/cowRightWalk"));
            GameTextures.Add("cowLeftWalk", contentManager.Load<Texture2D>("AnimalMovements/cowLeftWalk"));
            GameTextures.Add("cowUpWalk", contentManager.Load<Texture2D>("AnimalMovements/cowUpWalk"));
            GameTextures.Add("cowDownWalk", contentManager.Load<Texture2D>("AnimalMovements/cowDownWalk"));
        }

        private static void LoadCat(ContentManager contentManager)
        {
            GameTextures.Add("catRightWalk", contentManager.Load<Texture2D>("AnimalMovements/catRightWalk"));
            GameTextures.Add("catLeftWalk", contentManager.Load<Texture2D>("AnimalMovements/catLeftWalk"));
            GameTextures.Add("catDownWalk", contentManager.Load<Texture2D>("AnimalMovements/catUpWalk"));
            GameTextures.Add("catUpWalk", contentManager.Load<Texture2D>("AnimalMovements/catDownWalk"));
        }

        private static void PlantLoad(ContentManager contentManager)
        {
            GameTextures.Add("grassMovement", contentManager.Load<Texture2D>("Plants/grassMovement"));

            GameTextures.Add("greenTreeMovement", contentManager.Load<Texture2D>("Plants/greenTreeMovement"));
            GameTextures.Add("orangeTreeMovement", contentManager.Load<Texture2D>("Plants/orangeTreeMovement"));

            GameTextures.Add("eatenGrassMovement", contentManager.Load<Texture2D>("Plants/eatenGrassMovement"));

            GameTextures.Add("bushMovement", contentManager.Load<Texture2D>("Plants/bushMovement"));
            GameTextures.Add("berryBushMovement", contentManager.Load<Texture2D>("Plants/berryBushMovement"));
        }

        private static void RockLoad(ContentManager contentManager)
        {
            GameTextures.Add("rockMovement", contentManager.Load<Texture2D>("DecorationMovements/rockMovement"));
            GameTextures.Add("boulderRockMovement", contentManager.Load<Texture2D>("DecorationMovements/boulderRockMovement"));
        }

        private static void BackGroundLoad(ContentManager contentManager)
        {
            GameTextures.Add("firstWorldBackGround", contentManager.Load<Texture2D>("WorldsBackgrounds/firstWorldBackGround"));
            GameTextures.Add("secondWorldBackGround", contentManager.Load<Texture2D>("WorldsBackgrounds/secondWorldBackGround"));

        }

        private static void LoadFood(ContentManager contentManager)
        {
            GameTextures.Add("appleMovement", contentManager.Load<Texture2D>("Food/appleMovement"));
            GameTextures.Add("eatenAppleMovement", contentManager.Load<Texture2D>("Food/eatenAppleMovement"));
        }

        private static void LoadIcons(ContentManager contentManager)
        {
            GameTextures.Add("appleIcon", contentManager.Load<Texture2D>("ItemIcons/appleIcon"));
        }
        private static void LoadMisc(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            GameTextures.Add("timerTexture", contentManager.Load<Texture2D>("timerTexture"));
            GameTextures.Add("inventoryPanel", contentManager.Load<Texture2D>("Miscs/inventoryPanel"));

            GameTextures.Add("sprintBorder", contentManager.Load<Texture2D>("sprintBorder"));

            var sprintTexture = new Texture2D(graphicsDevice, 1, 1);
            sprintTexture.SetData(new Color[] { new Color(52, 101, 181) });
            GameTextures.Add("sprintTexture", sprintTexture);
        }

        private static void LoadButtons(ContentManager contentManager)
        {
            GameTextures.Add("eButtonMovement", contentManager.Load<Texture2D>("eButtonMovement"));
        }

    }
}