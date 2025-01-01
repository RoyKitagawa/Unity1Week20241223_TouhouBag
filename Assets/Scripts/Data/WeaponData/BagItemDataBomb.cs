using System.Numerics;
using TMPro;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataBombLv1 : BagItemDataBase
{
    // スコア: Damage / CoolDown / Cost = 0.37
    protected override int GetBaseCost() { return 3; }
    protected override int GetBaseDamage() { return 5; }
    protected override float GetBaseCoolDown() { return 4.5f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.Item; }
    public override BagItemName GetItemName() { return BagItemName.Bomb; }
    public override string GetTag() { return Consts.Tags.Item; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Square1x1; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv1; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv1; }

    // // バッグ編集画面用
    // public override int GetCost() { return baseCost; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.Damage; }
    public override LaunchType GetLaunchType() { return LaunchType.Unique; }
    public override TargetType GetTargetType() { return TargetType.LowestLife; }
    // public override float GetCooldown() { return baseCooldown; }
    // public override int GetDamage() { return (int)(baseDamage * Mathf.Pow(perLevelBuff, GetLevelAsInt())); }
}

public class BagItemDataBombLv2 : BagItemDataBombLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv2; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier); }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff); }
}

public class BagItemDataBombLv3 : BagItemDataBombLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv3; }
    
    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier * perLevelCostMultiplier); }
    
    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff * perLevelBuff); }
}

public class BagItemDataBombLv4 : BagItemDataBombLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    // public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv4; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier * perLevelCostMultiplier * perLevelCostMultiplier); }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff * perLevelBuff * perLevelBuff); }
}
