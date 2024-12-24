using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムのセル
/// アイテム側で管理運用を行う
/// </summary>
public class BagItemCell : MonoBehaviour
{
    // アイテム内でのセル座標。値はPrefab側で設定すること
    public Vector2Int CellPos;
    // ステージ上でどのスロットに設置されたか識別用。アイテム設置の段階で設定され、アイテム解除の段階で初期かされる
    public Vector2Int SlotPos;
    // セルが接触中のスロット一覧
    private HashSet<BagItemCell> hitSlots = new HashSet<BagItemCell>();
    // アイテムのコンポーネント
    private SpriteRenderer overlay = null;
    private BoxCollider2D BoxCollider = null;

    public bool IsPosOverlapWithCell(Vector2 worldPos)
    {
        return GetBoxCollider2D().bounds.Contains(worldPos);
    }

    /// <summary>
    /// 衝突中のスロットはセルで各自記録する
    /// </summary>
    /// <param name="target"></param>
    protected void OnTriggerEnter2D(Collider2D target)
    {
        if(!IsHitTargetCollider(target)) return;
        BagItemCell cell = target.GetComponent<BagItemCell>();
        if(cell != null) hitSlots.Add(cell);
        else Debug.Log("OnTriggerEnter: BagItemCellDoesNotExist: This object = [" + this.transform.parent.name + "/" + this.name + "] Tag = " + this.tag + "// TargetHitCollder object = [" + target.transform.parent.name + "/" + target.name + "] Tag = " + target.tag);
    }

    private bool IsHitTargetCollider(Collider2D target)
    {
        if(transform.tag is Consts.Tags.ItemCell
            && target.tag is Consts.Tags.BagCell)
            return true;
        if(transform.tag is Consts.Tags.BagCell
            && target.tag is Consts.Tags.StageSlotCell)
            return true;

        return false;
    }

    /// <summary>
    /// 衝突中のスロットはセルで各自記録する
    /// </summary>
    /// <param name="target"></param>
    protected void OnTriggerExit2D(Collider2D target)
    {
        if(!IsHitTargetCollider(target)) return;
        hitSlots.Remove(target.GetComponent<BagItemCell>());
    }

    /// <summary>
    /// 現在接触中のスロットを全て取得する
    /// </summary>
    /// <returns></returns>
    public HashSet<BagItemCell> GetHitSlots()
    {
        return hitSlots;
    }

    /// <summary>
    /// 指定PosのBagSlotと衝突しているか
    /// </summary>
    /// <param name="slotPos"></param>
    /// <returns></returns>
    public bool IsCollideWithSlot(Vector2Int slotPos)
    {
        return GetHitSlotAt(slotPos) != null;
    }

    /// <summary>
    /// 接触中の指定スロット座標のスロットを取得する
    /// 該当スロットと接触していない場合はNullを返す
    /// </summary>
    /// <param name="slotPos"></param>
    /// <returns></returns>
    public BagItemCell GetHitSlotAt(Vector2 slotPos)
    {
        foreach(BagItemCell slotCollider in hitSlots)
        {
            BagItemCell slot = slotCollider.GetComponent<BagItemCell>();
            if(slot.SlotPos == slotPos) return slot;
        }
        return null;
    }

    /// <summary>
    /// 接触中で、最も近いスロットを取得する
    /// </summary>
    /// <returns></returns>
    public BagItemCell GetClosestHitSlot()
    {
        BagItemCell closestSlot = null;
        float closestDist = 0;
        foreach(BagItemCell slot in hitSlots)
        {
            if(slot == null) continue;
            float dist = Vector2.Distance(transform.position, slot.transform.position);
            if(dist < closestDist || closestSlot == null)
            {
                closestSlot = slot;
                closestDist = dist;
            }
        }
        // 最も近いバッグを返す
        return closestSlot;
    }

    /// <summary>
    /// Overlay用のSpriteRenderを取得する
    /// </summary>
    /// <returns></returns>
    private SpriteRenderer GetOverlay()
    {
        if(overlay == null) overlay = transform.Find(Consts.Names.Overlay).GetComponent<SpriteRenderer>();
        return overlay;
    }

    /// <summary>
    /// BoxCollider2Dを取得する
    /// </summary>
    /// <returns></returns>
    private BoxCollider2D GetBoxCollider2D()
    {
        if(BoxCollider == null) BoxCollider = GetComponent<BoxCollider2D>();
        return BoxCollider;
    }

    /// <summary>
    /// スロットの色味を変更する
    /// </summary>
    /// <param name="isPlacable"></param>
    public void SetOverlayColor(bool isPlacable)
    {
        if(isPlacable) GetOverlay().color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
        else GetOverlay().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
    }

    /// <summary>
    /// スロットの色味を消す
    /// </summary>
    public void ClearOverlayColor()
    {
        GetOverlay().color = new Color(1f, 1f, 1f, 0f);
    }

    public BagItem GetRootItem()
    {
        return transform.GetComponentInParent<BagItem>();
    }
}
