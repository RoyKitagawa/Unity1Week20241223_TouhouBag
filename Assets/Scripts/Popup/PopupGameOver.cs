using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopupGameOver : PopupBase
{
    [SerializeField]
    private CanvasGroup confirmButtonCanvasGroup;
    [SerializeField]
    private TextMeshProUGUI gameOverText;

    private float titleFadeDuration = 3.0f;
    public float dropDistance = 50f; // 落下距離
    public float dropDuration = 1f; // フェードインの時間
    public float delayBetweenCharacters = 1f; // 一文字ごとの遅延

    private string fullText = "GAME OVER..."; // 全体のテキストを保存

    protected override void ShowPopup()
    {
        base.ShowPopup();
        // gameOverText.text = "";
    }

    protected override void OnShown()
    {
        // gameOverText.text = fullText;
        // gameOverText.ForceMeshUpdate();
        // StartCoroutine(AnimateText());
        gameOverText.alpha = 0.0f;
        gameOverText.DOFade(1.0f, titleFadeDuration).SetUpdate(true).OnComplete(() => {
            confirmButtonCanvasGroup.alpha = 0.0f;
            confirmButtonCanvasGroup.DOFade(1.0f, 1f).SetEase(Ease.InQuad).SetUpdate(true).SetDelay(0.5f);
        });
        gameOverText.text = "";
        StartCoroutine(ScrollText());
    }

    public void OnClickMove2Title()
    {
        ManagerSceneTransition.Instance.Move2Scene(SceneType.Title);
    }

    private IEnumerator ScrollText()
    {
        yield return new WaitForSecondsRealtime(titleFadeDuration / fullText.Length);
        gameOverText.text = fullText.Substring(0, gameOverText.text.Length + 1);
        if(gameOverText.text.Length < fullText.Length)
        {
            StartCoroutine(ScrollText());
        }
    }

    // private IEnumerator AnimateText()
    // {
    //     gameOverText.ForceMeshUpdate(); // メッシュ情報を更新
    //     TMP_TextInfo textInfo = gameOverText.textInfo;

    //     // 全文字を非表示にする
    //     gameOverText.maxVisibleCharacters = 0;

    //     for (int i = 0; i < textInfo.characterCount; i++)
    //     {
    //         TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
    //         if (!charInfo.isVisible) continue; // 非表示の文字はスキップ

    //         int vertexIndex = charInfo.vertexIndex;
    //         Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

    //         // 初期位置を計算（上にずらす）
    //         Vector3[] initialVertices = new Vector3[4];
    //         for (int j = 0; j < 4; j++)
    //         {
    //             initialVertices[j] = vertices[vertexIndex + j] + new Vector3(0, dropDistance, 0);
    //         }

    //         // 頂点を一時的に変更
    //         for (int j = 0; j < 4; j++)
    //         {
    //             vertices[vertexIndex + j] = initialVertices[j];
    //         }
    //         gameOverText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);

    //         // DOTweenで頂点を移動させる
    //         DOTween.To(() => initialVertices[0].y, y =>
    //         {
    //             for (int j = 0; j < 4; j++)
    //             {
    //                 vertices[vertexIndex + j] = new Vector3(vertices[vertexIndex + j].x, y, vertices[vertexIndex + j].z);
    //             }
    //             gameOverText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    //         }, charInfo.bottomLeft.y, dropDuration).SetEase(Ease.OutBounce).SetUpdate(true);

    //         // 一文字ずつ表示
    //         gameOverText.maxVisibleCharacters = i + 1;

    //         yield return new WaitForSecondsRealtime(delayBetweenCharacters);
    //     }
    // }
}
