using System.Numerics;
using UnityEngine;

/// <summary>
/// 通常アップル
/// </summary>
public class BagItemDataBase : BagItemData
{
    protected int baseCost { get { return GetBaseCost(); } }
    protected int baseDamage { get { return GetBaseDamage(); } }
    protected float baseCooldown { get { return GetBaseCoolDown(); } }
    protected float perLevelBuff { get { return GetPerLevelBuff(); } }
    protected float perLevelCostMultiplier { get { return GetPerLevelCostMultiplier(); } }

    protected virtual int GetBaseCost() { return 1; }
    protected virtual int GetBaseDamage() { return 1; }
    protected virtual float GetBaseCoolDown() { return 1.0f; }
    protected virtual float GetPerLevelBuff() { return 2.5f; }
    protected virtual float GetPerLevelCostMultiplier() { return 1.5f; }

    // 基本情報系
    public virtual BagItemType GetItemType() { return BagItemType.None; }
    public virtual BagItemName GetItemName() { return BagItemName.None; }
    public virtual string GetTag() { return ""; }
    public virtual ColliderShape GetColliderShape() { return ColliderShape.Square1x1; }

    // レベル関連
    public virtual BagItemLevel GetLevel() { return BagItemLevel.Lv1; }
    public virtual bool GetIsMergable() { return GetLevel() != BagItemLevel.Lv4; }
    public virtual string GetSpritePathItem() { return ""; }
    public virtual string GetSpritePathItemThumb() { return ""; }
    public virtual int GetLevelAsInt()
    {
        switch(GetLevel())
        {
            case BagItemLevel.Lv1:
                return 1;
            case BagItemLevel.Lv2:
                return 2;
            case BagItemLevel.Lv3:
                return 3;
            case BagItemLevel.Lv4:
                return 4;
            default:
                Debug.LogError("このレベルは未対応です: " + GetLevel());
                return 1;
        }
    }

    // バッグ編集画面用
    public virtual int GetCost() { return (int)(baseCost * Mathf.Pow(perLevelCostMultiplier, GetLevelAsInt() - 1)); }

    // バトル画面用
    public virtual DamageType GetDamageType() { return DamageType.None; }
    public virtual LaunchType GetLaunchType() { return LaunchType.None; }
    public virtual TargetType GetTargetType() { return TargetType.None; }
    public virtual float GetCooldown() { return baseCooldown; }
    public virtual int GetDamage() {
        int value = (int)(baseDamage * Mathf.Pow(perLevelBuff, GetLevelAsInt() - 1));
        Debug.Log("Weapon: " + GetItemName() + " / Level: " + GetLevelAsInt() + " / " + value);
        return value;
    }

}
