using DG.Tweening;
using TMPro;
using UnityEngine;

public enum DamageType
{
    NormalDamage,
    CriticalDamage,
    Heal,
}

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
        damage.transform.position = pos;
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
        Vector2 jumpVector = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 2.0f));
        Vector2 jumpFallVector = new Vector2(jumpVector.x * 1.5f, jumpVector.y / 2f);

        Sequence jumpSequence = DOTween.Sequence();
        jumpSequence.Append(damage.transform.DOMove(pos + jumpVector, 0.5f).SetEase(Ease.OutQuad));
        jumpSequence.Append(damage.transform.DOMove(pos + jumpFallVector, 0.5f).SetEase(Ease.InQuad));
        // damage.transform.DOMove(pos + jumpVector, 0.5f).SetEase(Ease.OutQuart);
        damage.text.DOFade(0.0f, 0.6f).SetDelay(0.4f).OnComplete(() => {
            Destroy(damage.gameObject);
        });
        // 背景がONの場合はそれもフェードアウトする
        damage.criticalBG.DOFade(0.0f, 0.8f).SetDelay(0.3f);
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
