using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataCucumberLv1 : BagItemDataBase
{
    protected int baseDamage = 10;
    protected float baseCooldown = 2.0f;

    // 基本情報系
    public virtual BagItemType GetItemType() { return BagItemType.Item; }
    public virtual BagItemName GetItemName() { return BagItemName.Cucumber; }
    public virtual string GetTag() { return Consts.Tags.Item; }
    public virtual ColliderShape GetColliderShape() { return ColliderShape.Rectangle1x2; }

    // レベル関連
    public virtual BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public virtual bool GetIsMergable() { return true; }
    public virtual string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv1; }
    public virtual string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv1; }

    // バッグ編集画面用
    public virtual int GetCost() { return 2; }

    // バトル画面用
    public virtual DamageType GetDamageType() { return DamageType.Heal; }
    public virtual LaunchType GetLaunchType() { return LaunchType.ThrowParabola; }
    public virtual TargetType GetTargetType() { return TargetType.Self; }
    public virtual float GetCooldown() { return baseCooldown; }
    public virtual int GetDamage() { return baseDamage; }
}

public class BagItemDataCucumberLv2 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv2; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataCucumberLv3 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv3; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 5.0f); }
}

public class BagItemDataCucumberLv4 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv4; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 12.0f); }
}