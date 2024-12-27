
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public static BagItem InstantiateItem(BagItemName itemName, BagItemLevel lv)
    {
        BagItemDataBase data = BagItemDataList.GetItemData(itemName, lv);
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
        GameObject obj = new GameObject(data.ItemName.ToString());
        obj.transform.SetParent(BasicUtil.GetRootObject(Consts.Roots.BagItemsRoot).transform);
        // BagItem初期設定
        BagItem item = obj.AddComponent<BagItem>();
        item.SetItemData(data);
        item.tag = item.GetData().Tag;
        // 画像関連
        SpriteRenderer sr = new GameObject("Image").AddComponent<SpriteRenderer>();
        sr.transform.SetParent(item.transform);
        sr.sprite = BasicUtil.LoadSprite4Resources(data.SpritePathBagEdit);
        switch(data.Tag)
        {
            case Consts.Tags.Bag:
                sr.sortingLayerName = Consts.SortingLayer.Bag;
                break;
            case Consts.Tags.Item:
                sr.sortingLayerName = Consts.SortingLayer.Item;
                break;
            case Consts.Tags.StageSlot:
                sr.sortingLayerName = Consts.SortingLayer.BagSlot;
                break;
        }
        // セル関連
        InitCells(item);
        // Collider関連
        InitCollider(item);
        // Rigidbody関連
        Rigidbody2D rb = item.AddComponent<Rigidbody2D>();
        item.SetPhysicSimulator(false);
        return item;


        // // アイテムPrefabが保持されていない場合、新たに生成して保存する
        // if(!itemPrefabs.ContainsKey(data.ItemName))
        // {
        //     // Prefab追加
        //     GameObject prefab = Resources.Load<GameObject>(data.BagPrefabPath);
        //     itemPrefabs.Add(data.ItemName, prefab);
        // }

        // // アイテムPrefabが保持済みなら生成して返す
        // if(itemPrefabs.ContainsKey(data.ItemName))
        // {
        //     GameObject prefab = itemPrefabs[data.ItemName];
        //     if(prefab == null)
        //     {
        //         Debug.LogError("Prefabの取得に失敗: アイテム名 = " + data.ItemName + " / Path = " + data.BagPrefabPath);
        //         return null;
        //     }
        //     GameObject item = Instantiate(prefab);

        //     // セル設定処理
        //     GameObject cellPrefab = GetCellPrefab(item.tag);
        //     for(int x = 0; x < data.Size.x; x++)
        //     {
        //         for(int y = 0; y < data.Size.y; y++)
        //         {
        //             GameObject cellObj = Instantiate(cellPrefab);
        //             cellObj.transform.SetParent(item.transform);
        //             cellObj.transform.localPosition = CalcCellPos(x, y, data.Size);
        //             cellObj.transform.localScale = Vector2.one;
        //             BagItemCell cell = cellObj.GetComponent<BagItemCell>();
        //             cell.CellPos = new Vector2Int(x, y);
        //             // セルの座標を初期化する
        //             cell.SlotPos = new Vector2Int(-1, -1);
        //         }
        //     }

        //     return item.GetComponent<BagItem>();                
        // }

        // Debug.LogError("アイテムの生成に失敗: " + data.GetType());
        // return null;
    }

    private static BagItem InitCollider(BagItem item)
    {
        PolygonCollider2D collider = item.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
        Vector2[] points = null;
        switch(item.GetData().Shape)
        {
            case ColliderShape.Square1x1:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-0.5f, 0.5f), // 左上
                    new Vector2(-0.5f, -0.5f), // 左下
                    new Vector2(0.5f, -0.5f), // 右下
                    new Vector2(0.5f, 0.5f), // 右上
                };            
                break;
            case ColliderShape.Square2x2:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-1.0f, 1.0f), // 左上
                    new Vector2(-1.0f, -1.0f), // 左下
                    new Vector2(1.0f, -1.0f), // 右下
                    new Vector2(1.0f, 1.0f), // 右上
                };            
                break;
            case ColliderShape.Rectangle2x1:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-1.0f, 0.5f), // 左上
                    new Vector2(-1.0f, -0.5f), // 左下
                    new Vector2(1.0f, -0.5f), // 右下
                    new Vector2(1.0f, 0.5f), // 右上
                };            
                break;
            case ColliderShape.Rectangle3x1:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-1.5f, 0.5f), // 左上
                    new Vector2(-1.5f, -0.5f), // 左下
                    new Vector2(1.5f, -0.5f), // 右下
                    new Vector2(1.5f, 0.5f), // 右上
                };            
                break;
            case ColliderShape.Rectangle4x1:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-2.0f, 0.5f), // 左上
                    new Vector2(-2.0f, -0.5f), // 左下
                    new Vector2(2.0f, -0.5f), // 右下
                    new Vector2(2.0f, 0.5f), // 右上
                };            
                break;
            case ColliderShape.Rectangle1x2:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-0.5f, 1.0f), // 左上
                    new Vector2(-0.5f, -1.0f), // 左下
                    new Vector2(0.5f, -1.0f), // 右下
                    new Vector2(0.5f, 1.0f), // 右上
                };            
                break;
            case ColliderShape.Rectangle1x3:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-0.5f, 1.5f), // 左上
                    new Vector2(-0.5f, -1.5f), // 左下
                    new Vector2(0.5f, -1.5f), // 右下
                    new Vector2(0.5f, 1.5f), // 右上
                };            
                break;
            case ColliderShape.Rectangle1x4:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-0.5f, 2.0f), // 左上
                    new Vector2(-0.5f, -2.0f), // 左下
                    new Vector2(0.5f, -2.0f), // 右下
                    new Vector2(0.5f, 2.0f), // 右上
                };            
                break;
            case ColliderShape.TShape:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-1.5f, 1.5f), // 左上
                    new Vector2(-1.5f, 0.5f),
                    new Vector2(-0.5f, 0.5f),
                    new Vector2(-0.5f, -1.5f), // 左下
                    new Vector2(0.5f, -1.5f), // 右下
                    new Vector2(0.5f, 0.5f),
                    new Vector2(1.5f, 0.5f),
                    new Vector2(1.5f, 1.5f),
                };
                break;
            case ColliderShape.TriangleVToR:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-1.0f, 1.0f), // 左上
                    new Vector2(-1.0f, -1.0f), // 左下
                    new Vector2(0.0f, -1.0f), 
                    new Vector2(0.0f, 0.0f), // 中央
                    new Vector2(1.0f, 0.0f),                     
                    new Vector2(1.0f, 1.0f), // 右上
                };
                break;
            case ColliderShape.TriangleVToL:
                points = new Vector2[]
                {
                    // 左上から反時計回りに
                    new Vector2(-1.0f, 1.0f), // 左上
                    new Vector2(-1.0f, 0.0f), 
                    new Vector2(0.0f, 0.0f), // 中央
                    new Vector2(0.0f, -1.0f), 
                    new Vector2(1.0f, -1.0f), // 右下
                    new Vector2(1.0f, 1.0f), // 右上
                };
                break;
            default:
                Debug.LogError("不正のShape: " + item.GetData().Shape);
                break;
        }
        collider.points = points;
        return item;
    }

    private static BagItem InitCells(BagItem item)
    {
        GameObject cellPrefab = GetCellPrefab(item.tag);
        switch(item.GetData().Shape)
        {
            case ColliderShape.Square1x1:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                break;
            case ColliderShape.Square2x2:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 1));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 1));
                break;
            case ColliderShape.Rectangle2x1:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 0));
                break;
            case ColliderShape.Rectangle3x1:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(2, 0));
                break;
            case ColliderShape.Rectangle4x1:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(2, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(3, 0));
                break;
            case ColliderShape.Rectangle1x2:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 1));
                break;
            case ColliderShape.Rectangle1x3:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 1));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 2));
                break;
            case ColliderShape.Rectangle1x4:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 1));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 2));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 3));
                break;
            case ColliderShape.TShape:
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 1));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 2));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 2));
                CreateCellAt(item, cellPrefab, new Vector2Int(2, 2));
                break;
            case ColliderShape.TriangleVToR:
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 1));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 0));
                break;
            case ColliderShape.TriangleVToL:
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 0));
                CreateCellAt(item, cellPrefab, new Vector2Int(1, 1));
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                break;
            default:
                Debug.LogError("不正のShape: " + item.GetData().Shape);
                CreateCellAt(item, cellPrefab, new Vector2Int(0, 0));
                break;
        }
        return item;
    }

    private static BagItemCell CreateCellAt(BagItem item, GameObject cellPrefab, Vector2Int cellPos)
    {
        GameObject cellObj = Instantiate(cellPrefab);
        cellObj.transform.SetParent(item.transform);
        cellObj.transform.localPosition = CalcCellPos(cellPos.x, cellPos.y, item.GetData().Size);
        cellObj.transform.localScale = Vector2.one;
        BagItemCell cell = cellObj.GetComponent<BagItemCell>();
        cell.CellPos = cellPos;
        // セルの座標を初期化する
        cell.SlotPos = new Vector2Int(-1, -1);
        return cell;
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

        string prefabPath;
        switch(cellName)
        {
            case BagCellName.CellStageSlot:
                prefabPath = Consts.Resources.Prefabs.BagItems.CellStageSlot;
                break;
            case BagCellName.CellBag:
                prefabPath = Consts.Resources.Prefabs.BagItems.CellBag;
                break;
            case BagCellName.CellItem:
                prefabPath = Consts.Resources.Prefabs.BagItems.CellItem;
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