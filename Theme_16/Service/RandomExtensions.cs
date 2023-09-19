using System;

namespace Theme_16.Service
{
    internal static class RandomExtensions
    {
        public static T NextItem<T>(this Random rnd, params T[] items) => items[rnd.Next(items.Length)];
    }
}
