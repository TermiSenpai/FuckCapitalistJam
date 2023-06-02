using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerAttackConfig", menuName ="Player/Player Attack Config")]
public class PlayerAttackConfig : ScriptableObject
{
    public float attackDistance = 3f;
    public float attackDelay = 0.2f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swing;
    public AudioClip hitSound;
}
