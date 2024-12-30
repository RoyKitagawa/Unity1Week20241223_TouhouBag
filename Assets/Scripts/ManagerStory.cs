using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerStory : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Text;
    private int pageIndex = 0;
    private int characterIndex = 0;
    private bool isFastForward = false;
    private Coroutine TextShowCoroutine = null;

    HashSet<char> stopper = new HashSet<char> { '!', '?', '.', ',', '！', '？', '、', '。'};

    private string[] story = new string[]
    {
        "その日にとりは…、思い出した………。",
        "今まで開発していた機械が、すべて無くなっていたことに………。",
        "めーっちゃ長文描いてみる。テストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテストテスト",
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Text.text = "";
       ShowPage(pageIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(IsPageShowComplete())
            {
                if(IsNextPageExist()) ShowNextPage();
                else ManagerSceneTransition.Instance.Move2Scene(SceneType.InGameBagEdit);
            }
            else
            {
                isFastForward = true;
                if(TextShowCoroutine != null) StopCoroutine(TextShowCoroutine);
                // 現在のページを改めて再開（早送り状態で）
                ShowPage(pageIndex);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isFastForward = false;
        }
    }

    private bool IsNextPageExist()
    {
        return IsPageExist(pageIndex + 1);
    }

    private bool IsPageExist(int index)
    {
        return story.Length > index;
    }

    private void ShowNextPage()
    {
        ShowPage(pageIndex + 1);
    }

    private void ShowPage(int index)
    {
        if(pageIndex != index) characterIndex = 0;
        pageIndex = index;
        TextShowCoroutine = StartCoroutine(CoShowNextText(0.1f));
    }

    private IEnumerator CoShowNextText(float delay)
    {
        if(!IsPageShowComplete())
        {
            Text.text = story[pageIndex].Substring(0, characterIndex);
        }

        // 早送り含めて時間を設定
        float cooldown = isFastForward ? delay / 20.0f : delay;
        // 句読点を見て、時間を遅らせるか計算
        if(characterIndex > 0)
            if(stopper.Contains(story[pageIndex][characterIndex - 1])) // 最後に表示した文字が句読点か
                if(story[pageIndex].Length > characterIndex) // 更に1文字追加で存在するか（最後の文字じゃないか）
                    if(!stopper.Contains(story[pageIndex][characterIndex])) // 次の文字が句読点じゃないか（「！？」等対策）
                    {
                        cooldown *= 5.0f;
                    }

        characterIndex ++;

        yield return new WaitForSeconds(cooldown);

        if(!IsPageShowComplete())
        {
            TextShowCoroutine = StartCoroutine(CoShowNextText(delay));
        }
    }

    private bool IsPageShowComplete()
    {
        return Text.text.Length >= story[pageIndex].Length;
    }
}
