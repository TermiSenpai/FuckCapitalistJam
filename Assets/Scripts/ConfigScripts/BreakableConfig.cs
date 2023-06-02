using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BreackableItem", menuName = "Config/New breakable item")]
public class BreakableConfig : ScriptableObject
{
    [Tooltip("Fuerza aplicada al recibir un golpe. El tama�o y peso del objeto es relativo")]
    public float knockbackForce;
    [Tooltip("Sonido que se aplicar� cuando se rompa el objeto")]
    public AudioClip onBreakSound;
    [Tooltip("Radio de una esfera invisible que al hacer contacto con otra parte, aplicar� otro knockback en cadena")]
    public float knockbackOtherRadius;
    [Tooltip("Tiempo antes de empezar a desaparecer")]
    public float timeBeforeDisapear = 3;
}
