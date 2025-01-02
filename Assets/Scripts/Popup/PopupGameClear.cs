using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class PopupGameClear : PopupBase
{
    [SerializeField]
    GameObject bg;
    [SerializeField]
    TextMeshProUGUI titleText, descriptionText;
    [SerializeField]
    CanvasGroup toTitleButton;

    protected override void ShowPopup()
    {
        base.ShowPopup();

        // キャンバスの手前に花火を置けるように設定する
        Canvas canvas = RootCanvas.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = Consts.SortingLayer.UIOverlay;

        titleText.alpha = 0.0f;
        titleText.transform.localScale = Vector2.zero;
        descriptionText.alpha = 0.0f;
        descriptionText.transform.localScale = Vector2.zero;
        toTitleButton.alpha = 0.0f;
        toTitleButton.transform.localScale = Vector2.zero;

        bg.transform.localScale = new Vector2(1.0f, 0.0f);
        bg.transform.DOScaleY(0.6f, 0.5f).OnComplete(() => {
            StartCoroutine(CoShowFireFlower(0.0f));
        }).SetUpdate(true);

        float duration = 2.0f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(titleText.transform.DOLocalRotate(new Vector2(0.0f, 360f * 3f), duration, RotateMode.FastBeyond360)).SetEase(Ease.OutQuad);
        sequence.Join(titleText.transform.DOScale(1.0f, 1.0f));
        sequence.Join(titleText.DOFade(1.0f, 1.0f));
        sequence.Join(descriptionText.transform.DOLocalRotate(new Vector2(0.0f, 360f * 3f), duration, RotateMode.FastBeyond360)).SetEase(Ease.OutQuad);
        sequence.Join(descriptionText.transform.DOScale(1.0f, 1.0f));
        sequence.Join(descriptionText.DOFade(1.0f, 1.0f));
        sequence.Join(descriptionText.transform.DOScale(1.0f, duration));
        sequence.SetDelay(0.5f);
        sequence.SetUpdate(true);
        sequence.OnComplete(() => {

            toTitleButton.gameObject.SetActive(true);
            Sequence seq2 = DOTween.Sequence();
            seq2.Append(toTitleButton.transform.DOScale(1.0f, duration / 2f));
            seq2.Join(toTitleButton.DOFade(1.0f, duration / 2f));
            seq2.SetUpdate(true);
        });

        ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipGameClear);
    }

    private IEnumerator CoShowFireFlower(float delay)
    {
        ShowFireFlower();
        yield return new WaitForSecondsRealtime(delay);

        bool isInterval = RandUtil.GetRandomBool(0.1f);
        float nextDelay = isInterval ? Random.Range(1f, 5f) : Random.Range(0.1f, 1.0f);
        StartCoroutine(CoShowFireFlower(nextDelay));
    }

    private void ShowFireFlower()
    {
        float x = Random.Range(-5.0f, 5f);
        float startY = Random.Range(-5.0f, -3f);
        float endY = Random.Range(-2.0f, 3f);
        float scale = Random.Range(0.2f, 1.0f);

        FireFlower.Launch(new Vector2(x, startY), new Vector2(x, endY), scale, (Transform fireflower) => { Destroy(fireflower.gameObject); });
    }

    public void OnClickToTitleButton()
    {
        // ランキング送信
        // 最終ステータスの合算（多い方がつよい）
        int totalStatus = ManagerGame.Instance.GetStatusTotal();
        UnityroomApiClient.Instance.SendScore(1, totalStatus, ScoreboardWriteMode.HighScoreDesc);

        // ゲームクリア時のステータスの合算（小さい方がつよい）
        UnityroomApiClient.Instance.SendScore(2, totalStatus, ScoreboardWriteMode.HighScoreAsc);
        
        // // 進捗データを消す
        // ManagerGame.Instance.ResetAllData();

        ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipButtonClickOK);
        ManagerSceneTransition.Instance.Move2Scene(SceneType.Title);
    }
}
