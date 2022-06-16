using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SkateboardNeverDie.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void TryAdd<T>(this ObservableCollection<T> source, T obj)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Contains(obj))
                source.Add(obj);
        }

        public static void TryAddRange<T>(this ObservableCollection<T> source, IEnumerable<T> objs)
        {
            foreach (var obj in objs)
                source.TryAdd(obj);
        }
    }
}