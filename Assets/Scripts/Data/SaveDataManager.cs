using UnityEngine;

public class SaveDataManager
{
    public static void SaveBattleSpeed(float speed)
    {
        PlayerPrefs.SetFloat(Consts.PlayerPrefs.Keys.GameSpeed, speed);
        PlayerPrefs.Save();
    }

    public static float LoadBattleSpeed()
    {
        float speed = PlayerPrefs.GetFloat(Consts.PlayerPrefs.Keys.GameSpeed, 1.0f);
        return speed;
    }

    // public static void ClearProgress()
    // {
    //     PlayerPrefs.DeleteKey(Consts.PlayerPrefs.Keys.ProgressData);
    // }

    // public static void SaveProgress()
    // {
    //     // セーブ用の情報をセット
    //     SaveData saveData = new SaveData();
    //     saveData.SetDataMoney(ManagerGame.Instance.GetMoney());
    //     saveData.SetDataClearedWaves(ManagerGame.Instance.GetClearedWave());
    //     foreach(BagItem bag in ManagerGame.Bags)
    //     {
    //         if(!bag.IsPlaced()) continue;
    //         BagItemData data = bag.GetData();
    //         saveData.AddBagData(new ItemData(data.ItemName, data.Level, bag.GetRotation(), bag.GetSlotPos()));
    //     }
    //     foreach(BagItem item in ManagerGame.Items)
    //     {
    //         if(!item.IsPlaced()) continue;
    //         BagItemData data = item.GetData();
    //         saveData.AddWeaponData(new ItemData(data.ItemName, data.Level, item.GetRotation(), item.GetSlotPos()));
    //     }
    //     // 実際にセーブ
    //     string json = JsonUtility.ToJson(saveData);
    //     PlayerPrefs.SetString(Consts.PlayerPrefs.Keys.ProgressData, json);
    //     PlayerPrefs.Save();
    //     Debug.Log("データを保存しました： " + json);
    // }

    // public static SaveData LoadProgress()
    // {
    //     if(!PlayerPrefs.HasKey(Consts.PlayerPrefs.Keys.ProgressData))
    //     {
    //         Debug.Log("ロードするデータが存在しませんでした。");
    //         return null;
    //     }
    //     string json = PlayerPrefs.GetString(Consts.PlayerPrefs.Keys.ProgressData);
    //     SaveData saveData = JsonUtility.FromJson<SaveData>(json);
    //     Debug.Log("データのロードに成功しました： バッグ数 = " + saveData.Bags.Count + "武器数 = " + saveData.Weapons.Count);

    //     return saveData;
    // }

    // public static void ApplyMoneySaveData(SaveData saveData)
    // {
    //     if(saveData == null)
    //     {
    //         Debug.Log("セーブデータが存在しません");
    //         return;
    //     }
    //     Debug.Log("セーブデータの反映を実施します");

    //     // 金銭情報
    //     ManagerGame.Instance.SetMoney(saveData.CurrentMoney);
    // }

    // public static void ApplyWavesSaveData(SaveData saveData)
    // {
    //     if(saveData == null)
    //     {
    //         Debug.Log("セーブデータが存在しません");
    //         return;
    //     }
    //     Debug.Log("セーブデータの反映を実施します");
        
    //     // ウェーブ情報
    //     ManagerGame.Instance.SetClearedWave(saveData.ClearedWaves);
    //     ManagerGame.Instance.SetCurrentWave(saveData.ClearedWaves + 1);
    // }

    // public static void ApplyItemsSavedData(SaveData saveData, bool doPlaceItem)
    // {
    //     if(saveData == null)
    //     {
    //         Debug.Log("セーブデータが存在しません");
    //         return;
    //     }
    //     Debug.Log("セーブデータの反映を実施します");

    //     // バッグ系
    //     ManagerGame.Bags.Clear();
    //     foreach(ItemData itemData in saveData.Bags)
    //     {
    //         BagItem item = BagItemManager.InstantiateItem(itemData.GetName(), itemData.GetLevel());
    //         // item.SetIsPurchased(true);
    //         item.SetIsPlaced(true);
    //         if(doPlaceItem)
    //         {
    //             Rotation rot = itemData.GetRotation();
    //             Vector2Int[] slotPos = itemData.GetPlacedSlots();
    //             item.PlaceItemAt(rot, slotPos);
    //         }
    //         else
    //         {
    //             item.gameObject.SetActive(false);
    //         }
    //         ManagerGame.Instance.Add2List(item);
    //     }
    //     // 武器系
    //     ManagerGame.Items.Clear();
    //     foreach(ItemData itemData in saveData.Weapons)
    //     {
    //         BagItem item = BagItemManager.InstantiateItem(itemData.GetName(), itemData.GetLevel());
    //         // item.SetIsPurchased(true);
    //         item.SetIsPlaced(true);
    //         if(doPlaceItem)
    //         {
    //             Rotation rot = itemData.GetRotation();
    //             Vector2Int[] slotPos = itemData.GetPlacedSlots();
    //             item.PlaceItemAt(rot, slotPos);
    //         }
    //         else
    //         {
    //             item.gameObject.SetActive(false);
    //         }
    //         ManagerGame.Instance.Add2List(item);
    //     }
    //     Debug.Log("セーブデータの反映が完了しました");
    // }
}