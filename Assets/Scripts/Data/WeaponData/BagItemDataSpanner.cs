using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataSpannerLv1 : BagItemDataBase
{
    // スコア: Damage / CoolDown / Cost = 0.96
    protected override int GetBaseCost() { return 3; }
    protected override int GetBaseDamage() { return 15; }
    protected override float GetBaseCoolDown() { return 5.2f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.Item; }
    public override BagItemName GetItemName() { return BagItemName.Spanner; }
    public override string GetTag() { return Consts.Tags.Item; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Rectangle1x2; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.SpannerLv1; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.SpannerLv1; }

    // バッグ編集画面用
    // public override int GetCost() { return 2; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.Damage; }
    public override LaunchType GetLaunchType() { return LaunchType.ThrowParabola; }
    public override TargetType GetTargetType() { return TargetType.Random; }
    // public override float GetCooldown() { return baseCooldown; }
    // public override int GetDamage() { return baseDamage; }
}

public class BagItemDataSpannerLv2 : BagItemDataSpannerLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.SpannerLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.SpannerLv2; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataSpannerLv3 : BagItemDataSpannerLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.SpannerLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.SpannerLv3; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 5f); }
}

public class BagItemDataSpannerLv4 : BagItemDataSpannerLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    // public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.SpannerLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.SpannerLv4; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 12f); }
}

