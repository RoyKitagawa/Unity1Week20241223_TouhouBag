using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    // キャラクターのステータス関連
    protected bool isAttackMode = false; // 攻撃モードか（自機付近まで到達済みか）    
    protected float elapsedCooldownTime = 0.0f; // 攻撃後の経過時間

    public virtual void Update()
    {
        if(IsDead()) return;
        if(isAttackMode)
        {
            elapsedCooldownTime += Time.deltaTime;
            if(elapsedCooldownTime >= data.Cooldown)
            {
                // 攻撃をトリガーする
                TriggerAttack();

                // クールダウンの超過時間をリセット
                elapsedCooldownTime = 0.0f;
            }
        }
    }

    /// <summary>
    /// 攻撃をトリガーする
    /// </summary>
    protected virtual void TriggerAttack()
    {
        // アタック処理を発生させる
        bool isCritical = RandUtil.GetRandomBool(0.1f);
        float damage = Random.Range(data.AttackDamage * 0.8f, data.AttackDamage * 1.2f);
        CharacterBase player = ManagerBattleMode.Instance.GetPlayer();
        player.GainDamage(BagItemName.None, isCritical ? damage * 1.5f : damage, DamageType.Damage, isCritical);
        // ヒットパーティクル
        ManagerParticle.Instance.ShowOnDamageParticle(player.transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
        // ヒット時の揺れ
        player.ShakeOnDamage();

        // 自分を揺らす
        transform.DOShakePosition(0.5f, 0.3f, 20);
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == Consts.Tags.Bullet)
        {
            Bullet bullet = target.GetComponent<Bullet>();
            OnWeaponHit(bullet.data);
        }
        if(target.tag == Consts.Tags.BattlePlayerHome)
        {
            // 自機付近の範囲に触れた場合、移動終了からの攻撃開始に移行する
            rb.linearVelocity = Vector2.zero;
            isAttackMode = true;
        }
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag != Consts.Tags.BattlePlayerHome) return;
        // ノックバック等で自機範囲のエリアから離れた場合などを想定し、攻撃から移動へ逆戻りパターンも用意しておく
        rb.linearVelocity = data.Velocity;
        isAttackMode = false;
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
    }


    /// <summary>
    /// キャラクター死亡時に呼び出される処理
    /// </summary>
    protected override void OnDead()
    {
        // TODO 死亡処理
        rb.linearVelocity = Vector2.zero;
        ManagerParticle.Instance.ShowOnDeadParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
        ManagerBattleMode.Instance.OnEnemyDead(this);

        GetImage().DOFade(0.0f, 0.1f).OnComplete(() => {
            Destroy(gameObject);
        }).SetUpdate(true);
    }

    /// <summary>
    /// 一定時間後に想定されるキャラクター座標を取得
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public override Vector2 GetFuturePosition(float time)
    {
        Vector2 futurePos = VectorUtil.Add(transform.position, rb.linearVelocity * time);
        // TODO 敵が自機に近づいて止まる場合を考慮する。まだ未実装なため一旦はずっと左に行ってもらう
        if(futurePos.x < ManagerBattleMode.Instance.GetPlayerAttackableBoundaryX())
            futurePos = new Vector2(ManagerBattleMode.Instance.GetPlayerAttackableBoundaryX(), futurePos.y);

        return futurePos;
    }
}
