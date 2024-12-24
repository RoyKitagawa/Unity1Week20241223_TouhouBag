using System.Numerics;
using UnityEngine;

/// <summary>
/// 初期からステージ上に配置されているステージスロット
/// 処理の共通化のため、アイテムとして識別しておく
/// </summary>
public class BagItemDataStageSlot : SimpleSingleton<BagItemDataStageSlot>, BagItemDataBase
{
    BagItemType BagItemDataBase.GetType() { return BagItemType.StageSlot; }
    BagItemName BagItemDataBase.GetName() { return BagItemName.StageSlot; }
    string BagItemDataBase.GetPrefabPath() { return "Prefabs/StageSlot"; }
    int BagItemDataBase.GetCost() { return 0; }
    Vector2Int BagItemDataBase.GetSize() { return new Vector2Int(1, 1); }
}