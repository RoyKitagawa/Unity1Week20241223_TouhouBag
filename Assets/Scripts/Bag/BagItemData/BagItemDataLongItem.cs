using System.Numerics;
using UnityEngine;

public class BagItemDataLongItem : SimpleSingleton<BagItemDataLongItem>, BagItemDataBase
{
    // 基本情報系
    BagItemType BagItemDataBase.GetType() { return BagItemType.Item; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.LongItem; }
    string BagItemDataBase.GetTag() { return Consts.Tags.Item; }
    ColliderShape BagItemDataBase.GetColliderShape() { return ColliderShape.Rectangle2x1; }
    string BagItemDataBase.GetBagPrefabPath() { return Consts.Resources.Prefabs.BagItems.ItemLong; }
    string BagItemDataBase.GetBattlePrefabPath() { return Consts.Resources.Prefabs.ProjectileItems.Long; }    
    string BagItemDataBase.GetSpritePathBagEdit() { return Consts.Resources.Sprites.BattleItem.Long; }
    string BagItemDataBase.GetSpritePathBattleItemList() { return Consts.Resources.Sprites.BattleItem.Thumb.Long; }

    // バッグ編集画面用
    int BagItemDataBase.GetCost() { return 2; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(2, 1); }

    // バトル画面用
    DamageType BagItemDataBase.GetDamageType() { return DamageType.NormalDamage; }
    TargetType BagItemDataBase.GetTargetType() { return TargetType.Random; }
    float BagItemDataBase.GetCooldown() { return 1.25f; }
    int BagItemDataBase.GetDamage() { return 17; }

    // レベル
    int BagItemDataBase.GetLevel() { return 1; }
}