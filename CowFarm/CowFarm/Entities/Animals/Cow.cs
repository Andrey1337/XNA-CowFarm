using System;
using System.Collections.Generic;
using System.Linq;
using CowFarm.DrowingSystem;
using CowFarm.Entities.Items;
using CowFarm.Enums;
using CowFarm.Interfaces;
using CowFarm.ScreenSystem;
using CowFarm.StatusBars;
using CowFarm.TileEntities;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using World = CowFarm.Worlds.World;

namespace CowFarm.Entities.Animals
{
    public sealed class Cow : Animal, IDynamic
    {
        public Inventory.Inventory Inventory { get; }
        public List<StatusBar> ListBars { get; }
        public CraftPanel CraftPanel { get; }

        public float Boost { get; private set; }
        public float StarvePoint { get; private set; }

        private IEnumerable<Entity> _interactableList;
        private IEnumerable<Entity> _attackList;

        public Cow(CowGameScreen cowGameScreen, World world, Vector2 position)
        : base(cowGameScreen, world,
              new Rectangle((int)position.X, (int)position.Y, 54, 49),
              new AnimatedSprites(cowGameScreen.GameTextures["cowRightWalk"], 3, 16),
              new AnimatedSprites(cowGameScreen.GameTextures["cowLeftWalk"], 3, 16),
              new AnimatedSprites(cowGameScreen.GameTextures["cowUpWalk"], 3, 16),
              new AnimatedSprites(cowGameScreen.GameTextures["cowDownWalk"], 3, 16))
        {
            Inventory = new Inventory.Inventory(cowGameScreen);
            CraftPanel = new CraftPanel(cowGameScreen);
            ListBars = new List<StatusBar>
            {
                new HealthBar(cowGameScreen, this),
                new FoodBar(cowGameScreen, this),
                new SprintBar(cowGameScreen, this)
            };
            Delay = 200f;
            HealthPoint = 100;
            StarvePoint = 100;
            CurrentWorld = world;
            Boost = 1;

            CurrentWorld = world;

            _interactableList = new List<Entity>();
            _attackList = new List<Entity>();

            _previousFocusInteractables = new HashSet<Entity>();
            _previousFocusAttackables = new HashSet<Entity>();

            Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2((float)DestRect.X / 100, (float)DestRect.Y / 100));
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;

            CurrentAnim = LeftWalk;
            Body.BodyTypeName = "cow";

            CowGameScreen.WordlsList.ForEach(worldInList => worldInList.ContactManager.Nearby += NearbyCow);
        }

        private void NearbyCow(object sender, NearbyEventArg nearby)
        {
            if (!nearby.Dictionary.ContainsKey(BodyId))
                return;

            _interactableList = nearby.Dictionary[BodyId]
                .Where(body => CurrentWorld.InteractablesDictionary.ContainsKey(body.BodyId))
                .Select(body => CurrentWorld.InteractablesDictionary[body.BodyId]);

            _attackList = nearby.Dictionary[BodyId]
                .Where(body => CurrentWorld.AttackableDictionary.ContainsKey(body.BodyId))
                .Select(body => CurrentWorld.AttackableDictionary[body.BodyId]);
        }

        private List<Entity> InteractableSortCowNearby()
        {
            if (_interactableList == null)
                return null;

            List<Entity> canBeOnFocusList = new List<Entity>();
            if (CurrentAnim == RightWalk)
            {
                canBeOnFocusList.AddRange(_interactableList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + (float)(GetPosition().Width / 1.5), GetPosition().Y + GetPosition().Height / 2),
                ((IInteractable)entity).GetInteractablePosition()) < 50
                && GetPosition().X + GetPosition().Width / 1.1 < entity.GetPosition().X + GetPosition().Width / 2
                && Vector2.Distance(new Vector2(0, GetPosition().Y + (float)(GetPosition().Height / 2)), new Vector2(0, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 20));
            }
            if (CurrentAnim == LeftWalk)
            {
                canBeOnFocusList.AddRange(_interactableList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + (float)(GetPosition().Width * 0.5), GetPosition().Y + GetPosition().Height / 2),
                ((IInteractable)entity).GetInteractablePosition()) < 50
                && Vector2.Distance(new Vector2(0, GetPosition().Y + (float)(GetPosition().Height / 2)), new Vector2(0, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 20
                && GetPosition().X + GetPosition().Width * 0.1 > entity.GetPosition().X));
            }
            if (CurrentAnim == DownWalk)
            {
                canBeOnFocusList.AddRange(_interactableList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height),
                ((IInteractable)entity).GetInteractablePosition()) < 25
                && GetPosition().Y + GetPosition().Height < entity.GetPosition().Y + GetPosition().Height));
            }
            if (CurrentAnim == UpWalk)
            {
                canBeOnFocusList.AddRange(_interactableList.Where(entity => ((IInteractable)entity).CanInteract
                && Vector2.Distance(new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height / 2),
                ((IInteractable)entity).GetInteractablePosition()) < 25
                && GetPosition().Y + GetPosition().Height > entity.GetPosition().Y + entity.GetPosition().Height));
            }

            return canBeOnFocusList;
        }

        private List<Entity> AttackableSortCowNearby()
        {
            if (_attackList == null)
                return null;

            List<Entity> canBeOnAttackList = new List<Entity>();
            if (CurrentAnim == RightWalk)
            {
                canBeOnAttackList.AddRange(_attackList
                    .Where(entity =>
                    Vector2.Distance(new Vector2(GetPosition().X + (float)(GetPosition().Width / 1.5), GetPosition().Y + GetPosition().Height / 2),
                    ((IAttackable)entity).GetAttackPosition()) < 50
                    && GetPosition().X + GetPosition().Width / 1.1 < entity.GetPosition().X + GetPosition().Width / 2
                    && Vector2.Distance(new Vector2(0, GetPosition().Y + (float)(GetPosition().Height / 2)),
                    new Vector2(0, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 20));
            }
            if (CurrentAnim == LeftWalk)
            {
                canBeOnAttackList.AddRange(_attackList
                    .Where(entity =>
                    Vector2.Distance(new Vector2(GetPosition().X + (float)(GetPosition().Width * 0.5), GetPosition().Y + GetPosition().Height / 2),
                        ((IAttackable)entity).GetAttackPosition()) < 50
                    && Vector2.Distance(new Vector2(0, GetPosition().Y + (float)(GetPosition().Height / 2)),
                    new Vector2(0, entity.GetPosition().Y + entity.GetPosition().Height / 2)) < 20
                    && GetPosition().X + GetPosition().Width * 0.1 > entity.GetPosition().X));
            }
            if (CurrentAnim == DownWalk)
            {
                canBeOnAttackList.AddRange(_attackList
                    .Where(entity =>
                    Vector2.Distance(new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height),
                    ((IAttackable)entity).GetAttackPosition()) < 25
                    && GetPosition().Y + GetPosition().Height < entity.GetPosition().Y + GetPosition().Height));
            }
            if (CurrentAnim == UpWalk)
            {
                canBeOnAttackList.AddRange(_attackList
                    .Where(entity =>
                    Vector2.Distance(new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height / 2),
                    ((IAttackable)entity).GetAttackPosition()) < 25
                    && GetPosition().Y + GetPosition().Height > entity.GetPosition().Y + entity.GetPosition().Height));
            }

            return canBeOnAttackList;
        }

        public override void Eat(IEatable food)
        {
            StarvePoint += food.Satiety;
            if (StarvePoint > 100f)
                StarvePoint = 100f;
            food.Eat();
        }



        private int _focusNumber;

        private KeyboardState _prevKeyState;

        public override void Update(GameTime gameTime)
        {
            if (StarvePoint <= 0)
                HealthPoint -= 0.03f;

            if (HealthPoint <= 0)
                CowGameScreen.FinishGame();

            KeyboardState ks = Keyboard.GetState();

            HandleUserAgent(gameTime);
            HandleInventory();
            HandleInteractables(ks);
            HandleAttackable(ks);
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
                SourceRect = CurrentAnim.Animate(gameTime, ObjectMovingType, Delay);
            }

            if (GetCenterPosition().X > CowGameScreen.Graphics.PreferredBackBufferWidth && CowGameScreen.WorldOnFocus.RightWorld != null)
            {
                CowGameScreen.ChangeWorld(this, Direction.Right);
            }

            if (GetCenterPosition().X < 0 && CowGameScreen.WorldOnFocus.LeftWorld != null)
            {
                CowGameScreen.ChangeWorld(this, Direction.Left);
            }

            _prevKeyState = ks;
        }

        private Vector2 GetCenterPosition()
        {
            return new Vector2(GetPosition().X + GetPosition().Width / 2, GetPosition().Y + GetPosition().Height / 2);
        }

        private HashSet<Entity> _previousFocusInteractables;
        private IInteractable _previousInteractableOnFocus;
        private void HandleInteractables(KeyboardState ks)
        {
            List<Entity> canBeOnFocusList = InteractableSortCowNearby();

            if (canBeOnFocusList.Count > 0)
            {
                List<Entity> interactablesList = canBeOnFocusList.ToList();

                HashSet<Entity> hash = new HashSet<Entity>(canBeOnFocusList);

                IInteractable interactableOnFocus = null;

                if (!hash.SequenceEqual(_previousFocusInteractables))
                {
                    _focusNumber = 0;
                }
                if (_focusNumber < interactablesList.Count && interactablesList[_focusNumber] != null)
                {
                    var interactable = interactablesList[_focusNumber] as IInteractable;
                    if (interactable != null)
                    {
                        interactable.OnFocus = true;
                        interactableOnFocus = interactable;
                    }
                }

                if (_previousInteractableOnFocus != null &&
                    (interactableOnFocus != null && interactableOnFocus != _previousInteractableOnFocus ||
                     _focusNumber != 0 && _focusNumber == interactablesList.Count))
                    _previousInteractableOnFocus.OnFocus = false;

                foreach (var entity in _previousFocusInteractables.Where(interacteble => !hash.Contains(interacteble)).Select(interactable => (IInteractable)interactable))
                {
                    entity.OnFocus = false;
                }


                if (ks.IsKeyDown(Keys.Tab) && _prevKeyState.IsKeyUp(Keys.Tab))
                {
                    if (_focusNumber >= interactablesList.Count)
                    {
                        _focusNumber = 0;
                    }
                    else
                    {
                        _focusNumber++;
                    }
                }

                if (ks.IsKeyDown(Keys.E) && _prevKeyState.IsKeyUp(Keys.E))
                {
                    var food = interactableOnFocus as IEatable;
                    if (food != null)
                        Eat(food);
                }

                if (ks.IsKeyDown(Keys.F) && _prevKeyState.IsKeyUp(Keys.F) && interactableOnFocus != null)
                {
                    interactableOnFocus.Interact();

                    var item = interactableOnFocus as Item;
                    if (item != null)
                    {
                        Inventory.Add(item);
                    }
                }

                _previousFocusInteractables = hash;
                _previousInteractableOnFocus = interactableOnFocus;
            }
            else
            {
                _focusNumber = 0;
                if (_previousInteractableOnFocus != null)
                    _previousInteractableOnFocus.OnFocus = false;

            }
        }

        private HashSet<Entity> _previousFocusAttackables;
        private IAttackable _previousAttackableOnFocus;

        private void HandleAttackable(KeyboardState ks)
        {
            List<Entity> canBeAttackedList = AttackableSortCowNearby();
            if (canBeAttackedList.Count > 0)
            {
                List<Entity> attackableList = canBeAttackedList.ToList();

                HashSet<Entity> hash = new HashSet<Entity>(canBeAttackedList);

                IAttackable attackableOnFocus = null;

                if (!hash.SequenceEqual(_previousFocusInteractables))
                {
                    _focusNumber = 0;
                }
                if (_focusNumber < attackableList.Count && attackableList[_focusNumber] != null)
                {
                    var interactable = attackableList[_focusNumber] as IAttackable;
                    if (interactable != null)
                    {
                        interactable.OnFocus = true;
                        attackableOnFocus = interactable;
                    }
                }

                if (_previousAttackableOnFocus != null &&
                    (attackableOnFocus != null && attackableOnFocus != _previousAttackableOnFocus ||
                     _focusNumber != 0 && _focusNumber == attackableList.Count))
                    _previousAttackableOnFocus.OnFocus = false;

                foreach (var attackable in _previousFocusAttackables.Where(attackable => !hash.Contains(attackable)).Select(attackable => (IAttackable)attackable))
                {
                    attackable.OnFocus = false;
                }
                if (ks.IsKeyDown(Keys.R) && _prevKeyState.IsKeyUp(Keys.R) && attackableOnFocus != null)
                {
                    attackableOnFocus.GetDamage(10);
                }

                _previousAttackableOnFocus = attackableOnFocus;
                _previousFocusAttackables = hash;
            }
            else
            {
                _focusNumber = 0;
                if (_previousAttackableOnFocus != null)
                    _previousAttackableOnFocus.OnFocus = false;
            }
        }

        private void HandleInventory()
        {
            var ks = Keyboard.GetState();

            for (int i = 0; i < 10; i++)
            {
                if (ks.IsKeyDown((Keys)i + 49) && _prevKeyState.IsKeyUp((Keys)i + 49))
                {
                    Inventory.ItemOnFocus(i);
                }
            }
            if (ks.IsKeyDown(Keys.G) && _prevKeyState.IsKeyUp(Keys.G))
            {
                Inventory.Drop(CurrentWorld, ItemDropPos());
            }
        }

        private Vector2 ItemDropPos()
        {
            var dropPos = new Vector2(GetPosition().X, GetPosition().Y);
            if (CurrentAnim == LeftWalk)
            {
                dropPos.X -= 10;
                dropPos.Y += (float)GetPosition().Height / 2;
            }
            if (CurrentAnim == RightWalk)
            {
                dropPos.X += GetPosition().Width + 10;
                dropPos.Y += (float)GetPosition().Height / 2;
            }
            if (CurrentAnim == UpWalk)
            {
                dropPos.X += (float)GetPosition().Width / 2;
                dropPos.Y += 10;
            }
            if (CurrentAnim == DownWalk)
            {
                dropPos.X += (float)GetPosition().Width / 2;
                dropPos.Y += GetPosition().Height;
            }

            return dropPos;
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
                    //_timeInSprint += gameTime.ElapsedGameTime;
                    Delay = 150f;
                    Boost -= 0.01f;
                    _force *= 2f;
                    if (StarvePoint > 0)
                        StarvePoint -= 0.01f;
                }
                else
                {
                    //_timeInSprint = TimeSpan.Zero;
                    Delay = 180f;
                    _force *= 1.3f;
                    if (StarvePoint > 0)
                        StarvePoint -= 0.008f;
                }
            }
            else
            {
                //_timeInSprint = TimeSpan.Zero;
                Boost += 0.003f;
                Delay = 200f;
                if (Boost > 1)
                    Boost = 1;
                StarvePoint -= 0.006f;
            }

            Body.Move(_force);
            Body.ApplyForce(_force);
        }
        public bool IsSelected { get; set; }
        public void ChangeWorld(World world, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2(0, Body.Position.Y));
                    break;

                case Direction.Left:
                    Body = BodyFactory.CreateRectangle(world, 0.54f, 0.15f, 0, new Vector2((float)CowGameScreen.Graphics.PreferredBackBufferWidth / 100, Body.Position.Y));
                    break;
            }

            Body.BodyTypeName = "cow";
            Body.BodyType = BodyType.Dynamic;
            Body.CollisionCategories = Category.All & ~Category.Cat10;
            Body.CollidesWith = Category.All & ~Category.Cat10;
            CurrentWorld = world;
        }
    }
}