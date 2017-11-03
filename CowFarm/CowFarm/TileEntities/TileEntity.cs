using System;
using System.Collections.Generic;
using System.Linq;
using CowFarm.Containers;
using CowFarm.Entities.Items;
using CowFarm.ScreenSystem;
using CowFarm.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.TileEntities
{
    public abstract class TileEntity
    {
        protected Container[,] Containers;
        protected readonly CowGameScreen CowGameScreen;
        protected readonly Dictionary<Type[,], Type> RecipesBook;
        protected CraftContainer CraftContainer;

        private HashSet<Container> _containersOnFocus;
        private HashSet<Container> _prevContainersOnFocus;

        protected TileEntity(CowGameScreen cowGameScreen)
        {
            CowGameScreen = cowGameScreen;
            RecipesBook = Recipes.GetRecipes();

            _containersOnFocus = new HashSet<Container>();
            _prevContainersOnFocus = new HashSet<Container>();
        }

        private MouseState _prevMouseState;

        public virtual void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            foreach (var container in Containers)
            {
                if (container.Position.Contains(mousePoint))
                {

                    container.OnFocus = true;
                    _containersOnFocus.Add(container);

                    if (mouseState.LeftButton != ButtonState.Pressed || _prevMouseState.LeftButton == ButtonState.Pressed) continue;

                    CowGameScreen.Cow.Inventory.SwapContainer.Swap(container);
                }
            }

            if (CraftContainer.Position.Contains(mousePoint))
            {
                CraftContainer.OnFocus = true;

                _containersOnFocus.Add(CraftContainer);
                if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton != ButtonState.Pressed)
                    CraftContainer.Swap(CowGameScreen.Cow.Inventory.SwapContainer);
            }

            FindRecipes();


            _prevContainersOnFocus.ToList().Where(container => !_containersOnFocus.Contains(container)).ToList().ForEach(container => container.OnFocus = false);

            _prevMouseState = mouseState;
            _prevContainersOnFocus = _containersOnFocus;
            _containersOnFocus = new HashSet<Container>();
        }

        private void FindRecipes()
        {
            foreach (var recipe in RecipesBook.Keys)
            {
                if (!FindRecipe(recipe)) continue;

                var o = Activator.CreateInstance(RecipesBook[recipe], CowGameScreen);
                CraftContainer.Result((Item)o, MinimalCount());
                return;
            }

            CraftContainer.ItemStack.Item = null;
            CraftContainer.ItemStack.ItemsCount = 0;

        }

        private int MinimalCount()
        {
            int min = int.MaxValue;
            for (var i = 0; i < Containers.GetLength(0); i++)
            {
                for (var j = 0; j < Containers.GetLength(1); j++)
                {
                    if (Containers[i, j].ItemStack.ItemsCount < min)
                    {
                        min = Containers[i, j].ItemStack.ItemsCount;
                    }
                }
            }
            return min;
        }

        public void Craft()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (!Containers[i, j].ItemStack.IsEmpty())
                    {
                        Containers[i, j].ItemStack.Remove();
                    }

                }
            }
        }

        private bool FindRecipe(Type[,] recipeArr)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (recipeArr[i, j] == null && Containers[i, j].ItemStack.Item == null)
                        continue;

                    if (Containers[i, j].ItemStack.Item == null || recipeArr[i, j] != Containers[i, j].ItemStack.Item.GetType())
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}