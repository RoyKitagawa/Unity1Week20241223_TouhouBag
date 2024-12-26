using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataApple : SimpleSingleton<BagItemDataApple>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Apple; }
    string BagItemDataBase.GetPrefabPath() { return Consts.Resources.BagItem.ItemApple; }    
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }
    float BagItemDataBase.GetCooldown() { return 1.0f; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattle() { return Consts.Sprites.ItemAppleBagEdit; }
}

/// <summary>
/// でかアップル
/// </summary>
public class BagItemDataBigApple : SimpleSingleton<BagItemDataBigApple>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.BigApple; }
    string BagItemDataBase.GetPrefabPath() { return Consts.Resources.BagItem.ItemApple4; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 2); }
    float BagItemDataBase.GetCooldown() { return 1.5f; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattle() { return Consts.Sprites.ItemAppleBagEdit; }
}