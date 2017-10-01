using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using CowFarm.DrowingSystem;
using CowFarm.Enums;
using CowFarm.ScreenSystem;
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
        private HashSet<IInteractable>[,] _interactableEntities;
        private HashSet<IInteractable> _previousFocusInteractables;

        private float Delay = 200f;

        private readonly CowGameScreen _cowGameScreen;

        public float Boost;

        public Cow(CowGameScreen cowGameScreen, World world, Rectangle destRect, Dictionary<string, Texture2D> gameTextures)
        : base(world, destRect,
              new AnimatedSprites(gameTextures["cowRightWalk"], 3, 16),
              new AnimatedSprites(gameTextures["cowLeftWalk"], 3, 16),
              new AnimatedSprites(gameTextures["cowUpWalk"], 3, 16),
              new AnimatedSprites(gameTextures["cowDownWalk"], 3, 16))
        {
            Boost = 1;


            _cowGameScreen = cowGameScreen;
            _interactableEntities = world.InteractableEntities;
            Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All;
            Body.CollidesWith = Category.All;
            _focusNumber = 0;
            this.CurrentAnim = RightWalk;
            _previousFocusInteractables = new HashSet<IInteractable>(NearbyInteractables());
        }

        public override Rectangle GetPosition()
        {
            Vector2 vector = ConvertUnits.ToDisplayUnits(Body.Position);
            vector.X -= (float)CurrentAnim.SpriteWidth / 2;
            vector.Y -= (float)CurrentAnim.SpriteHeight / 2;

            return new Rectangle((int)vector.X, (int)vector.Y, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
        }



        public override void Eat(IEatable food)
        {
            food.Interact();
            if (food is Grass)
                _cowGameScreen.Score += 20;
        }

        private Rectangle _rectangle;

        public IEnumerable<IInteractable> NearbyInteractables()
        {
            List<IInteractable> interactableList = new List<IInteractable>();
            if (CurrentAnim == RightWalk)
            {
                _rectangle = new Rectangle(GetPosition().X + CurrentAnim.SpriteWidth - 20
                    , GetPosition().Y + CurrentAnim.SpriteHeight / 4
                    , 70
                    , GetPosition().Height + CurrentAnim.SpriteHeight / 3);
                //Debug.WriteLine(_rectangle);
            }
            if (CurrentAnim == LeftWalk)
            {
                _rectangle = new Rectangle(GetPosition().X + 20 - 70
                    , GetPosition().Y + CurrentAnim.SpriteHeight / 4
                    , 70
                    , GetPosition().Height + CurrentAnim.SpriteHeight / 3);
            }
            if (CurrentAnim == UpWalk)
            {
                _rectangle = new Rectangle(GetPosition().X - 5, GetPosition().Y - 50, GetPosition().Width + 5 * 2,
                    GetPosition().Height + 40);
            }
            if (CurrentAnim == DownWalk)
            {
                _rectangle = new Rectangle(GetPosition().X - 5, GetPosition().Y + GetPosition().Height,
                    GetPosition().Width + 5 * 2, GetPosition().Height);
            }

            if (CurrentAnim == RightWalk)
            {
                int middleY = _rectangle.Y + _rectangle.Height / 2;

                for (int i = _rectangle.X; i < _rectangle.X + _rectangle.Width; i++)
                {
                    if (i < 0 || i >= _interactableEntities.GetLength(0))
                        continue;
                    for (int j = 0; j < +_rectangle.Height / 2; j++)
                    {
                        if (middleY - j < 0 || middleY + j >= _interactableEntities.GetLength(1))
                            continue;

                        if (_interactableEntities[i, middleY - j] != null)
                            foreach (var interactable in _interactableEntities[i, middleY - j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }
                        if (_interactableEntities[i, middleY + j] != null)
                            foreach (var interactable in _interactableEntities[i, middleY + j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }
                    }
                }
            }
            if (CurrentAnim == LeftWalk)
            {
                int middleY = _rectangle.Y + _rectangle.Height / 2;
                for (int i = _rectangle.X + _rectangle.Width; i > _rectangle.X; i--)
                {
                    if (i < 0 || i >= _interactableEntities.GetLength(0))
                        continue;
                    for (int j = 0; j < +_rectangle.Height / 2; j++)
                    {
                        if (middleY - j < 0 || middleY + j >= _interactableEntities.GetLength(1))
                            continue;

                        if (_interactableEntities[i, middleY + j] != null)
                            foreach (var interactable in _interactableEntities[i, middleY + j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }

                        if (_interactableEntities[i, middleY - j] != null)
                            foreach (var interactable in _interactableEntities[i, middleY - j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }
                    }
                }
            }
            if (CurrentAnim == UpWalk)
            {
                int middleX = _rectangle.X + _rectangle.Width / 2;
                for (int j = _rectangle.Y + _rectangle.Height; j > _rectangle.Y; j--)
                {
                    if (j < 0 || j >= _interactableEntities.GetLength(1))
                        continue;
                    for (int i = 0; i < _rectangle.Width / 2; i++)
                    {
                        if (middleX - i < 0 || middleX + i >= _interactableEntities.GetLength(0))
                            continue;
                        if (_interactableEntities[middleX + i, j] != null)
                            foreach (var interactable in _interactableEntities[middleX + i, j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }

                        if (_interactableEntities[middleX - i, j] != null)
                            foreach (var interactable in _interactableEntities[middleX - i, j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }
                    }
                }
            }

            if (CurrentAnim == DownWalk)
            {
                int middleX = _rectangle.X + _rectangle.Width / 2;
                for (int j = _rectangle.Y; j < _rectangle.Y + +_rectangle.Height; j++)
                {
                    if (j < 0 || j >= _interactableEntities.GetLength(1))
                        continue;
                    for (int i = 0; i < _rectangle.Width / 2; i++)
                    {
                        if (middleX - i < 0 || middleX + i >= _interactableEntities.GetLength(0))
                            continue;
                        if (_interactableEntities[middleX + i, j] != null)
                            foreach (var interactable in _interactableEntities[middleX + i, j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }

                        if (_interactableEntities[middleX - i, j] != null)
                            foreach (var interactable in _interactableEntities[middleX - i, j])
                            {
                                AddInteractableToList(interactable, interactableList);
                            }
                    }
                }
            }

            return interactableList;
        }

        private void AddInteractableToList(IInteractable interactable, List<IInteractable> interactableList)
        {
            if (!interactable.CanInteract) return;
            var grass = interactable as Grass;
            if (grass != null &&
                Vector2.Distance(GetCenterPosition(), grass.GetInteractablePosition()) <
                70)
            {
                if ((CurrentAnim == RightWalk || CurrentAnim == LeftWalk || CurrentAnim == DownWalk) &&
                    Vector2.Distance(GetCenterPosition(), grass.GetInteractablePosition()) < 70)
                {
                    interactableList.Add(interactable);
                    return;
                }

                if (Vector2.Distance(GetCenterPosition(), grass.GetInteractablePosition()) < 40)
                    interactableList.Add(interactable);
            }
        }


        public override void Load(ContentManager content)
        {

        }

        private IInteractable _previousInteractableOnFocus;

        private int _focusNumber;

        private bool _tabKeyIsPressed;
        private bool _eKeyIsPressed;

        public override void Update(GameTime gameTime)
        {
            HandleUserAgent();
            KeyboardState ks = Keyboard.GetState();

            IInteractable interactableOnFocus = null;
            var savedList = NearbyInteractables();

            var interactables = savedList as IList<IInteractable> ?? savedList.ToList();
            List<IInteractable> interactablesList = interactables.ToList();

            HashSet<IInteractable> hash = new HashSet<IInteractable>(interactables);

            if (hash.SequenceEqual(_previousFocusInteractables))
            {
                if (_focusNumber < interactablesList.Count && interactablesList[_focusNumber] != null)
                {
                    interactablesList[_focusNumber].OnFocus = true;
                    interactableOnFocus = interactablesList[_focusNumber];
                }
            }
            else
            {
                _focusNumber = 0;
            }

            if (_previousInteractableOnFocus != null && (interactableOnFocus != null && interactableOnFocus != _previousInteractableOnFocus || _focusNumber != 0 && _focusNumber == interactables.Count))
                _previousInteractableOnFocus.OnFocus = false;

            _previousFocusInteractables.Where(interacteble => !interactablesList.Contains(interacteble)).ToList().ForEach(interactable => interactable.OnFocus = false);

            if (ks.IsKeyDown(Keys.Tab))
                _tabKeyIsPressed = true;

            if (_tabKeyIsPressed && ks.IsKeyUp(Keys.Tab))
            {
                _tabKeyIsPressed = false;
                if (_focusNumber >= interactablesList.Count)
                {
                    _focusNumber = 0;
                }
                else
                {
                    _focusNumber++;
                }
            }
            if (ks.IsKeyDown(Keys.E))
                _eKeyIsPressed = true;

            if (_eKeyIsPressed && ks.IsKeyUp(Keys.E))
            {
                _eKeyIsPressed = false;
                var food = interactableOnFocus as IEatable;
                if (food != null)
                    Eat(food);
            }



            if (_force.X + _force.Y == 0)
            {
                SourceRect = new Rectangle(0, 0, CurrentAnim.SpriteWidth, CurrentAnim.Animation.Height);
            }
            else
            {
                if (_force.Y < 0)
                {
                    CurrentAnim = UpWalk;
                }
                if (_force.Y > 0)
                {
                    CurrentAnim = DownWalk;
                }
                if (_force.X > 0)
                {
                    CurrentAnim = RightWalk;
                }
                if (_force.X < 0)
                {
                    CurrentAnim = LeftWalk;
                }
                SourceRect = CurrentAnim.Animate(gameTime, Delay, ObjectMovingType);
            }

            if (GetCenterPosition().X > Graphics.PreferredBackBufferWidth && _cowGameScreen.WorldOnFocus.RightWorld != null)
            {
                _cowGameScreen.ChangeWorld(this, Direction.Right);
                _interactableEntities = _cowGameScreen.WorldOnFocus.InteractableEntities;
                Body = BodyFactory.CreateRectangle(_cowGameScreen.WorldOnFocus, 0.54f, 0.15f, 0, new Vector2((float)GetCenterPosition().X / 100, (float)(GetPosition().Y + GetPosition().Height) / 100));
                Body.BodyType = BodyType.Dynamic;
                Body.CollisionCategories = Category.All;
                Body.CollidesWith = Category.All;
            }

            if (GetCenterPosition().X < 0 && _cowGameScreen.WorldOnFocus.LeftWorld != null)
            {
                _cowGameScreen.ChangeWorld(this, Direction.Left);
                _interactableEntities = _cowGameScreen.WorldOnFocus.InteractableEntities;
                Body = BodyFactory.CreateRectangle(_cowGameScreen.WorldOnFocus, 0.54f, 0.15f, 0, new Vector2((float)Graphics.PreferredBackBufferWidth / 100, (float)(GetPosition().Y + GetPosition().Height) / 100));
                Body.BodyType = BodyType.Dynamic;
                Body.CollisionCategories = Category.All;
                Body.CollidesWith = Category.All;
            }

            _previousFocusInteractables = new HashSet<IInteractable>(interactables);
            _previousInteractableOnFocus = interactableOnFocus;


        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            Debug.WriteLine("Cow " + GetPosition().Y + GetPosition().Height);
            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), SourceRect, Color.White);
        }

        private KeyboardState _input;
        private Vector2 _force = new Vector2(0, 0);
        public void HandleUserAgent()
        {
            _force = new Vector2(0, 0);

            const float forceAmountX = 1.5f;
            const float forceAmountY = 1f;

            _input = Keyboard.GetState();

            if (_input.IsKeyDown(Keys.D))
            {
                _force += new Vector2(forceAmountX, 0);
            }
            if (_input.IsKeyDown(Keys.A))
            {
                _force += new Vector2(-forceAmountX, 0);
            }
            if (_input.IsKeyDown(Keys.W))
            {
                _force += new Vector2(0, -forceAmountY);
            }
            if (_input.IsKeyDown(Keys.S))
            {
                _force += new Vector2(0, forceAmountY);
            }

            if (_input.IsKeyDown(Keys.Space))
            {
                if (Boost > 0)
                {
                    Boost -= 0.01f;
                    Delay = 150f;
                    _force *= 1.7f;

                }
                else
                {
                    Delay = 180f;
                    _force *= 1.2f;
                }

            }
            if (_input.IsKeyUp(Keys.Space) && Boost < 1f)
            {
                Boost += 0.003f;
                Delay = 200f;
                if (Boost > 1)
                    Boost = 1;
            }
            Debug.WriteLine(Boost);

            Body.Move(_force);
            Body.ApplyForce(_force);
        }

        public bool IsSelected { get; set; }
    }
}
