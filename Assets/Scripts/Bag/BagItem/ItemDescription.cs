using DG.Tweening;
using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    private static ItemDescription currentDescription = null;
    [SerializeField]
    RectTransform textRRT, textLRT;
    [SerializeField]
    TextMeshProUGUI textR, textL;
    [SerializeField]
    SpriteRenderer imageR, imageL;
    [SerializeField]
    Canvas canvas;

    Vector2 offsetTextR;
    Vector2 offsetTextL;

    float useRMaxX = 4.0f;
    float useLMinX = -4.0f;
    CanvasGroup canvasGroup;
    Vector2 targetPos;

    Vector3 offset = Vector3.zero;
    Sequence showHideSequence = null;

    public static ItemDescription Show(BagItemData data, Vector2 pos)
    {
        GameObject prefab = BasicUtil.LoadGameObject4Resources(Consts.Resources.Prefabs.ItemDescription);
        ItemDescription itemDescription = Instantiate(prefab).GetComponent<ItemDescription>();
        itemDescription.canvas.worldCamera = Camera.main;
        itemDescription.SetDescriptionText(data);
        itemDescription.SetDescriptionVisibility();
        itemDescription.canvasGroup = itemDescription.GetComponent<CanvasGroup>();
        itemDescription.canvasGroup.alpha = 0.0f;
        
        itemDescription.imageR.color = new Color(1f,1f,1f,0f);
        itemDescription.imageL.color = new Color(1f,1f,1f,0f);
        itemDescription.showHideSequence = DOTween.Sequence();
        itemDescription.showHideSequence.Append(itemDescription.canvasGroup.DOFade(1.0f, 1f))
            .Join(itemDescription.imageR.DOFade(1.0f, 1f))
            .Join(itemDescription.imageL.DOFade(1.0f, 1f));

        itemDescription.RecordOffset();
        itemDescription.transform.position = pos;
        // itemDescription.targetPos = pos;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(itemDescription.transform.position + itemDescription.offset);
        itemDescription.textRRT.localPosition = screenPos;
        itemDescription.textLRT.localPosition = screenPos;

        itemDescription.SetDescriptionVisibility();
        return itemDescription;
    }
    
    public void Hide()
    {
        if(showHideSequence != null && !showHideSequence.IsComplete()) showHideSequence.Kill();
        showHideSequence = DOTween.Sequence();
        showHideSequence.Append(canvasGroup.DOFade(0.0f, 1f))
            .Join(imageR.DOFade(0.0f, 1.0f))
            .Join(imageL.DOFade(0.0f, 1.0f))
            .OnComplete(() => { Destroy(gameObject); });
    }

    private void RecordOffset()
    {
        // Transformのワールド座標をスクリーン座標に変換
        Vector3 worldPosition = transform.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // RectTransformのワールド座標をスクリーン座標に変換
        Vector3 rectWorldPosition = textLRT.position;
        Vector3 rectScreenPosition = Camera.main.WorldToScreenPoint(rectWorldPosition);
        // 初期オフセットを計算
        offsetTextL = new Vector2(rectScreenPosition.x - screenPosition.x, rectScreenPosition.y - screenPosition.y);

        // RectTransformのワールド座標をスクリーン座標に変換
        rectWorldPosition = textRRT.position;
        rectScreenPosition = Camera.main.WorldToScreenPoint(rectWorldPosition);
        // 初期オフセットを計算
        offsetTextR = new Vector2(rectScreenPosition.x - screenPosition.x, rectScreenPosition.y - screenPosition.y);
    }

    public void Update()
    {
        ApplyPositionOffsets();
    }

    private void ApplyPositionOffsets()
    {
                // Transformのワールド座標を取得
        Vector3 worldPosition = transform.position;

        // ワールド座標をスクリーン座標に変換
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        // スクリーン座標にオフセットを加算
        Vector2 targetScreenPosition = new Vector2(screenPosition.x + offsetTextR.x, screenPosition.y + offsetTextR.y);
        // スクリーン座標をCanvasのローカル座標に変換
        RectTransform canvasRect = textRRT.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            targetScreenPosition,
            Camera.main,
            out Vector2 localPosition
        );
        // RectTransformのローカル座標を更新
        textRRT.localPosition = localPosition;

        // スクリーン座標にオフセットを加算
        targetScreenPosition = new Vector2(screenPosition.x + offsetTextL.x, screenPosition.y + offsetTextL.y);
        // スクリーン座標をCanvasのローカル座標に変換
        canvasRect = textLRT.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            targetScreenPosition,
            Camera.main,
            out localPosition
        );
        // RectTransformのローカル座標を更新
        textLRT.localPosition = localPosition;

    }

    private void SetDescriptionVisibility()
    {
        if(transform.position.x < useLMinX)
        {
            textLRT.gameObject.SetActive(false);
            imageL.gameObject.SetActive(false);
            textRRT.gameObject.SetActive(true);
            imageR.gameObject.SetActive(true);
        }
        else if(transform.position.x > useRMaxX)
        {
            textLRT.gameObject.SetActive(true);
            imageL.gameObject.SetActive(true);
            textRRT.gameObject.SetActive(false);
            imageR.gameObject.SetActive(false);
        }
        else
        {
            textLRT.gameObject.SetActive(false);
            imageL.gameObject.SetActive(false);
            textRRT.gameObject.SetActive(true);
            imageR.gameObject.SetActive(true);
        }
    }

    private void SetDescriptionText(BagItemData data)
    {
        string description = BagItemDataList.GetItemDescriptionName(data.GetItemName(), data.GetLevel());
        description += "\n";
        switch(data.GetDamageType())
        {
            case DamageType.Damage:
                description += "攻撃力: " + data.GetDamage() + "\n";
                description += "クールタイム: " + data.GetCooldown() + "\n";
                break;
            case DamageType.Heal:
                description += "HP回復力: " + data.GetDamage() + "\n";
                description += "クールタイム: " + data.GetCooldown() + "\n";
                break;
            case DamageType.Shield:
                description += "シールド回復力: " + data.GetDamage() + "\n";
                description += "クールタイム: " + data.GetCooldown() + "\n";
                break;
            case DamageType.None:
                break;
        }
        description += "特徴: " + BagItemDataList.GetItemDescriptionContent(data.GetItemName(), data.GetLevel());
        textL.text = description;
        textR.text = description;
    }
}
