using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerAttackConfig", menuName ="Player/Player Attack Config")]
public class PlayerAttackConfig : ScriptableObject
{
    [Header("Basic config")]
    [Tooltip("Distancia a la que el jugador puede golpear")]
    public float attackDistance = 3f;
    [Tooltip("Tiempo entre el que termina un ataque y empieza el siguiente")]
    public float attackDelay = 0.2f;
    [Tooltip("Velocidad de la animaci�n")]
    public float attackSpeed = 1f;

    [Header("Destructible layers")]
    [Tooltip("Layer con la que se interactuar� durante el ataque")]
    public LayerMask attackLayer;

    [Header("Effects")]
    [Tooltip("Efecto del golpe que ser� visible en el momento del impacto")]
    public GameObject hitEffect;
    [Tooltip("Sonido realizado durante la animaci�n")]
    public AudioClip swing;
    [Tooltip("Sonido al impactar")]
    public AudioClip hitSound;
}
