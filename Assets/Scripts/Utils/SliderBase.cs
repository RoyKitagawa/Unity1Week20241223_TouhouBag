using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SliderのPrefabに付与する管理クラス
/// Sliderに関する基本的な汎用クラス
/// </summary>
public class SliderBase : MonoBehaviour
{
    protected Slider slider;
    public delegate void OnSliderValueUpdate(float newValue, bool isMax, bool isEmpty);
    public OnSliderValueUpdate SliderValueUpdateHandler = null;

    // MAXを超えた値の管理用
    public float ExceedValue = 0.0f;

    public virtual void Awake()
    {
        slider = this.GetComponent<Slider>();
    }

    public void Initialize(float max, float current)
    {
        slider.maxValue = max;
        slider.value = current;
    }
    public float GetMaxValue() { return slider.maxValue; }
    public float GetCurrentValue() { return slider.value; }

    /// <summary>
    /// スライダーの最大値を変更する
    /// これにより現在値が変更される場合、変更分はフラグが正なら超過の値として記録される
    /// </summary>
    /// <param name="newValue"></param>
    public void SetMaxValue(float newValue, bool recordExceedAmt = false)
    {
        if(recordExceedAmt && slider.value > newValue) ExceedValue += slider.value - newValue;
        slider.maxValue = newValue;
    }

    /// <summary>
    /// スライダーの現在値を変更する
    /// 現在値が最大値を超えている場合、超過分はフラグが正なら超過の値として記録される
    /// </summary>
    /// <param name="newValue"></param>
    public void SetCurrentValue(float newValue, bool recordExceedAmt = false)
    {
        if(recordExceedAmt && newValue > slider.maxValue) ExceedValue += newValue - slider.maxValue;
        slider.value = newValue;
    }

    /// <summary>
    /// スライダーの値を減少する
    /// 0未満になった場合は0となる
    /// </summary>
    /// <param name="reduceAmt">減少分</param>
    public virtual void Reduce(float reduceAmt)
    {
        if(IsEmpty()) return;

        slider.value -= reduceAmt;
        SliderValueUpdateHandler?.Invoke(slider.value, IsMax(), IsEmpty());
    }

    /// <summary>
    /// スライダーの値を増加する
    /// 最大値を超えた場合、フラグが正なら超過分を記録する
    /// </summary>
    /// <param name="gainAmt">増加分</param>
    /// <param name="recordExceedAmt">超過分を記録するか</param>
    public virtual void Gain(float gainAmt, bool recordExceedAmt = true)
    {
        if(recordExceedAmt && slider.value + gainAmt > slider.maxValue) ExceedValue += slider.value + gainAmt - slider.maxValue;

        slider.value += gainAmt;
        SliderValueUpdateHandler?.Invoke(slider.value, IsMax(), IsEmpty());
    }

    public bool IsMax()
    {
        return slider.value >= slider.maxValue;
    }

    public bool IsEmpty()
    {
        return slider.value <= 0.0f;
    }
}