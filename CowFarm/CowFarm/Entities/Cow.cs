using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using CowFarm.DrowingSystem;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Samples.DrawingSystem;
using FarseerPhysics.Samples.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities
{
    public class Cow : Animal
    {
        private readonly List<IInteractable>[,] _interactableEntities;

        private HashSet<IInteractable> _previousFocusInteractables;

        private Rectangle _sourceRect;

        private const float Delay = 200f;

        private int _score;

        public Cow(World world, GraphicsDeviceManager graphics, Rectangle destRect, AnimatedSprites currentAnim, AnimatedSprites rightWalk, AnimatedSprites leftWalk, AnimatedSprites downWalk, AnimatedSprites upWalk)
        : base(graphics, destRect, currentAnim, rightWalk, leftWalk, downWalk, upWalk)
        {
            _interactableEntities = world.InteractableEntities;
            Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            _focusNumber = 0;
            _previousFocusInteractables = new HashSet<IInteractable>(NearbyInteractables());
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= 27;
            vector.Y -= 27;

            return new Rectangle((int)vector.X, (int)vector.Y, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }

        public override void Eat(IEatable food)
        {
            if (food is Grass)
            {
                _score += 20;
            }
            food.IsEaten = true;
        }

        private Rectangle _rectangle;
        public IEnumerable<IInteractable> NearbyInteractables()
        {
            List<IInteractable> interactableList = new List<IInteractable>();
            if (CurrentAnim == RightWalk)
            {
                _rectangle = new Rectangle(GetPosition().X + CurrentAnim.SpriteWidth - 10, GetPosition().Y + 27, 40, 50);
            }
            if (CurrentAnim == LeftWalk)
            {
                _rectangle = new Rectangle(GetPosition().X - 20, GetPosition().Y + 27, 40, 50);
            }
            if (CurrentAnim == UpWalk)
            {
                _rectangle = new Rectangle(GetPosition().X, GetPosition().Y, 50, 40);
            }
            if (CurrentAnim == DownWalk)
            {
                _rectangle = new Rectangle(GetPosition().X, GetPosition().Y + GetPosition().Height, 50, 40);
            }

            for (int i = _rectangle.X; i < _rectangle.X + _rectangle.Width; i++)
            {
                if (i < 0 || i >= _interactableEntities.GetLength(0))
                    continue;
                for (int j = _rectangle.Y; j < _rectangle.Y + _rectangle.Height; j++)
                {
                    if (j < 0 || j >= _interactableEntities.GetLength(1))
                        continue;
                    if (_interactableEntities[i, j] == null) continue;
                    foreach (var interactable in _interactableEntities[i, j])
                    {
                        interactableList.Add(interactable);
                    }
                }
            }
            return interactableList;
        }

        public override void Load(ContentManager content)
        {

        }

        private IInteractable _interactableOnFocus;
        private int _focusNumber;

        public override void Update(GameTime gameTime)
        {
            HandleUserAgent();
            KeyboardState ks = Keyboard.GetState();

            var savedList = NearbyInteractables();
            List<IInteractable> interactablesList = savedList.ToList();


            if (_focusNumber >= interactablesList.Count)
                _focusNumber = 0;

            if (_focusNumber < interactablesList?.Count && interactablesList[_focusNumber] != null)
            {
                _interactableOnFocus = interactablesList[_focusNumber];
                _interactableOnFocus.OnFocus = true;

            }

            _previousFocusInteractables.Where(interacteble => !interactablesList.Contains(interacteble)).ToList().ForEach(interactable => interactable.OnFocus = false);

            //foreach (var interactable in _previousFocusInteractables)
            //{
            //    if (!interactablesList.Contains(interactable))
            //    {
            //        interactable.OnFocus = false;
            //    }
            //}

            //if (ks.IsKeyDown(Keys.Tab))
            //{
            //    if (interactablesList.Count != 0 && _focusNumber >= interactablesList.Count)
            //        _focusNumber = 0;
            //    else
            //        _focusNumber++;
            //}

            if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            {
                CurrentAnim = RightWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);

            }
            else if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            {

                CurrentAnim = LeftWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            {
                CurrentAnim = UpWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            {
                CurrentAnim = DownWalk;
                _sourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }
            else
            {

                _sourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
            }

            _previousFocusInteractables = new HashSet<IInteractable>(savedList);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), _sourceRect, Color.White);
        }

        Vector2 _force = new Vector2(0, 0);
        public void HandleUserAgent()
        {
            _force = new Vector2(0, 0);

            float forceAmountX = 1.5f;
            float forceAmountY = 1f;

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Keys.D))
            {
                _force += new Vector2(forceAmountX, 0);
            }
            if (input.IsKeyDown(Keys.A))
            {
                _force += new Vector2(-forceAmountX, 0);
            }
            if (input.IsKeyDown(Keys.W))
            {
                _force += new Vector2(0, -forceAmountY);
            }
            if (input.IsKeyDown(Keys.S))
            {
                _force += new Vector2(0, forceAmountY);
            }

            if (input.IsKeyUp(Keys.A) && input.IsKeyUp(Keys.S) &&
               input.IsKeyUp(Keys.W) && input.IsKeyUp(Keys.D))
            {
                Body.Stop();
            }


            Body.Move(_force);
            Body.ApplyForce(_force);
        }

        public bool IsSelected { get; set; }
    }
}
