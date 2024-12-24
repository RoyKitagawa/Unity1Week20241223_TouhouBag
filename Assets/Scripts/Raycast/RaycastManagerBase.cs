using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Raycastを用いたオブジェクトとのHit判定を管理するマネージャークラスの基底クラス
/// 各ゲームで必要に応じてこのクラスを継承、複製して用いること
/// </summary>
public class RaycastManagerBase : MonoBehaviourSingleton<RaycastManagerBase>
{
    public void Update()
    {
        RunOnMouseDownAction();
        RunOnMouseUpAction();
    }

    protected virtual void RunOnMouseDownAction()
    {
        // クリックが発生していない場合即座に終了
        if(!Input.GetMouseButtonDown(0)) return;
        // Hitしたオブジェクトを取得
        RaycastHit2D[] hits = GetRaycastHit2Ds();

        // ヒットしたオブジェクトを各種検知
        foreach(RaycastHit2D hit in hits)
        {
            // ここに処理を記述
        }
    }

    protected virtual void RunOnMouseUpAction()
    {
        // クリック終了が発生していない場合即座に終了
        if(!Input.GetMouseButtonUp(0)) return;

        RaycastHit2D[] hits = GetRaycastHit2Ds();
        // ヒット対象が無ければ即座に終了
        if(hits == null || hits.Length <= 0) return;

        // ヒットしたオブジェクトを各種検知
        foreach(RaycastHit2D hit in hits)
        {
            // ここに処理を記述
        }
    }


    /// <summary>
    /// マウスクリックの座標で、RaycastがHitしたオブジェクト一覧を取得する
    /// </summary>
    /// <returns></returns>
    protected virtual RaycastHit2D[] GetRaycastHit2Ds()
    {
        // RaycastHit2D[] hits = GetRaycastHit2Ds();  
        // // ヒット対象が無ければ即座に終了
        // if(hits == null || hits.Length <= 0) return null;

        Vector2 mousePos = BasicUtil.GetMousePos();
        return Physics2D.RaycastAll(mousePos, Vector2.zero);
    }
}
