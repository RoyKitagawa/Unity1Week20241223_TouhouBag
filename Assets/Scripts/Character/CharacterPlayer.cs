using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterPlayer : CharacterBase
{
    [SerializeField]
    private HPBar hpBar, armorBar;
    
    public override void InitializeCharacter(CharacterDataBase characterData)
    {
        if(isInitialized) return;
        base.InitializeCharacter(characterData);

        // HPバー初期化
        armorBar.Initialize(data.MaxLife, 0.0f); // アーマーは一旦HPと同じ値をMAXとする
        hpBar.Initialize(data.MaxLife, currentLife);
    }

    /// <summary>
    /// ダメージ付与
    /// </summary>
    /// <param name="damageAmt"></param>
    /// <param name="damageType"></param>
    public override void GainDamage(BagItemName weaponName, float damageAmt, DamageType damageType, bool isCritical)
    {
        base.GainDamage(weaponName, damageAmt, damageType, isCritical);
        armorBar.SetCurrentValue(currentArmor);
        hpBar.SetCurrentValue(currentLife);
    }

    /// <summary>
    /// キャラクター死亡時に呼び出される処理
    /// </summary>
    protected override void OnDead()
    {
        if(!isAlive) return;

        // TODO 死亡処理
        Debug.Log("プレイヤー死亡！");

        isAlive = false;
        ManagerParticle.Instance.ShowOnDeadParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
        GetImage().DOFade(0.0f, 0.2f).SetUpdate(true);
        // TODO GAMEOVER演出表示＆リザルトへ
        ManagerBattleMode.Instance.OnWaveFail();
    }

    private bool isAlive = true;
}
