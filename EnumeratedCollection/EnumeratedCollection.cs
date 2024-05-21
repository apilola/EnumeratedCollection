using AP.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif

namespace AP.Collections
{
    [Serializable]
    public class EnumeratedCollection<TKey, TValue> : IEnumerable<TValue> where TKey : Enum
    {
        /// <summary>
        /// Gets the keys of the collection.
        /// </summary>
        public ReadOnlyCollection<TKey> Keys => EnumUtility<TKey>.Keys;

#if UNITY_2017_1_OR_NEWER
        [SerializeField]
#endif
        private TValue[] _values;
        private bool _isReadOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumeratedCollection{TKey, TValue}"/> class.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public EnumeratedCollection()
        {
            if (!EnumUtility<TKey>.IsContiguous) throw new InvalidOperationException($"Invalid Type Error: The type [{typeof(TKey)}] is not contiguous.");

            Type underlyingType = Enum.GetUnderlyingType(typeof(TKey));
            bool isUnsigned = underlyingType == typeof(byte) || underlyingType == typeof(ushort) ||
                              underlyingType == typeof(uint) || underlyingType == typeof(ulong);

            _values = new TValue[Keys.Count];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumeratedCollection{TKey, TValue}"/> class.
        /// <param name="defaultValue">The default value to initialize the collection with.</param>
        public EnumeratedCollection(TValue defaultValue) : this()
        {
            for (var i = 0; i < _values.Length; i++)
            {
                _values[i] = defaultValue;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="enumIndex">The enum as a key to get or set the value.</param>
        /// <returns>A value associated with the specified key.</returns>
        /// <exception cref="InvalidOperationException">A InvalidOperationException is thrown if the collection is read-only.</exception>
        public TValue this[TKey enumIndex]
        {
            get => _values[enumIndex.AsCollectionIndex()];
            set
            {
                if (_isReadOnly) throw new InvalidOperationException("Collection is read-only.");
                _values[enumIndex.AsCollectionIndex()] = value;
            }
        }

        /// <summary>
        /// Sets the value at the specified index.
        /// </summary>
        /// <param name="index">
        /// The index of the value to set.
        /// </param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public TValue this[int index]
        {
            get => _values[index];
            set
            {
                if (_isReadOnly) throw new InvalidOperationException("Collection is read-only.");
                _values[index] = value;
            }
        }

        /// <summary>
        /// Adds/Sets a key-value pair to the collection.
        /// This method exists to allow the collection to be initialized with a collection initializer.
        /// </summary>
        /// <param name="key">
        /// The key to add/set the value for.
        /// </param>
        /// <param name="value">
        /// The value that is will be set.
        /// </param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Add(TKey key, TValue value)
        {
            if (_isReadOnly) throw new InvalidOperationException("Collection is read-only.");
            this[key] = value;
        }

        /// <summary>
        /// The number of elements in the collection.
        /// </summary>
        public int Count => _values.Length;

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly => _isReadOnly;

        /// <summary>
        /// Makes the collection readonly
        /// </summary>
        /// <returns>
        /// The collection after being modified to be read-only.
        /// </returns>
        public EnumeratedCollection<TKey, TValue> MakeReadonly()
        {
            _isReadOnly = true;
            return this;
        }

        /// <summary>
        /// Copies the elements of the collection to a new array and returns the copy.
        /// </summary>
        /// <returns>
        /// The newly created array.
        /// </returns>
        public TValue[] ToArray() => _values.ToArray();


        /// <summary>
        /// Gets an enumerator that will iterate through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (var value in _values)
            {
                if (value != null) yield return value;
            }
        }

        /// <summary>
        /// If the array contains the value, it will return the index of first occurrence of the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(TValue value)
        {
            if (value == null)
                return -1;

            for (var i = 0; i < _values.Length; i++)
            {
                if (value.Equals(_values[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Tries to get the key for a particular value.
        /// </summary>
        /// <param name="value">
        /// The value to get the key for.
        /// </param>
        /// <param name="key">
        /// Returns the key if the value is found.
        /// </param>
        /// <returns>
        /// Returns true if the value is found, otherwise false.
        /// </returns>
        public bool TryGetKeyOf(TValue value, out TKey key)
        {
            var index = IndexOf(value);
            if (index == -1)
            {
                key = default(TKey);
                return false;
            }
            else
            {
                key = Keys[index];
                return true;
            }
        }

        /// <summary>
        /// Gets an enumerator that will iterate through the collection.
        /// </summary>
        /// <returns>
        /// The enumerator.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}