using DG.Tweening;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    public Vector3 offset;
    [SerializeField]
    private HPBar hpBar;
    private RectTransform hpBarRectTransform;
    private Sequence moveImageSequence;

    public void Start()
    {
        data = CharacterDataList.GetCharacterData(CharacterName.EnemyStrongA);
        moveImageSequence = DOTween.Sequence();
        moveImageSequence.Append(GetImage().transform.DORotate(new Vector3(0.0f, 0.0f, 10.0f), 0.0f))
            .AppendInterval(1f)
            .Append(GetImage().transform.DORotate(Vector3.zero, 0.0f))
            .AppendInterval(1f)
            .SetLoops(-1)
            .SetAutoKill(true);

        // HPBar対応
        hpBar.Initialize(data.MaxLife, data.MaxLife);
        hpBarRectTransform = hpBar.GetComponent<RectTransform>();
    }

    public override void Update()
    {
        base.Update();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBarRectTransform.position = screenPos;
    }

    /// <summary>
    /// ダメージ付与
    /// </summary>
    /// <param name="damageAmt"></param>
    /// <param name="damageType"></param>
    public override void GainDamage(BagItemName weaponName, float damageAmt, DamageType damageType, bool isCritical)
    {
        // ダメージ演出
        base.GainDamage(weaponName, damageAmt, damageType, isCritical);
        hpBar.SetCurrentValue(currentLife);
    }

    public void OnDestroy()
    {
        if(moveImageSequence != null && moveImageSequence.IsActive())
        {
            moveImageSequence.Kill();
        }
    }
}
