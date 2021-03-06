﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm.Utility
{
    public static class ResourceLoader
    {
        public static Dictionary<string, Texture2D> GameTextures { get; private set; }
        public static Dictionary<string, SpriteFont> GameFonts { get; private set; }
        public static Dictionary<string, SoundEffect> GameSounds { get; private set; }

        public static Dictionary<string, Texture2D> LoadTextures(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            if (GameTextures != null)
            {
                return GameTextures;
            }
            GameTextures = new Dictionary<string, Texture2D>();
            LoadSelectionTexture(graphicsDevice, contentManager, 50, 50);
            LoadClean(graphicsDevice);
            PlantLoad(contentManager);
            LoadMisc(contentManager, graphicsDevice);
            LoadCow(contentManager);
            LoadCat(contentManager);
            LoadChicken(contentManager);
            LoadFood(contentManager);
            RockLoad(contentManager);
            BackGroundLoad(contentManager);
            LoadButtons(contentManager);
            LoadIcons(contentManager);
            LoadItems(contentManager);
            LoadAlertWindows(contentManager);
            return GameTextures;
        }

        public static void LoadClean(GraphicsDevice graphicsDevice)
        {
            Texture2D selectionTexture = new Texture2D(graphicsDevice, 1, 1);
            Color[] color = new Color[1];
            selectionTexture.GetData(color);
            color[0] = Color.White;
            selectionTexture.SetData(color);
            GameTextures.Add("cleanTexture", selectionTexture);
        }

        public static Dictionary<string, SoundEffect> LoadSongs(ContentManager contentManager)
        {
            if (GameSounds != null)
            {
                return GameSounds;
            }
            GameSounds = new Dictionary<string, SoundEffect>();
            GameSounds.Add("gameSound", contentManager.Load<SoundEffect>("Sounds/gameSound"));

            return GameSounds;

        }


        public static void LoadSelectionTexture(GraphicsDevice graphicsDevice, ContentManager contentManager, int textureWidth, int textureHeight)
        {
            Texture2D selectionTexture = new Texture2D(graphicsDevice, textureWidth, textureHeight);
            Color[] color = new Color[textureWidth * textureHeight];
            selectionTexture.GetData(color);
            for (int i = 0; i < textureWidth; i++)
            {
                for (int j = 0; j < textureHeight; j++)
                {
                    if (i < 4 || i > textureWidth - 5)
                    {
                        color[i * textureWidth + j] = Color.Black;
                    }
                    if (j < 4 || j > textureHeight - 5)
                    {
                        color[i * textureWidth + j] = Color.Black;
                    }
                }
            }
            selectionTexture.SetData(color);
            GameTextures.Add("selectionTexture", selectionTexture);
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
            GameTextures.Add("cowRightWalk", contentManager.Load<Texture2D>("AnimalMovements/Cow/cowRightWalk"));
            GameTextures.Add("cowLeftWalk", contentManager.Load<Texture2D>("AnimalMovements/Cow/cowLeftWalk"));
            GameTextures.Add("cowUpWalk", contentManager.Load<Texture2D>("AnimalMovements/Cow/cowUpWalk"));
            GameTextures.Add("cowDownWalk", contentManager.Load<Texture2D>("AnimalMovements/Cow/cowDownWalk"));
        }

        private static void LoadCat(ContentManager contentManager)
        {
            GameTextures.Add("catRightWalk", contentManager.Load<Texture2D>("AnimalMovements/Cat/catRightWalk"));
            GameTextures.Add("catLeftWalk", contentManager.Load<Texture2D>("AnimalMovements/Cat/catLeftWalk"));
            GameTextures.Add("catUpWalk", contentManager.Load<Texture2D>("AnimalMovements/Cat/catUpWalk"));
            GameTextures.Add("catDownWalk", contentManager.Load<Texture2D>("AnimalMovements/Cat/catDownWalk"));
        }

        private static void LoadChicken(ContentManager contentManager)
        {
            GameTextures.Add("whiteChickenRightWalk", contentManager.Load<Texture2D>("AnimalMovements/Chicken/whiteChickenRightWalk"));
            GameTextures.Add("whiteChickenLeftWalk", contentManager.Load<Texture2D>("AnimalMovements/Chicken/whiteChickenLeftWalk"));
            GameTextures.Add("whiteChickenUpWalk", contentManager.Load<Texture2D>("AnimalMovements/Chicken/whiteChickenUpWalk"));
            GameTextures.Add("whiteChickenDownWalk", contentManager.Load<Texture2D>("AnimalMovements/Chicken/whiteChickenDownWalk"));
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
            //GameTextures.Add("firstWorldBackGround", contentManager.Load<Texture2D>("WorldsBackgrounds/firstWorldBackGround"));
            //GameTextures.Add("secondWorldBackGround", contentManager.Load<Texture2D>("WorldsBackgrounds/secondWorldBackGround"));

        }

        private static void LoadFood(ContentManager contentManager)
        {
            GameTextures.Add("appleMovement", contentManager.Load<Texture2D>("Food/appleMovement"));
            GameTextures.Add("eatenAppleMovement", contentManager.Load<Texture2D>("Food/eatenAppleMovement"));
        }

        private static void LoadItems(ContentManager contentManager)
        {
            GameTextures.Add("rocksMovement", contentManager.Load<Texture2D>("Items/rocksMovement"));
            GameTextures.Add("cutGrassMovement", contentManager.Load<Texture2D>("Items/cutGrassMovement"));
            GameTextures.Add("ropeMovement", contentManager.Load<Texture2D>("Items/ropeMovement"));
            GameTextures.Add("logMovement", contentManager.Load<Texture2D>("Items/logMovement"));
        }

        private static void LoadIcons(ContentManager contentManager)
        {
            GameTextures.Add("appleIcon", contentManager.Load<Texture2D>("ItemIcons/appleIcon"));
            GameTextures.Add("rocksIcon", contentManager.Load<Texture2D>("ItemIcons/rocksIcon"));
            GameTextures.Add("cutGrassIcon", contentManager.Load<Texture2D>("ItemIcons/cutGrassIcon"));
            GameTextures.Add("ropeIcon", contentManager.Load<Texture2D>("ItemIcons/ropeIcon"));
            GameTextures.Add("logIcon", contentManager.Load<Texture2D>("ItemIcons/logIcon"));

            GameTextures.Add("fullHearthIcon", contentManager.Load<Texture2D>("MiscsIcons/fullHearthIcon"));
            GameTextures.Add("halfHearthIcon", contentManager.Load<Texture2D>("MiscsIcons/halfHearthIcon"));
            GameTextures.Add("emptyHearthIcon", contentManager.Load<Texture2D>("MiscsIcons/emptyHearthIcon"));

            GameTextures.Add("fullFoodIcon", contentManager.Load<Texture2D>("MiscsIcons/fullFoodIcon"));
            GameTextures.Add("halfFoodIcon", contentManager.Load<Texture2D>("MiscsIcons/halfFoodIcon"));
            GameTextures.Add("emptyFoodIcon", contentManager.Load<Texture2D>("MiscsIcons/emptyFoodIcon"));
        }
        private static void LoadMisc(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            GameTextures.Add("timerTexture", contentManager.Load<Texture2D>("timerTexture"));
            GameTextures.Add("inventoryPanel", contentManager.Load<Texture2D>("Miscs/inventoryPanel"));
            GameTextures.Add("craftPanel", contentManager.Load<Texture2D>("Miscs/craftPanel"));

            GameTextures.Add("sprintBorder", contentManager.Load<Texture2D>("sprintBorder"));

            var sprintTexture = new Texture2D(graphicsDevice, 1, 1);
            sprintTexture.SetData(new Color[] { new Color(52, 101, 181) });
            GameTextures.Add("sprintTexture", sprintTexture);
        }

        private static void LoadButtons(ContentManager contentManager)
        {
            GameTextures.Add("eButtonMovement", contentManager.Load<Texture2D>("eButtonMovement"));
            GameTextures.Add("restartButton", contentManager.Load<Texture2D>("AlertWindows/Buttons/restartButton"));
            GameTextures.Add("mainMenuButton", contentManager.Load<Texture2D>("AlertWindows/Buttons/mainMenuButton"));
            GameTextures.Add("resumeButton", contentManager.Load<Texture2D>("AlertWindows/Buttons/resumeButton"));
        }
        private static void LoadAlertWindows(ContentManager contentManager)
        {
            GameTextures.Add("endGameAlertWindow", contentManager.Load<Texture2D>("AlertWindows/endGameAlertWindow"));
            GameTextures.Add("menuAlertWindow", contentManager.Load<Texture2D>("AlertWindows/menuAlertWindow"));
        }
    }
}