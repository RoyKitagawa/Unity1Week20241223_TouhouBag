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
    // 基本情報系
    // アイテムの種別
    public BagItemType ItemType { get { return GetType(); } }
    // アイテムの名前
    public BagItemName ItemName { get { return GetName(); } }
    // アイテムのPrefabパス（現状はResource内想定）
    public string BagPrefabPath { get { return GetBagPrefabPath(); } }
    // バトル用のPrefabパス（現状はResource内想定）
    public string BattlePrefabPath { get { return GetBattlePrefabPath(); } }
    // アイテムの画像パス（バッグ編集画面）
    public string SpritePathBagEdit { get { return GetSpritePathBagEdit(); } }
    // アイテムの画像パス（バトル画面）
    public string SpritePathBattle { get { return GetSpritePathBattleItemList(); } }

    // バッグ編集画面用
    // アイテムの金額
    public int Cost { get { return GetCost(); } }
    // アイテムの総セル数
    public Vector2Int Size { get { return GetSize(); } }
    // アイテムの総セル数
    public int CellCount { get { return Size.x * Size.y; } }

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