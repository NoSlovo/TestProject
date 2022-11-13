using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class MyExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> callback)
        {
            foreach (var item in enumerable)
                callback.Invoke(item);
        }

        public static void ForEachIndexed<T>(this IEnumerable<T> enumerable, Action<T, int> callback)
        {
            int count = 0;

            foreach (var item in enumerable)
                callback.Invoke(item, count++);
        }

        public static Vector3 Copy(this Vector3 vector, float? x = null, float? y = null, float? z = null) =>
            new Vector3(
                x ?? vector.x,
                y ?? vector.y,
                z ?? vector.z
            );
    }
}