
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

/// <summary>
/// バッグアイテム管理用マネージャー
/// </summary>
public class BagItemManager : MonoBehaviourSingleton<BagItemManager>
{
    // アイテム生成用Prefabのキャッシュ管理
    private static Dictionary<BagItemName, GameObject> itemPrefabs = new Dictionary<BagItemName, GameObject>();
    private static Dictionary<BagCellName, GameObject> cellPrefabs = new Dictionary<BagCellName, GameObject>();

    /// <summary>
    /// アイテムを生成、初期化する
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public static BagItem InstantiateItem(BagItemName itemName)
    {
        BagItemDataBase data = BagItemDataList.GetItemData(itemName);
        if(data == null) return null;
        BagItem item = GetItemInstance(data);
        if(item == null) return null;
        item.transform.SetParent(BasicUtil.GetRootObject(Consts.Roots.BagItemsRoot).transform);
        item.SetItemData(data);

        // SpriteRenderer sr = item.GetComponentInChildren<SpriteRenderer>();
        // Sprite sprite = sr.sprite;

        // // スケール補正して1unit単位に合わせる
        // float height = sprite.texture.height;
        // float width = sprite.texture.width;
        // float currentPPU = sprite.pixelsPerUnit;
        // float newScaleV = currentPPU / width * data.Size.x;
        // float newScaleH = currentPPU / height * data.Size.y;
        // item.SetScale(new Vector2(newScaleH, newScaleV));
        // item.transform.localScale = new Vector2(newScaleV, newScaleH);

        // 配置設定
        item.transform.localPosition = Vector2.zero;
        item.transform.localScale = Vector2.one;

        return item;
    }

    /// <summary>
    /// アイテムインスタンス生成
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private static BagItem GetItemInstance(BagItemDataBase data)
    {
        // アイテムPrefabが保持されていない場合、新たに生成して保存する
        if(!itemPrefabs.ContainsKey(data.ItemName))
        {
            // Prefab追加
            GameObject prefab = Resources.Load<GameObject>(data.PrefabPath);
            itemPrefabs.Add(data.ItemName, prefab);
        }

        // アイテムPrefabが保持済みなら生成して返す
        if(itemPrefabs.ContainsKey(data.ItemName))
        {
            GameObject item = Instantiate(itemPrefabs[data.ItemName]);

            // セル設定処理
            GameObject cellPrefab = GetCellPrefab(item.tag);
            for(int x = 0; x < data.Size.x; x++)
            {
                for(int y = 0; y < data.Size.y; y++)
                {
                    GameObject cellObj = Instantiate(cellPrefab);
                    cellObj.transform.SetParent(item.transform);
                    cellObj.transform.localPosition = CalcCellPos(x, y, data.Size);
                    cellObj.transform.localScale = Vector2.one;
                    BagItemCell cell = cellObj.GetComponent<BagItemCell>();
                    cell.CellPos = new Vector2Int(x, y);
                    // セルの座標を初期化する
                    cell.SlotPos = new Vector2Int(-1, -1);
                }
            }

            return item.GetComponent<BagItem>();                
        }

        Debug.LogError("アイテムの生成に失敗: " + data.GetType());
        return null;
    }

    private static Vector2 CalcCellPos(int x, int y, Vector2Int itemSize)
    {
        // 中心からのオフセットを計算
        float offsetX = (itemSize.x - 1) / 2f;  // 横方向のオフセット
        float offsetY = (itemSize.y - 1) / 2f;  // 縦方向のオフセット

        // 指定されたセルの位置を計算
        float posX = x - offsetX;
        float posY = offsetY - y; // 縦方向は反転していることを考慮

        return new Vector2(posX, posY);
    }

    public static GameObject GetCellPrefab(string tag)
    {
        BagCellName cellName = BagCellName.CellItem;
        switch(tag)
        {
            case Consts.Tags.StageSlot:
                cellName = BagCellName.CellStageSlot;
                break;
            case Consts.Tags.Bag:
                cellName = BagCellName.CellBag;
                break;
            case Consts.Tags.Item:
                cellName = BagCellName.CellItem;
                break;
            default:
                Debug.LogError("不明なアイテムタグ：" + tag);
                return null;
        }
        return GetCellPrefab(cellName);
    }

    public static GameObject GetCellPrefab(BagCellName cellName)
    {
        // 存在する場合は即座に返す
        if(cellPrefabs.ContainsKey(cellName))
            return cellPrefabs[cellName];

        string prefabPath = "";
        switch(cellName)
        {
            case BagCellName.CellStageSlot:
                prefabPath = Consts.Resources.CellStageSlot;
                break;
            case BagCellName.CellBag:
                prefabPath = Consts.Resources.CellBag;
                break;
            case BagCellName.CellItem:
                prefabPath = Consts.Resources.CellItem;
                break;
            default:
                Debug.LogError("不明なセル名：" + cellName);
                return null;
        }

        // セル生成
        GameObject cellPrefab = Resources.Load<GameObject>(prefabPath);
        // キャッシュに保存
        if(cellPrefab != null) cellPrefabs.Add(cellName, cellPrefab);
        else Debug.LogError("生成したCellPrefabがNull: cellName = " + cellName + " / path = " + prefabPath);
        return cellPrefab;
    }
}