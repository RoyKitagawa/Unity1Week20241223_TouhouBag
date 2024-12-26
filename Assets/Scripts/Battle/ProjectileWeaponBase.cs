using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileWeaponBase : MonoBehaviour
{
    private static Dictionary<BagItemName, GameObject> weaponPrefab = new Dictionary<BagItemName, GameObject>();
    private BagItemDataBase data;
    // public static void LaunchProjectileWeapon(BagItemName itemName, CharacterBase target)
    // {
    //     if(target == null || target.transform == null)
    //     {
    //         Debug.LogError("敵機が存在しません。武器投擲を終了します: " + itemName);
    //     }
    // }

    public static ProjectileWeaponBase Launch(BagItemDataBase data, CharacterBase target, Vector2 startPosition)
    {
        GameObject prefab = GetWeaponPrefab(data);
        if(prefab == null)
        {
            Debug.LogError("投てき武器の生成に失敗: " + data.ItemName);
            return null;
        }
        ProjectileWeaponBase weapon = Instantiate(prefab).GetComponent<ProjectileWeaponBase>();
        weapon.transform.position = startPosition;
        weapon.data = data;

        // 武器を到着地点まで移動させる
        float duration = 1.0f;
        float height = Random.Range(1.5f, 2.5f);
        Vector2 endPosition = target.GetFuturePosition(duration);

        // 中間地点（高さを含む）を計算
        Vector3 peakPosition = (startPosition + endPosition) / 2;
        peakPosition.y += Mathf.Abs(endPosition.x - startPosition.x) / 4; // y軸方向に高さを追加（調整可能）

        // シーケンスを作成
        Sequence sequence = DOTween.Sequence();

        // カスタムパスを設定して放物線移動を実現
        sequence.Append(DOTween.To(() => (Vector2)weapon.transform.position, x => weapon.transform.position = x, endPosition, duration)
            .OnUpdate(() =>
            {
                float progress = sequence.Elapsed() / duration;
                float heightOffset = Mathf.Sin(progress * Mathf.PI) * height;
                Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, progress);
                currentPosition.y += heightOffset;
                weapon.transform.position = currentPosition;
            }));
        // 回転を追加
        bool isRotPlus = RandUtil.GetRandomBool(0.5f);
        sequence.Join(weapon.transform.DORotate(new Vector3(0, 0, isRotPlus ? Random.Range(180f, 540) : Random.Range(-540f, -180f)), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear));

        // 終了処理
        sequence.OnComplete(() => {
            weapon.OnWeaponHit(target);
        });

        return weapon;
    }

    private void OnWeaponHit(CharacterBase hitTarget)
    {
        // 通常攻撃系以外クリティカルは発生しない
        bool isCritical = data.WeaponDamageType == DamageType.NormalDamage ? RandUtil.GetRandomBool(0.1f) : false;
        if(hitTarget != null) hitTarget.GainDamage(isCritical ? data.WeaponDamage * 1.5f : data.WeaponDamage, data.WeaponDamageType);

        Destroy(gameObject);
    }

    private static GameObject GetWeaponPrefab(BagItemDataBase data)
    {
        // Prefabが登録済みならそれを返す
        if(weaponPrefab.ContainsKey(data.ItemName)) return weaponPrefab[data.ItemName];

        GameObject prefab = Resources.Load<GameObject>(data.BattlePrefabPath);
        if(prefab == null)
        {
            Debug.LogError("不正なWeapon名: " + data.ItemName);
            return null;
        }
        weaponPrefab.Add(data.ItemName, prefab);
        return prefab;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
