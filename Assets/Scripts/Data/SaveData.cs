
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// JSONで保存する用のデータを管理するクラス
/// </summary>
[System.Serializable]
public class SaveData
{
    public int ClearedWaves;
    public int CurrentMoney;
    public List<ItemData> Bags = new List<ItemData>();
    public List<ItemData> Weapons = new List<ItemData>();

    public void SetDataMoney(int currentMoney) { CurrentMoney = currentMoney; }
    public void SetDataClearedWaves(int clearedWaves) { ClearedWaves = clearedWaves; }
    public void AddBagData(ItemData data) { Bags.Add(data); }
    public void ClearBagData() { Bags.Clear(); }
    public void AddWeaponData(ItemData data) { Weapons.Add(data); }
    public void ClearWeaponsData() { Weapons.Clear(); }
}

[System.Serializable]
public class ItemData
{
    public string Name; // アイテムの名前
    public string Level; // アイテムのレベル
    public string Rot; // アイテムがどの角度ではまっていたか
    public Vector2Int[] PlacedSlots; // 各セルがはまっていたスロット座標

    /// <summary>
    /// 武器データを登録する
    /// </summary>
    /// <param name="data"></param>
    public ItemData(BagItemName name, BagItemLevel level, Rotation rot, Vector2Int[] plcacedSlots)
    {
        Name = name.ToString();
        Level = level.ToString();
        Rot = rot.ToString();
        PlacedSlots = plcacedSlots;
    }

    /// <summary>
    /// 名前を取得する
    /// </summary>
    /// <returns></returns>
    public BagItemName GetName()
    {
        return BasicUtil.ParseEnum<BagItemName>(Name);
    }

    /// <summary>
    /// レベルを取得する
    /// </summary>
    /// <returns></returns>
    public BagItemLevel GetLevel()
    {
        return BasicUtil.ParseEnum<BagItemLevel>(Level);
    }

    /// <summary>
    /// 角度を取得する
    /// </summary>
    /// <returns></returns>
    public Rotation GetRotation()
    {
        return BasicUtil.ParseEnum<Rotation>(Rot);
    }

    /// <summary>
    /// 各セルが配置されているスロットを取得する
    /// </summary>
    /// <returns></returns>
    public Vector2Int[] GetPlacedSlots()
    {
        return PlacedSlots;
    }
}