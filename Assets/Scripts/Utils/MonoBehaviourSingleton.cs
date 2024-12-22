using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    private static T instance;
    public static T Instance{ get { return instance; } }

    /// <summary>
    /// シーン遷移時に削除するかを設定する
    /// デフォルトは遷移時に削除されるようになっている
    /// </summary>
    /// <param name="isDontDestroyOnLoad"></param>
    public virtual void SetIsDontDestroyOnLoad(bool isDontDestroyOnLoad)
    {
        if(isDontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        else SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
    
    /// <summary>
    /// 開始時にシングルトン処理を行う
    /// Start時に呼び出されても良いようにAwakeで呼び出す
    /// </summary>
    public virtual void Awake()
    {
        SetAsSingletonInstance();
    }

    /// <summary>
    /// オブジェクトをシングルトンインスタンスとして保持
    /// オブジェクトが複数存在する場合は自身を排除
    /// </summary>
    protected virtual void SetAsSingletonInstance()
    {
        if(instance == null)
        {
            instance = (T)this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
