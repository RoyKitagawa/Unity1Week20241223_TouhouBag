using DG.Tweening;
using UnityEngine;

public enum PopupType
{
    Tutorial,
    Settings,
    GameOver,
}

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject RootCanvas;
    protected float duration = 0.2f;

    public static void Show(PopupType type)
    {
        // Time.timeScale = 0.0f;
        GameObject obj = BasicUtil.LoadGameObject4Resources(GetPopupResourcesPath(type));
        PopupBase popup = Instantiate(obj).GetComponentInChildren<PopupBase>();
        popup.ShowPopup();
    }

    protected virtual void ShowPopup()
    {
        RootCanvas.SetActive(true);
        transform.localScale = Vector2.zero;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.0f, duration));
        sequence.Join(canvasGroup.DOFade(1.0f, duration));
        sequence.OnComplete(() => { OnShown(); });
        sequence.SetUpdate(true);
    }

    protected virtual void OnShown() {}

    protected virtual void HidePopup()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0.0f, duration));
        sequence.Join(canvasGroup.DOFade(0.0f, duration));
        sequence.OnComplete(() => {
            Time.timeScale = 1.0f;
            Destroy(RootCanvas.gameObject);
        });
        sequence.OnComplete(() => { OnHidden(); });
        sequence.SetUpdate(true);
    }

    protected virtual void OnHidden() {}

    protected static string GetPopupResourcesPath(PopupType type)
    {
        switch(type)
        {
            case PopupType.Tutorial:
                return "Prefabs/Popup/TutorialPopupWindowCanvas";
            case PopupType.Settings:
                return "Prefabs/Popup/SettingsPopupWindowCanvas";
            case PopupType.GameOver:
                return "Prefabs/Popup/GameOverPopupWindowCanvas";
            default:
                Debug.LogError("不正なポップアップタイプ: " + type);
                return "";
        }
    }
}