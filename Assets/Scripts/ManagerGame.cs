using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体で管理する必要がある要素用マネージャー
/// </summary>
public class ManagerGame : MonoBehaviourSingleton<ManagerGame>
{
    // ステージに存在するスロットの一覧
    public static HashSet<BagItem> Slots = new HashSet<BagItem>();
    // ステージに存在するバッグの一覧
    public static HashSet<BagItem> Bags = new HashSet<BagItem>();
    // ステージに存在するアイテムの一覧
    public static HashSet<BagItem> Items = new HashSet<BagItem>();
    // 所持金情報
    public int InitialMoneyAmt = 999;
    private int moneyAmt;
    // WAVE関連
    private const int totalWaves = 1;
    private int currentWave;
    private int clearedWave;


    // 金銭関連
    public int GetMoney() { return moneyAmt; }
    public void SetMoney(int value) { moneyAmt = value; }
    public void AddMoney(int value) { moneyAmt += value; }
    public void AddMoneyForNewWave(int nextWave) { AddMoney(7 + nextWave); }
    // WAVE関連
    public string GetWaveStatusText() { return "WAVE " + GetCurrentWave() + " / " + GetTotalWaves(); }
    public int GetTotalWaves() { return totalWaves; }
    public int GetCurrentWave() { return currentWave; }
    public void SetCurrentWave(int wave) { currentWave = wave; }
    public int GetClearedWave() { return clearedWave; }
    public void SetClearedWave(int wave) { clearedWave = wave; }
    public bool IsGameClear() { return clearedWave >= totalWaves; }


    public BagItem SpawnRandomItem(BagItemType type)
    {
        BagItem item;
        switch(type)
        {
            case BagItemType.StageSlot:
                item = BagItemManager.InstantiateItem(RandUtil.GetRandomItem(BagItemDataList.GetItemNames(BagItemType.StageSlot)), BagItemLevel.Lv1);
                break;

            case BagItemType.Item:
                item = BagItemManager.InstantiateItem(RandUtil.GetRandomItem(BagItemDataList.GetItemNames(BagItemType.Item)), BagItemLevel.Lv1);
                break;

            case BagItemType.Bag:
                item = BagItemManager.InstantiateItem(RandUtil.GetRandomItem(BagItemDataList.GetItemNames(BagItemType.Bag)), BagItemLevel.Lv1);
                break;

            default:
                Debug.LogError("不正なBagItemType: " + type);
                return null;
        }
        // 生成したアイテムをリストに追加
        Add2List(item);
        return item;
    }

    public Vector2Int GetClosestSlotPos(Vector2 pos)
    {
        float closestDist = -1;
        Vector2Int closestSlotPos = Vector2Int.zero;
        foreach(BagItem slot in Slots)
        {
            BagItemCell cell = slot.GetCellAtCellPos(Vector2Int.zero);
            float dist = Vector2.Distance(pos, cell.transform.position);
            if(closestDist < 0 || dist < closestDist)
            {
                closestSlotPos = cell.SlotPos;
                closestDist = dist;
            }
        }
        return closestSlotPos;
    }

    public Vector2Int GetSlotPosAt(Vector2 worldPos)
    {
        float dist = -1;
        Vector2Int slotPos = Vector2Int.zero;

        foreach(BagItem slot in Slots)
        {
            if(dist == -1 || Vector2.Distance(worldPos, slot.transform.position) < dist)
            {
                BagItemCell targetCell = slot.GetCellAtCellPos(Vector2Int.zero);
                // 座標がセルと被っている場合は判定OKとする
                if(targetCell.IsPosOverlapWithCell(BasicUtil.GetMousePos()))
                {   
                    dist = Vector2.Distance(worldPos, slot.transform.position);
                    slotPos = targetCell.SlotPos;
                }
            }
        }
        return slotPos;
    }

    public Vector2 GetSlotsCenterPos(Vector2Int[] slotPos)
    {
        Vector2 totalPos = Vector2.zero;
        foreach(Vector2Int _slotPos in slotPos)
        {
            totalPos += GetSlotWorldPosAt(_slotPos);
        }
        return totalPos / slotPos.Length;
    }

    public Vector2 GetSlotWorldPosAt(Vector2Int slotPos)
    {
        foreach(BagItem slot in Slots)
        {
            BagItemCell cell = slot.GetCellAtSlotPos(slotPos);
            if(cell != null) return cell.transform.position;
        }
        return Vector2.zero;
    }

    public bool IsSlotExistAtSlot(Vector2Int slotPos) { return GetSlotExistAtSlot(slotPos).Count > 0; }
    public HashSet<BagItem> GetSlotExistAtSlot(Vector2Int slotPos)
    {
         HashSet<BagItem> targets = new HashSet<BagItem>();
        foreach(BagItem slot in Slots)
        {
            if(slot.IsCellExistsAtSlotPos(slotPos)) targets.Add(slot);
        }
        return targets;
    }

    public bool IsBagExistAtSlot(Vector2Int slotPos) { return GetBagsExistAtSlot(slotPos).Count > 0; }
    public HashSet<BagItem> GetBagsExistAtSlot(Vector2Int slotPos)
    {
        HashSet<BagItem> targets = new HashSet<BagItem>();
        foreach(BagItem bag in Bags)
        {
            if(bag.IsCellExistsAtSlotPos(slotPos)) targets.Add(bag);
        }
        return targets;
    }

    public bool IsItemExistAtSlot(Vector2Int slotPos) { return GetItemsExistAtSlot(slotPos).Count > 0; }
    public HashSet<BagItem> GetItemsExistAtSlot(Vector2Int slotPos)
    {
        HashSet<BagItem> targets = new HashSet<BagItem>();
        foreach(BagItem item in Items)
        {
            if(item.IsCellExistsAtSlotPos(slotPos)) targets.Add(item); 
        }
        return targets;
    }

    public bool IsItemExistAtWorld(Vector2 worldPos) { return GetItemExistAtWorld(worldPos) != null; }
    public BagItem GetItemExistAtWorld(Vector2 worldPos)
    {
        foreach(BagItem item in Items)
        {
            if(item.IsPosOverlapWithItem(worldPos)) return item;
        }
        return null;
    }
    
    public void Add2List(BagItem item)
    {
        if(item == null) return;
        switch(item.GetData().ItemType)
        {
            case BagItemType.StageSlot:
                AddSlot2List(item);
                break;
            case BagItemType.Bag:
                AddBag2List(item);
                break;
            case BagItemType.Item:
                AddItem2List(item);
                break;
            default:
                Debug.LogError("追加するアイテムの種別が不正：" + item.GetData().ItemType);
                break;
        }
    }
    public void RemoveFromList(BagItem item)
    {
        if(item == null) return;
        switch(item.GetData().ItemType)
        {
            case BagItemType.StageSlot:
                RemoveFromSlotList(item);
                break;
            case BagItemType.Bag:
                RemoveFromBagList(item);
                break;
            case BagItemType.Item:
                RemoveFromItemList(item);
                break;
            default:
                Debug.LogError("削除するアイテムの種別が不正：" + item.GetData().ItemType);
                break;
        }
    }
    private void AddSlot2List(BagItem slot) { if(slot != null) Slots.Add(slot); }
    private void AddBag2List(BagItem bag) { if(bag != null) Bags.Add(bag); }
    private void AddItem2List(BagItem item) { if(item != null) Items.Add(item); }
    private void RemoveFromSlotList(BagItem slot) { if(slot != null) Slots.Remove(slot); }
    private void RemoveFromBagList(BagItem bag) { if(bag != null) Bags.Remove(bag); }
    private void RemoveFromItemList(BagItem item) { if(item != null) Items.Remove(item); }


    public void ClearAllInStageObjectLists()
    {
        Slots.Clear();
        Bags.Clear();
        Items.Clear();
    }
}
