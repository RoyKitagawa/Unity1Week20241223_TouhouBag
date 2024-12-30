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
                else if(lv == BagItemLevel.Lv3) return new BagItemDataCucumberLv3();
                else return new BagItemDataCucumberLv4();

            case BagItemName.Screw:
                if(lv == BagItemLevel.Lv1) return new BagItemDataScrewLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataScrewLv2();
                else if(lv == BagItemLevel.Lv3) return new BagItemDataScrewLv3();
                else return new BagItemDataScrewLv4();

            case BagItemName.Spanner:
                if(lv == BagItemLevel.Lv1) return new BagItemDataSpannerLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataSpannerLv2();
                else if(lv == BagItemLevel.Lv3) return new BagItemDataSpannerLv3();
                else return new BagItemDataSpannerLv4();

            case BagItemName.Bomb:
                if(lv == BagItemLevel.Lv1) return new BagItemDataBombLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataBombLv2();
                else if(lv == BagItemLevel.Lv3) return new BagItemDataBombLv3();
                else return new BagItemDataBombLv4();

            case BagItemName.Driver:
                if(lv == BagItemLevel.Lv1) return new BagItemDataDriverLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataDriverLv2();
                else if(lv == BagItemLevel.Lv3) return new BagItemDataDriverLv3();
                else return new BagItemDataDriverLv4();

            case BagItemName.Canon:
                if(lv == BagItemLevel.Lv1) return new BagItemDataCanonLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataCanonLv2();
                else if(lv == BagItemLevel.Lv3) return new BagItemDataCanonLv3();
                else return new BagItemDataCanonLv4();

            case BagItemName.Glove:
                if(lv == BagItemLevel.Lv1) return new BagItemDataGloveLv1();
                else if(lv == BagItemLevel.Lv2) return new BagItemDataGloveLv2();
                else if(lv == BagItemLevel.Lv3) return new BagItemDataGloveLv3();
                else return new BagItemDataGloveLv4();

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
                names = new HashSet<BagItemName> {
                    BagItemName.Cucumber, BagItemName.Screw, BagItemName.Spanner, BagItemName.Bomb,
                    BagItemName.Driver, BagItemName.Canon, BagItemName.Glove };
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