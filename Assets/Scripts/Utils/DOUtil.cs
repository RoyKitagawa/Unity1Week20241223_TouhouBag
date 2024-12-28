using DG.Tweening;
using UnityEngine;

public static class DOUtil
{
    public static void ShakeH(Transform transform, int shakeCount, int perShakeDuration, float shakeMoveAmtMin, float shakeMoveAmtMax)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveX(Random.Range(0.15f, 0.3f), 0.05f).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveX(0.0f, 0.05f).SetEase(Ease.InQuad));
        sequence.Append(transform.DOLocalMoveX(Random.Range(-0.15f, -0.3f), 0.05f).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveX(0.0f, 0.05f).SetEase(Ease.InQuad));

        sequence.Append(transform.DOLocalMoveX(Random.Range(0.15f, 0.3f), 0.05f).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveX(0.0f, 0.05f).SetEase(Ease.InQuad));
        sequence.Append(transform.DOLocalMoveX(Random.Range(-0.15f, -0.3f), 0.05f).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveX(0.0f, 0.05f).SetEase(Ease.InOutQuad));
    }
}