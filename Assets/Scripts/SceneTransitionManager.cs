using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    InGame,
    Result,
}

/// <summary>
/// シーン間の遷移を管理する
/// 全てのシーンで存在し、破壊されない前提
/// </summary>
public class SceneTransitionManager : MonoBehaviourSingleton<SceneTransitionManager>
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
    /// タイトルシーンへ移動する
    /// </summary>
    public void Move2SceneTitle() { SceneManager.LoadScene(GetSceneName(SceneType.Title)); }
    
    /// <summary>
    /// インゲームシーンへ移動する
    /// </summary>
    public void Move2SceneInGame() { SceneManager.LoadScene(GetSceneName(SceneType.InGame)); }

    /// <summary>
    /// リザルトシーンへ移動する
    /// </summary>
    public void Move2SceneResult() { SceneManager.LoadScene(GetSceneName(SceneType.Result)); }

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
            case SceneType.InGame:
                return "InGame";
            case SceneType.Result:
                return "Result";
            default:
                Debug.LogError("非対応のSceneType: " + sceneType);
                return "";
        }
    }
}
