using DG.Tweening;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer srPrice;

    public void PlaceItem(BagItem item)
    {
        item.SetShopSlot(this);
        item.transform.position = transform.position;
        item.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 30.0f));
        srPrice.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.Prices.Price(item.GetData().Cost));
        srPrice.DOFade(1.0f, 0.2f);
    }

    public void OnPurchase()
    {
        srPrice.DOFade(0.0f, 0.2f);
    }
}
