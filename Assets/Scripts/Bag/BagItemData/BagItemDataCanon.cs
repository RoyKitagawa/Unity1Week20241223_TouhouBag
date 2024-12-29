using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataCanonLv1 : BagItemDataBase
{
    protected int baseDamage = 10;
    protected float baseCooldown = 5.0f;

    // 基本情報系
    public virtual BagItemType GetItemType() { return BagItemType.Item; }
    public virtual BagItemName GetItemName() { return BagItemName.Canon; }
    public virtual string GetTag() { return Consts.Tags.Item; }
    public virtual ColliderShape GetColliderShape() { return ColliderShape.Rectangle4x1; }

    // レベル関連
    public virtual BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public virtual bool GetIsMergable() { return true; }
    public virtual string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv1; }
    public virtual string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv1; }

    // バッグ編集画面用
    public virtual int GetCost() { return 6; }

    // バトル画面用
    public virtual DamageType GetDamageType() { return DamageType.NormalDamage; }
    public virtual LaunchType GetLaunchType() { return LaunchType.Unique; }
    public virtual TargetType GetTargetType() { return TargetType.Random; }
    public virtual float GetCooldown() { return baseCooldown; }
    public virtual int GetDamage() { return baseDamage; }
}

public class BagItemDataCanonLv2 : BagItemDataCanonLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv2; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataCanonLv3 : BagItemDataCanonLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv3; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 5.0f); }
}

public class BagItemDataCanonLv4 : BagItemDataCanonLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CanonLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CanonLv4; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 12.0f); }
}

