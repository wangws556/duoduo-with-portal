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
    public class SafeList<T> : IList<T>
    {
        private List<T> data ;
        private ReaderWriterLockSlim slim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public SafeList():this(new List<T>())
        {

        }

        public SafeList(IList<T> list)
        {
            data = new List<T>(list);
        }

        public int IndexOf(T item)
        {
            slim.EnterReadLock();
            var result = data.IndexOf(item);
            slim.ExitReadLock();
            return result;
        }

        public void Insert(int index, T item)
        {
            slim.EnterWriteLock();
            data.Insert(index, item);
            slim.ExitWriteLock();
        }

        public void RemoveAt(int index)
        {
            slim.EnterWriteLock();
            data.RemoveAt(index);
            slim.ExitWriteLock();
        }

        public T this[int index]
        {
            get
            {
                slim.EnterReadLock();
                var result = data[index];
                slim.ExitReadLock();
                return result;
            }
            set
            {
                slim.EnterWriteLock();
                data[index] = value;
                slim.ExitWriteLock();
            }
        }

        public void Add(T item)
        {
            slim.EnterWriteLock();
            data.Add(item);
            slim.ExitWriteLock();
        }

        public void Clear()
        {
            slim.EnterWriteLock();
            data.Clear();
            slim.ExitWriteLock();
        }

        public bool Contains(T item)
        {
            slim.EnterReadLock();
            var result = data.Contains(item);
            slim.ExitReadLock();
            return result;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            slim.EnterReadLock();
            for (int i = 0; i < data.Count; i++)
            {
                array[arrayIndex + i] = data[i];
            }
            slim.ExitReadLock();
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

        public bool Remove(T item)
        {
            slim.EnterWriteLock();
            var result = data.Remove(item);
            slim.ExitWriteLock();
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            slim.EnterWriteLock();
            var result = data.GetEnumerator();
            slim.ExitWriteLock();
            return result;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
