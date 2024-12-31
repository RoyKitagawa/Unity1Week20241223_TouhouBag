using DG.Tweening;
using UnityEngine;

public enum PopupType
{
    Tutorial,
    Settings,
    GameOver,
    GameClear
}

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject RootCanvas;
    protected float duration = 0.2f;

    public static bool Show(PopupType type)
    {
        // 既に同名のオブジェクトが存在する場合は何もしない
        if(GameObject.Find(GetPopupResourceName(type) + "(Clone)") != null)
        {
            return false;
        }

        // Time.timeScale = 0.0f;
        GameObject obj = BasicUtil.LoadGameObject4Resources(GetPopupResourcesPath(type));
        PopupBase popup = Instantiate(obj).GetComponentInChildren<PopupBase>();
        popup.ShowPopup();
        return true;
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
            OnHidden();
        });
        sequence.SetUpdate(true);
    }

    protected virtual void OnHidden() {}

    protected static string GetPopupResourcesPath(PopupType type)
    {
        return "Prefabs/Popup/" + GetPopupResourceName(type);
    }

    protected static string GetPopupResourceName(PopupType type)
    {
        switch(type)
        {
            case PopupType.Tutorial:
                return "TutorialPopupWindowCanvas";
            case PopupType.Settings:
                return "SettingsPopupWindowCanvas";
            case PopupType.GameOver:
                return "GameOverPopupWindowCanvas";
            case PopupType.GameClear:
                return "GameClearPopupWindowCanvas";
            default:
                Debug.LogError("不正なポップアップタイプ: " + type);
                return "";
        }
    }
}