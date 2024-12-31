using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireFlower
{
    public static void Launch(Vector2 startPos, Vector2 destPos, float scale, Action<Transform> onComplete)
    {
        HashSet<Color> colors = new HashSet<Color> {
            new Color32(0xFF, 0xFF, 0x00, 0xFF), // 黄色
            new Color32(0xFF, 0xC0, 0xCB, 0xFF), // ピンク
            new Color32(0xFF, 0xA5, 0x00, 0xFF), // オレンジ
            new Color32(0x7F, 0xFF, 0xD4, 0xFF), // 緑
            new Color32(0x87, 0xCE, 0xFA, 0xFF), // 青
            // new Color32(0xFF, 0x63, 0x47, 0xFF), // 赤
            new Color32(0xFF, 0xF0, 0xF5, 0xFF), // 白っぽい赤 
            new Color32(0xFF, 0xFA, 0xF0, 0xFF), // 白っぽい黄色 
            new Color32(0xFF, 0xFF, 0xFF, 0xFF), // 白
        };
        Launch(startPos, destPos, RandUtil.GetRandomItem(colors), scale, onComplete);
    }
    public static void Launch(Vector2 startPos, Vector2 destPos, Color color, float scale, Action<Transform> onComplete)
    {
        Transform root = BasicUtil.GetRootObject("FireFlowers").transform;
        Transform fireFlower = new GameObject("FireFlower").transform;
        fireFlower.SetParent(root);
        fireFlower.transform.position = startPos;
        float seedLaunchDuration = 2.0f;
        
        int maxSeeds = 5;
        float max = maxSeeds;
        for(int i = 0; i < maxSeeds; i++)
        {
            SpriteRenderer sr = new GameObject("FireFlowerLauncher").AddComponent<SpriteRenderer>();
            sr.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.FireFlowerParticle);
            sr.transform.SetParent(fireFlower);
            sr.transform.localPosition = new Vector2(0.0f, i * 0.1f);
            sr.transform.localScale = new Vector2(scale, scale);
            sr.sortingLayerName = Consts.SortingLayer.UIOverlay;
            sr.sortingOrder = 100;
            float index = i;
            sr.color = new Color(color.r, color.g, color.b, index / max);
            Sequence seq = DOTween.Sequence();
            seq.Append(sr.DOFade(0.0f, seedLaunchDuration));
            seq.SetUpdate(true);
        }

        Sequence seedSeq = DOTween.Sequence();
        seedSeq.Append(fireFlower.transform.DOMoveY(destPos.y, seedLaunchDuration));
        seedSeq.SetUpdate(true);
        seedSeq.OnComplete(() => {
            Debug.Log("TriggerExplode");
            Explode(fireFlower, color, scale, onComplete);
        });
    }

    private static void Explode(Transform fireflower, Color color, float scale, Action<Transform> onComplete)
    {
        // foreach(SpriteRenderer seeds in fireflower.GetComponentsInChildren<SpriteRenderer>())
        // {
        //     Destroy(seeds.gameObject);
        // }

        Debug.Log("OnTriggerExplode");
        ManagerParticle.Instance.ShowFireFlowerExplodePartocle(fireflower.position, fireflower, color, scale, () => {
            Debug.Log("Complete");
            onComplete(fireflower);
        });
        // public void ShowFireFlowerExplodePartocle(Vector2 pos, Transform parent, Color color, float scale, Action onComplete = null)

    }
}
