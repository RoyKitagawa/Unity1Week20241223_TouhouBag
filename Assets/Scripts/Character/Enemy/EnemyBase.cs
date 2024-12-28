using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    // キャラクターのステータス関連
    protected bool isAttackMode = false; // 攻撃モードか（自機付近まで到達済みか）    
    protected float elapsedCooldownTime = 0.0f; // 攻撃後の経過時間

    public void Update()
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
        CharacterBase player = ManagerBattlePhase.Instance.GetPlayer();
        player.GainDamage(isCritical ? damage * 1.5f : damage, isCritical ? DamageType.CriticalDamage : DamageType.NormalDamage);
        // ヒットパーティクル
        ManagerParticle.Instance.ShowOnDamageParticle(player.transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
        // ヒット時の揺れ
        player.ShakeOnDamage();
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag != Consts.Tags.BattlePlayerHome) return;
        // 自機付近の範囲に触れた場合、移動終了からの攻撃開始に移行する
        rb.linearVelocity = Vector2.zero;
        isAttackMode = true;
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
    public override void GainDamage(float damageAmt, DamageType damageType)
    {
        // ダメージ演出
        base.GainDamage(damageAmt, damageType);
    }


    /// <summary>
    /// キャラクター死亡時に呼び出される処理
    /// </summary>
    protected override void OnDead()
    {
        // TODO 死亡処理
        rb.linearVelocity = Vector2.zero;
        ManagerEnemy.Instance.RemoveEnemyFromList(this);
        ManagerParticle.Instance.ShowOnDeadParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);

        GetImage().DOFade(0.0f, 0.1f).OnComplete(() => {
            Destroy(gameObject);
        });
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
        if(futurePos.x < ManagerBattlePhase.Instance.GetPlayerAttackableBoundaryX())
            futurePos = new Vector2(ManagerBattlePhase.Instance.GetPlayerAttackableBoundaryX(), futurePos.y);

        return futurePos;
    }
}
