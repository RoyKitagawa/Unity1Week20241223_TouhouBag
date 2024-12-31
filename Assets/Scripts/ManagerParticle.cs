using System;
using DG.Tweening;
using UnityEngine;

public class ManagerParticle : MonoBehaviourSingleton<ManagerParticle>
{
    public void Start()
    {
        SetIsDontDestroyOnLoad(true);
    }

    public void ShowFireFlowerExplodePartocle(Vector2 pos, Transform parent, Color color, float scale, Action onComplete = null)
    {
        // ShowParticle(pos, parent, Consts.Resources.Prefabs.Particles.FireFlowerExplode, onComplete);
        // GameObject particlePrefab = BasicUtil.LoadGameObject4Resources(Consts.Resources.Prefabs.Particles.FireFlowerExplode);
        // ParticleSystem particle = Instantiate(particlePrefab).GetComponent<ParticleSystem>();
        // // particle.transform.SetParent(parent);
        // // particle.transform.localScale = Vector2.one;
        // // particle.transform.position = pos;
        // particle.transform.rotation = Quaternion.Euler(Vector3.zero);

        // // ParticleSystem particle = CreateParticleSystem(pos, parent, resourcesPath);
        // ParticleSystem.MainModule mainModule = particle.main;
        // mainModule.useUnscaledTime = true;
        // particle.Play();
        // // SetOnParticleComplete(particle, () => {
        // //     Destroy(particle.gameObject);
        // //     onComplete?.Invoke();
        // // });




        
        ParticleSystem particle = CreateParticleSystem(pos, parent, Consts.Resources.Prefabs.Particles.FireFlowerExplode);
        particle.transform.rotation = Quaternion.Euler(Vector3.zero);
        ParticleSystem.MainModule mainModule = particle.main;
        mainModule.useUnscaledTime = true;

        // ランダム性
        float randValue = UnityEngine.Random.Range(0.8f, 1.2f);

        // 花火特有の情報を設定する
        float baseStartSpeed = 7f;
        float baseStartSize = 0.25f;
        float actualSpeed = baseStartSpeed * scale * randValue;
        // 速度
        ParticleSystem.MainModule main = particle.main;
        //  main.simulationSpeed = 1.0f;
        main.startSpeed = actualSpeed;
        main.startSize = baseStartSize * scale;
        
        // 射出量
        float defaultEmissionBurstCount = 200;
        float actualEmissionCount = defaultEmissionBurstCount * scale * randValue;
        if(actualEmissionCount < defaultEmissionBurstCount / 2) actualEmissionCount = defaultEmissionBurstCount / 2;
        ParticleSystem.EmissionModule emission = particle.emission;
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0.0f, actualEmissionCount);
        emission.SetBursts(new ParticleSystem.Burst[] { burst });

        // 色味
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(color, 0.0f), // 開始時
                new GradientColorKey(color, 1.0f), // 終了時
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f), // 開始時
                new GradientAlphaKey(1.0f, 0.5f),
                new GradientAlphaKey(0.0f, 1.0f), // 終了時
            }
        );

        ParticleSystem.ColorOverLifetimeModule colorOverLifetime =  particle.colorOverLifetime;
        colorOverLifetime.enabled = true;
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);

        ParticleSystem.TrailModule trails = particle.trails;
        trails.colorOverLifetime = new ParticleSystem.MinMaxGradient(gradient);
        trails.colorOverTrail = new ParticleSystem.MinMaxGradient(gradient);


        particle.Play();
        SetOnParticleComplete(particle, () => {
            Destroy(particle.gameObject);
            onComplete?.Invoke();
        });
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
    public void ShowParticle(Vector2 pos, Transform parent, string resourcesPath, Action onComplete = null)
    {
        ParticleSystem particle = CreateParticleSystem(pos, parent, resourcesPath);
        ParticleSystem.MainModule mainModule = particle.main;
        // mainModule.useUnscaledTime = true;
        particle.Play();
        // SetOnParticleComplete(particle, () => {
        //     Destroy(particle.gameObject);
        //     onComplete?.Invoke();
        // });
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
        // particle.transform.localScale = Vector2.one;
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