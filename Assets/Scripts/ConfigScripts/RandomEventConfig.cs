using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RandomEventConfig", menuName ="Config/NewRandomEvents")]
public class RandomEventConfig : ScriptableObject
{
    [Tooltip("Posibles opciones de audio que se podrán escuchar")]
    public List<AudioClip> audioEvents;
    [Tooltip("Segundos antes del primer evento")]
    public float secondsBeforeFirstEvent;
    [Tooltip("Cantidad fija que aumentará el estres al escuchar el audio")]
    public float increaseStress = 15;
    [Tooltip("Cantidad minima antes del siguiente evento")]
    public float minSeconds = 15f;
    [Tooltip("Cantidad maxima antes del siguiente evento")]
    public float maxSeconds = 60f;
}
