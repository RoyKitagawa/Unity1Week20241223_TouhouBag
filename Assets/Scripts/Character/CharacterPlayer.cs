using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : CharacterBase
{
    /// <summary>
    /// キャラクター死亡時に呼び出される処理
    /// </summary>
    public override void OnDead()
    {
        // TODO 死亡処理
        Debug.Log("プレイヤー死亡！");
    }
}
