using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBagEditMode : MonoBehaviourSingleton<ManagerBagEditMode>
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    
    [SerializeField]
    private Transform rerollButton;
    [SerializeField]
    private Image rerollPriceImage;
    private Sequence rerollButtonShakeSequence = null;
    private Vector2 defaultRerollButtonPos = Vector2.zero;

    // リロールの値段関連
    private int rerollCount = 0;
    private int rerollPrice = 0;
    
    // アイテムスポーン座標:Min~Max
    private Rect itemSpawnArea;
    private bool isInitialized = false;
    
    // 現状のステージ進捗ステータス
    private Vector2Int stageSize = new Vector2Int(6, 8);
    // private int currentWave;
    // private int totalWave;

    // 画面サイズ
    private Rect screenCorners;

    void Start()
    {
        Initialize();
        InitializeReroll();
    }

    private void InitializeReroll()
    {
        ReRollItems(); // 初期リロール
        rerollCount = 0;
        UpdateRerollPriceImage();   
    }

    private void Initialize()
    {
        screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);

        // スロット初期化処理
        ManagerGame.Instance.ClearAllInStageObjectLists();
        CreateBorderColliders();

        // ステージ情報整理
        InitStageScale();
    
        // Reroll関連
        defaultRerollButtonPos = rerollButton.transform.position;
        UpdateRerollPriceImage();
        
        // アイテムスポーン地点整理
        InitItemSpawnAreaPos();

        // 金銭初期化
        ManagerGame.Instance.SetMoney(ManagerGame.Instance.InitialMoneyAmt);

        // ロード処理
        SaveData saveData = SaveDataManager.LoadProgress();
        if(saveData == null)
        {
            // 初期バッグ配置
            BagItem bagA = BagItemManager.InstantiateItem(BagItemName.Bag2x2, BagItemLevel.Lv1);
            bagA.SetIsPurchased(true);
            bagA.PlaceItemAt(Rotation.Default, new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(1, 1) });
            ManagerGame.Instance.Add2List(bagA);
            BagItem bagB = BagItemManager.InstantiateItem(BagItemName.Bag2x2, BagItemLevel.Lv1);
            bagB.SetIsPurchased(true);
            bagB.PlaceItemAt(Rotation.Default, new Vector2Int[] { new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(3, 0), new Vector2Int(3, 1) });
            ManagerGame.Instance.Add2List(bagB);
        }
        else
        {
            // データをもとにゲーム進捗、バッグの状況を復元
            SaveDataManager.ApplySavedData(saveData, true);
        }
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
                ManagerGame.Slots.Add(slot);
            }
        }

        Transform root = BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform;
        root.localPosition = new Vector2((-sizeX / 2.0f) + 0.5f, (-sizeY / 2.0f) + 0.5f);
    }

    private void InitStageScale()
    {
        float ratioH;
        float ratioV;

        // スロット生成
        CreateStageSlots(stageSize.x, stageSize.y);
        
        // 画像サイズ取得
        Rect screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
        Vector2 rightScreenSize = new Vector2(screenCorners.width / 2.0f, screenCorners.height);
        Vector2 rightScreenCenterPos = new Vector2(rightScreenSize.x / 2.0f, 0.0f);

        // 画面の右半分の中央に配置する。画面右半分になるべくしっかり入るスケールにする
        ratioH = rightScreenSize.x / stageSize.x  * 0.8f; // 0.8係数はマージンが割り
        ratioV = rightScreenSize.y / stageSize.y * 0.8f;
        float scale = ratioH < ratioV ? ratioH : ratioV;

        BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.localScale = new Vector2(scale, scale);
        BasicUtil.GetRootObject(Consts.Roots.BagRoot).transform.position = new Vector2(
            rightScreenCenterPos.x - (stageSize.x / 2f),
            rightScreenCenterPos.y - (stageSize.y / 2f)
        );
    }

    private BagItemType[] spawnItemTypes = {
        BagItemType.Bag, BagItemType.Bag, BagItemType.Item,
        BagItemType.Item, BagItemType.Item, BagItemType.Item,      
        BagItemType.Item, BagItemType.Item, BagItemType.Item,      
        BagItemType.Item
    };

    private void InitItemSpawnAreaPos()
    {
        Rect screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
        Vector2 leftScreenSize = new Vector2(screenCorners.width / 2.0f, screenCorners.height);
        Vector2 leftScreenCenterPos = new Vector2(-leftScreenSize.x / 2.0f, 0.0f);

        itemSpawnArea = BasicUtil.CreateRectFromCenter(new Vector2(
            leftScreenCenterPos.x, screenCorners.min.y + 2.0f),
            leftScreenSize.x * 0.7f, 0f);
    }

    public void OnClickReRollItems()
    {
        if(rerollPrice > ManagerGame.Instance.GetMoney())
        {
            // シェイク処理
            float posX = defaultRerollButtonPos.x;
            float moveMin = 5;
            float moveMax = 10;
            float duration = 0.5f;
            if(rerollButtonShakeSequence != null && !rerollButtonShakeSequence.IsComplete()) rerollButtonShakeSequence.Complete();
            rerollButtonShakeSequence = DOTween.Sequence();
            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX + moveMin, posX + moveMax), duration / 8f).SetEase(Ease.OutQuad));
            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InQuad));
            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX - moveMin, posX - moveMax), duration / 8f).SetEase(Ease.OutQuad));
            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InQuad));

            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX + moveMin, posX + moveMax), duration / 8f).SetEase(Ease.OutQuad));
            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InQuad));
            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(Random.Range(posX - moveMin, posX - moveMax), duration / 8f).SetEase(Ease.OutQuad));
            rerollButtonShakeSequence.Append(rerollButton.DOLocalMoveX(posX, duration / 8f).SetEase(Ease.InOutQuad));

            return;
        }

        ReRollItems();
    }

    public void ReRollItems()
    {
        if(rerollPrice > ManagerGame.Instance.GetMoney())
        {
            Debug.LogError("リロールの金銭不足");
            return;
        }
        // 支払う
        ManagerGame.Instance.AddMoney(-rerollPrice);

        // 未購入のものは削除する
        ManagerGame.Items.RemoveWhere(item => {
            if(!item.IsPurchased())
            {
                Destroy(item.gameObject);
                return true;
            }
            return false; // 残す
        });
        ManagerGame.Bags.RemoveWhere(item => {
            if(!item.IsPurchased())
            {
                Destroy(item.gameObject);
                return true;
            }
            return false; // 残す
        });

        // アイテムの生成
        int spawnMaxItem = 3;
        for(int i = 0; i < spawnMaxItem; i++)
        {
            BagItem item = ManagerGame.Instance.SpawnRandomItem(RandUtil.GetRandomItem(spawnItemTypes));
            item.transform.position = new Vector2(
                itemSpawnArea.min.x + (itemSpawnArea.width / (spawnMaxItem - 1) * i), 
                itemSpawnArea.y);
            item.SetPhysicSimulator(false);
            item.SetIsPlaced(false); // 初期のアイテムは「設置状態」ではない
            item.SetIsPurchased(false); // 購入前
        }

        // 新たな値段の設定
        rerollCount++;
        UpdateRerollPriceImage();
    }

    private void UpdateRerollPriceImage()
    {
        if(rerollCount <= 0) rerollPrice = 0;
        else if(rerollCount <= 3) rerollPrice = 1;
        else if(rerollCount <= 5) rerollPrice = 2;
        else if(rerollCount <= 7) rerollPrice = 3;
        else if(rerollCount <= 10) rerollPrice = 4;
        else rerollPrice = 5;
        rerollPriceImage.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.Prices.Price(rerollPrice));
    }

    public void RemoveUnPlacedItems()
    {
        HashSet<BagItem> removeList = new HashSet<BagItem>();
        // バッグリスト
        foreach(BagItem bag in ManagerGame.Bags)
        {
            if(!bag.IsPlaced())
            {
                removeList.Add(bag);
                Destroy(bag.gameObject);
            }
        }
        // アイテムリスト
        foreach(BagItem item in ManagerGame.Items)
        {
            if(!item.IsPlaced())
            {
                removeList.Add(item);
                Destroy(item.gameObject);
            }
        }
        // 実際にリストから削除
        foreach(BagItem item in removeList)
        {
            ManagerGame.Instance.RemoveFromList(item);
        }
    }




    private void CreateBorderColliders()
    {
        // 画面サイズ取得
        Rect corners = BasicUtil.GetScreenWorldCorners(Camera.main);
        // Root取得
        Transform root = BasicUtil.GetRootObject(Consts.Roots.StageBorders).transform;
        // 壁サイズ
        float wallSize = 1.0f;
        // 上
        GameObject border = new GameObject("TopCollider");
        border.transform.SetParent(root);
        border.transform.localPosition = new Vector2(0.0f, corners.yMax + wallSize / 2.0f);
        BoxCollider2D collider = border.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(corners.width, wallSize);
        // 下
        border = new GameObject("BottomCollider");
        border.transform.SetParent(root);
        border.transform.localPosition = new Vector2(0.0f, corners.yMin - wallSize / 2.0f);
        collider = border.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(corners.width, 1.0f);
        // 右
        border = new GameObject("RightCollider");
        border.transform.SetParent(root);
        border.transform.localPosition = new Vector2(corners.xMax + wallSize / 2.0f, 0.0f);
        collider = border.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(wallSize, corners.height);
        // 左
        border = new GameObject("LeftCollider");
        border.transform.SetParent(root);
        border.transform.localPosition = new Vector2(corners.xMin - wallSize / 2.0f, 0.0f);
        collider = border.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(wallSize, corners.height);

    }


//    ＝＝＝＝＝＝＝＝＝＝＝＝＝



//  ==========================



    // public int GetCurrentMoney()
    // {
    //     return currentMoneyAmt;
    // }

    // public void AddMoney(int moneyAmt)
    // {
    //     SetMoneyAmt(currentMoneyAmt + moneyAmt);
    // }

    // public void SetMoneyAmt(int moneyAmt)
    // {
    //     currentMoneyAmt = moneyAmt;
    //     moneyText.text = "所持金：" + currentMoneyAmt + "コイン";
    // }

    // public void InitializeStage()
    // {
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


    // public void StartModeBagEdit()
    // {
    //     // IsCurrentStateBagEdit = true;

    //     // rootBagEdit.SetActive(true);
    //     // rootBattle.SetActive(false);

    //     ManagerGame.Instance.SetMoney(ManagerGame.Instance.InitialMoneyAmt);
    //     // SetMoneyAmt(currentMoneyAmt);
    //     StageManager.Instance.InitializeStage();
    //     StageManager.Instance.OnStartBagEditMode();

    //     // ロード処理
    //     SaveData saveData = SaveDataManager.LoadProgress();
    //     if(saveData == null)
    //     {
    //         // 初期バッグ配置
    //         BagItem bagA = BagItemManager.InstantiateItem(BagItemName.Bag2x2, BagItemLevel.Lv1);
    //         bagA.SetIsPurchased(true);
    //         bagA.PlaceItemAt(Rotation.Default, new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(1, 1) });
    //         StageManager.Instance.Add2List(bagA);
    //         BagItem bagB = BagItemManager.InstantiateItem(BagItemName.Bag2x2, BagItemLevel.Lv1);
    //         bagB.SetIsPurchased(true);
    //         bagB.PlaceItemAt(Rotation.Default, new Vector2Int[] { new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(3, 0), new Vector2Int(3, 1) });
    //         StageManager.Instance.Add2List(bagB);
    //     }
    //     else
    //     {
    //         // データをもとにゲーム進捗、バッグの状況を復元
    //         SaveDataManager.ApplySavedData(saveData);
    //     }
    // }

    /// <summary>
    /// リザルトシーンへ遷移する
    /// </summary>
    public void Move2SceneResult()
    {
        ManagerSceneTransition.Instance.Move2Scene(SceneType.Result);
    }

    /// <summary>
    /// バトル開始
    /// </summary>
    public void StartModeBattle()
    {
        // IsCurrentStateBagEdit = false;
        
        // rootBagEdit.SetActive(false);
        // rootBattle.SetActive(true);

        ManagerSceneTransition.Instance.Move2Scene(SceneType.InGameBattle);

        // ManagerBattleMode.Instance.OnStartBattlePhase();
    }

}
