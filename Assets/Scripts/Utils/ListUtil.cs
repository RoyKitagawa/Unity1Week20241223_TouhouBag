using System.Collections.Generic;

public static class ListUtil
{
    public static void Add<T>(List<T> list, T item, bool addIfNull = false)
    {
        if(list == null) return;
        if(item == null && !addIfNull) return;
        list.Add(item);
    }
}