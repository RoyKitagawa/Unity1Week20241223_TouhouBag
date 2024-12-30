using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// public enum StageType
// {
//     None,
//     BattleMode,
//     BagEditMode,
// }

public class StageManager : MonoBehaviourSingleton<StageManager>
{
    // ステージに存在するスロットの一覧
    public static HashSet<BagItem> Slots = new HashSet<BagItem>();
    // ステージに存在するバッグの一覧
    public static HashSet<BagItem> Bags = new HashSet<BagItem>();
    // ステージに存在するアイテムの一覧
    public static HashSet<BagItem> Items = new HashSet<BagItem>();
    // ステージサイズ
    private Vector2Int stageSize = Vector2Int.zero;
    // [SerializeField]
    // private Transform rerollButton;
    // [SerializeField]
    // private Image rerollPriceImage;
    // private Sequence rerollButtonShakeSequence = null;
    // private Vector2 defaultRerollButtonPos = Vector2.zero;

    // // リロールの値段関連
    // private int rerollCount = 0;
    // private int rerollPrice = 0;

    // private class StageInfo
    // {
    //     public StageType stageType = StageType.BagEditMode;
    //     public Vector2 stageCenterPos = Vector2.zero;
    //     public Vector2 stageWorldSize = Vector2.zero;
    //     public float stageScale = 1.0f;

    //     public StageInfo(StageType type) { stageType = type; }
    // }

    // private StageInfo battleStageInfo = new StageInfo(StageType.BattleMode);
    // private StageInfo bagEditStageInfo = new StageInfo(StageType.BagEditMode);
    // private StageType currentStageType = StageType.None;

    // // アイテムスポーン座標:Min~Max
    // private Rect itemSpawnArea;
    // private bool isInitialized = false;
    // // private Vector2 itemSpawnPosMin;
    // // private Vector2 itemSpawnPosMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public void InitializeStage()
    // {
    //     if(isInitialized) return;
    //     isInitialized = true;

    //     // スロット初期化処理
    //     ClearAllInStageObjectLists();
    //     CreateBorderColliders();

    //     // ステージ情報整理
    //     InitStage();
    //     // Reroll関連
    //     defaultRerollButtonPos = rerollButton.transform.position;
    //     UpdateRerollPriceImage();
        
    //     // SwitchMode(StageType.BagEditMode, 0.0f);

    //     // アイテムスポーン地点整理
    //     InitItemSpawnAreaPos();
    // }

    // public void OnStartBagEditMode()
    // {
    //     // リロール処理。初回なのでカウントしない
    //     ReRollItems();
    //     rerollCount = 0;
    //     UpdateRerollPriceImage();
    // }

    // private void PlaceItemAt()

    // private void SpawnAndPlaceItemAt(BagItemName itemName, BagItemLevel lv, Vector2Int[] slots)
    // {
    //     BagItem item = BagItemManager.InstantiateItem(itemName, lv); // アイテム生成
    //     item.transform.position = GetSlotsCenterPos(slots); // 指定スロットの中心に配置
    //     item.SetPhysicSimulator(false); // 物理判定OFF
    //     item.SetItemCellSlotAtClosestSlot(); // セルのスロット情報設定
    //     // item.AddPriceRenderer(); // 値段追加
    //     item.SetIsPlaced(true); // アイテムを設置
    //     item.SetIsPurchased(true); // 追加支払いが発生しないように購入済みにしておく
    //     Add2List(item); // 一覧に追加
    // }

    // private void InitItemSpawnAreaPos()
    // {
    //     Rect screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
    //     Vector2 leftScreenSize = new Vector2(screenCorners.width / 2.0f, screenCorners.height);
    //     Vector2 leftScreenCenterPos = new Vector2(-leftScreenSize.x / 2.0f, 0.0f);

    //     itemSpawnArea = BasicUtil.CreateRectFromCenter(new Vector2(
    //         leftScreenCenterPos.x, screenCorners.min.y + 2.0f),
    //         leftScreenSize.x * 0.7f, 0f);
    // }

    // private BagItemType[] spawnItemTypes = {
    //     BagItemType.Bag, BagItemType.Bag, BagItemType.Item,
    //     BagItemType.Item, BagItemType.Item, BagItemType.Item,      
    //     BagItemType.Item, BagItemType.Item, BagItemType.Item,      
    //     BagItemType.Item
    // };

    // public void OnClickReRollItems()
    // {
    //     if(rerollPrice > ManagerGame.Instance.GetMoney())
    //     {
    //         // シェイク処理
    //         float posX = defaultRerollButtonPos.x;
    //         float moveMin = 5;
    //         float moveMax = 10;
    //         float duration = 0.5f;
    //         if(rerollButtonShakeSequence != null && !rerollButtonShakeSequence.IsComplete()) rerollButtonShakeSequence.Complete();
    //         rerollButtonShakeSequence = DOTween.Sequence();
    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX + moveMin, posX + moveMax), duration / 8f).SetEase(Ease.OutQuad));
    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InQuad));
    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX - moveMin, posX - moveMax), duration / 8f).SetEase(Ease.OutQuad));
    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InQuad));

    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX + moveMin, posX + moveMax), duration / 8f).SetEase(Ease.OutQuad));
    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InQuad));
    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX - moveMin, posX - moveMax), duration / 8f).SetEase(Ease.OutQuad));
    //         rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InOutQuad));

    //         return;
    //     }

    //     ReRollItems();
    // }

    // public void ReRollItems()
    // {
    //     if(rerollPrice > ManagerGame.Instance.GetMoney())
    //     {
    //         Debug.LogError("リロールの金銭不足");
    //         return;
    //     }
    //     // 支払う
    //     ManagerGame.Instance.AddMoney(-rerollPrice);

    //     // 未購入のものは削除する
    //     Items.RemoveWhere(item => {
    //         if(!item.IsPurchased())
    //         {
    //             Destroy(item.gameObject);
    //             return true;
    //         }
    //         return false; // 残す
    //     });
    //     Bags.RemoveWhere(item => {
    //         if(!item.IsPurchased())
    //         {
    //             Destroy(item.gameObject);
    //             return true;
    //         }
    //         return false; // 残す
    //     });

    //     // アイテムの生成
    //     int spawnMaxItem = 3;
    //     for(int i = 0; i < spawnMaxItem; i++)
    //     {
    //         BagItem item = SpawnRandomItem(RandUtil.GetRandomItem(spawnItemTypes));
    //         item.transform.position = new Vector2(
    //             itemSpawnArea.min.x + (itemSpawnArea.width / (spawnMaxItem - 1) * i), 
    //             itemSpawnArea.y);
    //         item.SetPhysicSimulator(false);
    //         item.SetIsPlaced(false); // 初期のアイテムは「設置状態」ではない
    //         item.SetIsPurchased(false); // 購入前
    //     }

    //     // 新たな値段の設定
    //     rerollCount++;
    //     UpdateRerollPriceImage();
    // }

    // private void UpdateRerollPriceImage()
    // {
    //     if(rerollCount <= 0) rerollPrice = 0;
    //     else if(rerollCount <= 3) rerollPrice = 1;
    //     else if(rerollCount <= 5) rerollPrice = 2;
    //     else if(rerollCount <= 7) rerollPrice = 3;
    //     else if(rerollCount <= 10) rerollPrice = 4;
    //     else rerollPrice = 5;
    //     rerollPriceImage.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.Prices.Price(rerollPrice));
    // }

    // public void RemoveUnPlacedItems()
    // {
    //     HashSet<BagItem> removeList = new HashSet<BagItem>();
    //     // バッグリスト
    //     foreach(BagItem bag in Bags)
    //     {
    //         if(!bag.IsPlaced())
    //         {
    //             removeList.Add(bag);
    //             Destroy(bag.gameObject);
    //         }
    //     }
    //     // アイテムリスト
    //     foreach(BagItem item in Items)
    //     {
    //         if(!item.IsPlaced())
    //         {
    //             removeList.Add(item);
    //             Destroy(item.gameObject);
    //         }
    //     }
    //     // 実際にリストから削除
    //     foreach(BagItem item in removeList)
    //     {
    //         RemoveFromList(item);
    //     }
    // }

    // public void SwitchMode(float duration = 1.0f)
    // {
    //     switch(currentStageType)
    //     {
    //         case StageType.BattleMode:
    //             SwitchMode(StageType.BagEditMode, duration);
    //             break;
            
    //         case StageType.BagEditMode:
    //             SwitchMode(StageType.BattleMode, duration);
    //             break;
    //     }
    // }

    // private void SwitchMode(StageType stageType, float duration = 0.0f)
    // {
    //     // 同じモードの場合は何もしない
    //     if(currentStageType == stageType) return;
    //     currentStageType = stageType;

    //     // バッグ全体のスケールを調整
    //     StageInfo stageInfo = GetStageInfo(stageType);
    //     if(duration <= 0.0f)
    //     {
    //         BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.localScale = new Vector2(stageInfo.stageScale, stageInfo.stageScale);
    //         BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.position = GetBagRootPosition(stageType);
    //     }
    //     else
    //     {
    //         BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.DOScale(stageInfo.stageScale, duration);
    //         BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.DOMove(GetBagRootPosition(stageType), duration);
    //     }
    // }

    // private StageInfo GetStageInfo(StageType stageType)
    // {
    //     if(stageType == StageType.BattleMode) return battleStageInfo;
    //     return bagEditStageInfo;
    // }

    // private Vector2 GetBagRootPosition(StageType stageType)
    // {
    //     StageInfo stageInfo = GetStageInfo(stageType);
    //     Vector2 bagSlotAdjustment = new Vector2((-stageSize.x * stageInfo.stageScale / 2.0f) + 0.5f, (-stageSize.y * stageInfo.stageScale / 2.0f) + 0.5f);
    //     return stageInfo.stageCenterPos + bagSlotAdjustment;
    // }


    public void InitStage()
    {
        stageSize = new Vector2Int(6, 8);
        float ratioH;
        float ratioV;

        // スロット生成
        CreateStageSlots(stageSize.x, stageSize.y);
        
        // 画像サイズ取得
        Rect screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
        Vector2 rightScreenSize = new Vector2(screenCorners.width / 2.0f, screenCorners.height);
        Vector2 rightScreenCenterPos = new Vector2(rightScreenSize.x / 2.0f, 0.0f);

        // 画面の右半分の中央に配置する。画面右半分になるべくしっかり入るスケールにする
        // bagEditStageInfo.stageCenterPos = rightScreenCenterPos;
        ratioH = rightScreenSize.x / stageSize.x  * 0.8f; // 0.8係数はマージンが割り
        ratioV = rightScreenSize.y / stageSize.y * 0.8f;
        float scale = ratioH < ratioV ? ratioH : ratioV;
        // bagEditStageInfo.stageScale = ratioH < ratioV ? ratioH : ratioV;

        // // // 画面の高さを半分にした状態で、同じく右半分の中央で入り切るスケールにする
        // battleStageInfo.stageCenterPos = new Vector2(rightScreenCenterPos.x, -screenCorners.height / 4.0f);
        // ratioH = rightScreenSize.x / stageSize.x * 0.8f; // 0.8係数はマージンが割り
        // ratioV = (screenCorners.height / 2.0f) / stageSize.y * 0.8f;
        // battleStageInfo.stageScale = ratioH < ratioV ? ratioH : ratioV;

        BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.localScale = new Vector2(scale, scale);
        BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.position = new Vector2(
            rightScreenCenterPos.x - (stageSize.x / 2f),
            rightScreenCenterPos.y - (stageSize.y / 2f)
        ); //new Vector2(rightScreenCenterPos.x - stageSize.x / 2f, );

    }

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

    /// <summary>
    /// 指定マス分のバッグ配置用ステージスロットを作成する
    /// </summary>
    /// <param name="sizeX"></param>
    /// <param name="sizeY"></param>
    private void CreateStageSlots(int sizeX, int sizeY)
    {
        for(int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                BagItem slot = BagItemManager.InstantiateItem(BagItemName.StageSlot, BagItemLevel.Lv1);
                slot.SetPhysicSimulator(false);
                slot.transform.localPosition = new Vector2(x, y);
                slot.transform.SetParent(BasicUtil.GetRootObject(Consts.Roots.BagSlotRoot).transform);

                // 色味お試し変更
                if((x + y) % 2 == 0) slot.SetImageColor(Color.gray);
                else slot.SetImageColor(Color.white);
                // スロット情報設定
                slot.InitCells();
                BagItemCell cell = slot.GetCellAtCellPos(Vector2Int.zero);
                cell.SlotPos = new Vector2Int(x, y);
                cell.ClearOverlayColor();
                slot.SetIsPlaced(true);

                // リストに追加
                Slots.Add(slot);
            }
        }

        Transform root = BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform;
        root.localPosition = new Vector2((-sizeX / 2.0f) + 0.5f, (-sizeY / 2.0f) + 0.5f);
    }

    // private void CreateBorderColliders()
    // {
    //     // 画面サイズ取得
    //     Rect corners = BasicUtil.GetScreenWorldCorners(Camera.main);
    //     // Root取得
    //     Transform root = BasicUtil.GetRootObject(Consts.Roots.StageBorders).transform;
    //     // 壁サイズ
    //     float wallSize = 1.0f;
    //     // 上
    //     GameObject border = new GameObject("TopCollider");
    //     border.transform.SetParent(root);
    //     border.transform.localPosition = new Vector2(0.0f, corners.yMax + wallSize / 2.0f);
    //     BoxCollider2D collider = border.AddComponent<BoxCollider2D>();
    //     collider.size = new Vector2(corners.width, wallSize);
    //     // 下
    //     border = new GameObject("BottomCollider");
    //     border.transform.SetParent(root);
    //     border.transform.localPosition = new Vector2(0.0f, corners.yMin - wallSize / 2.0f);
    //     collider = border.AddComponent<BoxCollider2D>();
    //     collider.size = new Vector2(corners.width, 1.0f);
    //     // 右
    //     border = new GameObject("RightCollider");
    //     border.transform.SetParent(root);
    //     border.transform.localPosition = new Vector2(corners.xMax + wallSize / 2.0f, 0.0f);
    //     collider = border.AddComponent<BoxCollider2D>();
    //     collider.size = new Vector2(wallSize, corners.height);
    //     // 左
    //     border = new GameObject("LeftCollider");
    //     border.transform.SetParent(root);
    //     border.transform.localPosition = new Vector2(corners.xMin - wallSize / 2.0f, 0.0f);
    //     collider = border.AddComponent<BoxCollider2D>();
    //     collider.size = new Vector2(wallSize, corners.height);

    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
