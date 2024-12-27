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

public enum ColliderShape
{
    Square1x1,
    Square2x2,
    Rectangle2x1,
    Rectangle3x1,
    Rectangle4x1,
    Rectangle1x2,
    Rectangle1x3,
    Rectangle1x4,
    TShape,
    TriangleVToR, // 縦2マス、右に追加1マス
    TriangleVToL, // 縦2マス、左に追加1マス
}

/// <summary>
/// ダメージ種別
/// </summary>
public enum DamageType
{
    None,
    NormalDamage,
    CriticalDamage,
    Heal,
}

public enum TargetType
{
    None,
    Self,
    Random,
    Nearest,
    Farthest,
    HighestLife,
    LowestLife,
}

public interface BagItemDataBase
{
    // 基本情報系（レベルによって変動しないもの）
    // アイテムの種別
    public BagItemType ItemType { get { return GetType(); } }
    // アイテムの名前
    public BagItemName ItemName { get { return GetName(); } }
    // アイテムのタグ
    public string Tag { get { return GetTag(); } }
    // アイテムのPrefabパス（現状はResource内想定）
    public string BagPrefabPath { get { return GetBagPrefabPath(); } }
    // バトル用のPrefabパス（現状はResource内想定）
    public string BattlePrefabPath { get { return GetBattlePrefabPath(); } }
    // アイテムの画像パス（バッグ編集画面）
    public string SpritePathBagEdit { get { return GetSpritePathBagEdit(); } }
    // アイテムの画像パス（バトル画面）
    public string SpritePathBattle { get { return GetSpritePathBattleItemList(); } }
    // コライダーの形状
    public ColliderShape Shape { get { return GetColliderShape(); } } 
    // アイテムの総セル数
    public Vector2Int Size { get { return GetSize(); } }
    // アイテムの総セル数
    public int CellCount { get { return GetCellCount(); } }

    // レベル
    public int Level { get { return GetLevel(); } }

    // バッグ編集画面用
    // アイテムの金額
    public int Cost { get { return GetCost(); } }

    // バトル画面用
    // 攻撃種別
    public DamageType WeaponDamageType { get { return GetDamageType(); } }
    // 攻撃力
    public int WeaponDamage { get { return GetDamage(); } }
    // クールダウン時間
    public float Cooldown { get { return GetCooldown(); } }
    // ターゲット選定方法
    public TargetType WeaponTargetType { get { return GetTargetType(); } }

    /// <summary>
    /// アイテムに含まれるセル数
    /// 形状がシンプルな四角形でない場合、この処理を派生クラスで都度記述すること
    /// </summary>
    /// <returns></returns>
    protected int GetCellCount()
    {
        switch(Shape)
        {
            case ColliderShape.Square1x1:
                return 1;
            case ColliderShape.Rectangle2x1:
            case ColliderShape.Rectangle1x2:
                return 2;
            case ColliderShape.Rectangle1x3:
            case ColliderShape.Rectangle3x1:
            case ColliderShape.TriangleVToR:
            case ColliderShape.TriangleVToL:
                return 3;
            case ColliderShape.Square2x2:
            case ColliderShape.Rectangle4x1:
            case ColliderShape.Rectangle1x4:
                return 4;
            case ColliderShape.TShape:
                return 5;
            default:
                Debug.LogError("不正のColliderShape: " + Shape);
                return 1;
        }
    }

    /// <summary>
    /// コライダーの形状を取得
    /// </summary>
    /// <returns></returns>
    protected ColliderShape GetColliderShape();

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
    /// アイテムオブジェクトのタグを取得
    /// </summary>
    /// <returns></returns>
    protected string GetTag();

    /// <summary>
    /// アイテムのレベルを取得
    /// </summary>
    /// <returns></returns>
    protected int GetLevel();

    /// <summary>
    /// バッグ編集時用アイテムのResourcesPrefabパスを取得
    /// </summary>
    /// <returns></returns>
    protected string GetBagPrefabPath();

    /// <summary>
    /// バトル時用アイテムのResourcesPrefabパスを取得
    /// </summary>
    /// <returns></returns>
    protected string GetBattlePrefabPath();

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
    protected Vector2Int GetSize()
    {
        switch(Shape)
        {
            case ColliderShape.Square1x1:
                return Vector2Int.one;
            case ColliderShape.Square2x2:
                return new Vector2Int(2, 2);
            case ColliderShape.Rectangle2x1:
                return new Vector2Int(2, 1);
            case ColliderShape.Rectangle3x1:
                return new Vector2Int(3, 1);
            case ColliderShape.Rectangle4x1:
                return new Vector2Int(4, 1);
            case ColliderShape.Rectangle1x2:
                return new Vector2Int(1, 2);
            case ColliderShape.Rectangle1x3:
                return new Vector2Int(1, 3);
            case ColliderShape.Rectangle1x4:
                return new Vector2Int(1, 4);
            case ColliderShape.TShape:
                return new Vector2Int(3, 3);
            case ColliderShape.TriangleVToR:
            case ColliderShape.TriangleVToL:
                return new Vector2Int(2, 2);
            default:
                Debug.LogError("不正のShape: " + Shape);
                return Vector2Int.one;
        }
    }

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
    protected string GetSpritePathBattleItemList();

    /// <summary>
    /// アイテムのダメージ種別
    /// </summary>
    /// <returns></returns>
    protected DamageType GetDamageType();

    /// <summary>
    /// アイテムの攻撃力
    /// </summary>
    /// <returns></returns>
    protected int GetDamage();

    /// <summary>
    /// 攻撃対象の選定方法
    /// </summary>
    /// <returns></returns>
    protected TargetType GetTargetType();
}