using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : PopupBase
{
    [SerializeField]
    Slider MasterSlider, SESlider, BGMSlider;
    [SerializeField]
    TextMeshProUGUI MasterPercentage, SEPercentage, BGMPercentage;

    float defaultMasterVolume, defaultSEVolume, defaultBGMVolume;

    protected override void ShowPopup()
    {
        base.ShowPopup();
        defaultMasterVolume = PlayerPrefs.GetFloat(Consts.PlayerPrefs.Keys.VolumeMaster, 1.0f);
        SetSliderValue(MasterSlider, MasterPercentage, defaultMasterVolume);

        defaultSEVolume = PlayerPrefs.GetFloat(Consts.PlayerPrefs.Keys.VolumeSE, 1.0f);
        SetSliderValue(SESlider, SEPercentage, defaultSEVolume);

        defaultBGMVolume = PlayerPrefs.GetFloat(Consts.PlayerPrefs.Keys.VolumeBGM, 1.0f);
        SetSliderValue(BGMSlider, BGMPercentage, defaultBGMVolume);
    }

    public void OnMasterSliderUpdate()
    {
        // ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipScrollSlider);
        MasterPercentage.text = GetPercentageText(MasterSlider);
        ManagerGame.Instance.SetVolume(MasterSlider.value, SESlider.value, BGMSlider.value);
    }

    public void OnSESliderUpdate()
    {
        // ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipScrollSlider);
        SEPercentage.text = GetPercentageText(SESlider);
        ManagerGame.Instance.SetVolume(MasterSlider.value, SESlider.value, BGMSlider.value);
    }

    public void OnBGMSliderUpdate()
    {
        // ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipScrollSlider);
        BGMPercentage.text = GetPercentageText(BGMSlider);
        ManagerGame.Instance.SetVolume(MasterSlider.value, SESlider.value, BGMSlider.value);
    }

    public void OnClickConfirm()
    {
        ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipButtonClickOK);
        ManagerGame.Instance.SetVolume(MasterSlider.value, SESlider.value, BGMSlider.value);
        PlayerPrefs.SetFloat(Consts.PlayerPrefs.Keys.VolumeMaster,  MasterSlider.value);
        PlayerPrefs.SetFloat(Consts.PlayerPrefs.Keys.VolumeSE, SESlider.value);
        PlayerPrefs.SetFloat(Consts.PlayerPrefs.Keys.VolumeBGM, BGMSlider.value);
        PlayerPrefs.Save();

        HidePopup();
    }

    public void OnClickCancel()
    {
        ManagerSE.Instance.PlaySE(ManagerSE.Instance.ClipButtonClickCancel);
        ManagerGame.Instance.SetVolume(defaultMasterVolume, defaultSEVolume, defaultBGMVolume);
        HidePopup();
    }

    private string GetPercentageText(Slider slider)
    {
        return string.Format("{0}%", GetPercentage(slider));
    }

    private int GetPercentage(Slider slider)
    {
        return (int)(slider.value * 100 / slider.maxValue);
    }

    private void SetSliderValue(Slider slider, TextMeshProUGUI text, float value)
    {
        slider.value = value;
        text.text = GetPercentageText(slider);
    }
}
