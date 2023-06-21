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
    [SerializeField] private AttackAnim attackAnim;

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
        if (barAnim != null)
            StopCoroutine(barAnim);


        if (PlayerStress.Stress < stressSlider.value)
            decreaseStress();
        else if (PlayerStress.Stress > stressSlider.value)
            increaseStress();

        checkStress();
    }

    public void increaseStress()
    {
        barAnim = StartCoroutine(increaseStressBarAnim(PlayerStress.Stress));
    }

    public void decreaseStress()
    {
        barAnim = StartCoroutine(decreaseStressBarAnim(PlayerStress.Stress, speedValue));
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
            stressSlider.value -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator decreaseStressBarAnim(float newValue, float speed)
    {
        while (stressSlider.value > newValue)
        {
            stressSlider.value -= 0.5f;
            yield return new WaitForSeconds(speed);
        }
    }

    public void checkStress()
    {
        if (PlayerStress.Stress >= 100)
        {
            stressSlider.maxValue = PlayerStress.Stress;
            stressSlider.value = PlayerStress.Stress;
        }

        else if (PlayerStress.Stress <= 0 && stressSlider.value <= 0)
        {
            stopFuriaMode();
        }
    }

    public void startFuriaMode()
    {
        speedValue = 0.1f;
        PlayerStress.isFuriaMode = true;
        PlayerStress.Stress = 0;
        barAnim = StartCoroutine(decreaseStressBarAnim(PlayerStress.Stress));
        PlayerStress.canModify = false;
        attackAnim.enabled = true;
    }

    private void stopFuriaMode()
    {
        PlayerStress.isFuriaMode = false;
        speedValue = 0.02f;
        PlayerStress.canModify = true;
        attackAnim.enabled = false;
        stressSlider.maxValue = 100;
    }
}
