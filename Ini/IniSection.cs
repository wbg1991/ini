﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Ini
{
    public class IniSection : IEnumerable<KeyValuePair<string, IniValue>>, IDictionary<string, IniValue>
    {
        private Dictionary<string, IniValue> values;

        #region Ordered
        private List<string> orderedKeys;

        public int IndexOf(string key)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call IndexOf(string) on IniSection: section was not ordered.");
            }
            return IndexOf(key, 0, orderedKeys.Count);
        }

        public int IndexOf(string key, int index)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call IndexOf(string, int) on IniSection: section was not ordered.");
            }
            return IndexOf(key, index, orderedKeys.Count - index);
        }

        public int IndexOf(string key, int index, int count)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call IndexOf(string, int, int) on IniSection: section was not ordered.");
            }
            if (index < 0 || index > orderedKeys.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if (count < 0)
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if (index + count > orderedKeys.Count)
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            var end = index + count;
            for (int i = index; i < end; i++)
            {
                if (Comparer.Equals(orderedKeys[i], key))
                {
                    return i;
                }
            }
            return -1;
        }

        public int LastIndexOf(string key)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call LastIndexOf(string) on IniSection: section was not ordered.");
            }
            return LastIndexOf(key, 0, orderedKeys.Count);
        }

        public int LastIndexOf(string key, int index)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call LastIndexOf(string, int) on IniSection: section was not ordered.");
            }
            return LastIndexOf(key, index, orderedKeys.Count - index);
        }

        public int LastIndexOf(string key, int index, int count)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call LastIndexOf(string, int, int) on IniSection: section was not ordered.");
            }
            if (index < 0 || index > orderedKeys.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if (count < 0)
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if (index + count > orderedKeys.Count)
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            var end = index + count;
            for (int i = end - 1; i >= index; i--)
            {
                if (Comparer.Equals(orderedKeys[i], key))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, string key, IniValue value)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call Insert(int, string, IniValue) on IniSection: section was not ordered.");
            }
            if (index < 0 || index > orderedKeys.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            values.Add(key, value);
            orderedKeys.Insert(index, key);
        }

        public void InsertRange(int index, IEnumerable<KeyValuePair<string, IniValue>> collection)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call InsertRange(int, IEnumerable<KeyValuePair<string, IniValue>>) on IniSection: section was not ordered.");
            }
            if (collection == null)
            {
                throw new ArgumentNullException("Value cannot be null." + Environment.NewLine + "Parameter name: collection");
            }
            if (index < 0 || index > orderedKeys.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            foreach (var kvp in collection)
            {
                Insert(index, kvp.Key, kvp.Value);
                index++;
            }
        }

        public void RemoveAt(int index)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call RemoveAt(int) on IniSection: section was not ordered.");
            }
            if (index < 0 || index > orderedKeys.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            var key = orderedKeys[index];
            orderedKeys.RemoveAt(index);
            values.Remove(key);
        }

        public void RemoveRange(int index, int count)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call RemoveRange(int, int) on IniSection: section was not ordered.");
            }
            if (index < 0 || index > orderedKeys.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if (count < 0)
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if (index + count > orderedKeys.Count)
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            for (int i = 0; i < count; i++)
            {
                RemoveAt(index);
            }
        }

        public void Reverse()
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call Reverse() on IniSection: section was not ordered.");
            }
            orderedKeys.Reverse();
        }

        public void Reverse(int index, int count)
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call Reverse(int, int) on IniSection: section was not ordered.");
            }
            if (index < 0 || index > orderedKeys.Count)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
            }
            if (count < 0)
            {
                throw new IndexOutOfRangeException("Count cannot be less than zero." + Environment.NewLine + "Parameter name: count");
            }
            if (index + count > orderedKeys.Count)
            {
                throw new ArgumentException("Index and count were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            orderedKeys.Reverse(index, count);
        }

        public ICollection<IniValue> GetOrderedValues()
        {
            if (!Ordered)
            {
                throw new InvalidOperationException("Cannot call GetOrderedValues() on IniSection: section was not ordered.");
            }
            var list = new List<IniValue>();
            for (int i = 0; i < orderedKeys.Count; i++)
            {
                list.Add(values[orderedKeys[i]]);
            }
            return list;
        }

        public IniValue this[int index]
        {
            get
            {
                if (!Ordered)
                {
                    throw new InvalidOperationException("Cannot index IniSection using integer key: section was not ordered.");
                }
                if (index < 0 || index >= orderedKeys.Count)
                {
                    throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
                }
                return values[orderedKeys[index]];
            }
            set
            {
                if (!Ordered)
                {
                    throw new InvalidOperationException("Cannot index IniSection using integer key: section was not ordered.");
                }
                if (index < 0 || index >= orderedKeys.Count)
                {
                    throw new IndexOutOfRangeException("Index must be within the bounds." + Environment.NewLine + "Parameter name: index");
                }
                var key = orderedKeys[index];
                values[key] = value;
            }
        }

        public bool Ordered
        {
            get => orderedKeys != null;
            set
            {
                if (Ordered != value)
                {
                    orderedKeys = value ? new List<string>(values.Keys) : null;
                }
            }
        }
        #endregion

        public IniSection()
            : this(IniFile.DefaultComparer)
        {
        }

        public IniSection(IEqualityComparer<string> stringComparer)
        {
            this.values = new Dictionary<string, IniValue>(stringComparer);
        }

        public IniSection(Dictionary<string, IniValue> values)
            : this(values, IniFile.DefaultComparer)
        {
        }

        public IniSection(Dictionary<string, IniValue> values, IEqualityComparer<string> stringComparer)
        {
            this.values = new Dictionary<string, IniValue>(values, stringComparer);
        }

        public IniSection(IniSection values)
            : this(values, IniFile.DefaultComparer)
        {
        }

        public IniSection(IniSection values, IEqualityComparer<string> stringComparer)
        {
            this.values = new Dictionary<string, IniValue>(values.values, stringComparer);
        }

        public void Add(string key, IniValue value)
        {
            values.Add(key, value);
            if (Ordered)
            {
                orderedKeys.Add(key);
            }
        }

        public bool ContainsKey(string key)
        {
            return values.ContainsKey(key);
        }

        /// <summary>
        /// Returns this IniSection's collection of keys. If the IniSection is ordered, the keys will be returned in order.
        /// </summary>
        public ICollection<string> Keys => Ordered ? (ICollection<string>)orderedKeys : values.Keys;

        public bool Remove(string key)
        {
            var ret = values.Remove(key);
            if (Ordered && ret)
            {
                for (int i = 0; i < orderedKeys.Count; i++)
                {
                    if (Comparer.Equals(orderedKeys[i], key))
                    {
                        orderedKeys.RemoveAt(i);
                        break;
                    }
                }
            }
            return ret;
        }

        public bool TryGetValue(string key, out IniValue value)
        {
            return values.TryGetValue(key, out value);
        }

        /// <summary>
        /// Returns the values in this IniSection. These values are always out of order. To get ordered values from an IniSection call GetOrderedValues instead.
        /// </summary>
        public ICollection<IniValue> Values => values.Values;

        void ICollection<KeyValuePair<string, IniValue>>.Add(KeyValuePair<string, IniValue> item)
        {
            ((IDictionary<string, IniValue>)values).Add(item);
            if (Ordered)
            {
                orderedKeys.Add(item.Key);
            }
        }

        public void Clear()
        {
            values.Clear();
            if (Ordered)
            {
                orderedKeys.Clear();
            }
        }

        bool ICollection<KeyValuePair<string, IniValue>>.Contains(KeyValuePair<string, IniValue> item)
        {
            return ((IDictionary<string, IniValue>)values).Contains(item);
        }

        void ICollection<KeyValuePair<string, IniValue>>.CopyTo(KeyValuePair<string, IniValue>[] array, int arrayIndex)
        {
            ((IDictionary<string, IniValue>)values).CopyTo(array, arrayIndex);
        }

        public int Count => values.Count;

        bool ICollection<KeyValuePair<string, IniValue>>.IsReadOnly => ((IDictionary<string, IniValue>)values).IsReadOnly;

        bool ICollection<KeyValuePair<string, IniValue>>.Remove(KeyValuePair<string, IniValue> item)
        {
            var ret = ((IDictionary<string, IniValue>)values).Remove(item);
            if (Ordered && ret)
            {
                for (int i = 0; i < orderedKeys.Count; i++)
                {
                    if (Comparer.Equals(orderedKeys[i], item.Key))
                    {
                        orderedKeys.RemoveAt(i);
                        break;
                    }
                }
            }
            return ret;
        }

        public IEnumerator<KeyValuePair<string, IniValue>> GetEnumerator()
        {
            if (Ordered)
            {
                return GetOrderedEnumerator();
            }
            else
            {
                return values.GetEnumerator();
            }
        }

        private IEnumerator<KeyValuePair<string, IniValue>> GetOrderedEnumerator()
        {
            for (int i = 0; i < orderedKeys.Count; i++)
            {
                yield return new KeyValuePair<string, IniValue>(orderedKeys[i], values[orderedKeys[i]]);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEqualityComparer<string> Comparer => values.Comparer;

        public IniValue this[string name]
        {
            get => values.TryGetValue(name, out var val) ? val : IniValue.Default;
            set
            {
                if (Ordered && !orderedKeys.Contains(name, Comparer))
                {
                    orderedKeys.Add(name);
                }
                values[name] = value;
            }
        }

        public static implicit operator IniSection(Dictionary<string, IniValue> dict)
        {
            return new IniSection(dict);
        }

        public static explicit operator Dictionary<string, IniValue>(IniSection section)
        {
            return section.values;
        }
    }
}
