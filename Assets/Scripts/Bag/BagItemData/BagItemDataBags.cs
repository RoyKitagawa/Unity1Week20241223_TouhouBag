using System.Numerics;
using UnityEngine;

public class BagItemDataBag1x1 : SimpleSingleton<BagItemDataBag1x1>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag1x1; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.BagItem.ItemBag1x1; }
    string BagItemDataBase.GetBattlePrefabPath() { return ""; }
    string BagItemDataBase.GetSpritePathBagEdit() { return ""; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return ""; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }
}

public class BagItemDataBag3x1 : SimpleSingleton<BagItemDataBag3x1>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag3x1; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.BagItem.ItemBag3x1; }
    string BagItemDataBase.GetBattlePrefabPath() { return ""; }
    string BagItemDataBase.GetSpritePathBagEdit() { return ""; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return ""; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(3, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }
}

public class BagItemDataBag2x2 : SimpleSingleton<BagItemDataBag2x2>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag2x2; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.BagItem.ItemBag2x2; }
    string BagItemDataBase.GetBattlePrefabPath() { return ""; }
    string BagItemDataBase.GetSpritePathBagEdit() { return ""; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return ""; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 2); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }
}

public class BagItemDataBag2x1 : SimpleSingleton<BagItemDataBag2x1>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Bag2x1; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.BagItem.ItemBag2x1; }
    string BagItemDataBase.GetBattlePrefabPath() { return ""; }
    string BagItemDataBase.GetSpritePathBagEdit() { return ""; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return ""; }
 
    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }
}