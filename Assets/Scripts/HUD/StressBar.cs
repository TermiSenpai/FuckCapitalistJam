using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    [SerializeField] Slider stressSlider;

    public float debugValue;
    [Tooltip("Valor más bajo, más velocidad"), Range(0f, 0.1f)]
    public float speedValue;
    Coroutine barAnim;

    private void Start()
    {
        changeStress(PlayerStress.Stress);
    }

    public void changeStress(float value)
    {
        stressSlider.value = value;
    }

    private void OnEnable()
    {
        PlayerStress.ValueChanged += onStressChanged;
    }

    private void OnDisable()
    {
        PlayerStress.ValueChanged -= onStressChanged;
    }

    private void onStressChanged(object sender, EventArgs e)
    {
        if (PlayerStress.Stress < stressSlider.value)
            decreaseStress();
        else if (PlayerStress.Stress > stressSlider.value)
            increaseStress();
    }

    public void increaseStress()
    {
        barAnim = StartCoroutine(increaseStressBarAnim(PlayerStress.Stress));
    }

    public void decreaseStress()
    {
        barAnim = StartCoroutine(decreaseStressBarAnim(PlayerStress.Stress));
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
