using System;
using System.Collections.Generic;


public static class Utils
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    
    public static T Random<T>(this T[] array)
    {
        int n = array.Length;
        int rn = rng.Next(n);
        return array[rn];
    }
    
    public static T Random<T>(this List<T> list)
    {
        int n = list.Count;
        int rn = rng.Next(n);
        return list[rn];
    }
}