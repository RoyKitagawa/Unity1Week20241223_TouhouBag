using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataScrewLv1 : BagItemDataBase
{
    // スコア: Damage / CoolDown / Cost = 0.9
    protected override int GetBaseCost() { return 1; }
    protected override int GetBaseDamage() { return 2; }
    protected override float GetBaseCoolDown() { return 2.2f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.Item; }
    public override BagItemName GetItemName() { return BagItemName.Screw; }
    public override string GetTag() { return Consts.Tags.Item; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Square1x1; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.ScrewLv1; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.ScrewLv1; }

    // バッグ編集画面用
    // public override int GetCost() { return 1; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.Damage; }
    public override LaunchType GetLaunchType() { return LaunchType.ThrowParabola; }
    public override TargetType GetTargetType() { return TargetType.Nearest; }
    // public override float GetCooldown() { return baseCooldown; }
    // public override int GetDamage() { return baseDamage; }
}

public class BagItemDataScrewLv2 : BagItemDataScrewLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.ScrewLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.ScrewLv2; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataScrewLv3 : BagItemDataScrewLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.ScrewLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.ScrewLv3; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 5f); }
}

public class BagItemDataScrewLv4 : BagItemDataScrewLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    // public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.ScrewLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.ScrewLv4; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 12f); }
}
