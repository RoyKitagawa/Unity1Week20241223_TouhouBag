using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataApple : SimpleSingleton<BagItemDataApple>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Apple; }
    string BagItemDataBase.GetPrefabPath() { return "Prefabs/BagItem_Apple"; }    
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }
}

/// <summary>
/// でかアップル
/// </summary>
public class BagItemDataBigApple : SimpleSingleton<BagItemDataBigApple>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.BigApple; }
    string BagItemDataBase.GetPrefabPath() { return "Prefabs/BagItem_Apple4"; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 2); }
}