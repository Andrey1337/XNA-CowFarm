using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CowFarm
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CowFarmGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Color _backGroundColor;

        private Texture2D _cowRightWalk;
        private Texture2D _cowLeftWalk;
        private Texture2D _cowDownWalk;
        private Texture2D _cowUpWalk;

        private Texture2D _grassMovement;

        private Cow _cow;
        private Grass _grass;
        private List<Entity> _allEntities;

        private DateTime _currentTime;

        private SpriteFont _font;

        private GrassGenerator _grassGenerator;

        private FirstWorld _firstWorld;

        public CowFarmGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            var a = DateTime.Now;
            List<Entity>[] arr = new List<Entity>[800];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new List<Entity>();
            }
            var b = DateTime.Now;

            var c = b - a;
            _backGroundColor = new Color(57, 172, 57);

            _currentTime = DateTime.Now;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadCow();
            GrassLoad();
            LoadFonts();
            //_allEntities = new List<Entity>() { _cow };

            _firstWorld = new FirstWorld(new List<Entity>() { _grass }, new List<Entity>() { _cow });
        }

        private void LoadFonts()
        {
            _font = Content.Load<SpriteFont>("gameFont");
        }

        private void LoadCow()
        {
            _cowRightWalk = Content.Load<Texture2D>("cowRightWalk");
            _cowLeftWalk = Content.Load<Texture2D>("cowLeftWalk");
            _cowDownWalk = Content.Load<Texture2D>("cowDownWalk");
            _cowUpWalk = Content.Load<Texture2D>("cowUpWalk");
            _cow = new Cow(new Rectangle(100, 100, 54, 48), _cowRightWalk, _cowRightWalk, _cowLeftWalk, _cowDownWalk, _cowUpWalk);
        }
        private void GrassLoad()
        {
            _grassMovement = Content.Load<Texture2D>("grassMovement");
            _grass = new Grass(new Rectangle(200, 100, 24, 24), _grassMovement);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        protected override void Update(GameTime gameTime)
        {
            //_allEntities.ForEach(entity => entity.Update(gameTime, _graphics));
            _firstWorld.Update(gameTime, _graphics);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backGroundColor);
            _spriteBatch.Begin();

            //_allEntities.Sort(new EntityYPositionComparer());
            //_allEntities.ForEach(entity => entity.Draw(gameTime, _spriteBatch));

            _firstWorld.Draw(gameTime, _spriteBatch);

            DrowTime();

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrowTime()
        {
            var inGametime = DateTime.Now - _currentTime;
            _spriteBatch.DrawString(_font, $"Time: {inGametime.ToString(@"mm\:ss\.ff") }", new Vector2(_graphics.PreferredBackBufferWidth - _graphics.PreferredBackBufferWidth / 5, 0), Color.Black);
            //spriteBatch.DrawString(_font, $"Cow pos x:{_cow.GetPosition().X + _cow.GetPosition().Width} y:{_cow.GetPosition().Y + _cow.GetPosition().Height}", new Vector2(500, 100), Color.AliceBlue);
            //spriteBatch.DrawString(_font, $"Grass pos x:{_grass.GetPosition().X + _grass.GetPosition().Width} y:{_grass.GetPosition().Y + _grass.GetPosition().Height}", new Vector2(500, 150), Color.AliceBlue);
            _spriteBatch.DrawString(_font, DateTime.Now.ToString(@"mm\:ss\.ff"), new Vector2(500, 150), Color.AliceBlue);
        }
    }
}