using DG.Tweening;
using UnityEngine;

public class ManagerTitle : MonoBehaviourSingleton<ManagerTitle>
{


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSettings()
    {
        PopupBase.Show(PopupType.Settings);
    }

    public void OnClickTutorialStart()
    {
        PopupBase.Show(PopupType.Tutorial);
    }


    public void DeleteSavedData()
    {
        PlayerPrefs.DeleteKey(Consts.PlayerPrefs.Keys.ProgressData);
    }

    /// <summary>
    /// タイトルシーンへ遷移する
    /// </summary>
    public void Move2SceneInGame()
    {
        ManagerSceneTransition.Instance.Move2Scene(SceneType.InGame);
    }
}
