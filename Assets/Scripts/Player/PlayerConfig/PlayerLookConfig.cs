using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLookConfig", menuName = "PlayerConfig/Player Look Config")]
public class PlayerLookConfig : ScriptableObject
{
    [Tooltip("Rotaci�n en grados m�nimo en el eje vertical"), Range(-90f, 0f)]
    public float minYLook = -85f;
    [Tooltip("Rotaci�n en grados m�ximo en el eje vertical"), Range(0f, 90f)]
    public float maxYLook = 85f;
    [Tooltip("Sensibilidad de la c�mara"), Range(0.1f, 1f)] public float lookSensityivity = 0.3f;
}
