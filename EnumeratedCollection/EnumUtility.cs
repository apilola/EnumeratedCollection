using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AP.Utilities
{
    public static class EnumUtility<T> where T : Enum
    {
        static EnumUtility()
        {
            // Check for contiguity
            IsContiguous = ValidateIsContiguous();
            Offset = Convert.ToInt32(Keys[0]);
        }

        public static readonly ReadOnlyCollection<T> Keys = new ReadOnlyCollection<T>(((T[])Enum.GetValues(typeof(T))).OrderBy(b => (b)).ToArray());
        public static readonly bool IsContiguous;
        public static readonly int Offset;

        public static bool ValidateIsContiguous()
        {
            for (int i = 1; i < Keys.Count; i++)
            {
                if (System.Math.Abs(Convert.ToInt32(Keys[i]) - Convert.ToInt32(Keys[i - 1])) != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<T> InRange(T start, T end)
        {
            var keys = EnumUtility<T>.Keys;
            var a = start.AsCollectionIndex();
            var b = end.AsCollectionIndex() + 1;
            for (var i = a; i < b; i++)
            {
                yield return keys[i];
            }
        }
    }

    public static class EnumExtensions
    {
        public static bool IsInRange(this Enum value, Enum start, Enum end)
        {
            var interger = (int)(object) value;
            return (int)(object)start <= interger && (int)(object)end >= interger;
        }



        public static int AsCollectionIndex<T>(this T key) where T : Enum
        {
            return (int) (object) key - EnumUtility<T>.Offset;
        }
    }
    
}
