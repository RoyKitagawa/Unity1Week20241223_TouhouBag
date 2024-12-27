using DG.Tweening;
using TMPro;
using UnityEngine;



public class BattleDamage : MonoBehaviour
{
    private static GameObject damagePrefab = null;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private SpriteRenderer criticalBG;

    public static BattleDamage ShowDamageEffect(int damageAmt, DamageType damageType, Vector2 pos)
    {
        BattleDamage damage = Instantiate(GetDamagePrefab()).GetComponent<BattleDamage>();
        if(damage == null)
        {
            Debug.LogError("Damageインスタンスの生成に失敗");
            return null;
        }
        // オブジェクト初期化
        // 横座標を少し散らす
        damage.transform.position = new Vector2(pos.x + Random.Range(-0.5f, 0.5f), pos.y);
        // ダメージ量設定
        damage.text.text = damageAmt.ToString();
        // ダメージ表記サイズ微調整
        float sizeMultiplier = Mathf.Clamp(damageAmt / 100f, 0.8f, 1.5f);
        damage.transform.localScale *= sizeMultiplier;

        // 色味設定／背景設定
        switch(damageType)
        {
            case DamageType.NormalDamage:
                damage.text.color = Color.white;
                break;
            case DamageType.CriticalDamage:
                damage.text.color = Color.red;
                damage.criticalBG.gameObject.SetActive(true);
                break;
            case DamageType.Heal:
                damage.text.color = Color.green;
                break;
        }

        // ダメージ演出を動かす
        float height = Random.Range(0.5f, 1.5f);
        float duration = Random.Range(0.8f, 1.0f);
        Vector2 endPosition = new Vector2(pos.x + Random.Range(-0.5f, 0.5f), pos.y);
        // カスタムパスを設定して放物線移動を実現
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => (Vector2)damage.transform.position, x => damage.transform.position = x, endPosition, duration)
            .OnUpdate(() =>
            {
                float progress = sequence.Elapsed() / duration;
                // 開始地点まで降りて欲しくないので、0.8の係数をかけておく
                float heightOffset = Mathf.Sin(progress * Mathf.PI * 0.8f) * height;
                Vector3 currentPosition = Vector3.Lerp(pos, endPosition, progress);
                currentPosition.y += heightOffset;
                damage.transform.position = currentPosition;
            }));
        sequence.Join(damage.text.DOFade(0.0f, duration * 0.6f).SetDelay(duration * 0.4f));
        // 終了処理
        sequence.OnComplete(() => {
            Destroy(damage.gameObject);
        });

        // 背景がONの場合はそれもフェードアウトする
        damage.criticalBG.DOFade(0.0f, duration * 0.6f).SetDelay(duration * 0.4f);
        return damage;
    }

    private static GameObject GetDamagePrefab()
    {
        if(damagePrefab != null) return damagePrefab;
        damagePrefab = Resources.Load<GameObject>(Consts.Resources.BattleDamagePrefab);
        if(damagePrefab == null)
        {
            Debug.LogError("DamagePrefab取得失敗");
        }
        return damagePrefab;
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
