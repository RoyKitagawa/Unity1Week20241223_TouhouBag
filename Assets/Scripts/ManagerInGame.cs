using DG.Tweening;
using TMPro;
using UnityEngine;

public class ManagerInGame : MonoBehaviourSingleton<ManagerInGame>
{
    // デバッグ処理用
    [SerializeField]
    private bool startWithBattle;

    public bool IsCurrentStateBagEdit;

    [SerializeField]
    private GameObject rootBagEdit;
    [SerializeField]
    private GameObject rootBattle;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject waveClearPanel;
    [SerializeField]
    private GameObject gameClearPanel;
    [SerializeField]
    private TextMeshProUGUI moneyText;

    // 画面サイズ
    private Rect screenCorners;

    // 所持金情報
    private int initialMoneyAmt = 999;
    private int currentMoneyAmt = 999;

    // 現状のステージ進捗ステータス
    private int currentWave;
    private int totalWave;

    private void Initialize()
    {
        currentWave = 0;
        totalWave = 20;        
        screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
    }

    public int GetCurrentMoney()
    {
        return currentMoneyAmt;
    }

    public void AddMoney(int moneyAmt)
    {
        SetMoneyAmt(currentMoneyAmt + moneyAmt);
    }

    public void SetMoneyAmt(int moneyAmt)
    {
        currentMoneyAmt = moneyAmt;
        moneyText.text = "所持金：" + currentMoneyAmt + "コイン";
    }

    public void StartModeBagEdit()
    {
        IsCurrentStateBagEdit = true;

        rootBagEdit.SetActive(true);
        rootBattle.SetActive(false);
        SetMoneyAmt(currentMoneyAmt);
        StageManager.Instance.InitializeStage();
        StageManager.Instance.OnStartBagEditMode();

        // ロード処理
        SaveData saveData = LoadProgress();
        if(saveData == null)
        {
            // 初期バッグ配置
            BagItem bagA = BagItemManager.InstantiateItem(BagItemName.Bag2x2, BagItemLevel.Lv1);
            bagA.SetIsPurchased(true);
            bagA.PlaceItemAt(Rotation.Default, new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(1, 1) });
            StageManager.Instance.Add2List(bagA);
            BagItem bagB = BagItemManager.InstantiateItem(BagItemName.Bag2x2, BagItemLevel.Lv1);
            bagB.SetIsPurchased(true);
            bagB.PlaceItemAt(Rotation.Default, new Vector2Int[] { new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(3, 0), new Vector2Int(3, 1) });
            StageManager.Instance.Add2List(bagB);
        }
        else
        {
            // データをもとにゲーム進捗、バッグの状況を復元
            ApplySavedData(saveData);
        }
    }

    public void StartModeBattle()
    {
        IsCurrentStateBagEdit = false;
        
        rootBagEdit.SetActive(false);
        rootBattle.SetActive(true);

        ManagerBattlePhase.Instance.OnStartBattlePhase();
    }

    public void ShowGameClearResult()
    {
        gameClearPanel.transform.position = new Vector2(BasicUtil.GetScreenWorldCorners(Camera.main).width, 0.0f);
    }

    public void ShowWaveClearResult()
    {
        // 時間を止める
        waveClearPanel.gameObject.SetActive(true);
        waveClearPanel.transform.position = new Vector3(screenCorners.width, 0.0f, 0.0f);
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(waveClearPanel.transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.Linear));
        sequence.Append(waveClearPanel.transform.DOMove(new Vector3(-screenCorners.width, 0.0f, 0.0f), 0.5f).SetDelay(1.0f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {
            // TODO ユーザーがクリックしたらタイトルに遷移するようにしたい
            ManagerSceneTransition.Instance.Move2Scene(SceneType.InGame);
            waveClearPanel.gameObject.SetActive(false);
            ResumeTimer();

            // 各種オブジェクト削除
            GameObject weapons = BasicUtil.GetRootObject(Consts.Roots.BattleWeapons);
            Destroy(weapons);
            GameObject items = BasicUtil.GetRootObject(Consts.Roots.BattleItemList);
            Destroy(items.gameObject);

        }).SetDelay(0.5f);
        PauseTimer();

        // 進捗を保存する
        SaveProgress(currentWave);
    }

    public void ShowGameOverResult()
    {
        // 時間を止める
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.transform.position = new Vector3(screenCorners.width, 0.0f, 0.0f);
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(gameOverPanel.transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.Linear));
        sequence.Append(gameOverPanel.transform.DOMove(new Vector3(-screenCorners.width, 0.0f, 0.0f), 0.5f).SetDelay(1.0f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {
            // TODO ユーザーがクリックしたらタイトルに遷移するようにしたい
            ManagerSceneTransition.Instance.Move2Scene(SceneType.Title);
            gameOverPanel.gameObject.SetActive(false);
            ResumeTimer();
        }).SetDelay(0.5f);
        PauseTimer();
    }

    public void SaveProgress(int clearedWaves)
    {
        // セーブ用の情報をセット
        SaveData saveData = new SaveData();
        saveData.SetDataMoney(currentMoneyAmt);
        saveData.SetDataClearedWaves(clearedWaves);
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

    public SaveData LoadProgress()
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

    public void ApplySavedData(SaveData saveData)
    {
        if(saveData == null)
        {
            Debug.LogError("セーブデータが存在しません");
            return;
        }
        currentMoneyAmt = saveData.CurrentMoney;
        currentWave = saveData.ClearedWaves + 1;
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

    private void PauseTimer()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeTimer()
    {
        Time.timeScale = 1.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        if(startWithBattle) StartModeBattle();
        else StartModeBagEdit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
