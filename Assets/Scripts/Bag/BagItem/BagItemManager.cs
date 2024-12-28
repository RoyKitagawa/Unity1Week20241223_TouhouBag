
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        switch(item.GetData().Shape)
        {
            case ColliderShape.Square1x1:
                CreateCellAt(item, new Vector2Int(0, 0));
                break;
            case ColliderShape.Square2x2:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(0, 1));
                CreateCellAt(item, new Vector2Int(1, 0));
                CreateCellAt(item, new Vector2Int(1, 1));
                break;
            case ColliderShape.Rectangle2x1:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(1, 0));
                break;
            case ColliderShape.Rectangle3x1:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(1, 0));
                CreateCellAt(item, new Vector2Int(2, 0));
                break;
            case ColliderShape.Rectangle4x1:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(1, 0));
                CreateCellAt(item, new Vector2Int(2, 0));
                CreateCellAt(item, new Vector2Int(3, 0));
                break;
            case ColliderShape.Rectangle1x2:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(0, 1));
                break;
            case ColliderShape.Rectangle1x3:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(0, 1));
                CreateCellAt(item, new Vector2Int(0, 2));
                break;
            case ColliderShape.Rectangle1x4:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(0, 1));
                CreateCellAt(item, new Vector2Int(0, 2));
                CreateCellAt(item, new Vector2Int(0, 3));
                break;
            case ColliderShape.TShape:
                CreateCellAt(item, new Vector2Int(1, 0));
                CreateCellAt(item, new Vector2Int(1, 1));
                CreateCellAt(item, new Vector2Int(1, 2));
                CreateCellAt(item, new Vector2Int(0, 2));
                CreateCellAt(item, new Vector2Int(2, 2));
                break;
            case ColliderShape.TriangleVToR:
                CreateCellAt(item, new Vector2Int(0, 0));
                CreateCellAt(item, new Vector2Int(0, 1));
                CreateCellAt(item, new Vector2Int(1, 0));
                break;
            case ColliderShape.TriangleVToL:
                CreateCellAt(item, new Vector2Int(1, 0));
                CreateCellAt(item, new Vector2Int(1, 1));
                CreateCellAt(item, new Vector2Int(0, 0));
                break;
            default:
                Debug.LogError("不正のShape: " + item.GetData().Shape);
                CreateCellAt(item, new Vector2Int(0, 0));
                break;
        }
        return item;
    }

    private static BagItemCell CreateCellAt(BagItem item, Vector2Int cellPos)
    {
        BagItemCell cell = new GameObject("Cell" + item.tag).AddComponent<BagItemCell>();
        cell.transform.SetParent(item.transform);
        cell.transform.localScale = Vector2.one;
        // コライダー設定
        BoxCollider2D collider = cell.AddComponent<BoxCollider2D>();
        collider.size = Vector2.one;
        collider.isTrigger = true;
        // Overlay
        SpriteRenderer overlay = new GameObject("Overlay").AddComponent<SpriteRenderer>();
        overlay.transform.SetParent(cell.transform);
        overlay.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.Cells.Overlay);
        overlay.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        // タグ、レイヤー設定
        switch(item.tag)
        {
            case Consts.Tags.Bag:
                cell.tag = Consts.Tags.BagCell;
                overlay.sortingLayerName = Consts.SortingLayer.BagOverlay;
                break;
            case Consts.Tags.Item:
                cell.tag = Consts.Tags.ItemCell;
                overlay.sortingLayerName = Consts.SortingLayer.ItemOverlay;
                break;
            case Consts.Tags.StageSlot:
                cell.tag = Consts.Tags.StageSlotCell;
                overlay.sortingLayerName = Consts.SortingLayer.SlotOverlay;
                break;
        }
        // Pos関連
        cell.transform.localPosition = CalcCellPos(cellPos.x, cellPos.y, item.GetData().Size);
        cell.CellPos = cellPos;
        cell.SlotPos = new Vector2Int(-1, -1); // 配置されたスロット座標の初期値は存在しない(-1, -1)にしておく

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
}