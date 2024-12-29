using System.Numerics;
using UnityEngine;

/// <summary>
/// グローブ
/// </summary>
public class BagItemDataGloveLv1 : BagItemDataBase
{
    protected int baseDamage = 3;
    protected float baseCooldown = 2.0f;
    
    // 基本情報系
    public virtual BagItemType GetItemType() { return BagItemType.Item; }
    public virtual BagItemName GetItemName() { return BagItemName.Glove; }
    public virtual string GetTag() { return Consts.Tags.Item; }
    public virtual ColliderShape GetColliderShape() { return ColliderShape.Square1x1; }

    // レベル関連
    public virtual BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public virtual bool GetIsMergable() { return true; }
    public virtual string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.GloveLv1; }
    public virtual string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.GloveLv1; }

    // バッグ編集画面用
    public virtual int GetCost() { return 1; }

    // バトル画面用
    public virtual DamageType GetDamageType() { return DamageType.Shield; }
    public virtual LaunchType GetLaunchType() { return LaunchType.ThrowParabola; }
    public virtual TargetType GetTargetType() { return TargetType.Self; }
    public virtual float GetCooldown() { return baseCooldown; }
    public virtual int GetDamage() { return baseDamage; }
}

public class BagItemDataGloveLv2 : BagItemDataGloveLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.GloveLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.GloveLv2; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataGloveLv3 : BagItemDataGloveLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.GloveLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.GloveLv3; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 5.0f); }
}

public class BagItemDataGloveLv4 : BagItemDataGloveLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.GloveLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.GloveLv4; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 12.0f); }
}
