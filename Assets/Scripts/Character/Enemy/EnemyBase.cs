using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    // キャラクターのステータス関連
    protected bool isAttackMode = false; // 攻撃モードか（自機付近まで到達済みか）    
    protected float elapsedCooldownTime = 0.0f; // 攻撃後の経過時間

    public void Update()
    {
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
        HashSet<DamageType> damageTypes = new HashSet<DamageType> { DamageType.NormalDamage, DamageType.CriticalDamage, DamageType.Heal };
        ManagerBattlePhase.Instance.ApplyDamage2Player(Random.Range(1, 150), RandUtil.GetRandomItem(damageTypes));
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
    /// キャラクター死亡時に呼び出される処理
    /// </summary>
    public override void OnDead()
    {
        // TODO 死亡処理
        Debug.Log("敵キャラクター死亡！");
    }

    /// <summary>
    /// 一定時間後に想定されるキャラクター座標を取得
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public Vector2 GetFuturePosition(float time)
    {
        Vector2 futurePos = VectorUtil.Add(transform.position, rb.linearVelocity * time);
        // TODO 敵が自機に近づいて止まる場合を考慮する。まだ未実装なため一旦はずっと左に行ってもらう
        if(futurePos.x < ManagerBattlePhase.Instance.GetPlayerAttackableBoundaryX())
            futurePos = new Vector2(ManagerBattlePhase.Instance.GetPlayerAttackableBoundaryX(), futurePos.y);

        return futurePos;
    }
}
