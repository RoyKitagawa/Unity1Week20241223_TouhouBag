using System.Numerics;
using UnityEngine;

/// <summary>
/// 初期からステージ上に配置されているステージスロット
/// 処理の共通化のため、アイテムとして識別しておく
/// </summary>
public class BagItemDataStageSlot : BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetItemType() { return BagItemType.StageSlot; }
    BagItemName BagItemDataBase.GetItemName() { return BagItemName.StageSlot; }
    string BagItemDataBase.GetTag() { return Consts.Tags.StageSlot; }
    ColliderShape BagItemDataBase.GetColliderShape() { return ColliderShape.Square1x1; }
    string BagItemDataBase.GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.StageSlot; }
    string BagItemDataBase.GetSpritePathItemThumb() { return ""; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 0; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.None; }
    LaunchType BagItemDataBase.GetLaunchType() { return LaunchType.None; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.None; }
    float BagItemDataBase.GetCooldown() { return 1.25f; }
    int BagItemDataBase.GetDamage() { return 17; }

    // レベル
    BagItemLevel BagItemDataBase.GetLevel() { return BagItemLevel.Lv1; }
    bool BagItemDataBase.GetIsMergable() { return false; }
}