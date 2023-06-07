using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public List<AudioClip> audioEvents;
    public float secondsBeforeFirstEvent;

    public float minSeconds = 15f;
    public float maxSeconds = 60f;

    [SerializeField] private AudioSource source;
    private float secondsBeforeEvent;
    private AudioClip selectedClip;

    private void Start()
    {
        selectedClip = randomClip();
        secondsBeforeEvent = secondsBeforeFirstEvent;
    }

    private void Update()
    {
        secondsBeforeEvent -= Time.deltaTime;
        
        if(secondsBeforeEvent <= 0)
        {
            playEvent();
        }
    }

    private void playEvent()
    {
        source.PlayOneShot(selectedClip);


        OnPlayEvent();
    }

    private void OnPlayEvent()
    {
        secondsBeforeEvent = randomSeconds();
        selectedClip = randomClip();
    }

    private AudioClip randomClip()
    {
        int index = Random.Range(0, audioEvents.Count);
        return audioEvents[index];
    }

    private float randomSeconds() { return Random.Range(minSeconds, maxSeconds); }
}
