using DG.Tweening;
using UnityEngine;

public class ManagerTitle : MonoBehaviourSingleton<ManagerTitle>
{
    public void Start()
    {
        ManagerBGM.Instance.SetVolume(ManagerGame.Instance.GetVolumeBGM());
        ManagerBGM.Instance.PlayBGM(ManagerBGM.Instance.ClipMainBGM);

        // 進捗データを消す
        ManagerGame.Instance.ResetAllData();
    }

    public void OnClickSettings()
    {
        ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipButtonClickOK);
        PopupBase.Show(PopupType.Settings);
    }

    public void OnClickTutorialStart()
    {
        ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipButtonClickOK);
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
        ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipSceneTransition);
        ManagerSceneTransition.Instance.Move2Scene(SceneType.InGameBagEdit);
    }
}
