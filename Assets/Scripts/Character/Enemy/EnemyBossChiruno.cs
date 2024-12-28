using DG.Tweening;
using UnityEngine;

public class EnemyBossChiruno : EnemyBase
{
    Sequence moveImageSequence;
    public void Start()
    {
        moveImageSequence = DOTween.Sequence();
        moveImageSequence.Append(GetImage().transform.DORotate(new Vector3(0.0f, 0.0f, 10.0f), 0.0f))
            .AppendInterval(1f)
            .Append(GetImage().transform.DORotate(Vector3.zero, 0.0f))
            .AppendInterval(1f)
            .SetLoops(-1)
            .SetAutoKill(true);
    }

    public void OnDestroy()
    {
        if(moveImageSequence != null && moveImageSequence.IsActive())
        {
            moveImageSequence.Kill();
        }
    }
}
