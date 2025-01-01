using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataCanonLv1 : BagItemDataBase
{
    // スコア: Damage / CoolDown / Cost = 0.36
    protected override int GetBaseCost() { return 6; }
    protected override int GetBaseDamage() { return 12; }
    protected override float GetBaseCoolDown() { return 5.5f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.Item; }
    public override BagItemName GetItemName() { return BagItemName.Canon; }
    public override string GetTag() { return Consts.Tags.Item; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Rectangle4x1; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv1; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv1; }

    // バッグ編集画面用
    // public override int GetCost() { return baseCost; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.Damage; }
    public override LaunchType GetLaunchType() { return LaunchType.Unique; }
    public override TargetType GetTargetType() { return TargetType.Random; }
    // public override float GetCooldown() { return baseCooldown; }
    // public override int GetDamage() { return baseDamage; }
}

public class BagItemDataCanonLv2 : BagItemDataCanonLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv2; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier); }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff); }
}

public class BagItemDataCanonLv3 : BagItemDataCanonLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv3; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier * perLevelCostMultiplier); }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff * perLevelBuff); }
}

public class BagItemDataCanonLv4 : BagItemDataCanonLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    // public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv4; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier * perLevelCostMultiplier * perLevelCostMultiplier); }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff * perLevelBuff * perLevelBuff); }
}

