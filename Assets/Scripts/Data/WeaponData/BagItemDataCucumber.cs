using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataCucumberLv1 : BagItemDataBase
{
    // スコア: Damage / CoolDown / Cost = 1.66
    protected override int GetBaseCost() { return 2; }
    protected override int GetBaseDamage() { return 10; }
    protected override float GetBaseCoolDown() { return 3.0f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.Item; }
    public override BagItemName GetItemName() { return BagItemName.Cucumber; }
    public override string GetTag() { return Consts.Tags.Item; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Rectangle1x2; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv1; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv1; }

    // バッグ編集画面用
    // public override int GetCost() { return baseCost; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.Heal; }
    public override LaunchType GetLaunchType() { return LaunchType.ThrowParabola; }
    public override TargetType GetTargetType() { return TargetType.Self; }
    // public override float GetCooldown() { return baseCooldown; }
    // public override int GetDamage() { return baseDamage; }
}

public class BagItemDataCucumberLv2 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv2; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier); }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff); }
}

public class BagItemDataCucumberLv3 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv3; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier * perLevelCostMultiplier); }
    
    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff * perLevelBuff); }
}

public class BagItemDataCucumberLv4 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    // public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv4; }

    // バッグ編集画面用
    // public override int GetCost() { return (int)(baseCost * perLevelCostMultiplier * perLevelCostMultiplier * perLevelCostMultiplier); }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * perLevelBuff * perLevelBuff * perLevelBuff); }
}
