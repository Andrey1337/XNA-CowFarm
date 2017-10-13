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

        private Dictionary<int, Entity> _interactablesDictionary;

        private IEnumerable<Entity> _nearbyList;

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
            _cowGameScreen = cowGameScreen;

            Boost = 1;
            _nearbyList = new List<Entity>();
            _canBeOnFocusList = _nearbyList.ToList();
            _previousFocusInteractables = new HashSet<Entity>();
            _interactablesDictionary = world.InteractablesDictionary;

            _timeInSprint = TimeSpan.Zero;

            Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2((float)destRect.X / 100, (float)destRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;

            CurrentAnim = LeftWalk;
            Body.BodyTypeName = "cow";

            _cowGameScreen.WordlsList.ForEach(worldInList => worldInList.ContactManager.Nearby += NearbyCow);
        }

        private void NearbyCow(object sender, NearbyEventArg nearby)
        {
            if (!nearby.Dictionary.ContainsKey(BodyId))
                return;
            _nearbyList = (from body in nearby.Dictionary[BodyId]
                           where _interactablesDictionary.ContainsKey(body.BodyId)
                           select _interactablesDictionary[body.BodyId]);

            //_nearbyList.ToList().ForEach(entity => Debug.WriteLine(entity.BodyTypeName));
        }

        private List<Entity> SortCowNearby()
        {
            if (_nearbyList == null)
                return null;
            List<Entity> canBeOnFocusList = new List<Entity>();
            if (CurrentAnim == RightWalk)
            {
                //_nearbyList.ToList().ForEach(entity => Debug.WriteLine(entity.BodyTypeName));
                canBeOnFocusList.AddRange(_nearbyList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + (float)(GetPosition().Width / 1.5), GetPosition().Y + GetPosition().Height / 2), new Vector2(entity.GetPosition().X + entity.GetPosition().Width / 2, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 50
                && GetPosition().X + GetPosition().Width / 1.1 < entity.GetPosition().X + GetPosition().Width / 2
                && Vector2.Distance(new Vector2(0, GetPosition().Y + (float)(GetPosition().Height / 2)), new Vector2(0, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 20));
                canBeOnFocusList.ToList().ForEach(entity => Debug.WriteLine(entity.BodyTypeName));
                Debug.WriteLine("/////");
            }
            if (CurrentAnim == LeftWalk)
            {
                canBeOnFocusList.AddRange(_nearbyList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + (float)(GetPosition().Width * 0.5), GetPosition().Y + GetPosition().Height / 2), new Vector2(entity.GetPosition().X + entity.GetPosition().Width / 2, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 50
                && GetPosition().X + GetPosition().Width * 0.1 > entity.GetPosition().X
                && Vector2.Distance(new Vector2(0, GetPosition().Y + (float)(GetPosition().Height / 2)), new Vector2(0, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 20));
            }
            if (CurrentAnim == DownWalk)
            {
                canBeOnFocusList.AddRange(_nearbyList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height / 2), new Vector2(entity.GetPosition().X + entity.GetPosition().Width / 2, entity.GetPosition().Y + (float)(entity.GetPosition().Height / 2))) < 25
                && GetPosition().Y + GetPosition().Height < entity.GetPosition().Y + GetPosition().Height
                ));
            }
            if (CurrentAnim == UpWalk)
            {
                canBeOnFocusList.AddRange(_nearbyList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height / 2), new Vector2(entity.GetPosition().X + entity.GetPosition().Width / 2, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 35
                && GetPosition().Y + GetPosition().Height + 2 > entity.GetPosition().Y + GetPosition().Height));
            }
            return canBeOnFocusList;
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


        public override void Load(ContentManager content)
        {

        }

        private IInteractable _previousInteractableOnFocus;

        private int _focusNumber;

        private bool _tabKeyIsPressed;
        private bool _eKeyIsPressed;
        private List<Entity> _canBeOnFocusList;
        public override void Update(GameTime gameTime)
        {
            HandleUserAgent(gameTime);
            KeyboardState ks = Keyboard.GetState();
            _canBeOnFocusList = SortCowNearby();

            if (_canBeOnFocusList.Count != 0)
            {
                
                List<Entity> interactablesList = _canBeOnFocusList.ToList();

                HashSet<Entity> hash = new HashSet<Entity>(_canBeOnFocusList);

                IInteractable interactableOnFocus = null;

                if (_focusNumber < interactablesList.Count && interactablesList[_focusNumber] != null)
                {
                    var interactable = interactablesList[_focusNumber] as IInteractable;
                    if (interactable != null)
                    {
                        interactable.OnFocus = true;
                        interactableOnFocus = interactable;
                    }
                }

                if (_previousInteractableOnFocus != null && (interactableOnFocus != null && interactableOnFocus != _previousInteractableOnFocus || _focusNumber != 0 && _focusNumber == interactablesList.Count))
                    _previousInteractableOnFocus.OnFocus = false;

                foreach (var entity in _previousFocusInteractables.Where(interacteble => !hash.Contains(interacteble)))
                {
                    var interactable = entity as IInteractable;
                    if (interactable != null) interactable.OnFocus = false;
                }

                if (ks.IsKeyDown(Keys.Tab))
                    _tabKeyIsPressed = true;

                if (ks.IsKeyDown(Keys.E))
                    _eKeyIsPressed = true;

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

                if (_eKeyIsPressed && ks.IsKeyUp(Keys.E))
                {
                    _eKeyIsPressed = false;
                    var food = interactableOnFocus as IEatable;
                    if (food != null)
                        Eat(food);
                }

                _previousFocusInteractables = new HashSet<Entity>(_canBeOnFocusList);
                _previousInteractableOnFocus = interactableOnFocus;
            }
            else
            {
                if (_previousInteractableOnFocus != null)
                    _previousInteractableOnFocus.OnFocus = false;
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

                _cowGameScreen.ChangeWorld(this, Direction.Right);
                _interactablesDictionary = _cowGameScreen.WorldOnFocus.InteractablesDictionary;
                Body = BodyFactory.CreateRectangle(_cowGameScreen.WorldOnFocus, 0.54f, 0.15f, 0, new Vector2((float)GetCenterPosition().X / 100, (float)(GetPosition().Y + GetPosition().Height) / 100));
                Body.BodyType = BodyType.Dynamic;
                Body.CollisionCategories = Category.All & ~Category.Cat10;
                Body.CollidesWith = Category.All & ~Category.Cat10;
            }

            if (GetCenterPosition().X < 0 && _cowGameScreen.WorldOnFocus.LeftWorld != null)
            {

                _cowGameScreen.ChangeWorld(this, Direction.Left);
                _interactablesDictionary = _cowGameScreen.WorldOnFocus.InteractablesDictionary;
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
