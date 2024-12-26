using UnityEngine;

/// <summary>
/// バトル画面で画面端に表示される所持アイテム一覧
/// </summary>
public class BattleListItem : MonoBehaviour
{
    private static GameObject itemPrefab; // 基礎となるアイテムオブジェクトのPrefab

    private BagItemDataBase data; // アイテムのデータ
    private float elapsedTime = 0.0f; // 経過時間
    private SpriteRenderer sr; // SpriteRenderer
    
    // public void Start()
    // {
    //     data = BagItemDataList.GetItemData(BagItemName.Apple);
    //     sr = GetComponent<SpriteRenderer>();
    // }

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        elapsedTime %= data.Cooldown;
        float progress = elapsedTime / data.Cooldown;
        // Debug.Log("Progress: " + progress);
        sr.material.SetFloat("_Progress", progress);
        sr.material.SetFloat("_UseCooldownEffect", 1);

        // material.SetFloat("_Cooldown", progress); // クールタイムの進行度 (0.0 ～ 1.0)
        sr.material.SetFloat("_GlowThickness", 0.05f); // グロウの境界線の厚さ
        sr.material.SetColor("_GlowColor", new Color(1, 1, 0, 0.1f)); // グロウの色
        // material.SetColor("_BaseColor", Color.white); // 基本のスプライトの色
        
    }

    /// <summary>
    /// 指定アイテム名をもとに、所持アイテム一覧用のオブジェクトを取得する
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public static BattleListItem InstantiateBattleListItem(BagItemName itemName)
    {
        BagItemDataBase data = BagItemDataList.GetItemData(itemName);
        if(data == null) return null;

        // Prefabの取得
        if(itemPrefab == null) itemPrefab = Resources.Load<GameObject>(Consts.Resources.BattleItemListItemPrefab);
        BattleListItem item = Instantiate(itemPrefab).GetComponent<BattleListItem>();

        // 初期化処理
        item.data = data;
        item.elapsedTime = 0.0f;
        item.sr = item.GetComponent<SpriteRenderer>();
        // アイテムの画像を設定する
        item.sr.sprite = BasicUtil.LoadSprite(item.data.SpritePathBattle);
        return item;
    }



}