using System;
using DG.Tweening;
using UnityEngine;

public class ManagerParticle : MonoBehaviourSingleton<ManagerParticle>
{
    public void Start()
    {
        SetIsDontDestroyOnLoad(true);
    }

    public void ShowOnBombExplodeParticle(Vector2 pos, Transform parent, Action onComplete = null)
    {
        ShowParticle(pos, parent, Consts.Resources.Prefabs.Particles.BombExplode, onComplete);
    }

    /// <summary>
    /// シールド時用のパーティクルを表示
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="onComplete"></param>
    public void ShowOnShieldParticle(Vector2 pos, Transform parent, Action onComplete = null)
    {
        ShowParticle(pos, parent, Consts.Resources.Prefabs.Particles.Shield, onComplete);
    }

    /// <summary>
    /// ヒール時用のパーティクルを表示
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="onComplete"></param>
    public void ShowOnHealParticle(Vector2 pos, Transform parent, Action onComplete = null)
    {
        ShowParticle(pos, parent, Consts.Resources.Prefabs.Particles.Heal, onComplete);
    }

    /// <summary>
    /// ダメージ発生時用パーティクルを表示
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="onComplete"></param>
    public void ShowOnDamageParticle(Vector2 pos, Transform parent, Action onComplete = null)
    {
        ShowParticle(pos, parent, Consts.Resources.Prefabs.Particles.Damage, onComplete);
    }

    /// <summary>
    /// キャラクター死亡時用パーティクルを表示
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="onComplete"></param>
    public void ShowOnDeadParticle(Vector2 pos, Transform parent, Action onComplete = null)
    {
        ShowParticle(pos, parent, Consts.Resources.Prefabs.Particles.Destroy, onComplete);
    }

    /// <summary>
    /// アイテム進化用パーティクルを表示する
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="onComplete"></param>
    public void ShowOnEvolveParticle(Vector2 pos, Transform parent, Action onComplete = null)
    {
        ShowParticle(pos, parent, Consts.Resources.Prefabs.Particles.Evolve, onComplete);
    }

    /// <summary>
    /// 基本的なパーティクル表示処理
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="resourcesPath"></param>
    /// <param name="onComplete"></param>
    private void ShowParticle(Vector2 pos, Transform parent, string resourcesPath, Action onComplete = null)
    {
        ParticleSystem particle = CreateParticleSystem(pos, parent, resourcesPath);
        ParticleSystem.MainModule mainModule = particle.main;
        mainModule.useUnscaledTime = true;
        particle.Play();
        SetOnParticleComplete(particle, () => {
            Destroy(particle.gameObject);
            onComplete?.Invoke();
        });
    }

    /// <summary>
    /// パーティクルシステムを生成
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="resourcesPath"></param>
    /// <returns></returns>
    private ParticleSystem CreateParticleSystem(Vector2 pos, Transform parent, string resourcesPath)
    {
        GameObject particlePrefab = BasicUtil.LoadGameObject4Resources(resourcesPath);
        ParticleSystem particle = Instantiate(particlePrefab).GetComponent<ParticleSystem>();
        particle.transform.SetParent(parent);
        particle.transform.localScale = Vector2.one;
        particle.transform.position = pos;
        return particle;
    }

    /// <summary>
    /// パーティクル終了時のコールバック
    /// </summary>
    /// <param name="particle"></param>
    /// <param name="OnComplete"></param>
    private void SetOnParticleComplete(ParticleSystem particle, Action OnComplete)
    {
        DOTween.To(() => particle != null ? 1.0f : 0.0f, x => {
            if (particle != null && !particle.IsAlive()) OnComplete?.Invoke();
        }, 0.0f, 2f).SetLoops(-1, LoopType.Restart).SetUpdate(true);
    }
}