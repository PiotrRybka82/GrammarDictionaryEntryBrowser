using System.Collections.Generic;
using System;
using System.Linq;

namespace Dictionary
{
    public static class LinqExtensions
    {

        public static IEnumerable<T> Clone<T>(this IEnumerable<T> objs) where T : ICloneable
        {
            foreach (var item in objs)
            {
                yield return (T)item.Clone();
            }
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> objs, T oldObj, T newObj) where T : ICloneable
        {
            if (!objs.Contains(oldObj)) return objs.Clone();

            var temp = objs.Clone().ToList();
            temp[temp.IndexOf(oldObj)] = newObj;

            return temp;
        }


        public static IEnumerable<T> Remove<T>(this IEnumerable<T> objs, T obj)
        {
            IEnumerable<T> newObjs = new List<T>(objs);
            newObjs.Remove(obj);
            return newObjs;
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> objs, T obj)
        {
            IEnumerable<T> newObjs = new List<T>(objs);
            newObjs.Add(obj);
            return newObjs;
        }



    }
}