using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerBattlePhase : MonoBehaviourSingleton<ManagerBattlePhase>
{
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

    public void OnStartBattlePhase()
    {
        ManagerEnemy.Instance.OnStartBattlePhase();
        InitializeBattle();
    }

    public void OnGameClear()
    {
        Debug.Log("ゲームクリア！");
        ManagerInGame.Instance.ShowGameClearResult();
    }

    public void OnWaveClear()
    {
        Debug.Log("Wave成功！");
        ManagerInGame.Instance.ShowWaveClearResult();
    }

    public void OnWaveFail()
    {
        Debug.Log("Wave失敗…");
        ManagerInGame.Instance.ShowGameOverResult();
    }

    /// <summary>
    /// バトルフェーズの初期化
    /// </summary>
    public void InitializeBattle()
    {
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
            ManagerInGame.Instance.ShowWaveClearResult();
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

    public float GetPlayerAttackableBoundaryX()
    {
        return playerAttackableArea.bounds.max.x;
    }
}
