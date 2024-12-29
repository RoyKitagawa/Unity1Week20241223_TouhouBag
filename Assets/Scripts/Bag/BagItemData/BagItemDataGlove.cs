using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataGloveLv1 : BagItemDataBase
{
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
    public virtual float GetCooldown() { return 2.0f; }
    public virtual int GetDamage() { return 3; }
}

public class BagItemDataGloveLv2 : BagItemDataGloveLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.GloveLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.GloveLv2; }

    // バトル画面用
    public override float GetCooldown() { return 2.0f; }
    public override int GetDamage() { return 8; }
}

public class BagItemDataGloveLv3 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.GloveLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.GloveLv3; }

    // バトル画面用
    public override float GetCooldown() { return 2.0f; }
    public override int GetDamage() { return 20; }
}

