using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    Story,
    InGameBagEdit,
    InGameBattle,
    Result,
}

/// <summary>
/// シーン間の遷移を管理する
/// 全てのシーンで存在し、破壊されない前提
/// </summary>
public class ManagerSceneTransition : MonoBehaviourSingleton<ManagerSceneTransition>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetIsDontDestroyOnLoad(true);
    }

    /// <summary>
    /// 指定のシーンに移動する
    /// </summary>
    public void Move2Scene(SceneType sceneType) { SceneManager.LoadScene(GetSceneName(sceneType)); }
    
    /// <summary>
    /// シーン名を取得する
    /// </summary>
    /// <param name="sceneType"></param>
    /// <returns></returns>
    private string GetSceneName(SceneType sceneType)
    {
        switch(sceneType)
        {
            case SceneType.Title:
                return "Title";
            case SceneType.InGameBagEdit:
                return "InGameBag";
            case SceneType.InGameBattle:
                return "InGameBattle";
            case SceneType.Result:
                return "Result";
            case SceneType.Story:
                return "Story";
            default:
                Debug.LogError("非対応のSceneType: " + sceneType);
                return "";
        }
    }
}
