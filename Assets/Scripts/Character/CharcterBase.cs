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
    protected float currentArmor = 0.0f;
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
    public virtual void GainDamage(BagItemName weaponName, float damageAmt, DamageType damageType, bool isCritical)
    {
        // ライフ更新
        ApplyDamage2Life(damageAmt, damageType);
        
        // ダメージ表記演出を行う
        BattleDamage.ShowDamageEffect(
            weaponName,
            (int)damageAmt,
            damageType,
            isCritical,
            new Vector2(transform.position.x, transform.position.y + boxCollider2D.bounds.size.y / 2f));
        
        // 死亡判定
        if(IsDead())
        {
            OnDead();
        }
    }

    private void ApplyDamage2Life(float damageAmt, DamageType damageType)
    {
        // ヒールは体力回復
        if(damageType == DamageType.Heal)
        {
            currentLife += damageAmt;
        }
        // シールドはアーマー回復
        else if(damageType == DamageType.Shield)
        {
            currentArmor += damageAmt;
        }
        // 通常時はシールドを優先してダメージを負う
        else
        {
            // アーマーだけで受けきれる場合
            if(currentArmor > damageAmt)
            {
                currentArmor -= damageAmt;
            }
            // 肉に貫通する場合
            else
            {
                damageAmt -= currentArmor;
                currentArmor = 0.0f;
                currentLife -= damageAmt;
            }
        }

        // 余剰対策
        if(currentArmor < 0) currentArmor = 0;
        else if(currentArmor > data.MaxLife) currentArmor = data.MaxLife;
        if(currentLife < 0) currentLife = 0;
        else if(currentLife > data.MaxLife) currentLife = data.MaxLife;
    }

    public void OnWeaponHit(BagItemData data)
    {
        // 通常攻撃系以外クリティカルは発生しない
        bool isCritical = data.WeaponDamageType == DamageType.Damage ? RandUtil.GetRandomBool(0.1f) : false;
        GainDamage(data.ItemName, isCritical ? data.WeaponDamage * 1.5f : data.WeaponDamage, data.WeaponDamageType, isCritical);

        // 回復系
        if(data.WeaponDamageType == DamageType.Heal)
        {
            // 回復パーティクル
            ManagerParticle.Instance.ShowOnHealParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
        }
        // アーマー付与
        else if(data.WeaponDamageType == DamageType.Shield)
        {
            // 回復パーティクル
            ManagerParticle.Instance.ShowOnShieldParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
        }
        // 攻撃系
        else
        {
            // ヒットパーティクル
            ManagerParticle.Instance.ShowOnDamageParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
            // ヒット時の揺れ
            ShakeOnDamage();
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