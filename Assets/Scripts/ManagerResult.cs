using UnityEngine;

public class ManagerResult : MonoBehaviourSingleton<ManagerResult>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// タイトルシーンへ遷移する
    /// </summary>
    public void Move2SceneTitle()
    {
        ManagerSceneTransition.Instance.Move2Scene(SceneType.Title);
    }
}
