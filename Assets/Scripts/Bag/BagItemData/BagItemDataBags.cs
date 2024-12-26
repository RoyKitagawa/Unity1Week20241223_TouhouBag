using System.Numerics;
using UnityEngine;

public class BagItemDataBag1x1 : SimpleSingleton<BagItemDataBag1x1>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag1x1; }
    string BagItemDataBase.GetPrefabPath() { return Consts.Resources.BagItem.ItemBag1x1; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }
    float BagItemDataBase.GetCooldown() { return 1.0f; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattle() { return Consts.Sprites.ItemAppleBagEdit; }
}

public class BagItemDataBag3x1 : SimpleSingleton<BagItemDataBag3x1>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag3x1; }
    string BagItemDataBase.GetPrefabPath() { return Consts.Resources.BagItem.ItemBag3x1; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(3, 1); }
    float BagItemDataBase.GetCooldown() { return 1.0f; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattle() { return Consts.Sprites.ItemAppleBagEdit; }
}

public class BagItemDataBag2x2 : SimpleSingleton<BagItemDataBag2x2>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag2x2; }
    string BagItemDataBase.GetPrefabPath() { return Consts.Resources.BagItem.ItemBag2x2; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 2); }
    float BagItemDataBase.GetCooldown() { return 1.0f; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattle() { return Consts.Sprites.ItemAppleBagEdit; }
}

public class BagItemDataBag2x1 : SimpleSingleton<BagItemDataBag2x1>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag2x1; }
    string BagItemDataBase.GetPrefabPath() { return Consts.Resources.BagItem.ItemBag2x1; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 1); }
    float BagItemDataBase.GetCooldown() { return 1.0f; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattle() { return Consts.Sprites.ItemAppleBagEdit; }
}