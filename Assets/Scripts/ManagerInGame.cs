using UnityEngine;

public class ManagerInGame : MonoBehaviourSingleton<ManagerInGame>
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
    /// リザルトシーンへ遷移する
    /// </summary>
    public void Move2SceneResult()
    {
        ManagerSceneTransition.Instance.Move2Scene(SceneType.Result);
    }
}
