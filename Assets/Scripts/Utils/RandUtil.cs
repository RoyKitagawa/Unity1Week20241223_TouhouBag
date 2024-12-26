using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandUtil
{
    public static T GetRandomItem<T>(params T[] values)
    {
        if(values == null || values.Length <= 0)
            return default(T);
        
        return values[Random.Range(0, values.Length)];
    }

    public static T GetRandomItem<T>(List<T> values)
    {
        if(values == null || values.Count <= 0)
            return default(T);
        
        return values[Random.Range(0, values.Count)];

    }

    public static T GetRandomItem<T>(HashSet<T> values)
    {
        if(values == null || values.Count <= 0)
            return default(T);
        
        return values.ElementAt(Random.Range(0, values.Count));
    }

    public static List<T> GetRandomItems<T>(int getCount, List<T> values)
    {
        List<T> ret = new List<T>();
        if(values.Count <= getCount)
            return values;

        List<T> calcList = new List<T>(values);
        for(int i = 0; i < getCount; i++)
        {
            T randValue = GetRandomItem(calcList);
            ret.Add(randValue);
            calcList.Remove(randValue);
        }
        return ret;
    }

    public static HashSet<T> GetRandomItems<T>(int getCount, HashSet<T> values)
    {
        HashSet<T> ret = new HashSet<T>();
        if(values.Count <= getCount)
            return values;

        HashSet<T> calcList = new HashSet<T>(values);
        for(int i = 0; i < getCount; i++)
        {
            T randValue = GetRandomItem(calcList);
            ret.Add(randValue);
            calcList.Remove(randValue);
        }
        return ret;
    }

    public static Vector2 GetRandomVector2In(Rect area)
    {
        float randomX = Random.Range(area.xMin, area.xMax);
        float randomY = Random.Range(area.yMin, area.yMax);
        return new Vector2(randomX, randomY);
    }

    public static bool GetRandomBool(float truePercentage = 0.50f)
    {
        float value = Random.Range(0.0f, 1.0f);
        return value <= truePercentage;
    }

}
