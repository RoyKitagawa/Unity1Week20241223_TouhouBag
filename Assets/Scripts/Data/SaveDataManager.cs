using UnityEngine;

public class SaveDataManager
{
    public static void SaveProgress()
    {
        // セーブ用の情報をセット
        SaveData saveData = new SaveData();
        saveData.SetDataMoney(ManagerGame.Instance.GetMoney());
        saveData.SetDataClearedWaves(ManagerGame.Instance.GetClearedWave());
        foreach(BagItem bag in StageManager.Bags)
        {
            if(!bag.IsPlaced()) continue;
            BagItemDataBase data = bag.GetData();
            saveData.AddBagData(new ItemData(data.ItemName, data.Level, bag.GetRotation(), bag.GetSlotPos()));
        }
        foreach(BagItem item in StageManager.Items)
        {
            if(!item.IsPlaced()) continue;
            BagItemDataBase data = item.GetData();
            saveData.AddBagData(new ItemData(data.ItemName, data.Level, item.GetRotation(), item.GetSlotPos()));
        }
        // 実際にセーブ
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(Consts.PlayerPrefs.Keys.ProgressData, json);
        PlayerPrefs.Save();
        Debug.Log("データを保存しました： " + json);
    }

    public static SaveData LoadProgress()
    {
        if(!PlayerPrefs.HasKey(Consts.PlayerPrefs.Keys.ProgressData))
        {
            Debug.Log("ロードするデータが存在しませんでした。");
            return null;
        }
        string json = PlayerPrefs.GetString(Consts.PlayerPrefs.Keys.ProgressData);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("データのロードに成功しました： バッグ数 = " + saveData.Bags.Count + "武器数 = " + saveData.Weapons.Count);

        return saveData;
    }

    public static void ApplySavedData(SaveData saveData)
    {
        if(saveData == null)
        {
            Debug.LogError("セーブデータが存在しません");
            return;
        }
        // 金銭情報
        ManagerGame.Instance.SetMoney(saveData.CurrentMoney);
        // ウェーブ情報
        ManagerGame.Instance.SetClearedWave(saveData.ClearedWaves);
        ManagerGame.Instance.SetCurrentWave(saveData.ClearedWaves + 1);

        foreach(ItemData itemData in saveData.Bags)
        {
            BagItem item = BagItemManager.InstantiateItem(itemData.GetName(), itemData.GetLevel());
            item.SetIsPurchased(true);
            Rotation rot = itemData.GetRotation();
            Vector2Int[] slotPos = itemData.GetPlacedSlots();
            item.PlaceItemAt(rot, slotPos);
            StageManager.Instance.Add2List(item);
        }
        foreach(ItemData itemData in saveData.Weapons)
        {
            BagItem item = BagItemManager.InstantiateItem(itemData.GetName(), itemData.GetLevel());
            item.SetIsPurchased(true);
            Rotation rot = itemData.GetRotation();
            Vector2Int[] slotPos = itemData.GetPlacedSlots();
            item.PlaceItemAt(rot, slotPos);
            StageManager.Instance.Add2List(item);
        }

    }
}