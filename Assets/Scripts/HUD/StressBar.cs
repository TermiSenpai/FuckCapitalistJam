using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    [SerializeField] Slider stressSlider;

    public float debugValue;
    [Tooltip("Valor más bajo, más velocidad"),Range(0f, 0.1f)]
    public float speedValue;
    Coroutine barAnim;

    private void Start()
    {
        changeStress(PlayerStress.stress);
    }

    public void changeStress(float value)
    {
        stressSlider.value = value;
    }


    public void increaseStress(float value)
    {
        barAnim = StartCoroutine(increaseStressBarAnim(PlayerStress.stress));
    }

    public void decreaseStress(float value)
    {
        barAnim = StartCoroutine(decreaseStressBarAnim(PlayerStress.stress));
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
