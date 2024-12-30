using DG.Tweening;
using UnityEngine;

public class PopupTutorial : PopupBase
{
    [SerializeField]
    GameObject TutorialButtonNext, TutorialButtonPrev;
    [SerializeField]
    GameObject TutorialP1, TutorialP2;

    private int currentTutorialPage = 0;

    protected override void ShowPopup()
    {
        base.ShowPopup();
        ShowTutorialPage(0);
    }

    private void ShowTutorialPage(int page)
    {
        currentTutorialPage = page;
        // ページ1の内容を表示する
        if(page == 0)
        {
            TutorialButtonPrev.SetActive(false);
            TutorialButtonNext.SetActive(true);
            TutorialP1.SetActive(true);
            TutorialP2.SetActive(false);
        }
        else if(page == 1)
        {
            TutorialButtonPrev.SetActive(true);
            TutorialButtonNext.SetActive(true);
            TutorialP1.SetActive(false);
            TutorialP2.SetActive(true);
        }
        else
        {
            HidePopup();
        }
    }

    public void OnClickTutorialNext()
    {
        ShowTutorialPage(currentTutorialPage + 1);
    }

    public void OnClickTutorialPrev()
    {
        ShowTutorialPage(currentTutorialPage -1);
    }
}
