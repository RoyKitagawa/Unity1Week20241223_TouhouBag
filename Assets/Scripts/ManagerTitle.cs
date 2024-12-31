using DG.Tweening;
using UnityEngine;

public class ManagerTitle : MonoBehaviourSingleton<ManagerTitle>
{
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
    public void Move2SceneInGameBagEdit()
    {
        ManagerSceneTransition.Instance.Move2Scene(SceneType.InGameBagEdit);
    }
}
