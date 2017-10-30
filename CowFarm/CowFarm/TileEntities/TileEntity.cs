﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using CowFarm.Containers;
using CowFarm.Entities.Items;
using CowFarm.Inventory;
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

        protected TileEntity(CowGameScreen cowGameScreen)
        {
            CowGameScreen = cowGameScreen;
            RecipesBook = Recipes.GetRecipes();

        }

        private MouseState _prevMouseState;
        private Container _containerOnFocus;
        public virtual void Update()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);

            foreach (var container in Containers)
            {
                if (container.Position.Contains(mousePoint))
                {
                    _containerOnFocus = container;
                    _containerOnFocus.OnFocus = true;

                    if (mouseState.LeftButton != ButtonState.Pressed || _prevMouseState.LeftButton == ButtonState.Pressed) continue;

                    CowGameScreen.Cow.Inventory.SwapContainer.Swap(container);
                }
            }

            if (CraftContainer.Position.Contains(mousePoint))
            {
                _containerOnFocus = CraftContainer;
                _containerOnFocus.OnFocus = true;
                if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton != ButtonState.Pressed)
                    CraftContainer.Swap(CowGameScreen.Cow.Inventory.SwapContainer);
            }

            foreach (var recipe in RecipesBook.Keys)
            {
                if (FindRecipe(recipe))
                {
                    object[] args = { CowGameScreen };
                    object o = Activator.CreateInstance(RecipesBook[recipe], args);
                    CraftContainer.Result((Item)o);
                }
            }

            if (_containerOnFocus != null && !_containerOnFocus.Position.Contains(mousePoint))
            {
                _containerOnFocus.OnFocus = false;
            }
            _prevMouseState = mouseState;
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