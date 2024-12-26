
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
            case CharacterName.EnemyA:
                return CharacterDataEnemyA.Instance;
            case CharacterName.Player:
                return CharacterDataPlayer.Instance;
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
            case CharacterType.EnemyNormal:
                return new HashSet<CharacterName> { CharacterName.EnemyA, };
            case CharacterType.Player:
                return new HashSet<CharacterName> { CharacterName.Player, };

            default:
                Debug.LogError("不正なキャラクタータイプ: " + characterType);
                return null;
        }
    }
}