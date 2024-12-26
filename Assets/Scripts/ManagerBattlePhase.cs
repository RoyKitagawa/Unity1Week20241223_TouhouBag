using System.Collections.Generic;
using UnityEngine;

public class ManagerBattlePhase : MonoBehaviourSingleton<ManagerBattlePhase>
{
    // 自機
    [SerializeField]
    private CharacterPlayer player;
    // 自機攻撃可能範囲
    [SerializeField]
    private BoxCollider2D playerAttackableArea;
    // キャラクターが所持しているアイテム一覧
    private HashSet<BattleListItem> items = new HashSet<BattleListItem>();

    public void OnStartBattlePhase()
    {
        ManagerEnemy.Instance.OnStartBattlePhase();
        InitializeBattle();

        Debug.Log("攻撃可能になるX座標：" + GetPlayerAttackableBoundaryX());
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
            BattleListItem battleItem = BattleListItem.InstantiateBattleListItem(item.GetDataItemName());
            if(battleItem != null) items.Add(battleItem);
        }

        // 画面内にアイテム一覧を表示する
        int index = 0;
        GameObject itemsRoot = BasicUtil.GetRootObject(Consts.Roots.BattleItemList);
        foreach(BattleListItem item in items)
        {
            item.transform.SetParent(itemsRoot.transform);
            item.transform.localPosition = new Vector2(0.0f, -index * 1.2f);
            index ++;
        }

        // プレイヤーキャラクターの初期化
        player.InitializeCharacter(CharacterDataList.GetCharacterData(CharacterName.Player));
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
