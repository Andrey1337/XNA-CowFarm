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
using FarseerPhysics.Dynamics.Contacts;
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
        private HashSet<Entity> _previousFocusInteractables;

        private readonly Dictionary<int, Entity> _interactablesDictionary;

        private IEnumerable<Entity> _savedList;

        private float _delay = 200f;

        private readonly CowGameScreen _cowGameScreen;

        public float Boost;

        private TimeSpan _timeInSprint;

        public Cow(CowGameScreen cowGameScreen, World world, Rectangle destRect, Dictionary<string, Texture2D> gameTextures)
        : base(world, destRect,
              new AnimatedSprites(gameTextures["cowRightWalk"], 3, 16),
              new AnimatedSprites(gameTextures["cowLeftWalk"], 3, 16),
              new AnimatedSprites(gameTextures["cowUpWalk"], 3, 16),
              new AnimatedSprites(gameTextures["cowDownWalk"], 3, 16))
        {
            Boost = 1;
            _savedList = new List<Entity>();
            _previousFocusInteractables = new HashSet<Entity>();
            _interactablesDictionary = world.InteractablesDictionary;

            _timeInSprint = TimeSpan.Zero;

            _cowGameScreen = cowGameScreen;

            Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
            _focusNumber = 0;
            this.CurrentAnim = RightWalk;

            Body.BodyTypeName = "cow";
            _cowGameScreen.WorldOnFocus.ContactManager.Nearby += NearbyCow;
        }

        private void NearbyCow(object sender, NearbyEventArg nearby)
        {
            if (!nearby.Dictionary.ContainsKey(BodyId))
            {
                return;
            }
            _savedList = (from body in nearby.Dictionary[BodyId]
                          where _interactablesDictionary.ContainsKey(body.BodyId)
                          select _interactablesDictionary[body.BodyId]);

            SortCowNearby();
            _savedList.ToList().ForEach(entity => Debug.WriteLine(entity.BodyTypeName));
            Debug.WriteLine("-------------");
        }

        private List<Entity> SortCowNearby()
        {
            List<Entity> list = new List<Entity>();
            if (CurrentAnim == RightWalk)
            {
                list.AddRange(_savedList.Where(entity => entity.GetPosition().X > GetPosition().X + GetPosition().Width / 2));
            }
            if (CurrentAnim == LeftWalk)
            {
                list.AddRange(_savedList.Where(entity => entity.GetPosition().X + entity.GetPosition().Width < GetPosition().X + GetPosition().Width / 2));
                Debug.WriteLine("TRUE");
            }
            if (CurrentAnim == DownWalk)
            {
                list.AddRange(_savedList.Where(entity => entity.GetPosition().Y + entity.GetPosition().Height / 2 + entity.GetPosition().Height / 4 > GetPosition().Y + GetPosition().Height / 2));
            }
            if (CurrentAnim == UpWalk)
            {
                list.AddRange(_savedList.Where(entity => entity.GetPosition().Y + entity.GetPosition().Height < GetPosition().Y + GetPosition().Height / 2));
            }
            return list;
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

        //private Rectangle _rectangle;

        //public IEnumerable<IInteractable> NearbyInteractables()
        //{
        //    List<IInteractable> interactableList = new List<IInteractable>();
        //    if (CurrentAnim == RightWalk)
        //    {
        //        _rectangle = new Rectangle(GetPosition().X + CurrentAnim.SpriteWidth - 20
        //            , GetPosition().Y + CurrentAnim.SpriteHeight / 4
        //            , 70
        //            , GetPosition().Height + CurrentAnim.SpriteHeight / 3);


        //    }
        //    if (CurrentAnim == LeftWalk)
        //    {
        //        _rectangle = new Rectangle(GetPosition().X + 20 - 70
        //            , GetPosition().Y + CurrentAnim.SpriteHeight / 4
        //            , 70
        //            , GetPosition().Height + CurrentAnim.SpriteHeight / 3);
        //    }
        //    if (CurrentAnim == UpWalk)
        //    {
        //        _rectangle = new Rectangle(GetPosition().X - 5, GetPosition().Y - 50, GetPosition().Width + 5 * 2,
        //            GetPosition().Height + 40);
        //    }
        //    if (CurrentAnim == DownWalk)
        //    {
        //        _rectangle = new Rectangle(GetPosition().X - 5, GetPosition().Y + GetPosition().Height,
        //            GetPosition().Width + 5 * 2, GetPosition().Height);
        //    }

        //    if (CurrentAnim == RightWalk)
        //    {
        //        int middleY = _rectangle.Y + _rectangle.Height / 2;

        //        for (int i = _rectangle.X; i < _rectangle.X + _rectangle.Width; i++)
        //        {
        //            if (i < 0 || i >= _interactableEntities.GetLength(0))
        //                continue;
        //            for (int j = 0; j < +_rectangle.Height / 2; j++)
        //            {
        //                if (middleY - j < 0 || middleY + j >= _interactableEntities.GetLength(1))
        //                    continue;

        //                if (_interactableEntities[i, middleY - j] != null)
        //                    foreach (var interactable in _interactableEntities[i, middleY - j])
        //                    {
        //                        AddInteractableToList(interactable, interactableList);
        //                    }
        //                if (_interactableEntities[i, middleY + j] != null)
        //                    foreach (var interactable in _interactableEntities[i, middleY + j])
        //                    {
        //                        AddInteractableToList(interactable, interactableList);
        //                    }
        //            }
        //        }
        //    }
        //    if (CurrentAnim == LeftWalk)
        //    {
        //        int middleY = _rectangle.Y + _rectangle.Height / 2;
        //        for (int i = _rectangle.X + _rectangle.Width; i > _rectangle.X; i--)
        //        {
        //            if (i < 0 || i >= _interactableEntities.GetLength(0))
        //                continue;
        //            for (int j = 0; j < +_rectangle.Height / 2; j++)
        //            {
        //                if (middleY - j < 0 || middleY + j >= _interactableEntities.GetLength(1))
        //                    continue;

        //                if (_interactableEntities[i, middleY + j] != null)
        //                    foreach (var interactable in _interactableEntities[i, middleY + j])
        //                    {
        //                        AddInteractableToList(interactable, interactableList);
        //                    }

        //                if (_interactableEntities[i, middleY - j] != null)
        //                    foreach (var interactable in _interactableEntities[i, middleY - j])
        //                    {
        //                        AddInteractableToList(interactable, interactableList);
        //                    }
        //            }
        //        }
        //    }
        //    if (CurrentAnim == UpWalk)
        //    {
        //        int middleX = _rectangle.X + _rectangle.Width / 2;
        //        for (int j = _rectangle.Y + _rectangle.Height; j > _rectangle.Y; j--)
        //        {
        //            if (j < 0 || j >= _interactableEntities.GetLength(1))
        //                continue;
        //            for (int i = 0; i < _rectangle.Width / 2; i++)
        //            {
        //                if (middleX - i < 0 || middleX + i >= _interactableEntities.GetLength(0))
        //                    continue;
        //                if (_interactableEntities[middleX + i, j] != null)
        //                    foreach (var interactable in _interactableEntities[middleX + i, j])
        //                    {
        //                        AddInteractableToList(interactable, interactableList);
        //                    }

        //                if (_interactableEntities[middleX - i, j] != null)
        //                    foreach (var interactable in _interactableEntities[middleX - i, j])
        //                    {
        //                        AddInteractableToList(interactable, interactableList);
        //                    }
        //            }
        //        }
        //    }

        //    if (CurrentAnim == DownWalk)
        //    {
        //        int middleX = _rectangle.X + _rectangle.Width / 2;
        //        for (int j = _rectangle.Y; j < _rectangle.Y + +_rectangle.Height; j++)
        //        {
        //            if (j < 0 || j >= _interactableEntities.GetLength(1))
        //                continue;
        //            for (int i = 0; i < _rectangle.Width / 2; i++)
        //            {
        //                if (middleX - i < 0 || middleX + i >= _interactableEntities.GetLength(0))
        //                    continue;
        //                if (_interactableEntities[middleX + i, j] != null)
        //                    foreach (var interactable in _interactableEntities[middleX + i, j])
        //                    {7
        //                        AddInteractableToList(interactable, interactableList);
        //                    }

        //                if (_interactableEntities[middleX - i, j] != null)
        //                    foreach (var interactable in _interactableEntities[middleX - i, j])
        //                    {
        //                        AddInteractableToList(interactable, interactableList);
        //                    }
        //            }
        //        }
        //    }

        //    return interactableList;
        //}

        //private void AddInteractableToList(IInteractable interactable, List<IInteractable> interactableList)
        //{
        //    if (!interactable.CanInteract) return;
        //    var grass = interactable as Grass;
        //    if (grass != null &&
        //        Vector2.Distance(GetCenterPosition(), grass.GetInteractablePosition()) <
        //        70)
        //    {
        //        if ((CurrentAnim == RightWalk || CurrentAnim == LeftWalk || CurrentAnim == DownWalk) &&
        //            Vector2.Distance(GetCenterPosition(), grass.GetInteractablePosition()) < 70)
        //        {
        //            interactableList.Add(interactable);
        //            return;
        //        }

        //        if (Vector2.Distance(GetCenterPosition(), grass.GetInteractablePosition()) < 40)
        //            interactableList.Add(interactable);
        //    }
        //}

        public override void Load(ContentManager content)
        {

        }

        private IInteractable _previousInteractableOnFocus;

        private int _focusNumber;

        private bool _tabKeyIsPressed;
        private bool _eKeyIsPressed;

        public override void Update(GameTime gameTime)
        {
            SortCowNearby();
            HandleUserAgent(gameTime);
            KeyboardState ks = Keyboard.GetState();


            if (_savedList != null)
            {
                //Debug.WriteLine("Helo");
                List<Entity> interactablesList = _savedList.ToList();

                HashSet<Entity> hash = new HashSet<Entity>(_savedList);

                IInteractable interactableOnFocus = null;

                if (_focusNumber < interactablesList.Count && interactablesList[_focusNumber] != null)
                {
                    var interactable = interactablesList[_focusNumber] as IInteractable;
                    if (interactable != null)
                    {
                        interactable.OnFocus = true;
                        interactableOnFocus = interactable;
                        Debug.WriteLine(interactableOnFocus);
                    }

                }

                if (_previousInteractableOnFocus != null &&
                    (interactableOnFocus != null && interactableOnFocus != _previousInteractableOnFocus ||
                     _focusNumber != 0 && _focusNumber == interactablesList.Count))
                    _previousInteractableOnFocus.OnFocus = false;

                foreach (var entity in _previousFocusInteractables.Where(
                    interacteble => !hash.Contains(interacteble)))
                {
                    var interactable = entity as IInteractable;
                    if (interactable != null) interactable.OnFocus = false;
                }


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

                _previousFocusInteractables = new HashSet<Entity>(_savedList);
                _previousInteractableOnFocus = interactableOnFocus;

            }

            if (_force == Vector2.Zero)
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
                SourceRect = CurrentAnim.Animate(gameTime, _delay, ObjectMovingType);
            }

            if (GetCenterPosition().X > Graphics.PreferredBackBufferWidth && _cowGameScreen.WorldOnFocus.RightWorld != null)
            {
                _cowGameScreen.WorldOnFocus.RemoveBody(this.Body);
                _cowGameScreen.ChangeWorld(this, Direction.Right);
                //TODO: FIX
                //_interactableEntities = _cowGameScreen.WorldOnFocus.InteractableEntities;
                Body = BodyFactory.CreateRectangle(_cowGameScreen.WorldOnFocus, 0.54f, 0.15f, 0, new Vector2((float)GetCenterPosition().X / 100, (float)(GetPosition().Y + GetPosition().Height) / 100));
                Body.BodyType = BodyType.Dynamic;
                Body.CollisionCategories = Category.All & ~Category.Cat10;
                Body.CollidesWith = Category.All & ~Category.Cat10;
            }

            if (GetCenterPosition().X < 0 && _cowGameScreen.WorldOnFocus.LeftWorld != null)
            {
                _cowGameScreen.WorldOnFocus.RemoveBody(this.Body);
                _cowGameScreen.ChangeWorld(this, Direction.Left);
                //_interactableEntities = _cowGameScreen.WorldOnFocus.InteractableEntities;
                Body = BodyFactory.CreateRectangle(_cowGameScreen.WorldOnFocus, 0.54f, 0.15f, 0, new Vector2((float)Graphics.PreferredBackBufferWidth / 100, (float)(GetPosition().Y + GetPosition().Height) / 100));
                Body.BodyType = BodyType.Dynamic;
                Body.CollisionCategories = Category.All & ~Category.Cat10;
                Body.CollidesWith = Category.All & ~Category.Cat10;
            }


        }

        public bool RunningAlreadyInSprint()
        {
            return _timeInSprint > TimeSpan.FromSeconds(0.3);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentAnim.Animation, GetPosition(), SourceRect, Color.White);
        }

        private KeyboardState _input;
        private Vector2 _force = new Vector2(0, 0);
        public void HandleUserAgent(GameTime gameTime)
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

            if (_input.IsKeyDown(Keys.Space) && _force != Vector2.Zero)
            {
                if (Boost > 0)
                {
                    _timeInSprint += gameTime.ElapsedGameTime;
                    _delay = 150f;
                    Boost -= 0.01f;
                    _force *= 2f;
                }
                else
                {
                    _timeInSprint = TimeSpan.Zero;
                    _delay = 180f;
                    _force *= 1.3f;
                }
            }
            else
            {
                _timeInSprint = TimeSpan.Zero;
                Boost += 0.003f;
                _delay = 200f;
                if (Boost > 1)
                    Boost = 1;
            }

            Body.Move(_force);
            Body.ApplyForce(_force);
        }

        public bool IsSelected { get; set; }
    }
}
