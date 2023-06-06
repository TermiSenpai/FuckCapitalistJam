using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    [SerializeField] Slider stressSlider;

    public float debugValue;
    [Range(0f, 0.1f)]
    public float speedValue;
    Coroutine barAnim;

    public void changeStress(float value)
    {
        stressSlider.value = value;
    }

    public void increaseStress(float value)
    {
        barAnim = StartCoroutine(increaseStressBarAnim(stressSlider.value + value));
    }

    public void decreaseStress(float value)
    {
        barAnim = StartCoroutine(decreaseStressBarAnim(stressSlider.value - value));
    }

    IEnumerator increaseStressBarAnim(float newValue)
    {
        if (newValue >= 100)
            newValue = 100;

        while (stressSlider.value < newValue)
        {
            stressSlider.value += 0.5f;
            yield return new WaitForSeconds(speedValue);
        }
    }


    IEnumerator decreaseStressBarAnim(float newValue)
    {
        if (newValue <= 0)
            newValue = 0;

        while (stressSlider.value > newValue)
        {
            stressSlider.value -= 0.5f;
            yield return new WaitForSeconds(speedValue);
        }
    }
}
