using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    [SerializeField] Slider stressSlider;

    public float debugValue;
    [Tooltip("Valor m�s bajo, m�s velocidad"), Range(0f, 0.1f)]
    public float speedValue;
    Coroutine barAnim;

    private void Start()
    {
        changeStress(PlayerStress.Stress);
    }

    private void OnEnable()
    {
        PlayerStress.ValueChanged += onStressChanged;
    }

    private void OnDisable()
    {
        PlayerStress.ValueChanged -= onStressChanged;
    }

    public void changeStress(float value)
    {
        stressSlider.value = value;
    }

    private void onStressChanged(object sender, EventArgs e)
    {
        if(barAnim != null)
            StopCoroutine(barAnim);       

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
        while (stressSlider.value < newValue)
        {
            stressSlider.value += 0.5f;
            yield return new WaitForSeconds(speedValue);
        }
    }


    IEnumerator decreaseStressBarAnim(float newValue)
    {
        while (stressSlider.value > newValue)
        {
            stressSlider.value -= 0.5f;
            yield return new WaitForSeconds(speedValue);
        }
    }

    public void checkStress()
    {
        if(stressSlider.value >= 100)
        {
            speedValue = 0.1f;
            PlayerStress.Stress = 0;
            PlayerStress.canModify = false;
        }

        else if(stressSlider.value <= 0)
        {
            speedValue = 0.02f;
            PlayerStress.canModify = true;
        }
    }
}
