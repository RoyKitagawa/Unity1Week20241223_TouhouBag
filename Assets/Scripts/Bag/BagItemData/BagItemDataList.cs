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
    public static BagItemDataBase GetItemData(BagItemName itemName, BagItemLevel lv)
    {
        switch(itemName)
        {
            case BagItemName.Cucumber:
                if(lv == BagItemLevel.Lv1) return new BagItemDataCucumberLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataCucumberLv2();
                else return new BagItemDataCucumberLv3();

            case BagItemName.Screw:
                if(lv == BagItemLevel.Lv1) return new BagItemDataScrewLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataScrewLv2();
                else return new BagItemDataScrewLv3();

            case BagItemName.Spanner:
                if(lv == BagItemLevel.Lv1) return new BagItemDataSpannerLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataSpannerLv2();
                else return new BagItemDataSpannerLv3();

            case BagItemName.Bag2x2:
                return new BagItemDataBag2x2();

            case BagItemName.Bag1x1:
                return new BagItemDataBag1x1();

            case BagItemName.Bag2x1:
                return new BagItemDataBag2x1();

            case BagItemName.Bag3x1:
                return new BagItemDataBag3x1();

            case BagItemName.StageSlot:
                return new BagItemDataStageSlot();

            default:
                Debug.LogError("アイテムデータ取得失敗。不正なタイプ: " + itemName + ", レベル: " + lv);
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
                names = new HashSet<BagItemName> { BagItemName.Cucumber, BagItemName.Screw, BagItemName.Spanner };
                break;

            case BagItemType.Bag:
                // names = new HashSet<BagItemName> { BagItemName.Bag2x2, BagItemName.Bag2x1, BagItemName.Bag1x1, BagItemName.Bag3x1 };
                names = new HashSet<BagItemName> { BagItemName.Bag2x2 };
                break;

            default:
                Debug.LogError("不正なBagItemType: " + type);
                names = new HashSet<BagItemName>();
                break;
        }
        return names;
    }
}