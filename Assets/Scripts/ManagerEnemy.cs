using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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

    // public void Update()
    // {
    //     // 敵機出現処理
    //     enemySpawnElapsedTime += Time.deltaTime;
    //     if(enemySpawnElapsedTime >= enemySpawnCooldown)
    //     {
    //         if(ManagerBattleMode.Instance.GetRemainEnemiesToBeSpawned() > 0)
    //         {
    //             enemySpawnElapsedTime = 0;
    //             // ラスト1体はボス
    //             EnemyBase enemy = ManagerBattleMode.Instance.GetRemainEnemiesToBeSpawned() > 1
    //                 ? SpawnNewEnemy(RandUtil.GetRandomItem(CharacterDataList.GetCharacterNames(CharacterType.EnemyNormal)))
    //                 : SpawnNewEnemy(CharacterName.EnemyBossChiruno);
    //             enemies.Add(enemy);

    //             ManagerBattleMode.Instance.OnEnemySpawn();
    //         }
    //     }
    // }

    public int GetEnemiesCount()
    {
        return enemies.Count;
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
        CharacterBase player = ManagerBattleMode.Instance.GetPlayer();
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
        CharacterBase player = ManagerBattleMode.Instance.GetPlayer();
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
        CharacterBase player = ManagerBattleMode.Instance.GetPlayer();
        CharacterBase targetEnemy = null;
        float lowestLife = -1;
        foreach(CharacterBase enemy in enemies)
        {
            if(lowestLife < 0 || lowestLife > enemy.GetCurrentLife())
            {
                lowestLife = enemy.GetCurrentLife();
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
        CharacterBase player = ManagerBattleMode.Instance.GetPlayer();
        CharacterBase targetEnemy = null;
        float highestLife = -1;
        foreach(CharacterBase enemy in enemies)
        {
            if(highestLife < 0 || highestLife < enemy.GetCurrentLife())
            {
                highestLife = enemy.GetCurrentLife();
                targetEnemy = enemy;
            }
        }
        return targetEnemy;
    }

    /// <summary>
    /// 残っている敵をDestroyする
    /// </summary>
    public void ClearAllEnemies()
    {
        if(enemies.Count > 0)
        {
            foreach(EnemyBase enemy in enemies)
            {
                if(enemy != null) Destroy(enemy.gameObject);
            }
            enemies.Clear();
        }
    }

    public void InitializeEnemySpawnArea()
    {
        Rect corners = BasicUtil.GetScreenWorldCorners(Camera.main);
        enemySpawnArea = BasicUtil.CreateRectFromCenter(
            new Vector2(corners.max.x + 2.0f, 0.0f),
            0.0f,
            corners.height - 5.0f);
    }

    /// <summary>
    /// 指定種別の敵を出現させる
    /// </summary>
    /// <param name="type"></param>
    public void SpawnEnemy(CharacterType type)
    {
        HashSet<CharacterName> names = CharacterDataList.GetCharacterNames(type);
        if(names.Count <= 0)
        {
            Debug.LogError("Spawnする敵種別が存在しません: " + type);
            return;
        }
        SpawnEnemy(type, RandUtil.GetRandomItem(names));
    }

    /// <summary>
    /// 指定の敵機を出現させる
    /// </summary>
    /// <param name="characterName"></param>
    /// <returns></returns>
    public EnemyBase SpawnEnemy(CharacterType type, CharacterName characterName)
    {
        GameObject prefab = GetCharacterPrefab(characterName);
        if(prefab == null) return null;

        EnemyBase enemy = Instantiate(prefab).GetComponent<EnemyBase>();
        enemy.InitializeCharacter(CharacterDataList.GetCharacterData(characterName));
        Vector2 spawnPos = RandUtil.GetRandomVector2In(enemySpawnArea);
        if(type == CharacterType.EnemyMidBoss || type == CharacterType.EnemyFinalBoss)
            enemy.transform.position = new Vector2(spawnPos.x, spawnPos.y / 2f);
        else
            enemy.transform.position = spawnPos;

        enemies.Add(enemy);
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
            // 一般兵
            case CharacterName.EnemyWeakA:
                return Consts.Resources.Character.EnemyWeakA;
            case CharacterName.EnemyWeakB:
                return Consts.Resources.Character.EnemyWeakB;
            case CharacterName.EnemyWeakC:
                return Consts.Resources.Character.EnemyWeakC;
            case CharacterName.EnemyNormalD:
                return Consts.Resources.Character.EnemyNormalD;
            case CharacterName.EnemyNormalE:
                return Consts.Resources.Character.EnemyNormalE;
            case CharacterName.EnemyNormalF:
                return Consts.Resources.Character.EnemyNormalF;
            
            // 中ボス
            case CharacterName.EnemyStrongA:
                return Consts.Resources.Character.EnemyStrongA;
            case CharacterName.EnemyStrongB:
                return Consts.Resources.Character.EnemyStrongB;
            case CharacterName.EnemyStrongC:
                return Consts.Resources.Character.EnemyStrongC;
            case CharacterName.EnemyStrongD:
                return Consts.Resources.Character.EnemyStrongD;
            case CharacterName.EnemyStrongE:
                return Consts.Resources.Character.EnemyStrongE;
            case CharacterName.EnemyStrongF:
                return Consts.Resources.Character.EnemyStrongF;

            // 大ボス
            case CharacterName.EnemyBossChiruno:
                return Consts.Resources.Character.EnemyBossChiruno;

            default:
                Debug.LogError("不正なキャラクター名: " + characterName);
                return "";
        }
    }
}