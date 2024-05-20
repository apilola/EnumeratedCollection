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
        public ReadOnlyCollection<TKey> Keys => EnumUtility<TKey>.Keys;

#if UNITY_2017_1_OR_NEWER
        [SerializeField]
#endif
        private TValue[] _values;
        private bool _isReadOnly = false;

        public EnumeratedCollection()
        {
            if (!EnumUtility<TKey>.IsContiguous) throw new InvalidOperationException($"Invalid Type Error: The type [{typeof(TKey)}] is not contiguous.");

            Type underlyingType = Enum.GetUnderlyingType(typeof(TKey));
            bool isUnsigned = underlyingType == typeof(byte) || underlyingType == typeof(ushort) ||
                              underlyingType == typeof(uint) || underlyingType == typeof(ulong);

            _values = new TValue[Keys.Count];
        }

        public EnumeratedCollection(TValue defaultValue) : this()
        {
            for (var i = 0; i < _values.Length; i++)
            {
                _values[i] = defaultValue;
            }
        }

        public TValue this[TKey enumIndex]
        {
            get => _values[enumIndex.AsCollectionIndex()];
            set
            {
                if (_isReadOnly) throw new InvalidOperationException("Collection is read-only.");
                _values[enumIndex.AsCollectionIndex()] = value;
            }
        }

        public TValue this[int index]
        {
            get => _values[index];
            set
            {
                if (_isReadOnly) throw new InvalidOperationException("Collection is read-only.");
                _values[index] = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (_isReadOnly) throw new InvalidOperationException("Collection is read-only.");
            this[key] = value;
        }

        public int Count => _values.Length;

        public bool IsReadOnly => _isReadOnly;

        public EnumeratedCollection<TKey, TValue> MakeReadonly()
        {
            _isReadOnly = true;
            return this;
        }

        public TValue[] ToArray() => _values.ToArray();

        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (var value in _values)
            {
                if (value != null) yield return value;
            }
        }

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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}