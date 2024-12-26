using System.Numerics;
using UnityEngine;

/// <summary>
/// 初期からステージ上に配置されているステージスロット
/// 処理の共通化のため、アイテムとして識別しておく
/// </summary>
public class BagItemDataStageSlot : SimpleSingleton<BagItemDataStageSlot>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.StageSlot; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.StageSlot; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.BagItem.StageSlot; }
    string BagItemDataBase.GetBattlePrefabPath() { return ""; }
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Sprites.ItemAppleBagEdit; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return Consts.Sprites.ItemAppleBagEdit; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 0; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 1.25f; }
    int BagItemDataBase.GetDamage() { return 17; }
}