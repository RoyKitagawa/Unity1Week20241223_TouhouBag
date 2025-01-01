using System.Numerics;
using UnityEngine;

public class BagItemDataBag1x1 : BagItemDataBase
{
    protected override int GetBaseCost() { return 2; }
    protected override int GetBaseDamage() { return 0; }
    protected override float GetBaseCoolDown() { return 99f; }
    protected override float GetPerLevelBuff() { return 1f; }
    protected override float GetPerLevelCostMultiplier() { return 1f; }

    // 基本情報系
    public override BagItemType GetItemType() { return BagItemType.Bag; }
    public override BagItemName GetItemName() { return BagItemName.Bag1x1; }
    public override string GetTag() { return Consts.Tags.Bag; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Square1x1; }

    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public override bool GetIsMergable() { return false; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.Bag1x1; }
    public override string GetSpritePathItemThumb() { return ""; }

    // バトル画面用
    public override DamageType GetDamageType() { return DamageType.None; }
    public override LaunchType GetLaunchType() { return LaunchType.None; }
    public override TargetType GetTargetType() { return TargetType.None; }
}

public class BagItemDataBag3x1 : BagItemDataBag1x1
{
    public override BagItemName GetItemName() { return BagItemName.Bag3x1; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Rectangle3x1; }
    public override string GetSpritePathItem() { return ""; }
    public override string GetSpritePathItemThumb() { return ""; }
}

public class BagItemDataBag2x2 : BagItemDataBag1x1
{
    protected override int GetBaseCost() { return 3; }
    public override BagItemName GetItemName() { return BagItemName.Bag2x2; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Square2x2; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.Bag2x2; }
    public override string GetSpritePathItemThumb() { return ""; }
}

public class BagItemDataBag2x1 : BagItemDataBag1x1
{
    protected new int baseCost = 2;
    public override BagItemName GetItemName() { return BagItemName.Bag2x1; }
    public override ColliderShape GetColliderShape() { return ColliderShape.Rectangle2x1; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.Bag2x1; }
    public override string GetSpritePathItemThumb() { return ""; }
}