using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataCucumberLv1 : SimpleSingleton<BagItemDataCucumberLv1>, BagItemDataBase
{
    // 基本情報系
    public virtual BagItemType GetItemType() { return BagItemType.Item; }
    public virtual BagItemName GetItemName() { return BagItemName.Cucumber; }
    public virtual string GetTag() { return Consts.Tags.Item; }
    public virtual ColliderShape GetColliderShape() { return ColliderShape.Rectangle1x2; }

    // レベル関連
    public virtual BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public virtual string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv1; }
    public virtual string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv1; }

    // バッグ編集画面用
    public virtual int GetCost() { return 2; }

    // バトル画面用
    public virtual DamageType GetDamageType() { return DamageType.Heal; }
    public virtual TargetType GetTargetType() { return TargetType.Self; }
    public virtual float GetCooldown() { return 2.0f; }
    public virtual int GetDamage() { return 10; }

    public virtual string GetBagPrefabPath() { return ""; }    
    public virtual string GetBattlePrefabPath() { return ""; }
}

public class BagItemDataCucumberLv2 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv2; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv2; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv2; }

    // バトル画面用
    public override float GetCooldown() { return 2.0f; }
    public override int GetDamage() { return 20; }
}

public class BagItemDataCucumberLv3 : BagItemDataCucumberLv1
{
    // レベル関連
    public override BagItemLevel GetLevel() { return BagItemLevel.Lv3; }
    public override string GetSpritePathItem() { return Consts.Resources.Sprites.BattleItem.CucumberLv3; }
    public override string GetSpritePathItemThumb() { return Consts.Resources.Sprites.BattleItem.Thumb.CucumberLv3; }

    // バトル画面用
    public override float GetCooldown() { return 2.0f; }
    public override int GetDamage() { return 30; }
}

