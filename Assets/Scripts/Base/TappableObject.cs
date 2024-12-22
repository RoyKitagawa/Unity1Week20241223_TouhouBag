using UnityEngine;

/// <summary>
/// タップ、ドラッグ可能なオブジェクト用の規定クラス
/// </summary>
public abstract class TappableObject : MonoBehaviour
{
    protected bool isRaycastTarget = false; // Raycast側でタップ検知する対象か
    protected bool isTappable = true; // タップ可能なオブジェクトか
    protected bool isTapped = false; // タップ中のオブジェクトか
    protected Vector2 lastTapPos = Vector2.zero; // 最後にタップしたワールド座標
    protected Vector2 currentTapPos = Vector2.zero; // 現在タップしているワールド座標
    protected Vector2 dragAmt = Vector2.zero; // 現フレームでの移動距離（最終フレームと現在フレームのタップしたワールド座標の差額）

    /// <summary>
    /// タップ中に発生する各種アップデート処理
    /// </summary>
    public virtual void Update()
    {
        // タップ不能設定の場合は何もしない
        if(!isTappable) return;

        // アイテム操作中の場合のみ、処理を行う
        if(!isTapped) return;

        // タップ終了後にタップ状態が維持されていないように強制解除
        if(!Input.GetKey(KeyCode.Mouse0))
            OnTapUp();
        // タップ継続中の場合、ドラッグ距離を取得
        else
            OnDrag();
    }

    /// <summary>
    /// 強制的にタップ状態を設定する
    /// </summary>
    /// <param name="_isTapped"></param>
    public void ForceSetTapStatus(bool _isTapped)
    {
        isTapped = _isTapped;
    }

    /// <summary>
    /// 実際のタップ時処理
    /// 自動呼び出しイベントとRaycastが重複するリスクがあるため、処理を分けておく
    /// 
    /// 無事処理が最後まで通った場合はTrueを返す
    /// 処理が無効化、中断された場合はFalseを返す
    /// </summary>
    public virtual bool OnTapDown()
    {
        isTapped = true;
        lastTapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTapPos = lastTapPos;
        dragAmt = Vector2.zero;
        return true;
    }

    /// <summary>
    /// 実際のタップ終了時処理
    /// 自動呼び出しイベントとRaycastが重複するリスクがあるため、処理を分けておく
    /// 
    /// 無事処理が最後まで通った場合はTrueを返す
    /// 処理が無効化、中断された場合はFalseを返す
    /// </summary>
    public virtual bool OnTapUp()
    {
        isTapped = false;
        lastTapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTapPos = lastTapPos;        
        return true;
    }

    /// <summary>
    /// タップ状態に、各フレームで呼びだされる
    /// ドラッグの距離を随時更新する
    /// </summary>
    protected virtual void OnDrag()
    {
        currentTapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragAmt = currentTapPos - lastTapPos;
        lastTapPos = currentTapPos;
    }

    /// <summary>
    /// Collider上でマウスクリックが発生した場合に呼び出される
    /// </summary>
    protected virtual void OnMouseDown()
    {
        // タップ可能オブジェクト、及びRaycast不使用時のみTapDownを呼び出す
        if(!isTappable || !isRaycastTarget) return;
        OnTapDown();
    }

    /// <summary>
    /// Collider上でマウスクリックが解除された場合に呼び出される
    /// </summary>
    protected virtual void OnMouseUp()
    {
        // タップ可能オブジェクト、及びRaycast不使用時のみTapDownを呼び出す
        if(!isTappable || !isRaycastTarget) return;
        OnTapUp();
    }
}