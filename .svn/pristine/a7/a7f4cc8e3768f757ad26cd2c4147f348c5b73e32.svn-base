using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YoYoStudio.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static void Foreach<T>(this ObservableCollection<T> collection, Action<T> action)
        {
            foreach (var t in collection)
            {
                action(t);
            }
        }
        public static void Foreach<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action(array[i]);
            }
        }
    }
}
