using UnityEngine;

public static class BasicUtil
{
    /// <summary>
    /// 指定名のルートオブジェクトを取得
    /// 存在しない場合は作成する
    /// name内に"/"が存在する場合、その分階層を掘る
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject GetRootObject(string name, string layerName = "")
    {
        string[] rootNames = name.Split("/");
        GameObject root = null;
        GameObject prevRoot = null;
        for(int i = 0; i < rootNames.Length; i++)
        {
            // Rootオブジェクトが指定階層で存在するか確認する
            root = root == null
                ? GameObject.Find(rootNames[i])
                : root.transform.Find(rootNames[i])?.gameObject;
            // 存在しない、その階層で必要なRootオブジェクトを生成
            if(root == null)
            {
                root = new GameObject(rootNames[i]);
                if(prevRoot != null) root.transform.SetParent(prevRoot.transform);
                // 初回作成時はスケールや座標を0とする
                root.transform.localPosition = Vector3.zero;
                root.transform.localScale = Vector3.one;
            }

            // ひとつ前のRootオブジェクトを保持しておく
            prevRoot = root;
        }

        // 最終的に保持されている、最後の階層のRootオブジェクトを返す
        // root.layer = 
        return root;
    }

    /// <summary>
    /// ファイルパスからSpriteを読み込む
    /// Pivotは画像中心、画像のユニットサイズは画像の横幅を基準に設定する
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadSprite4Resources(string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);
        if(sprite == null)
        {
            Debug.LogError("SpriteのLoadに失敗しました: path = " + path);
        }
        return sprite;
    }

    /// <summary>
    /// ファイルパスからSpriteを読み込む
    /// WebGLではSystem.IO.File.ReadAllBytesは正常に動かないため要注意
    /// Pivotは画像中心、画像のユニットサイズは画像の横幅を基準に設定する
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadSprite4NonResources(string path)
    {
        byte[] bytes = System.IO.File.ReadAllBytes(path);
        Texture2D texture2D = new Texture2D(0, 0);
        texture2D.LoadImage(bytes);
        return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), texture2D.width);
    }

    /// <summary>
    /// ワールド座標で画面の角の座標を取得する
    /// 引数は、通常時はCamera.mainを指定する
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Rect GetScreenWorldCorners(Camera camera)
    {
        return GetScreenWorldCorners(camera, Vector2.zero, Vector2.one);
    }

    /// <summary>
    /// ワールド座標で画面の角の座標を取得する
    /// 引数は、通常時はCamera.mainを指定する
    /// 
    /// minPercentage, maxPercentageは画面の座標範囲。0.0f ~ 1.0f。
    /// 0,0は左下、1,1は右上
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="minPercentage"></param>
    /// <param name="maxPercentage"></param>
    /// <returns></returns>
    public static Rect GetScreenWorldCorners(Camera camera, Vector2 minPercentage, Vector2 maxPercentage)
    {
        Vector2 min = camera.ViewportToWorldPoint(minPercentage);
        Vector2 max = camera.ViewportToWorldPoint(maxPercentage);
        Rect rect = new Rect();
        rect.min = min;
        rect.max = max;
        return rect;
    }

    /// <summary>
    /// マウスの座標を取得する
    /// マウスのクリックUp、Downイベント等のタイミングで使う想定
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// 指定範囲のRectを取得する
    /// CenterPosを基準に座標、範囲を設定する
    /// </summary>
    /// <param name="centerPos"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static Rect CreateRectFromCenter(Vector2 centerPos, float width, float height)
    {
        return new Rect(
            centerPos.x - width / 2.0f,
            centerPos.y - height / 2.0f,
            width,
            height
        );
    }

    // public static Material LoadMaterial(string path)
    // {
    //     byte[] bytes = System.IO.File.ReadAllBytes(path);
    //     Texture2D texture2D = new Texture2D(0, 0);
    //     texture2D.LoadImage(bytes);
    //     return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), texture2D.width);
    // }
}