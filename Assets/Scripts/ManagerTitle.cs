using UnityEngine;

public class ManagerTitle : MonoBehaviourSingleton<ManagerTitle>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject item = new GameObject("item");
        SpriteRenderer sr = item.AddComponent<SpriteRenderer>();
        PolygonCollider2D polyCollider = item.AddComponent<PolygonCollider2D>();
        
        sr.sprite = BasicUtil.LoadSprite4Resources(Consts.Resources.Sprites.BattleItem.Thumb.Apple);
        
        // T型の形状を定義
        Vector2[] points = new Vector2[]
        {
            // 左上から反時計回りに
            new Vector2(-1.5f, 1.5f), // 左上
            new Vector2(-1.5f, 0.5f),
            new Vector2(-0.5f, 0.5f),
            new Vector2(-0.5f, -1.5f), // 左下
            new Vector2(0.5f, -1.5f), // 右下
            new Vector2(0.5f, 0.5f),
            new Vector2(1.5f, 0.5f),
            new Vector2(1.5f, 1.5f),

            // new Vector2(-1, -1), // 左下
            // new Vector2(1, -1),  // 右下
            // new Vector2(1, 0),   // 右中
            // new Vector2(2, 0),   // 右外
            // new Vector2(2, 1),   // 上右
            // new Vector2(-2, 1),  // 上左
            // new Vector2(-2, 0),  // 左外
            // new Vector2(-1, 0)   // 左中
        };

        polyCollider.points = points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// タイトルシーンへ遷移する
    /// </summary>
    public void Move2SceneInGame()
    {
        ManagerSceneTransition.Instance.Move2Scene(SceneType.InGame);
    }
}
