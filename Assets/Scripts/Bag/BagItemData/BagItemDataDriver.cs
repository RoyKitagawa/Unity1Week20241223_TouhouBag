using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataDriverLv1 : BagItemDataBase
{
    protected int baseDamage = 5;
    protected float baseCooldown = 1.0f;
    
    // 基本情報系
    public virtual BagItemType GetItemType() { return BagItemType.Item; }
    public virtual BagItemName GetItemName() { return BagItemName.Driver; }
    public virtual string GetTag() { return Consts.Tags.Item; }
    public virtual ColliderShape GetColliderShape() { return ColliderShape.Rectangle1x2; }

    // レベル関連
    public virtual BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public virtual bool GetIsMergable() { return true; }
    public virtual string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv1; }
    public virtual string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv1; }

    // バッグ編集画面用
    public virtual int GetCost() { return 3; }

    // バトル画面用
    public virtual DamageType GetDamageType() { return DamageType.NormalDamage; }
    public virtual LaunchType GetLaunchType() { return LaunchType.ThrowStraight; }
    public virtual TargetType GetTargetType() { return TargetType.Nearest; }
    public virtual float GetCooldown() { return baseCooldown; }
    public virtual int GetDamage() { return baseDamage; }
}

public class BagItemDataDriverLv2 : BagItemDataDriverLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv2; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataDriverLv3 : BagItemDataDriverLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv3; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 5.0f); }
}

public class BagItemDataDriverLv4 : BagItemDataDriverLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv4; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 12.0f); }
}

