using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BreackableItem", menuName = "Config/New breakable item")]
public class BreakableConfig : ScriptableObject
{
    [Tooltip("Fuerza aplicada al recibir un golpe. El tamaño y peso del objeto es relativo")]
    public float knockbackForce;
    [Tooltip("Sonido que se aplicará cuando se rompa el objeto")]
    public AudioClip onBreakSound;
    [Tooltip("Radio de una esfera invisible que al hacer contacto con otra parte, aplicará otro knockback en cadena")]
    public float knockbackOtherRadius;
}
