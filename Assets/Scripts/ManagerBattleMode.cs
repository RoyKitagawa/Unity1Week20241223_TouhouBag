using System.Collections.Generic;
using DG.Tweening;
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

    // 自機
    [SerializeField]
    private CharacterPlayer player;
    // 自機攻撃可能範囲
    [SerializeField]
    private BoxCollider2D playerAttackableArea;

    // 敵機関連
    private int totalEnemyInStage = 15;
    [SerializeField]
    private Slider stageProgressSlider;

    // キャラクターが所持しているアイテム一覧
    private HashSet<BattleListItem> items = new HashSet<BattleListItem>();

    public bool IsBattleActive = false;

    public void Start()
    {
        OnStartBattlePhase();
    }
    public void OnStartBattlePhase()
    {
        ManagerEnemy.Instance.OnStartBattlePhase();
        InitializeBattle();
    }

    public void OnGameClear()
    {
        Debug.Log("ゲームクリア！");
        ShowGameClearResult();
    }

    public void OnWaveClear()
    {
        Debug.Log("Wave成功！");
        ShowWaveClearResult();
    }

    public void OnWaveFail()
    {
        Debug.Log("Wave失敗…");
        ShowGameOverResult();
    }

    /// <summary>
    /// バトルフェーズの初期化
    /// </summary>
    public void InitializeBattle()
    {
        // バトル開始フラグ
        IsBattleActive = true;

        // 既存の（過去WAVEの）アイテムが残っている場合は削除する
        foreach(BattleListItem item in items) { GameObject.Destroy(item); }
        items.Clear();

        // BagEditModeの最新アイテムを取得し、Battle用のアイテムリストを作成する
        foreach(BagItem item in StageManager.Items)
        {
            if(!item.IsPlaced()) continue;
            BattleListItem battleItem = BattleListItem.InstantiateBattleListItem(item.GetDataItemName(), item.GetDataItemLevel());
            if(battleItem != null) items.Add(battleItem);
        }

        // 画面内にアイテム一覧を表示する
        int index = 0;
        GameObject itemsRoot = BasicUtil.GetRootObject(Consts.Roots.BattleItemList);
        Vector2 itemListStartPos = new Vector2(-8f, -4.25f);
        foreach(BattleListItem item in items)
        {
            item.transform.SetParent(itemsRoot.transform);
            item.transform.localPosition = new Vector2(itemListStartPos.x + index * 1.2f, itemListStartPos.y);
            index ++;
        }

        // ミカン箱を設置する
        Transform root = BasicUtil.GetRootObject(Consts.Roots.BoxRoot).transform;
        float y = 5.5f;
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
        totalEnemyInStage = 20;
        stageProgressSlider.maxValue = totalEnemyInStage;
        stageProgressSlider.value = totalEnemyInStage;

        // プレイヤーキャラクターの初期化
        player.InitializeCharacter(CharacterDataList.GetCharacterData(CharacterName.Player));
    }

    public int GetRemainEnemiesToBeSpawned()
    {
        return (int)stageProgressSlider.value;
    }

    public void OnEnemySpawn()
    {
        stageProgressSlider.value --;
        if(stageProgressSlider.value <= 0) stageProgressSlider.value = 0;
    }

    public void OnEnemyDead(EnemyBase enemy)
    {
        ManagerEnemy.Instance.RemoveEnemyFromList(enemy);        
        if(stageProgressSlider.value <= 0 // ステージから全ての敵機が出現済み
            && ManagerEnemy.Instance.GetEnemiesCount() <= 0) // ステージ上の生存敵機が0
        {
            ShowWaveClearResult();
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

    public void ShowGameClearResult()
    {
        IsBattleActive = false;
        gameClearPanel.transform.position = new Vector2(BasicUtil.GetScreenWorldCorners(Camera.main).width, 0.0f);
    }

    public void ShowWaveClearResult()
    {
        IsBattleActive = false;
        Rect screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
        // 時間を止める
        waveClearPanel.gameObject.SetActive(true);
        waveClearPanel.transform.position = new Vector3(screenCorners.width, 0.0f, 0.0f);
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(waveClearPanel.transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.Linear));
        sequence.Append(waveClearPanel.transform.DOMove(new Vector3(-screenCorners.width, 0.0f, 0.0f), 0.5f).SetDelay(1.0f).SetEase(Ease.Linear));
        sequence.OnComplete(() => {
            // TODO ユーザーがクリックしたらタイトルに遷移するようにしたい
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

    public void ShowGameOverResult()
    {
        IsBattleActive = false;
        Rect screenCorners = BasicUtil.GetScreenWorldCorners(Camera.main);
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
