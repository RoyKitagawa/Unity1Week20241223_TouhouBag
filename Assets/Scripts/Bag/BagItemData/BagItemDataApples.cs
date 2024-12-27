using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataApple : SimpleSingleton<BagItemDataApple>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.Apple; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.Prefabs.BagItems.ItemApple; }    
    string BagItemDataBase.GetBattlePrefabPath() { return Consts.Resources.Prefabs.ProjectileItems.Apple; }    
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Resources.Sprites.BattleItem.List.Apple; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return Consts.Resources.Sprites.BattleItem.List.Apple; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.Heal; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.Self; }
    float BagItemDataBase.GetCooldown() { return 2.0f; }
    int BagItemDataBase.GetDamage() { return 5; }

    // レベル
    int BagItemDataBase.GetLevel() { return 1; }
}

/// <summary>
/// でかアップル
/// </summary>
public class BagItemDataBigApple : SimpleSingleton<BagItemDataBigApple>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.BigApple; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.Prefabs.BagItems.ItemApple4; }
    string BagItemDataBase.GetBattlePrefabPath() { return Consts.Resources.Prefabs.ProjectileItems.Apple4; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Resources.Sprites.BattleItem.List.Apple4; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return Consts.Resources.Sprites.BattleItem.List.Apple4; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 2); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.NormalDamage; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.Nearest; }
    float BagItemDataBase.GetCooldown() { return 3.5f; }
    int BagItemDataBase.GetDamage() { return 25; }

    // レベル
    int BagItemDataBase.GetLevel() { return 1; }
}