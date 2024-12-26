using UnityEngine;

/// <summary>
/// バッグを配置する用のスロット
/// ステージに始めから存在するスロットを管理
/// この上にバッグを配置し、その上にアイテムが設置できるようになる
/// 
/// そのためゲーム開始以降、このクラスを別途追加、変更等を行うことはない想定
/// </summary>
public class StageSlot : MonoBehaviour
{
    // 生成用Prefabキャッシュ
    private static GameObject bagSlotPrefab = null;

    private SpriteRenderer bg;
    private SpriteRenderer overlay;
    private Vector2Int pos;
    public Vector2Int Pos { get{ return pos; } }

    /// <summary>
    /// スロットの色味を変更する
    /// </summary>
    /// <param name="isPlacable"></param>
    public void SetOverlayColor(bool isPlacable)
    {
        if(isPlacable) overlay.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
        else overlay.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
    }

    /// <summary>
    /// スロットの色味を消す
    /// </summary>
    public void ClearOverlayColor()
    {
        overlay.color = new Color(1f, 1f, 1f, 0f);
    }

    /// <summary>
    /// バッグ用スロットを指定座標に生成する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static StageSlot InstantiateStageSlot(int x, int y)
    {
        // オブジェクト生成
        if(bagSlotPrefab == null) bagSlotPrefab = Resources.Load<GameObject>(Consts.Resources.BagItem.StageSlot);
        GameObject obj = Instantiate(bagSlotPrefab);        
        obj.transform.SetParent(BasicUtil.GetRootObject(Consts.Roots.BagSlotRoot).transform);

        // スロット情報初期化
        StageSlot slot = obj.GetComponent<StageSlot>();
        slot.bg = obj.transform.Find("SlotImage")?.GetComponent<SpriteRenderer>();    
        slot.overlay = obj.transform.Find("Overlay")?.GetComponent<SpriteRenderer>(); 
        slot.ClearOverlayColor();
        
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        Sprite sprite = sr.sprite;

        // // スケール補正して1unit単位に合わせる
        // // スロットは必ず1unit固定
        // float height = sprite.texture.height;
        // float width = sprite.texture.width;
        // float currentPPU = sprite.pixelsPerUnit;
        // float newScale = currentPPU / width;
        // obj.transform.localScale = new Vector2(newScale, newScale);

        // 配置設定
        obj.transform.localPosition = new Vector2(x, y);

        // 色味お試し変更
        if((x + y) % 2 == 0) slot.bg.color = Color.gray;
        else slot.bg.color = Color.white;
        // スロット情報設定
        slot.pos = new Vector2Int(x, y);

        return slot;
    }
}
