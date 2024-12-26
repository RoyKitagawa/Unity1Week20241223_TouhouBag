using UnityEngine;

/// <summary>
/// キャラクター基底クラス
/// </summary>
public class CharacterBase : MonoBehaviour
{
    protected CharacterDataBase data; // 敵機データ
    protected Rigidbody2D rb; // 移動用コンポーネント
    protected BoxCollider2D boxCollider2D; // サイズ判定用
    private bool isInitialized = false; // 初期化済みか
    // 現状のライフ
    protected float currentLife = 1.0f;

    /// <summary>
    /// キャラクターの初期化処理
    /// </summary>
    public void InitializeCharacter(CharacterDataBase characterData)
    {
        if(isInitialized) return;

        // データ設定
        data = characterData;
        // 移動速度設定
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = data.Velocity;
        // サイズ判定用
        boxCollider2D = GetComponent<BoxCollider2D>();
        // ライフ
        currentLife = data.MaxLife;

        isInitialized = true;
    }

    /// <summary>
    /// キャラクター死亡時に呼び出される処理
    /// </summary>
    public virtual void OnDead()
    {
        // TODO 死亡処理
        Debug.Log("キャラクター死亡！");
    }

    /// <summary>
    /// キャラクターが死亡しているか
    /// </summary>
    /// <returns></returns>
    public virtual bool IsDead()
    {
        return currentLife <= 0.0f;
    }

    /// <summary>
    /// ダメージ付与
    /// 回復の場合はマイナスを与える
    /// </summary>
    /// <param name="damageAmt"></param>
    /// <param name="damageType"></param>
    public virtual void GainDamage(float damageAmt, DamageType damageType)
    {
        currentLife -= damageAmt;
        if(currentLife < 0) currentLife = 0;
        else if(currentLife > data.MaxLife) currentLife = data.MaxLife;
        
        // ダメージ表記演出を行う
        BattleDamage.ShowDamageEffect(
            (int)damageAmt,
            damageType,
            new Vector2(transform.position.x, transform.position.y + boxCollider2D.bounds.size.y / 2f));
    }
}