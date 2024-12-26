using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// アイテムの90度単位の角度
/// </summary>
public enum Rotation
{
    Default,
    Angle90,
    Angle180,
    Angle270,
    Angle360,
}


/// <summary>
/// バッグ用アイテム
/// </summary>
public class BagItem : TappableObject
{
    // アイテムデータ
    private BagItemDataBase data = null;
    // アイテムのコンポーネント
    private SpriteRenderer image = null;
    private Rigidbody2D rb = null;
    private BoxCollider2D boxCollider = null;
    // アイテムの現在の回転
    private Rotation currentRotation = Rotation.Default;
    // 回転時の座標調整用GameObject
    private GameObject pivot = null;
    // アイテムが接触中のスロットコライダー一覧
    private HashSet<BagItemCell> hitSlotCells = new HashSet<BagItemCell>();
    // 内包するセル一覧
    private HashSet<BagItemCell> cells = new HashSet<BagItemCell>();
    // 内包するセルとスロットの接触判定に使うHashSet
    private HashSet<BagItemCell> targetSlots = new HashSet<BagItemCell>();
    private HashSet<BagItemCell> cellHitSlots = new HashSet<BagItemCell>();
    // アイテムが設置中か
    private bool isPlaced = false;

    public void Start()
    {
        pivot = new GameObject(Consts.Names.Pivot);
        pivot.transform.localPosition = Vector2.zero;
        pivot.transform.SetParent(transform);

        // アイテム内のセル取得
        if(cells.Count <= 0) InitCells();
    }

    public BagItemName GetDataItemName()
    {
        return data.ItemName;
    }

    private Material material;
    private float elapsedTime = 0.0f;
    public override void Update()
    {
        base.Update();
        
        if(tag == Consts.Tags.Bag)
        {
            float cooldown = 5.0f;
            elapsedTime += Time.deltaTime;
            elapsedTime %= cooldown;
            float progress = elapsedTime / cooldown;
            // Debug.Log("Progress: " + progress);
            if(material == null) material = GetImage().material;
            material.SetFloat("_Progress", progress);
            material.SetFloat("_UseCooldownEffect", 1);

            // material.SetFloat("_Cooldown", progress); // クールタイムの進行度 (0.0 ～ 1.0)
            material.SetFloat("_GlowThickness", 0.05f); // グロウの境界線の厚さ
            material.SetColor("_GlowColor", new Color(1, 1, 0, 0.1f)); // グロウの色
            // material.SetColor("_BaseColor", Color.white); // 基本のスプライトの色
        }

        // マウス右クリック処理
        if(Input.GetMouseButtonDown(1))
        {
            if(isTapped)
            {
                RotateToNext90();
            }
        }

        // スロット衝突中は常に呼び出す処理
        if(hitSlotCells != null && hitSlotCells.Count > 0 && isTapped)
            UpdateHitSlotStatus();
    }

    public BagItemDataBase GetData()
    {
        return data;
    }

    /// <summary>
    /// アイテムデータを設定する
    /// </summary>
    /// <param name="itemData"></param>
    public void SetItemData(BagItemDataBase itemData)
    {
        data = itemData;
    }

    public bool IsPosOverlapWithItem(Vector2 worldPos)
    {
        return GetCollider().bounds.Contains(worldPos);
    }

    /// <summary>
    /// 他Colliderとの衝突処理
    /// </summary>
    /// <param name="target"></param>
    public void OnTriggerEnter2D(Collider2D target)
    {
        // 設置対象以外は対象外
        // 操作している側のアイテムでのみ受け付ける
        if(transform.tag != Consts.Tags.Bag && transform.tag != Consts.Tags.Item) return;
        if(target.tag != Consts.Tags.BagCell && target.tag != Consts.Tags.StageSlotCell) return;
        
        if(transform.tag == Consts.Tags.Item)
            if(target.transform.tag != Consts.Tags.BagCell) return;
        if(transform.tag == Consts.Tags.Bag)
            if(target.transform.tag != Consts.Tags.StageSlotCell) return;
       
        // 衝突リストに追加
        BagItemCell cell = target.GetComponent<BagItemCell>();        
        // 設置済みの場合だけ対象とする
        BagItem item = cell.GetRootItem();
        if(!item.IsPlaced()) return;
        hitSlotCells.Add(cell);
    }

    /// <summary>
    /// 他Colliderとの衝突終了処理
    /// </summary>
    /// <param name="target"></param>
    public void OnTriggerExit2D(Collider2D target)
    {
        // 設置対象以外は対象外
        // 操作している側のアイテムでのみ受け付ける
        if(transform.tag != Consts.Tags.Bag && transform.tag != Consts.Tags.Item) return;
        if(target.tag != Consts.Tags.BagCell && target.tag != Consts.Tags.StageSlotCell) return;
        
        if(transform.tag == Consts.Tags.Item)
            if(target.transform.tag != Consts.Tags.BagCell) return;
        if(transform.tag == Consts.Tags.Bag)
            if(target.transform.tag != Consts.Tags.StageSlotCell) return;
        
        // 衝突リストから除外
        BagItemCell cell = target.GetComponent<BagItemCell>();
        hitSlotCells.Remove(cell);
        
        // 離れたスロットは強制的にOverlay表示を消す
        cell.ClearOverlayColor();
    }

    /// <summary>
    /// タップ開始時に呼び出される
    /// 
    /// 無事処理が最後まで通った場合はTrueを返す
    /// 処理が無効化、中断された場合はFalseを返す
    /// </summary>
    public override bool OnTapDown()
    {
        // これより上にアイテムが存在する場合、このアイテムでタップ処理は行わない
        if(transform.tag == Consts.Tags.Bag && isPlaced)
        {
            // タップした座標での確認
            BagItem item = StageManager.Instance.GetItemExistAtWorld(BasicUtil.GetMousePos());
            if(item != null)
            {
                // バッグの上にアイテムがあるため、アイテムの方の処理を優先する
                return false;
            }

            // バッグが配置されたスロットのいずれかにアイテムが被っている場合、何もしない
            foreach(BagItemCell cell in cells)
            {
                if(cell.SlotPos.Equals(new Vector2Int(-1, -1))) continue;
                if(StageManager.Instance.IsItemExistAtSlot(cell.SlotPos))
                {
                    // バッグ上にアイテムがあるため、なにもしない
                    return false;
                }
            }
        }

        base.OnTapDown();

        // 移動中はSortingLayerを最上位に
        GetImage().sortingLayerName = Consts.SortingLayer.DragItem;

        // 拡大処理
        transform.DOScale(1.2f, 0.2f);
        // 回転、物理演算関係の設定
        SetPhysicSimulator(false);
        RotateToNearest90();

        // 設置フラグをOFFに
        SetIsPlaced(false);

        // タップ処理完了を伝える
        return true;
    }

    /// <summary>
    /// タップ終了時に呼び出される
    /// 
    /// 無事処理が最後まで通った場合はTrueを返す
    /// 処理が無効化、中断された場合はFalseを返す
    /// </summary>
    public override bool OnTapUp()
    {
        // このオブジェクトがタップ状態じゃないなら処理をスキップする
        if(!isTapped) return false;

        base.OnTapUp();

        // 移動終了時はSortingLayerを下に戻す
        if(tag == Consts.Tags.Bag) GetImage().sortingLayerName = Consts.SortingLayer.Bag;
        else if(tag == Consts.Tags.Item) GetImage().sortingLayerName = Consts.SortingLayer.Item;

        // 拡大処理を戻す
        transform.DOScale(1.0f, 0.2f);

        // 全てのスロットとの接触表示をOFFに
        foreach(BagItemCell cell in cells)
        {
            foreach(BagItemCell slot in cell.GetHitSlots())
            {
                slot.ClearOverlayColor();
            }
        }

        // 必要であれば移動も行う
        if(!Move2HitSlot())
        {
            DropItemFromSlot();
        }

        // タップ処理が正常に終了
        return true;
    }

    public void DropItemFromSlot()
    {
        // 物理演算関係の設定
        SetPhysicSimulator(true);
        
        // 移動が発生していない場合、SlotPosを初期化する
        foreach(BagItemCell cell in cells)
        {
            cell.SlotPos = new Vector2Int(-1, -1);
        }
    }

    /// <summary>
    /// アイテムドラッグ中に毎フレーム呼び出される
    /// </summary>
    protected override void OnDrag()
    {
        base.OnDrag();
        Vector3 moveAmt = dragAmt;
        transform.position += moveAmt;
    }

    /// <summary>
    /// 接触しているスロットへと移動する
    /// 移動が発生したかをBooleanで返す
    /// </summary>
    private bool Move2HitSlot()
    {
        // セルサイズと接触セル数が見合ってなければ配置不能とみなし終了
        if(targetSlots.Count < data.CellCount) return false;
        // 起点となる(0,0)のセルを取得
        // （0,0）座標のセルは全てのアイテム共通で存在するはずのため、ここを起点とする
        BagItemCell cell = GetCellAtCellPos(Vector2Int.zero);
        if(cell == null)
        {
            Debug.LogError("移動の起点となる0,0セルが存在しません");
            return false;
        }

        // 対象のスロットを取得する。存在しない場合は終了
        BagItemCell slot = cell.GetClosestHitSlot();
        if(slot == null) return false;

        // セルの座標から、アイテムのサイズと回転を踏まえて中心座標を計算する
        BoxCollider2D slotCollider = slot.GetComponent<BoxCollider2D>();
        Vector2 slotPos = slot.transform.position;
        Vector2 slotWorldSize = slotCollider.size * slot.transform.lossyScale;

        // サイズ補正計算
        Vector2 halfSize = new Vector2(data.Size.x / 2.0f, data.Size.y / 2.0f);
        float adjustX = slotWorldSize.x * (halfSize.x - 0.5f);
        float adjustY = slotWorldSize.y * (halfSize.y - 0.5f);
        Vector2 adjustDiff;
        
        switch(currentRotation)
        {
            case Rotation.Default: // 時計回りに0度 or 360度
            case Rotation.Angle360:
            default:
                adjustDiff = new Vector2(adjustX, -adjustY);
                break;

            case Rotation.Angle90: // 時計回りに90度
                adjustDiff = new Vector2(adjustY, adjustX);
                break;

            case Rotation.Angle180: // 時計回りに180度
                adjustDiff = new Vector2(-adjustX, adjustY);
                break;

            case Rotation.Angle270: // 時計回りに270度
                adjustDiff = new Vector2(-adjustY, -adjustX);
                break;
        }
        // 移動
        Move2(slotPos + adjustDiff, OnItemPlaced);
        // 移動先に同レイヤーのアイテムが既に存在す場合、剥がす
        // バッグの場合
        if(transform.tag == Consts.Tags.Bag)
        {
            foreach(BagItemCell _cell in cells)
            {
                HashSet<BagItem> existItems = StageManager.Instance.GetBagsPlacedAtSlot(_cell.SlotPos);
                foreach(BagItem existItem in existItems)
                {
                    if(existItem != this)
                    {
                        existItem.DropItemFromSlot();
                    }
                }
            }
        }
        // アイテムの場合
        else if(transform.tag == Consts.Tags.Item)
        {
            foreach(BagItemCell _cell in cells)
            {
                HashSet<BagItem> existItems = StageManager.Instance.GetItemsExistAtSlot(_cell.SlotPos);
                foreach(BagItem existItem in existItems)
                {
                    if(existItem != this)
                    {
                        existItem.DropItemFromSlot();
                    }
                }
            }
        }
        return true;
    }

    // public void Move2Slot(Vector2Int slotPos, Action onComplete = null)
    // {
    //     Vector2 destPos = StageManager.Instance.GetSlotWorldPosAt(slotPos);
    //     transform.DOMove(destPos, 0.2f).OnComplete(() => { onComplete?.Invoke(); });
    //     SetPhysicSimulator(false);
    // }

    /// <summary>
    /// 指定の場所へアイテムを移動する
    /// </summary>
    /// <param name="destPos"></param>
    public void Move2(Vector2 destPos, Action onComplete = null)
    {
        Move2(destPos, 0.2f, onComplete);
    }

    /// <summary>
    /// 指定の場所へアイテムを移動する
    /// </summary>
    /// <param name="destPos"></param>
    public void Move2(Vector2 destPos, float duration = 0.0f, Action onComplete = null)
    {
        if(duration <= 0.0f)
        {
            transform.position = destPos;
            onComplete?.Invoke();
        }
        else
        {
            transform.DOMove(destPos, duration).OnComplete(() => { onComplete?.Invoke(); });
        }
        SetPhysicSimulator(false);
    }

    /// <summary>
    /// 各スロットのSlotPosを最も近いスロットのSlotPosに更新する
    /// </summary>
    public void SetItemCellSlotAtClosestSlot()
    {
        if(cells.Count <= 0) InitCells();
        foreach(BagItemCell cell in cells)
        {
            Vector2Int slotPos = StageManager.Instance.GetClosestSlotPos(cell.transform.position);
            cell.SlotPos = slotPos;
        }
    }

    /// <summary>
    /// アイテム設置完了時に呼び出される
    /// </summary>
    public void OnItemPlaced()
    {
        SetIsPlaced(true);
        // 設置中は衝突を起こさないように
        GetCollider().isTrigger = true;
    }

    public void SetIsPlaced(bool flag)
    {
        isPlaced = flag;
    }

    /// <summary>
    /// アイテムが現在設置中か
    /// </summary>
    /// <returns></returns>
    public bool IsPlaced()
    {
        return isPlaced;
    }

    /// <summary>
    /// 指定CellPosにあるセルを取得する
    /// 存在しない場合はNullを返す
    /// </summary>
    /// <param name="cellPos"></param>
    /// <returns></returns>
    public BagItemCell GetCellAtCellPos(Vector2Int cellPos)
    {
        if(cellPos.x < 0 || cellPos.x >= data.Size.x) return null;
        if(cellPos.y < 0 || cellPos.y >= data.Size.y) return null;
        
        foreach(BagItemCell cell in cells)
        {
            if(cell.CellPos.Equals(cellPos)) return cell;
        }
        return null;
    }

    /// <summary>
    /// 指定CellPosにセルが存在するか確認する
    /// </summary>
    /// <param name="cellPos"></param>
    /// <returns></returns>
    public bool IsCellExistAtCellPos(Vector2Int cellPos)
    {
        return GetCellAtCellPos(cellPos) != null;
    }

    /// <summary>
    /// 指定SlotPosにあるセルを取得する
    /// 存在しない場合はNullを返す
    /// </summary>
    /// <param name="slotPos"></param>
    /// <returns></returns>
    public BagItemCell GetCellAtSlotPos(Vector2Int slotPos)
    {
        foreach(BagItemCell cell in cells)
        {
            if(cell.SlotPos.Equals(slotPos)) return cell;
        }
        return null;
    }

    /// <summary>
    /// 指定SlotPosにセルが存在するか確認する
    /// </summary>
    /// <param name="slotPos"></param>
    /// <returns></returns>
    public bool IsCellExistsAtSlotPos(Vector2Int slotPos)
    {
        return GetCellAtSlotPos(slotPos) != null;
    }

    /// <summary>
    /// スロットとの接触ステータスを更新する
    /// </summary>
    private void UpdateHitSlotStatus()
    {
        // 各セルでスロットとマッチしているか確認する
        Vector2Int startCellPos = Vector2Int.zero;
        Vector2Int startSlotPos = Vector2Int.zero;
        // 管理用リスト初期化
        targetSlots.Clear();
        cellHitSlots.Clear();

        // 0,0のセルを先に計算する
        // 実際のアイテム移動の際も「0,0セルの最も近いセル」を基準にするため、可能な場合は0,0を必ず固定にすること
        foreach(BagItemCell cell in cells)
        {
            // 0,0以外の場合はスキップ
            if(cell.CellPos != Vector2Int.zero) continue;

            // そのセルが接触しているスロット一覧を取得する
            foreach(BagItemCell _slot in cell.GetHitSlots())
            {
                cellHitSlots.Add(_slot);
            }

            // 対象となるスロットを取得
            BagItemCell slot = cell.GetClosestHitSlot();
            // 対象が存在しない場合（セルがスロットに接触していない場合）スルーする
            if(slot == null) continue;

            startCellPos = cell.CellPos;
            startSlotPos = slot.SlotPos;
            targetSlots.Add(slot);

            // 設置候補場所として先に設置先スロットを記録
            cell.SlotPos = slot.SlotPos;
        }

        // 残りの各セルを見ていく
        foreach(BagItemCell cell in cells)
        {
            if(cell == null)
            {
                Debug.Log("Class " + transform.name + " のCellがNullです");
                continue;
            
            }
            // 0,0の場合はスキップ
            if(cell.CellPos == Vector2Int.zero) continue;

            // そのセルが接触しているスロット一覧を取得する
            foreach(BagItemCell _slot in cell.GetHitSlots())
            {
                cellHitSlots.Add(_slot);
            }

            // 起点スロットの場合
            if(targetSlots.Count <= 0)
            {
                // 対象となるスロットを取得
                BagItemCell slot = cell.GetClosestHitSlot();
                // 対象が存在しない場合（セルがスロットに接触していない場合）スルーする
                if(slot == null) continue;

                startCellPos = cell.CellPos;
                startSlotPos = slot.SlotPos;
                targetSlots.Add(slot);
                // 設置候補場所として先に設置先スロットを記録
                cell.SlotPos = slot.SlotPos;
            }
            // それ以外の場合
            else
            {
                Vector2Int cellPosDiff = cell.CellPos - startCellPos;
                Vector2Int targetSlotPos;
                // 対象スロットの場所を角度を踏まえて計算する
                switch(currentRotation)
                {
                    case Rotation.Default: // 時計回りに0度 or 360度
                    case Rotation.Angle360:
                    default:
                        targetSlotPos = new Vector2Int(startSlotPos.x + cellPosDiff.x, startSlotPos.y - cellPosDiff.y);
                        break;

                    case Rotation.Angle90: // 時計回りに90度
                        targetSlotPos = new Vector2Int(startSlotPos.x + cellPosDiff.y, startSlotPos.y + cellPosDiff.x);
                        break;

                    case Rotation.Angle180: // 時計回りに180度
                        targetSlotPos = new Vector2Int(startSlotPos.x - cellPosDiff.x, startSlotPos.y + cellPosDiff.y);
                        break;

                    case Rotation.Angle270: // 時計回りに270度
                        targetSlotPos = new Vector2Int(startSlotPos.x - cellPosDiff.y, startSlotPos.y - cellPosDiff.x);
                        break;
                }
                BagItemCell slot = cell.GetHitSlotAt(targetSlotPos);
                if(slot != null)
                {
                    targetSlots.Add(slot);
                    // 設置候補場所として先に設置先スロットを記録
                    cell.SlotPos = slot.SlotPos;
                }
            }
        }
        // ターゲットのスロットを除外する
        foreach(BagItemCell targetSlot in targetSlots) { cellHitSlots.Remove(targetSlot); }
        foreach(BagItemCell nonTargetSlot in cellHitSlots) { nonTargetSlot.ClearOverlayColor(); }
        // アイテムが設置可能か
        bool isPlacable = targetSlots.Count >= data.CellCount;
        // 衝突済みのスロットの色を設定する
        foreach(BagItemCell slot in targetSlots) { slot.SetOverlayColor(isPlacable); }
    }

    /// <summary>
    /// 一番近い90度単位へ回転する
    /// </summary>
    private void RotateToNearest90()
    {
        // オブジェクトの現在の角度（回転の軸はZ軸）
        Rotation rot = GetNearest90Angle();
        RotateTo(rot);
    }

    private void RotateToNext90()
    {
        // オブジェクトの現在の角度（回転の軸はZ軸）
        Rotation rot = GetNearest90Angle();
        switch(rot)
        {
            case Rotation.Angle90:
                rot = Rotation.Angle360;
                break;

            case Rotation.Angle180:
                rot = Rotation.Angle90;
                break;

            case Rotation.Angle270:
                rot = Rotation.Angle180;
                break;

            case Rotation.Default:
            case Rotation.Angle360:
            default:
                rot = Rotation.Angle270;
                break;
        }
        RotateTo(rot);
    }

    /// <summary>
    /// 指定角度へ回転する
    /// </summary>
    /// <param name="rot"></param>
    private void RotateTo(Rotation rot)
    {
        // Pivotの座標をタップ座標へ移動
        pivot.transform.position = currentTapPos;
        // 対象角度の取得
        float angle = 0;
        switch(rot)
        {
            case Rotation.Angle90:
                angle = 90;
                break;

            case Rotation.Angle180:
                angle = 180;
                break;

            case Rotation.Angle270:
                angle = 270;
                break;

            case Rotation.Default:
            case Rotation.Angle360:
            default:
                angle = 0;
                break;
        }
        // 指定の角度に変更
        transform.DORotate(new Vector3(0f, 0f, angle), 0.2f);
        // transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // どの角度に移動しているか記録する
        currentRotation = rot;
        // 回転によってずれたPivot座標とタップ座標の差分を取得
        Vector3 dist = VectorUtil.Sub(currentTapPos, pivot.transform.position);
        // その分アイテムオブジェクト自体を移動して、Pivotがタップ座標へ来るようにする
        transform.position = VectorUtil.Add(transform.position, dist);
    }

    /// <summary>
    /// 最も近い90度単位を計算する
    /// </summary>
    /// <returns></returns>
    private Rotation GetNearest90Angle()
    {
        // オブジェクトの現在の角度（回転の軸はZ軸）
        float currentAngle = transform.eulerAngles.z % 360;
        // 各アングルへの差分を取得
        float diff0 = currentAngle - 0;
        float diff90 = currentAngle - 90;
        float diff180 = currentAngle - 180;
        float diff270 = currentAngle - 270;
        float diff360 = currentAngle - 360;

        // 最も近い角度を取得。安全のために45を含めて確認
        if(Math.Abs(diff0) <= 45) return Rotation.Default;
        else if(Mathf.Abs(diff90) <= 45) return Rotation.Angle90;
        else if(Mathf.Abs(diff180) <= 45) return Rotation.Angle180;
        else if(Mathf.Abs(diff270) <= 45) return Rotation.Angle270;
        else if(Mathf.Abs(diff360) <= 45) return Rotation.Angle360;

        Debug.LogError("最も近い90度Angleの計算に失敗: 現在の角度 = " + currentAngle);
        return 0;
    }

    /// <summary>
    /// 画像の色味を設定する
    /// </summary>
    /// <param name="color"></param>
    public void SetImageColor(Color color)
    {
        GetImage().color = color;
    }

    /// <summary>
    /// RigidBody2Dにおける自由落下、自由回転を有効にするか
    /// </summary>
    /// <param name="flag"></param>
    public void SetPhysicSimulator(bool flag)
    {
        if(flag)
        {
            GetRigidbody2D().constraints = RigidbodyConstraints2D.None;
            // 自由落下中は物理判定も入れる
            GetCollider().isTrigger = false;
        }
        else
        {
            GetRigidbody2D().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            GetRigidbody2D().linearVelocity = Vector2.zero;
            GetRigidbody2D().angularVelocity = 0.0f;
            // スロット設置時は物理判定を外す
            GetCollider().isTrigger = true;
        }
    }

    private SpriteRenderer GetImage()
    {
        if(image == null) image = transform.Find("Image").GetComponent<SpriteRenderer>();
        return image;
    }

    private Rigidbody2D GetRigidbody2D()
    {
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        return rb;
    }

    private BoxCollider2D GetCollider()
    {
        if(boxCollider == null) boxCollider = GetComponent<BoxCollider2D>();
        return boxCollider;
    }

    public void InitCells()
    {
        cells.Clear();
        // アイテム内のセル取得
        BagItemCell[] cellsInItem = GetComponentsInChildren<BagItemCell>();
        foreach(BagItemCell cell in cellsInItem)
        {
            if(cell != null) cells.Add(cell);
        }
        if(cells == null || cells.Count <= 0) Debug.LogError("アイテム内にセルが設定されていません");
    }
}