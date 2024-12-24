using System.Numerics;
using UnityEngine;

public class BagItemDataLongItem : SimpleSingleton<BagItemDataLongItem>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.LongItem; }
    string BagItemDataBase.GetPrefabPath() { return "Prefabs/BagItem_Long"; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 1); }
}