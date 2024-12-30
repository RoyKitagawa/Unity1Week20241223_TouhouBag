using System.Numerics;
using UnityEngine;

public class BagItemDataBag1x1 : BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetItemType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetItemName() { return BagItemName.Bag1x1; }
    string BagItemDataBase.GetTag() { return Consts.Tags.Bag; }
    ColliderShape BagItemDataBase.GetColliderShape() { return ColliderShape.Square1x1; }
    string BagItemDataBase.GetSpritePathItem() { return ""; }
    string BagItemDataBase.GetSpritePathItemThumb() { return ""; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    LaunchType BagItemDataBase.GetLaunchType() { return LaunchType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }

    // レベル
    BagItemLevel BagItemDataBase.GetLevel() { return BagItemLevel.Lv1; }
    bool BagItemDataBase.GetIsMergable() { return false; }
}

public class BagItemDataBag3x1 : BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetItemType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetItemName() { return BagItemName.Bag3x1; }
    string BagItemDataBase.GetTag() { return Consts.Tags.Bag; }
    ColliderShape BagItemDataBase.GetColliderShape() { return ColliderShape.Rectangle3x1; }
    string BagItemDataBase.GetSpritePathItem() { return ""; }
    string BagItemDataBase.GetSpritePathItemThumb() { return ""; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(3, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    LaunchType BagItemDataBase.GetLaunchType() { return LaunchType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }

    // レベル
    BagItemLevel BagItemDataBase.GetLevel() { return BagItemLevel.Lv1; }
    bool BagItemDataBase.GetIsMergable() { return false; }
}

public class BagItemDataBag2x2 : BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetItemType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetItemName() { return BagItemName.Bag2x2; }
    string BagItemDataBase.GetTag() { return Consts.Tags.Bag; }
    ColliderShape BagItemDataBase.GetColliderShape() { return ColliderShape.Square2x2; }
    string BagItemDataBase.GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.Bag2x2; }
    string BagItemDataBase.GetSpritePathItemThumb() { return ""; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 2); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    LaunchType BagItemDataBase.GetLaunchType() { return LaunchType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }
    // レベル
    BagItemLevel BagItemDataBase.GetLevel() { return BagItemLevel.Lv1; }
    bool BagItemDataBase.GetIsMergable() { return false; }
}

public class BagItemDataBag2x1 : BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetItemType() { return BagItemType.Bag; }
    BagItemName BagItemDataBase.GetItemName() { return BagItemName.Bag2x1; }
    string BagItemDataBase.GetTag() { return Consts.Tags.Bag; }
    ColliderShape BagItemDataBase.GetColliderShape() { return ColliderShape.Rectangle1x2; }
    string BagItemDataBase.GetSpritePathItem() { return ""; }
    string BagItemDataBase.GetSpritePathItemThumb() { return ""; }
 
    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    LaunchType BagItemDataBase.GetLaunchType() { return LaunchType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 0f; }
    int BagItemDataBase.GetDamage() { return 0; }

    // レベル
    BagItemLevel BagItemDataBase.GetLevel() { return BagItemLevel.Lv1; }
    bool BagItemDataBase.GetIsMergable() { return false; }
}