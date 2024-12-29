using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileWeaponBase : MonoBehaviour
{
    private BagItemDataBase data;
    private SpriteRenderer sr;

    public static ProjectileWeaponBase Launch(BagItemDataBase data, CharacterBase target, Vector2 startPosition)
    {
        ProjectileWeaponBase weapon = new GameObject(data.ItemName.ToString()).AddComponent<ProjectileWeaponBase>();
        weapon.transform.localScale = new Vector2(0.5f, 0.5f);
        weapon.transform.position = startPosition;
        weapon.data = data;

        // 画像設定
        weapon.sr = new GameObject("Image").AddComponent<SpriteRenderer>();
        weapon.sr.transform.SetParent(weapon.transform);
        weapon.sr.transform.localPosition = Vector2.zero;
        weapon.sr.transform.localScale = Vector2.one;
        weapon.sr.sprite = BasicUtil.LoadSprite4Resources(data.SpritePathItemImage);
        weapon.sr.sortingLayerName = Consts.SortingLayer.BattleWeapon;

        // 武器を到着地点まで移動させる
        float duration = 1.0f;
        float height = UnityEngine.Random.Range(1.5f, 2.5f);
        Vector2 endPosition = target.GetFuturePosition(duration);

        Sequence sequence = DOTween.Sequence();
        switch(data.WeaponLaunchType)
        {
            case LaunchType.ThrowParabola:
                // 山なり移動
                sequence.Append(weapon.MoveInParabola(sequence, startPosition, endPosition, height, duration));
                // 終了処理
                sequence.OnComplete(() => {
                    weapon.OnWeaponHit(target);
                });
                break;
            case LaunchType.ThrowStraight:
                // まっすぐ刺す
                sequence.Append(weapon.MoveInStraight(sequence, endPosition, duration));
                // 終了処理
                sequence.OnComplete(() => {
                    weapon.OnWeaponHit(target);
                });
                break;
            case LaunchType.Unique:
                // 各武器で別途登録する
                if(data.ItemName == BagItemName.Canon)
                {
                    sequence.Append(weapon.LaunchCanon(sequence, endPosition, duration, () => {
                        weapon.OnWeaponHit(target);
                    }));
                }
                else
                {
                    Debug.LogError("未対応なUnique武器種別: " + data.ItemName);
                }
                break;
            default:
                Debug.LogError("未対応の武器射出タイプ: " + data.WeaponLaunchType);
                break;
        }

        return weapon;
    }
    
    private Sequence LaunchCanon(Sequence sequence, Vector2 endPosition, float duration, Action OnComplete)
    {
        Vector3 dest = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(0.8f, 1.2f), transform.position.z);
        Vector3 dir = (Vector3)endPosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float timeTillShot = UnityEngine.Random.Range(0.8f, 1.2f);

        if(sequence != null) sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(dest.y, timeTillShot))
            .OnComplete(() => {
                // 弾射出
                SpriteRenderer sr = new GameObject("CanonBullet").AddComponent<SpriteRenderer>();
                sr.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.BattleItem.CanonBullet);
                sr.transform.position = transform.position;
                sr.color = new Color(1f, 1f, 1f, 0f);
                sr.transform.localScale = new Vector2(0.1f, 0.1f);
                Sequence seq = DOTween.Sequence();
                seq.Append(sr.transform.DOMove(endPosition, duration).OnComplete(() =>
                {
                    OnComplete?.Invoke();
                    Destroy(sr.gameObject);
                }).SetEase(Ease.Linear))
                .Join(sr.transform.DOScale(1.0f, 1f))
                .Join(sr.DOFade(1.0f, 1f));
            })
            .Join(transform.DORotate(new Vector3(0.0f, 0.0f, angle), timeTillShot, RotateMode.FastBeyond360));
        return sequence;
        // // 終了処理
        // sequence.OnComplete(() => {
        //     weapon.OnWeaponHit(target);
        // });
    }

    private Sequence MoveInStraight(Sequence sequence, Vector2 endPosition, float duration)
    {
        // カスタムパスを設定して放物線移動を実現
        if(sequence != null) sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(endPosition, duration).SetEase(Ease.Linear));

        Vector3 dir = (Vector3)endPosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        return sequence;
    }

    private Sequence MoveInParabola(Sequence sequence, Vector2 startPosition, Vector2 endPosition, float parabolaHeight, float duration)
    {
        // カスタムパスを設定して放物線移動を実現
        if(sequence != null) sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => (Vector2)transform.position, x => transform.position = x, endPosition, duration)
            .OnUpdate(() =>
            {
                float progress = sequence.Elapsed() / duration;
                float heightOffset = Mathf.Sin(progress * Mathf.PI) * parabolaHeight;
                Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, progress);
                currentPosition.y += heightOffset;
                transform.position = currentPosition;
            }));
        // 回転を追加
        bool isRotPlus = RandUtil.GetRandomBool(0.5f);
        sequence.Join(transform.DORotate(new Vector3(0, 0, isRotPlus ? UnityEngine.Random.Range(180f, 540) : UnityEngine.Random.Range(-540f, -180f)), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        return sequence;
    }

    private void OnWeaponHit(CharacterBase hitTarget)
    {
        if(hitTarget != null)
        {
            // 通常攻撃系以外クリティカルは発生しない
            bool isCritical = data.WeaponDamageType == DamageType.NormalDamage ? RandUtil.GetRandomBool(0.1f) : false;
            hitTarget.GainDamage(isCritical ? data.WeaponDamage * 1.5f : data.WeaponDamage, data.WeaponDamageType);

            // 回復系
            if(data.WeaponDamageType == DamageType.Heal)
            {
                // 回復パーティクル
                ManagerParticle.Instance.ShowOnHealParticle(hitTarget.transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
            }
            // アーマー付与
            else if(data.WeaponDamageType == DamageType.Shield)
            {
                // 回復パーティクル
                ManagerParticle.Instance.ShowOnShieldParticle(hitTarget.transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
            }
            // 攻撃系
            else
            {
                // ヒットパーティクル
                ManagerParticle.Instance.ShowOnDamageParticle(hitTarget.transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);
                // ヒット時の揺れ
                hitTarget.ShakeOnDamage();
            }
            Destroy(gameObject);
        }
        else
        {
            // 対象がすでに死んでいる場合、2度ぐらい跳ねつつフェードアウトする
            // プレイヤーとの距離を取得
            Vector2 playerPos = ManagerBattlePhase.Instance.GetPlayer().transform.position;
            Vector2 bounce1 = transform.position + VectorUtil.Sub(transform.position, playerPos) / 3.0f;
            Vector2 bounce2 = bounce1 + VectorUtil.Sub(bounce1, (Vector2)transform.position) / 3.0f;

            // シーケンスを作成
            MoveInParabola(null, transform.position, bounce1, UnityEngine.Random.Range(0.5f, 1.0f), 0.5f).OnComplete(() => {
                MoveInParabola(null, bounce1, bounce2, UnityEngine.Random.Range(0.2f, 0.5f), 0.3f).OnComplete(() => {
                    Destroy(gameObject);
                });
            });
            sr.DOFade(0.0f, 0.8f);
        }
    }

    // private static GameObject GetWeaponPrefab(BagItemDataBase data)
    // {
    //     // Prefabが登録済みならそれを返す
    //     if(weaponPrefab.ContainsKey(data.ItemName)) return weaponPrefab[data.ItemName];

    //     GameObject prefab = Resources.Load<GameObject>(data.BattlePrefabPath);
    //     if(prefab == null)
    //     {
    //         Debug.LogError("不正なWeapon名: " + data.ItemName);
    //         return null;
    //     }
    //     weaponPrefab.Add(data.ItemName, prefab);
    //     return prefab;
    // }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
