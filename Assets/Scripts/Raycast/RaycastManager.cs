using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : RaycastManagerBase
{
    // Raycastで操作対象となるオブジェクトのタグ
    private HashSet<string> raycastTargetTags = new HashSet<string> { Consts.Tags.Bag, Consts.Tags.Item };

    protected override void RunOnMouseDownAction()
    {
        // クリックが発生していない場合即座に終了
        if(!Input.GetMouseButtonDown(0)) return;
        // Hitしたオブジェクトを取得
        RaycastHit2D[] hits = GetRaycastHit2Ds();

        // ヒットしたオブジェクトを各種検知
        foreach(RaycastHit2D hit in hits)
        {
            // バッグ、アイテムのみ検知
            if(!raycastTargetTags.Contains(hit.collider.tag)) continue;

            BagItem item = hit.collider.gameObject.GetComponent<BagItem>();
            // タップダウンが正常に完了した場合、即座にタップダウン処理を終了する
            // （複数のアイテムを持ち運ばないようにするため）
            if(item.OnTapDown()) return;
        }
    }

    protected override void RunOnMouseUpAction()
    {
        // クリック終了が発生していない場合即座に終了
        if(!Input.GetMouseButtonUp(0)) return;

        RaycastHit2D[] hits = GetRaycastHit2Ds();
        // ヒット対象が無ければ即座に終了
        if(hits == null || hits.Length <= 0) return;

        // ヒットしたオブジェクトを各種検知
        foreach(RaycastHit2D hit in hits)
        {
            // バッグ、アイテムのみ検知
            if(!raycastTargetTags.Contains(hit.collider.tag)) continue;
            // 衝突処理を呼び出し
            BagItem item = hit.collider.gameObject.GetComponent<BagItem>();
            item.OnTapUp();
        }
    }
}
