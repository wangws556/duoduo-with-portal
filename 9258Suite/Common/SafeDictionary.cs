/// <copyright>
/// Copyright ©  2013 YoYoStudio Corporation. All rights reserved. UNISYS CONFIDENTIAL
/// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace YoYoStudio.Common
{
    public class SafeDictionary<T,K> : IDictionary<T,K>
    {
        private Dictionary<T, K> data = new Dictionary<T, K>();

        private ReaderWriterLockSlim slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public void Add(T key, K value)
        {
            slim.EnterWriteLock();
            data[key] = value;
            slim.ExitWriteLock();
        }

        public bool ContainsKey(T key)
        {
            slim.EnterReadLock();
            bool result = data.ContainsKey(key);
            slim.ExitReadLock();
            return result;
        }

        public ICollection<T> Keys
        {
            get
            {
                slim.EnterReadLock();
                var result = data.Keys;
                slim.ExitReadLock();
                return result;
            }
        }

        public bool Remove(T key)
        {
            slim.EnterWriteLock();
            bool result = data.Remove(key);
            slim.ExitWriteLock();
            return result;
        }

        public bool TryGetValue(T key, out K value)
        {
            slim.EnterReadLock();
            bool result = data.TryGetValue(key, out value);
            slim.ExitReadLock();
            return result;
        }

        public ICollection<K> Values
        {
            get 
            {
                slim.EnterReadLock();
                var result = data.Values;
                slim.ExitReadLock();
                return result;
            }
        }

        public K this[T key]
        {
            get
            {
                slim.EnterReadLock();
                var result = default(K);
                if (data.ContainsKey(key))
                {
                    result = data[key];
                }
                slim.ExitReadLock();
                return result;
            }
            set
            {
                slim.EnterWriteLock();
                data[key] = value;
                slim.ExitWriteLock();
            }
        }

        public void Add(KeyValuePair<T, K> item)
        {
            slim.EnterWriteLock();
            data.Add(item.Key,item.Value);
            slim.ExitWriteLock();
        }

        public void Clear()
        {
            slim.EnterWriteLock();
            data.Clear();
            slim.ExitWriteLock();
        }

        public bool Contains(KeyValuePair<T, K> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<T, K>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get 
            {
                slim.EnterReadLock();
                var result = data.Count;
                slim.ExitReadLock();
                return result;
            }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<T, K> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<T, K>> GetEnumerator()
        {
            slim.EnterReadLock();
            var result = data.ToList().GetEnumerator();
            slim.ExitReadLock();
            return result;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            slim.EnterReadLock();
            var result = data.GetEnumerator();
            slim.ExitReadLock();
            return result;
        }
    }
}
