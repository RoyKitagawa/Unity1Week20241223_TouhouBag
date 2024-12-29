using System.Numerics;
using TMPro;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataBombLv1 : BagItemDataBase
{
    protected int baseDamage = 5;
    protected float baseCooldown = 3.0f;

    // 基本情報系
    public virtual BagItemType GetItemType() { return BagItemType.Item; }
    public virtual BagItemName GetItemName() { return BagItemName.Bomb; }
    public virtual string GetTag() { return Consts.Tags.Item; }
    public virtual ColliderShape GetColliderShape() { return ColliderShape.Square1x1; }

    // レベル関連
    public virtual BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public virtual bool GetIsMergable() { return true; }
    public virtual string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv1; }
    public virtual string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv1; }

    // バッグ編集画面用
    public virtual int GetCost() { return 3; }

    // バトル画面用
    public virtual DamageType GetDamageType() { return DamageType.NormalDamage; }
    public virtual LaunchType GetLaunchType() { return LaunchType.Unique; }
    public virtual TargetType GetTargetType() { return TargetType.LowestLife; }
    public virtual float GetCooldown() { return baseCooldown; }
    public virtual int GetDamage() { return baseDamage; }
}

public class BagItemDataBombLv2 : BagItemDataBombLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv2; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataBombLv3 : BagItemDataBombLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv3; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 5.0f); }
}

public class BagItemDataBombLv4 : BagItemDataBombLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.BombLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.BombLv4; }

    // バトル画面用
    public override int GetDamage() { return (int)(baseDamage * 12.0f); }
}
