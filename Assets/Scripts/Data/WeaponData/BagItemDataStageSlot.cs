using System.Numerics;
using UnityEngine;

/// <summary>
/// 初期からステージ上に配置されているステージスロット
/// 処理の共通化のため、アイテムとして識別しておく
/// </summary>
public class BagItemDataStageSlot : BagItemDataBase
{
    protected override int GetBaseCost() { return 0; }
    protected override int GetBaseDamage() { return 0; }
    protected override float GetBaseCoolDown() { return 99f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.StageSlot; }
    public override BagItemName GetItemName() { return BagItemName.StageSlot; }
    public override string GetTag() { return Consts.Tags.StageSlot; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Square1x1; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.StageSlot; }
    public override string GetSpritePathItemThumb() { return ""; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.None; }
    public override LaunchType GetLaunchType() { return LaunchType.None; }
    public override TargetType GetTargetType() { return TargetType.None; }
}