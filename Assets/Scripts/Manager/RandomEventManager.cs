using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomEventManager : MonoBehaviour
{
    [SerializeField] private RandomEventConfig config;
    private AudioSource source;
    private float secondsBeforeEvent;
    private AudioClip selectedClip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        //selectedClip = randomClip();
        secondsBeforeEvent = config.secondsBeforeFirstEvent;
    }

    private void Update()
    {
        secondsBeforeEvent -= Time.deltaTime;

        if (secondsBeforeEvent <= 0)
        {
            playEvent();
        }
    }

    private void playEvent()
    {
        //source.PlayOneShot(selectedClip);


        OnPlayEvent();
    }

    private void OnPlayEvent()
    {
        secondsBeforeEvent = randomSeconds();
        //selectedClip = randomClip();
        PlayerStress.Stress += config.increaseStress;
    }

    private AudioClip randomClip()
    {
        int index = Random.Range(0, config.audioEvents.Count);
        return config.audioEvents[index];
    }

    private float randomSeconds() { return Random.Range(config.minSeconds, config.maxSeconds); }
}
