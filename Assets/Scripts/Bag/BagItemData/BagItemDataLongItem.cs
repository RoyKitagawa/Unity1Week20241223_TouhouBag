using System.Numerics;
using UnityEngine;

public class BagItemDataLongItem : SimpleSingleton<BagItemDataLongItem>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.LongItem; }
    string BagItemDataBase.GetPrefabPath() { return Consts.Resources.BagItem.ItemLong; }
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 1); }
    float BagItemDataBase.GetCooldown() { return 2.0f; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattle() { return Consts.Sprites.ItemAppleBagEdit; }
}