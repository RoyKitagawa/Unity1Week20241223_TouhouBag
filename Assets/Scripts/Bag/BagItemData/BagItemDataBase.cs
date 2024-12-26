using System.Numerics;
using UnityEngine;

public enum BagItemName
{
    StageSlot, // スロットは操作不能のアイテムとして設定する
    Bag3x1,
    Bag2x2,
    Bag2x1,
    Bag1x1,
    Apple,
    LongItem,
    BigApple,
}

public enum BagCellName
{
    CellStageSlot,
    CellBag,
    CellItem,
}

/// <summary>
/// アイテムの種別（設置順番管理用）
/// </summary>
public enum BagItemType
{
    StageSlot, // 初期から設置されている地盤
    Bag, // アイテム配置用のバッグ
    Item, // 実際に効果を発動するバッグ
}

public interface BagItemDataBase
{
    // アイテムの種別
    public BagItemType ItemType { get { return GetType(); } }
    // アイテムの名前
    public BagItemName ItemName { get { return GetName(); } }
    // アイテムのPrefabパス（現状はResource内想定）
    public string PrefabPath { get { return GetPrefabPath(); } }
    // アイテムの金額
    public int Cost { get { return GetCost(); } }
    // クールダウン時間
    public float Cooldown { get { return GetCooldown(); } }
    // アイテムの総セル数
    public Vector2Int Size { get { return GetSize(); } }
    // アイテムの総セル数
    public int CellCount { get { return Size.x * Size.y; } }
    // アイテムの画像パス（バッグ編集画面）
    public string SpritePathBagEdit { get { return GetSpritePathBagEdit(); } }
    // アイテムの画像パス（バトル画面）
    public string SpritePathBattle { get { return GetSpritePathBattle(); } }

    /// <summary>
    /// アイテムに含まれるセル数
    /// 形状がシンプルな四角形でない場合、この処理を派生クラスで都度記述すること
    /// </summary>
    /// <returns></returns>
    protected int GetCellCount() { return Size.x * Size.y; }

    /// <summary>
    /// アイテムの名前を取得
    /// </summary>
    /// <returns></returns>
    protected BagItemName GetName();

    /// <summary>
    /// アイテムの種別を取得
    /// </summary>
    /// <returns></returns>
    protected BagItemType GetType();

    /// <summary>
    /// アイテムのResourcesPrefabパスを取得
    /// </summary>
    /// <returns></returns>
    protected string GetPrefabPath();

    /// <summary>
    /// アイテムの金額を取得
    /// </summary>
    /// <returns></returns>
    protected int GetCost();

    /// <summary>
    /// クールダウンを取得
    /// </summary>
    /// <returns></returns>
    protected float GetCooldown();

    /// <summary>
    /// アイテムのサイズを取得
    /// 四角形でない場合でも縦横それぞれの最大値を入れておく
    /// </summary>
    /// <returns></returns>
    protected Vector2Int GetSize();

    /// <summary>
    /// アイテムの画像パス
    /// バッグ編集画面用
    /// </summary>
    /// <returns></returns>
    protected string GetSpritePathBagEdit();

    /// <summary>
    /// アイテムの画像パス
    /// バトル画面用
    /// </summary>
    /// <returns></returns>
    protected string GetSpritePathBattle();
}