using System.Numerics;
using UnityEngine;


public class BagItemDataDriverLv1 : BagItemDataBase
{    
    // スコア: Damage / CoolDown / Cost = 0.95
    protected override int GetBaseCost() { return 3; }
    protected override int GetBaseDamage() { return 8; }
    protected override float GetBaseCoolDown() { return 2.8f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.Item; }
    public override BagItemName GetItemName() { return BagItemName.Driver; }
    public override string GetTag() { return Consts.Tags.Item; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Rectangle1x2; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv1; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv1; }

    // バッグ編集画面用
    // public override int GetCost() { return 3; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.Damage; }
    public override LaunchType GetLaunchType() { return LaunchType.ThrowStraight; }
    public override TargetType GetTargetType() { return TargetType.Nearest; }
    // public override float GetCooldown() { return baseCooldown; }
    // public override int GetDamage() { return baseDamage; }
}

public class BagItemDataDriverLv2 : BagItemDataDriverLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv2; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 2.25f); }
}

public class BagItemDataDriverLv3 : BagItemDataDriverLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    // public override bool GetIsMergable() { return true; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv3; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 5.0f); }
}

public class BagItemDataDriverLv4 : BagItemDataDriverLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv4; }
    // public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.DriverLv4; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.DriverLv4; }

    // バトル画面用
    // public override int GetDamage() { return (int)(baseDamage * 12.0f); }
}

