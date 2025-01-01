using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerSE : MonoBehaviourSingleton<ManagerSE>
{
//    public AudioSource SESource;
    public AudioClip
        ClipButtonClickOK, // ボタンクリックポジティブ
        ClipButtonClickCancel, // ボタンクリックネガティブ
        ClipButtonClickError, // ボタンクリックネガティブ
        ClipPurchaseItem, // アイテム購入
        ClipEvolveItem, // アイテム購入
        ClipGrabItem, // アイテム掴む
        ClipReleaseItem, // アイテムを離す
        ClipPlaceItem, // アイテムを設置
        ClipScrollSlider, // Slider値変更
        ClipSceneTransition, // シーン遷移
        ClipDamageHit, // ダメージ発生時
        ClipDamageHitHeavy, // ダメージ発生時
        ClipWeaponThrow,
        ClipWeaponBounce, // 武器落下時
        ClipWeaponBombExplode, // 爆発時
        ClipWeaponCanonShot, // キャノン発射
        ClipWeaponCucumber, // きゅうり
        ClipWeaponDriver, // ドライバー
        ClipWeaponGlove, // 手袋
        ClipWeaponScrew, // ねじ
        ClipWeaponSpanner, // スパナ
        ClipWaveClear, // ウェーブクリア
        ClipGameOver, // GameOver
        ClipGameClear, // Game Clear
        ClipFireFlowerLaunch, // 花火種射出
        ClipFireFlowerExplode, // 花火展開
        ClipCharacterNormalDead,
        ClipCharacterBossDead,
        ClipCharacterPlayerDead;

    private HashSet<AudioSource> audiorSources = new HashSet<AudioSource>();
    private float volume = 1.0f;

    public void Start()
    {
        SetIsDontDestroyOnLoad(true);
        // 初期音量設定はBGM側で行う
    }

    public void SetVolume(float volume)
    {
        volume = 1.0f;
    }

    public void PlaySE(AudioClip clip, float volumeScale = 1.0f)
    {
        if(clip == null) return;
        GetAudioSource().PlayOneShot(clip, volume * volumeScale);
    }

    private AudioSource GetAudioSource()
    {
        foreach(AudioSource source in audiorSources)
        {
            if(!source.isPlaying) return source;
        }
        AudioSource audioSource = transform.AddComponent<AudioSource>();
        audiorSources.Add(audioSource);
        return audioSource;
    }
}
