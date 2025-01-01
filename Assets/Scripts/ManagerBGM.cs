using System;
using DG.Tweening;
using UnityEngine;

public class ManagerBGM : MonoBehaviourSingleton<ManagerBGM>
{
    public AudioSource BGMSource;
    [SerializeField]
    public AudioClip ClipMainBGM;

    public void Start()
    {
        SetIsDontDestroyOnLoad(true);
        // ロードはBGM側で担当
        ManagerGame.Instance.LoadVolume();
        SetVolume(ManagerGame.Instance.GetVolumeBGM());
        // BGM側でSEの初期音量もセットする
        ManagerSE.Instance.SetVolume(ManagerGame.Instance.GetVolumeSE());

        // メインBGM再生（共通BGM）
        PlayBGM(ClipMainBGM);
    }
    public void FadeBGM(float duration, Action onComplete)
    {
        BGMSource.DOFade(0.0f, duration).OnComplete(() => { onComplete?.Invoke(); }).SetUpdate(true);
    }

    public void SetVolume(float volume)
    {
        BGMSource.volume = volume;
    }

    public bool IsPlaying()
    {
        return BGMSource.isPlaying;
    }

    public void PlayBGM(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.loop = true;
        BGMSource.Play();
    }

    public void StopBGM()
    {
        BGMSource.Stop();
    }
}
