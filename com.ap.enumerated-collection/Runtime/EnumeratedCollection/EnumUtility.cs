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

        /// <summary>
        /// Validates that the enum is contiguous.
        /// This is used to determine if the enum can be used as an index in an array.
        /// </summary>
        /// <returns>
        /// Returns true if the enum is contiguous; otherwise, false.
        /// </returns>
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

        /// <summary>
        /// Returns an IEnumerable of the enum values between the start and end values.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>
        /// The IEnumerable that can be used to iterate over the enum values in the range.
        /// </returns>
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
        /// <summary>
        /// Determines if the value is in the range of the start and end values.
        /// This is inclusive of the start and end values.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <param name="start">
        /// The start of the range.
        /// </param>
        /// <param name="end">
        /// The end of the range.
        /// </param>
        /// <returns>
        /// Returns true if the value is in the range; otherwise, false.
        /// </returns>
        public static bool IsInRange(this Enum value, Enum start, Enum end)
        {
            var interger = (int)(object) value;
            return (int)(object)start <= interger && (int)(object)end >= interger;
        }


        /// <summary>
        /// Returns the a valid array index for the enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int AsCollectionIndex<T>(this T key) where T : Enum
        {
            return (int) (object) key - EnumUtility<T>.Offset;
        }
    }
    
}
