using DG.Tweening;
using UnityEngine;

/// <summary>
/// キャラクター基底クラス
/// </summary>
public class CharacterBase : MonoBehaviour
{
    protected CharacterDataBase data; // 敵機データ
    protected Rigidbody2D rb; // 移動用コンポーネント
    protected BoxCollider2D boxCollider2D; // サイズ判定用
    protected SpriteRenderer sr;
    protected bool isInitialized = false; // 初期化済みか
    // 現状のライフ
    protected float currentLife = 1.0f;
    // ShakeSequence
    private Sequence shakeSequence = null;

    /// <summary>
    /// キャラクターの初期化処理
    /// </summary>
    public virtual void InitializeCharacter(CharacterDataBase characterData)
    {
        if(isInitialized) return;

        // データ設定
        data = characterData;
        // 移動速度設定
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = data.Velocity * Random.Range(0.9f, 1.1f);
        // サイズ判定用
        boxCollider2D = GetComponent<BoxCollider2D>();
        // ライフ
        currentLife = data.MaxLife;

        isInitialized = true;
    }

    /// <summary>
    /// キャラクター死亡時に呼び出される処理
    /// </summary>
    protected virtual void OnDead()
    {
        // TODO 死亡処理
        Debug.Log("キャラクター死亡！");
        Destroy(gameObject);

        rb.linearVelocity = Vector2.zero;
        ManagerParticle.Instance.ShowOnDeadParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
    }

    /// <summary>
    /// キャラクターが死亡しているか
    /// </summary>
    /// <returns></returns>
    protected bool IsDead()
    {
        return currentLife <= 0.0f;
    }

    /// <summary>
    /// ダメージ付与
    /// </summary>
    /// <param name="damageAmt"></param>
    /// <param name="damageType"></param>
    public virtual void GainDamage(float damageAmt, DamageType damageType)
    {
        // ライフ更新
        if(damageType == DamageType.Heal) currentLife += damageAmt;
        else if(damageType == DamageType.Shield) currentLife += damageAmt; // TODO シールド値とSliderを用意
        else currentLife -= damageAmt;
        // 余剰対策
        if(currentLife < 0) currentLife = 0;
        else if(currentLife > data.MaxLife) currentLife = data.MaxLife;
        
        // ダメージ表記演出を行う
        BattleDamage.ShowDamageEffect(
            (int)damageAmt,
            damageType,
            new Vector2(transform.position.x, transform.position.y + boxCollider2D.bounds.size.y / 2f));
        
        // 死亡判定
        if(IsDead())
        {
            OnDead();
        }
    }

    /// <summary>
    /// 現在のライフを取得する
    /// </summary>
    /// <returns></returns>
    public float GetCurrentLife()
    {
        return currentLife;
    }

    /// <summary>
    /// ダメージ時に揺らす
    /// </summary>
    public void ShakeOnDamage()
    {
        if(shakeSequence != null && !shakeSequence.IsComplete())
        {
            shakeSequence.Complete();
        }
        shakeSequence = DOTween.Sequence();
        shakeSequence.Append(GetImage().transform.DOLocalMoveX(data.Type == CharacterType.Player ? -0.05f : 0.05f, 0.05f).SetEase(Ease.OutQuad));
        shakeSequence.Append(GetImage().transform.DOLocalMoveX(0.0f, 0.05f).SetEase(Ease.InQuad));
    }

    protected SpriteRenderer GetImage()
    {
        if(sr == null) sr = GetComponentInChildren<SpriteRenderer>();
        return sr;
    }

    /// <summary>
    /// 一定時間後に想定されるキャラクター座標を取得
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public virtual Vector2 GetFuturePosition(float time)
    {
        Vector2 futurePos = VectorUtil.Add(transform.position, rb.linearVelocity * time);
        return futurePos;
    }
}