using UnityEngine;
using System.Collections.Generic;

public class ManagerEnemy : MonoBehaviourSingleton<ManagerEnemy>
{
    // 生成した敵機一覧管理用
    private static HashSet<EnemyBase> enemies = new HashSet<EnemyBase>();
    // 敵機生成用Prefabのキャッシュ管理
    private static Dictionary<CharacterName , GameObject> characterPrefabs = new Dictionary<CharacterName, GameObject>();
    // 敵機出現範囲
    private Rect enemySpawnArea;

    // デバッグ用
    private const float enemySpawnCooldown = 1f;
    private float enemySpawnElapsedTime = enemySpawnCooldown; // スポーン時間（初期値はすぐに1体敵が出るようにする）

    public void Update()
    {
        // 敵機出現処理
        enemySpawnElapsedTime += Time.deltaTime;
        if(enemySpawnElapsedTime >= enemySpawnCooldown)
        {
            enemySpawnElapsedTime = 0;
            EnemyBase enemy = SpawnNewEnemy(RandUtil.GetRandomItem(CharacterDataList.GetCharacterNames(CharacterType.EnemyNormal)));
            enemies.Add(enemy);
        }
    }

    /// <summary>
    /// 敵機をランダムで取得する
    /// </summary>
    /// <returns></returns>
    public CharacterBase GetRandomEnemy()
    {
        return RandUtil.GetRandomItem(enemies);
    }

    /// <summary>
    /// プレイヤーに最も近い敵機を取得する
    /// </summary>
    /// <returns></returns>
    public CharacterBase GetNearestEnemy()
    {
        CharacterBase player = ManagerBattlePhase.Instance.GetPlayer();
        CharacterBase nearestEnemy = null;
        float nearestDist = -1;
        foreach(CharacterBase enemy in enemies)
        {
            float dist = Vector2.Distance(player.transform.position, enemy.transform.position);
            if(nearestDist < 0 || dist < nearestDist)
            {
                nearestDist = dist;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    /// <summary>
    /// プレイヤーに最も遠い敵機を取得する
    /// </summary>
    /// <returns></returns>
    public CharacterBase GetFarthestEnemy()
    {
        CharacterBase player = ManagerBattlePhase.Instance.GetPlayer();
        CharacterBase farthestEnemy = null;
        float closestDist = -1;
        foreach(CharacterBase enemy in enemies)
        {
            float dist = Vector2.Distance(player.transform.position, enemy.transform.position);
            if(closestDist < 0 || dist > closestDist)
            {
                closestDist = dist;
                farthestEnemy = enemy;
            }
        }
        return farthestEnemy;
    }

    /// <summary>
    /// 最も残ライフの低い敵機を取得する
    /// </summary>
    /// <returns></returns>
    public CharacterBase GetEnemyWithLowestLife()
    {
        CharacterBase player = ManagerBattlePhase.Instance.GetPlayer();
        CharacterBase targetEnemy = null;
        float lowestLife = -1;
        foreach(CharacterBase enemy in enemies)
        {
            float dist = Vector2.Distance(player.transform.position, enemy.transform.position);
            if(lowestLife < 0 || lowestLife > targetEnemy.GetCurrentLife())
            {
                lowestLife = targetEnemy.GetCurrentLife();
                targetEnemy = enemy;
            }
        }
        return targetEnemy;
    }

    /// <summary>
    /// 最も残ライフの高い敵機を取得する
    /// </summary>
    /// <returns></returns>
    public CharacterBase GetEnemyWithHighestLife()
    {
        CharacterBase player = ManagerBattlePhase.Instance.GetPlayer();
        CharacterBase targetEnemy = null;
        float highestLife = -1;
        foreach(CharacterBase enemy in enemies)
        {
            float dist = Vector2.Distance(player.transform.position, enemy.transform.position);
            if(highestLife < 0 || highestLife < targetEnemy.GetCurrentLife())
            {
                highestLife = targetEnemy.GetCurrentLife();
                targetEnemy = enemy;
            }
        }
        return targetEnemy;
    }

    /// <summary>
    /// バトル開始時に呼び出す
    /// </summary>
    public void OnStartBattlePhase()
    {
        // 過去の敵機排除（念のため）
        if(enemies.Count > 0)
        {
            foreach(EnemyBase enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
            enemies.Clear();
        }

        Rect corners = BasicUtil.GetScreenWorldCorners(Camera.main);
        enemySpawnArea = BasicUtil.CreateRectFromCenter(
            new Vector2(corners.max.x + 2.0f, 0.5f),
            0.0f,
            corners.height - 2.0f);
    }

    /// <summary>
    /// 指定の敵機を出現させる
    /// </summary>
    /// <param name="characterName"></param>
    /// <returns></returns>
    public EnemyBase SpawnNewEnemy(CharacterName characterName)
    {
        GameObject prefab = GetCharacterPrefab(characterName);
        if(prefab == null) return null;
        EnemyBase enemy = Instantiate(prefab).GetComponent<EnemyBase>();
        enemy.InitializeCharacter(CharacterDataList.GetCharacterData(characterName));
        enemy.transform.position = RandUtil.GetRandomVector2In(enemySpawnArea);
        return enemy;
    }

    /// <summary>
    /// リストから敵機を除外する
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    public bool RemoveEnemyFromList(EnemyBase enemy)
    {
        return enemies.Remove(enemy);
    }

    /// <summary>
    /// 敵機のPrefabを取得する
    /// </summary>
    /// <param name="characterName"></param>
    /// <returns></returns>
    private GameObject GetCharacterPrefab(CharacterName characterName)
    {
        // 既にキャッシュが存在する場合はそれを返す
        if(characterPrefabs.ContainsKey(characterName)) return characterPrefabs[characterName];
        // キャッシュに新規登録
        GameObject prefab = Resources.Load<GameObject>(GetCharacterPrefabPath(characterName));
        if(prefab == null)
        {
            Debug.LogError("キャラクターのPrefab取得に失敗: " + characterName);
            return null;
        }
        characterPrefabs.Add(characterName, prefab);
        return prefab;
    }

    /// <summary>
    /// 敵機のPrefabのパスを取得する
    /// </summary>
    /// <param name="characterName"></param>
    /// <returns></returns>
    private string GetCharacterPrefabPath(CharacterName characterName)
    {
        switch(characterName)
        {
            case CharacterName.EnemyA:
                return Consts.Resources.Character.EnemyA;

            default:
                Debug.LogError("不正なキャラクター名: " + characterName);
                return "";
        }
    }
}