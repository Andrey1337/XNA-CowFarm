using System;
using System.Collections.Generic;
using CowFarm.Entities.Items;
using CowFarm.Entities.Items.Craftables;

namespace CowFarm.Utility
{
    public static class Recipes
    {
        private static Dictionary<Type[,], Type> _recipesBook;

        public static Dictionary<Type[,], Type> GetRecipes()
        {
            if (_recipesBook != null)
            {
                return _recipesBook;
            }
            _recipesBook = new Dictionary<Type[,], Type>();

            Type[,] temp = new Type[2, 2];
            temp[0, 0] = typeof(CutGrass);
            temp[0, 1] = typeof(CutGrass);
            temp[1, 0] = typeof(CutGrass);
            temp[1, 1] = typeof(CutGrass);

            _recipesBook.Add(temp, typeof(Rope));

            return _recipesBook;

        }

    }
}