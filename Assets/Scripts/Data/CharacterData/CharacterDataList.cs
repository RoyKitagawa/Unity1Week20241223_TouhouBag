
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataList
{
    /// <summary>
    /// 指定キャラクター名のデータを取得する
    /// </summary>
    /// <param name="characterName"></param>
    /// <returns></returns>
    public static CharacterDataBase GetCharacterData(CharacterName characterName)
    {
        switch(characterName)
        {
            // 一般兵
            case CharacterName.EnemyWeakA:
                return new CharacterDataEnemyWeakA();
            case CharacterName.EnemyWeakB:
                return new CharacterDataEnemyWeakB();
            case CharacterName.EnemyWeakC:
                return new CharacterDataEnemyWeakC();
            case CharacterName.EnemyNormalD:
                return new CharacterDataEnemyNormalD();
            case CharacterName.EnemyNormalE:
                return new CharacterDataEnemyNormalE();
            case CharacterName.EnemyNormalF:
                return new CharacterDataEnemyNormalF();

            // 中ボス
            case CharacterName.EnemyStrongA:
                return new CharacterDataEnemyStrongA();
            case CharacterName.EnemyStrongB:
                return new CharacterDataEnemyStrongB();
            case CharacterName.EnemyStrongC:
                return new CharacterDataEnemyStrongC();
            case CharacterName.EnemyStrongD:
                return new CharacterDataEnemyStrongD();
            case CharacterName.EnemyStrongE:
                return new CharacterDataEnemyStrongE();
            case CharacterName.EnemyStrongF:
                return new CharacterDataEnemyStrongF();

            // 大ボス
            case CharacterName.EnemyBossChiruno:
                return new CharacterDataEnemyBossChiruno();

            // プレイヤー
            case CharacterName.Player:
                return new CharacterDataPlayer();
            
            // エラー
            default:
                Debug.LogError("不正なキャラクター名: " + characterName);
                return null;
        }
    }

    /// <summary>
    /// 指定タイプのキャラクター名を全て取得する
    /// </summary>
    /// <param name="characterType"></param>
    /// <returns></returns>
    public static HashSet<CharacterName> GetCharacterNames(CharacterType characterType)
    {
        switch(characterType)
        {
            case CharacterType.EnemyWeak:
                return new HashSet<CharacterName> { CharacterName.EnemyWeakA, CharacterName.EnemyWeakB, CharacterName.EnemyWeakC, };
            case CharacterType.EnemyNormal:
                return new HashSet<CharacterName> { CharacterName.EnemyNormalD, CharacterName.EnemyNormalE, CharacterName.EnemyNormalF, };
            case CharacterType.EnemyMidBoss:
                return new HashSet<CharacterName> {
                    CharacterName.EnemyStrongA, CharacterName.EnemyStrongB, CharacterName.EnemyStrongC,
                    CharacterName.EnemyStrongD, CharacterName.EnemyStrongE, CharacterName.EnemyStrongF,
                };
            case CharacterType.EnemyFinalBoss:
                return new HashSet<CharacterName> { CharacterName.EnemyBossChiruno, };
            case CharacterType.Player:
                return new HashSet<CharacterName> { CharacterName.Player, };

            default:
                Debug.LogError("不正なキャラクタータイプ: " + characterType);
                return null;
        }
    }
}