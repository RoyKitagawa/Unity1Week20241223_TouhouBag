using System.Collections.Generic;
using UnityEngine;

public static class ArrayUtil
{
    // 配列から要素を取得する
    public static T Get<T>(T[] arr, int x)
    {
        if(IsIndexInRange(arr, x)) return arr[x];
        return default(T);
    }

    public static T Get<T>(T[] arr, int x, T defaultValue)
    {
        if(IsIndexInRange(arr, x)) return arr[x];
        return defaultValue;
    }

    public static T Get<T>(T[,] arr, int x, int y)
    {
        if(IsIndexInRange(arr, x, y)) return arr[x, y];
        return default(T);
    }

    public static T Get<T>(T[,] arr, int x, int y, T defaultValue)
    {
        if(IsIndexInRange(arr, x, y)) return arr[x, y];
        return defaultValue;
    }


    public static void Set<T>(T[] arr, T item, int x)
    {
        if(!IsIndexInRange(arr, x)) return;
        arr[x] = item;
    }

    public static void Set<T>(T[,] arr, T item, int x, int y)
    {
        if(!IsIndexInRange(arr, x, y)) return;
        arr[x, y] = item;
    }

    public static bool IsIndexInRange<T>(T[] arr, int x)
    {
        // 要素が範囲内か
        if(x >= 0 && arr.GetLength(0) < x) return true;
        return false;
    }

    public static bool IsIndexInRange<T>(T[,] arr, int x, int y)
    {
        int lengthA = arr.GetLength(0);
        int lengthB = arr.GetLength(1);
        // 要素が範囲内か
        if(0 <= x && x < arr.GetLength(0)
            && 0 <= y && y < arr.GetLength(1)) return true;
        return false;
    }
}