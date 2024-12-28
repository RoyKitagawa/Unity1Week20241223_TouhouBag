using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileWeaponBase : MonoBehaviour
{
    private static Dictionary<BagItemName, GameObject> weaponPrefab = new Dictionary<BagItemName, GameObject>();
    private BagItemDataBase data;
    private SpriteRenderer sr;
    // public static void LaunchProjectileWeapon(BagItemName itemName, CharacterBase target)
    // {
    //     if(target == null || target.transform == null)
    //     {
    //         Debug.LogError("敵機が存在しません。武器投擲を終了します: " + itemName);
    //     }
    // }

    public static ProjectileWeaponBase Launch(BagItemDataBase data, CharacterBase target, Vector2 startPosition)
    {
        // GameObject prefab = GetWeaponPrefab(data);
        // if(prefab == null)
        // {
        //     Debug.LogError("投てき武器の生成に失敗: " + data.ItemName);
        //     return null;
        // }
        // ProjectileWeaponBase weapon = Instantiate(prefab).GetComponent<ProjectileWeaponBase>();

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

        // // 中間地点（高さを含む）を計算
        // Vector3 peakPosition = (startPosition + endPosition) / 2;
        // peakPosition.y += Mathf.Abs(endPosition.x - startPosition.x) / 4; // y軸方向に高さを追加（調整可能）

        // シーケンスを作成
        Sequence sequence = weapon.MoveInParabola(startPosition, endPosition, height, duration);
        // 終了処理
        sequence.OnComplete(() => {
            weapon.OnWeaponHit(target);
        });

        return weapon;
    }

    private Sequence MoveInParabola(Vector2 startPosition, Vector2 endPosition, float parabolaHeight, float duration)
    {
        Sequence sequence = DOTween.Sequence();
        // カスタムパスを設定して放物線移動を実現
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
            MoveInParabola(transform.position, bounce1, UnityEngine.Random.Range(0.5f, 1.0f), 0.5f).OnComplete(() => {
                MoveInParabola(bounce1, bounce2, UnityEngine.Random.Range(0.2f, 0.5f), 0.3f).OnComplete(() => {
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
