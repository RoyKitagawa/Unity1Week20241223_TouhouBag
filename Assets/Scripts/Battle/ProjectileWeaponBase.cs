using System;
using DG.Tweening;
using UnityEngine;

public class ProjectileWeaponBase : MonoBehaviour
{
    private BagItemDataBase data;
    private SpriteRenderer sr;

    public static ProjectileWeaponBase Launch(BagItemDataBase data, CharacterBase target, Vector2 startPosition)
    {
        ProjectileWeaponBase weapon = new GameObject(data.ItemName.ToString()).AddComponent<ProjectileWeaponBase>();
        weapon.transform.SetParent(BasicUtil.GetRootObject(Consts.Roots.BattleWeapons).transform);
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
        float dist = Vector2.Distance(startPosition, target.transform.position);
        float duration;
        if(data.WeaponTargetType == TargetType.Self) duration = 1.0f;
        else
        {
            float speed = 10f;
            duration = Vector2.Distance(startPosition, target.transform.position) / speed;
        }
        float height = data.WeaponTargetType == TargetType.Self ? UnityEngine.Random.Range(1.5f, 2.5f) : dist / 10.0f;
        Vector2 endPosition = target.GetFuturePosition(duration);

        Sequence sequence = DOTween.Sequence();
        switch(data.WeaponLaunchType)
        {
            case LaunchType.ThrowParabola:
                // 山なり移動
                sequence.Append(weapon.MoveInParabola(startPosition, endPosition, height, duration));
                // 終了処理
                sequence.OnComplete(() => {
                    weapon.OnWeaponHit(target);
                });
                break;
            case LaunchType.ThrowStraight:
                // まっすぐ刺す
                sequence.Append(weapon.MoveInStraight(endPosition, duration / 2.0f));
                // 終了処理
                sequence.OnComplete(() => {
                    weapon.OnWeaponHit(target);
                });
                break;
            case LaunchType.Unique:
                // 各武器で別途登録する
                if(data.ItemName == BagItemName.Canon)
                {
                    sequence.Append(weapon.LaunchCanon(target, () => {
                        // weapon.sr.DOFade(0.0f, 0.5f).OnComplete(() => { Destroy(weapon.gameObject); });
                        // weapon.OnWeaponHit(target);
                    }));
                }
                else if(data.ItemName == BagItemName.Bomb)
                {
                    // 山なり移動
                    sequence.Append(weapon.MoveInParabola(startPosition, endPosition, height, duration));
                    // 終了処理
                    sequence.OnComplete(() => {
                        weapon.BombExplode();
                        // weapon.OnWeaponHit(target);
                    });
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

    private void BombExplode()
    {
        // 一定範囲に一定時間Colliderを展開＆パーティクル表示
        CircleCollider2D collider = new GameObject("BombExplode").AddComponent<CircleCollider2D>();
        collider.transform.SetParent(BasicUtil.GetRootObject(Consts.Roots.BattleWeapons).transform);
        collider.transform.position = transform.position;
        collider.radius = 2.0f;
        collider.tag = Consts.Tags.Bullet;
        collider.transform.DOLocalMoveX(0.0f, 0.2f).OnComplete(() => {
            Destroy(collider.gameObject);
        });

        // そこに触れた敵にダメージ
        collider.gameObject.AddComponent<Bullet>().data = data;

        // 爆発パーティクル表示
        ManagerParticle.Instance.ShowOnBombExplodeParticle(transform.position, BasicUtil.GetRootObject(Consts.Roots.ParticlesBattle).transform);

        // 自分自身を破壊
        Destroy(gameObject);
    }
    
    /// <summary>
    /// 河童キャノン
    /// </summary>
    /// <param name="sequence"></param>
    /// <param name="target"></param>
    /// <param name="OnComplete"></param>
    /// <returns></returns>
    private Sequence LaunchCanon(CharacterBase target, Action OnComplete)
    {
        float timeTillShot = 1.0f;
        Vector3 dest = new Vector3(transform.position.x, transform.position.y + UnityEngine.Random.Range(0.6f, 1.5f), transform.position.z);
        Vector3 dir = (Vector3)target.GetFuturePosition(timeTillShot + 0.2f) - dest;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 射出ポイントオブジェクトが無ければ追加する
        Transform launchPos = transform.Find("LaunchPos");
        if(launchPos == null)
        {
            launchPos = new GameObject("LaunchPos").transform;
            launchPos.SetParent(this.transform);
            launchPos.localPosition = new Vector2(data.Size.x / 2f + 0.5f, 0.05f);
        }

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(dest.y, timeTillShot))
            .OnComplete(() => {
                // ランチャー本体を消す
                sr.DOFade(0.0f, 0.5f).OnComplete(() => { Destroy(gameObject); });
                
                // 既にバトルが終了している場合は何もしない
                if(!ManagerBattleMode.Instance.IsBattleActive)
                {
                    return;
                }
                // 弾射出
                SpriteRenderer bulletSR = new GameObject("CanonBullet").AddComponent<SpriteRenderer>();
                bulletSR.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.BattleItem.CanonBullet);
                bulletSR.sortingLayerName = Consts.SortingLayer.BattleWeapon;
                bulletSR.transform.SetParent(BasicUtil.GetRootObject(Consts.Roots.BattleWeapons).transform);
                bulletSR.transform.position = launchPos.position;
                bulletSR.transform.rotation = Quaternion.Euler(0, 0, angle);
                // bulletSR.color = new Color(1f, 1f, 1f, 0f);
                // bulletSR.transform.localScale = new Vector2(0.1f, 0.1f);
                bulletSR.gameObject.AddComponent<Bullet>().data = data;

                // 当たり判定とか
                bulletSR.tag = Consts.Tags.Bullet; // 範囲攻撃用タグ
                Rigidbody2D rb = bulletSR.gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0.0f;
                rb.linearVelocity = (bulletSR.transform.position - dest).normalized * 16f;
                CircleCollider2D collider = bulletSR.gameObject.AddComponent<CircleCollider2D>();
                collider.radius = 0.5f;
                collider.isTrigger = true;
                
                // Sequence seq = DOTween.Sequence();
                // seq.Append(bulletSR.transform.DOScale(1.0f, 0.25f))
                //     // .Join(bulletSR.DOFade(1.0f, 0.25f))
                //     .OnComplete(() => {
                //         OnComplete?.Invoke();
                //     });
            })
            .Join(transform.DORotate(new Vector3(0.0f, 0.0f, angle), timeTillShot, RotateMode.FastBeyond360));
        return sequence;
    }

    private Sequence MoveInStraight(Vector2 endPosition, float duration)
    {
        // カスタムパスを設定して放物線移動を実現
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(endPosition, duration).SetEase(Ease.Linear));

        Vector3 dir = (Vector3)endPosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        return sequence;
    }

    private Sequence MoveInParabola(Vector2 startPosition, Vector2 endPosition, float parabolaHeight, float duration)
    {
        // カスタムパスを設定して放物線移動を実現
        Sequence sequence = DOTween.Sequence();
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
            hitTarget.OnWeaponHit(data);
            Destroy(gameObject);
        }
        else
        {
            // 対象がすでに死んでいる場合、2度ぐらい跳ねつつフェードアウトする
            // プレイヤーとの距離を取得
            Vector2 playerPos = ManagerBattleMode.Instance.GetPlayer().transform.position;
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
