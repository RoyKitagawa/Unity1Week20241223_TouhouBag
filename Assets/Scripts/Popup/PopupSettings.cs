using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : PopupBase
{
    [SerializeField]
    Slider MasterSlider, SESlider, BGMSlider;
    [SerializeField]
    TextMeshProUGUI MasterPercentage, SEPercentage, BGMPercentage;

    protected override void ShowPopup()
    {
        base.ShowPopup();
        SetSliderValue(MasterSlider, MasterPercentage, PlayerPrefs.GetInt(Consts.PlayerPrefs.Keys.VolumeMaster, 100));
        SetSliderValue(SESlider, SEPercentage, PlayerPrefs.GetInt(Consts.PlayerPrefs.Keys.VolumeSE, 100));
        SetSliderValue(BGMSlider, BGMPercentage, PlayerPrefs.GetInt(Consts.PlayerPrefs.Keys.VolumeBGM, 100));
    }

    public void OnMasterSliderUpdate()
    {
        MasterPercentage.text = GetPercentageText(MasterSlider);
    }

    public void OnSESliderUpdate()
    {
        SEPercentage.text = GetPercentageText(SESlider);
    }

    public void OnBGMSliderUpdate()
    {
        BGMPercentage.text = GetPercentageText(BGMSlider);
    }

    public void OnClickConfirm()
    {
        PlayerPrefs.SetInt(Consts.PlayerPrefs.Keys.VolumeMaster, GetPercentage(MasterSlider));
        PlayerPrefs.SetInt(Consts.PlayerPrefs.Keys.VolumeSE, GetPercentage(SESlider));
        PlayerPrefs.SetInt(Consts.PlayerPrefs.Keys.VolumeBGM, GetPercentage(BGMSlider));
        PlayerPrefs.Save();
        HidePopup();
    }

    public void OnClickCancel()
    {
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

    private void SetSliderValue(Slider slider, TextMeshProUGUI text, int percentage)
    {
        slider.value = slider.maxValue * percentage / 100;
        text.text = GetPercentageText(slider);
    }
}
