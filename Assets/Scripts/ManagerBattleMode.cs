using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBattleMode : MonoBehaviourSingleton<ManagerBattleMode>
{
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject waveClearPanel;
    [SerializeField]
    private GameObject gameClearPanel;
    [SerializeField]
    private TextMeshProUGUI enemyProgressText;
    [SerializeField]
    private TextMeshProUGUI waveText;

    // 自機
    [SerializeField]
    private CharacterPlayer player;
    // 自機攻撃可能範囲
    [SerializeField]
    private BoxCollider2D playerAttackableArea;

    // 敵機関連
    [SerializeField]
    private Slider stageProgressSlider;

    // キャラクターが所持しているアイテム一覧
    private HashSet<BattleListItem> items = new HashSet<BattleListItem>();

    public bool IsBattleActive = false;

    public void Start()
    {
        InitializeBattle();
        StartSpawnEnemy();
    }

    private void StartSpawnEnemy()
    {
        // ランダムで時間を定義
        // 時間ごとにSpawnよ呼びなおす
        // 最後の敵はボスか中ボス系
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int remainSpawn = GetRemainEnemiesToBeSpawned();
        if(remainSpawn <= 0) return;

        ManagerEnemy.Instance.SpawnEnemy(remainSpawn <= 1 ? CharacterType.EnemyBoss : CharacterType.EnemyNormal);

        // 1割の確率でラッシュを止める
        bool isInterval = RandUtil.GetRandomBool(0.1f);
        // ボス待ちで時間があると萎えるので、ボス直前だけはインターバル禁止
        if(remainSpawn <= 2) isInterval = false;
        // 再帰処理
        float nextSpawnDuration = isInterval ? Random.Range(5.0f, 7.0f) : Random.Range(1.0f, 3.0f);
        DOTween.Sequence()
            .AppendInterval(nextSpawnDuration)
            .AppendCallback(() => SpawnEnemy());
    }

    public void OnWaveClear()
    {
        Debug.Log("Wave成功！");
        ManagerGame.Instance.SetClearedWave(ManagerGame.Instance.GetCurrentWave());

        // 全てのWAVEを終えた場合
        if(ManagerGame.Instance.IsGameClear())
        {
            ShowGameClearResult();
        }
        // まだWAVEが残っている場合
        else
        {
            ShowWaveClearResult();
        }
    }

    public void OnWaveFail()
    {
        ShowGameOverResult();
    }

    /// <summary>
    /// バトルフェーズの初期化
    /// </summary>
    public void InitializeBattle()
    {
        // バトル開始フラグ
        IsBattleActive = true;

        // EnemyManager初期化処理
        ManagerEnemy.Instance.ClearAllEnemies();
        ManagerEnemy.Instance.InitializeEnemySpawnArea();

        // 既存の（過去WAVEの）アイテムが残っている場合は削除する
        foreach(BattleListItem item in items) { GameObject.Destroy(item); }
        items.Clear();

        // BagEditModeの最新アイテムを取得し、Battle用のアイテムリストを作成する
        foreach(BagItem item in ManagerGame.Items)
        {
            if(!item.IsPlaced()) continue;
            BattleListItem battleItem = BattleListItem.InstantiateBattleListItem(item.GetDataItemName(), item.GetDataItemLevel());
            if(battleItem != null) items.Add(battleItem);
        }

        // 画面内にアイテム一覧を表示する
        int index = 0;
        GameObject itemsRoot = BasicUtil.GetRootObject(Consts.Roots.BattleItemList);
        Vector2 itemListStartPos = new Vector2(-8f, -4.3f);
        foreach(BattleListItem item in items)
        {
            item.transform.SetParent(itemsRoot.transform);
            item.transform.localScale = new Vector2(0.8f, 0.8f);
            item.transform.localPosition = new Vector2(itemListStartPos.x + index * 1.2f * 0.8f, itemListStartPos.y);
            index ++;
        }

        // ミカン箱を設置する
        Transform root = BasicUtil.GetRootObject(Consts.Roots.BoxRoot).transform;
        float y = 4.0f;
        for(int i = 0; i < 20; i++) // 最大20個（-5 ~ 5 範囲の、最小単位0.5なため）
        {
            y -= Random.Range(0.5f, 2.0f);
            SpriteRenderer sr = new GameObject("Mikan").AddComponent<SpriteRenderer>();
            sr.transform.SetParent(root);
            sr.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.Box);
            sr.transform.position = new Vector2(-5.0f, y);
            if(y <= -5.0f) break;
        }

        // 敵機関連
        stageProgressSlider.maxValue = ManagerGame.Instance.GetTotalEnemyInStage();
        stageProgressSlider.value = stageProgressSlider.maxValue;

        // プレイヤーキャラクターの初期化
        player.InitializeCharacter(CharacterDataList.GetCharacterData(CharacterName.Player));

        // ステージ情報
        SetWaveInformationText();
    }

    public void Update()
    {
        UpdateEnemySpawnProgressText();
    }

    public void UpdateEnemySpawnProgressText()
    {
        enemyProgressText.text = "このWAVEの残りの敵：" + (int)(stageProgressSlider.value / stageProgressSlider.maxValue * 100) + "%";
    }

    public void SetWaveInformationText()
    {
        waveText.text = ManagerGame.Instance.GetWaveStatusText();
    }

    public int GetRemainEnemiesToBeSpawned()
    {
        return (int)stageProgressSlider.value;
    }

    public void OnEnemyDead(EnemyBase enemy)
    {
        stageProgressSlider.value --;
        if(stageProgressSlider.value <= 0) stageProgressSlider.value = 0;

        ManagerEnemy.Instance.RemoveEnemyFromList(enemy);        
        if(stageProgressSlider.value <= 0 // ステージから全ての敵機が出現済み
            && ManagerEnemy.Instance.GetEnemiesCount() <= 0) // ステージ上の生存敵機が0
        {
            OnWaveClear();
        }
    }

    public void TriggerPlayerAttack(BagItemDataBase data)
    {
        // 攻撃タイプが設定されていない場合、攻撃は発動しない
        if(data.WeaponTargetType == TargetType.None) return;
        
        // 対象が存在しない場合は終了する
        CharacterBase target = GetTargetCharacter(data);
        if(target == null) return;

        // 武器を射出する
        ProjectileWeaponBase.Launch(data, target, player.transform.position);
    }

    public CharacterBase GetPlayer()
    {
        return player;
    }

    private CharacterBase GetTargetCharacter(BagItemDataBase data)
    {
        switch(data.WeaponTargetType)
        {
            case TargetType.Self:
                return player;
            case TargetType.Random:
                return ManagerEnemy.Instance.GetRandomEnemy();
            case TargetType.Nearest:
                return ManagerEnemy.Instance.GetNearestEnemy();
            case TargetType.Farthest:
                return ManagerEnemy.Instance.GetFarthestEnemy();
            case TargetType.HighestLife:
                return ManagerEnemy.Instance.GetEnemyWithHighestLife();
            case TargetType.LowestLife:
                return ManagerEnemy.Instance.GetEnemyWithLowestLife();
            default:
                Debug.LogError("不正なターゲット種別: " + data.ItemName + "/" + data.WeaponTargetType);
                return null;
        }
    }

    // /// <summary>
    // /// プレイヤーにダメージを与える
    // /// </summary>
    // /// <param name="damageAmt"></param>
    // /// <param name="damageType"></param>
    // public void ApplyDamage2Player(float damageAmt, DamageType damageType)
    // {
    //     ApplyDamage2(player, damageAmt, damageType);
    // }

    // /// <summary>
    // /// 指定キャラクターにダメージを与える
    // /// </summary>
    // /// <param name="target"></param>
    // /// <param name="damageAmt"></param>
    // /// <param name="damageType"></param>
    // public void ApplyDamage2(CharacterBase target, float damageAmt, DamageType damageType)
    // {
    //     target.GainDamage(damageAmt, damageType);
    // }

    private void ShowGameClearResult()
    {
        IsBattleActive = false;
        PopupBase.Show(PopupType.GameClear);
        PauseTimer();

        // 進捗を削除する
        SaveDataManager.ClearProgress();
    }

    private void ShowWaveClearResult()
    {
        IsBattleActive = false;
        Rect screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);

        // ステージ情報の更新
        ManagerGame.Instance.SetClearedWave(ManagerGame.Instance.GetCurrentWave());
        
        // 時間を止める
        waveClearPanel.gameObject.SetActive(true);
        waveClearPanel.transform.position = new Vector3(screenCorners.width, 0.0f, 0.0f);
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(waveClearPanel.transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.Linear));
        sequence.Append(waveClearPanel.transform.DOMove(new Vector3(-screenCorners.width, 0.0f, 0.0f), 0.5f).SetDelay(1.0f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {

            // バッグ編集へと戻る
            ManagerSceneTransition.Instance.Move2Scene(SceneType.InGameBagEdit);
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
        SaveDataManager.SaveProgress();
    }

    private void ShowGameOverResult()
    {
        IsBattleActive = false;
        PopupBase.Show(PopupType.GameOver);
        PauseTimer();

        // 進捗を削除する
        SaveDataManager.ClearProgress();
    }


    private void PauseTimer()
    {
        Time.timeScale = 0.0f;
    }

    private void ResumeTimer()
    {
        Time.timeScale = 1.0f;
    }


    public float GetPlayerAttackableBoundaryX()
    {
        return playerAttackableArea.bounds.max.x;
    }
}
