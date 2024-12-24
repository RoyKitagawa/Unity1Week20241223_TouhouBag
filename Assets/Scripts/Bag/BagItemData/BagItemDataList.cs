using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム一覧管理専用クラス
/// 今後アイテムが増えることを想定してクラスを分けておく
/// </summary>
public class BagItemDataList
{
    /// <summary>
    /// アイテムデータを取得する
    /// アイテム種別を増やす際はこちらに必ず記述すること
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public static BagItemDataBase GetItemData(BagItemName itemName)
    {
        switch(itemName)
        {
            case BagItemName.Apple:
                return BagItemDataApple.Instance;

            case BagItemName.BigApple:
                return BagItemDataBigApple.Instance;

            case BagItemName.LongItem:
                return BagItemDataLongItem.Instance;

            case BagItemName.Bag2x2:
                return BagItemDataBag2x2.Instance;

            case BagItemName.Bag1x1:
                return BagItemDataBag1x1.Instance;

            case BagItemName.Bag2x1:
                return BagItemDataBag2x1.Instance;

            case BagItemName.Bag3x1:
                return BagItemDataBag3x1.Instance;

            case BagItemName.StageSlot:
                return BagItemDataStageSlot.Instance;

            default:
                Debug.LogError("アイテムデータ取得失敗。不正なタイプ: " + itemName);
                return null;
        }
    }

    /// <summary>
    /// タイプ別のアイテム名一覧を取得する
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static HashSet<BagItemName> GetItemNames(BagItemType type)
    {
        HashSet<BagItemName> names;
        switch(type)
        {
            case BagItemType.StageSlot:
                names = new HashSet<BagItemName> { BagItemName.StageSlot };
                break;

            case BagItemType.Item:
                names = new HashSet<BagItemName> { BagItemName.Apple, BagItemName.LongItem, BagItemName.BigApple };
                break;

            case BagItemType.Bag:
                names = new HashSet<BagItemName> { BagItemName.Bag2x2, BagItemName.Bag2x1, BagItemName.Bag1x1, BagItemName.Bag3x1 };
                // names = new HashSet<BagItemName> { BagItemName.Bag2x2 };
                break;

            default:
                Debug.LogError("不正なBagItemType: " + type);
                names = new HashSet<BagItemName>();
                break;
        }
        return names;
    }
}