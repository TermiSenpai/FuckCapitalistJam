using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BreackableItem", menuName = "Config/New breakable item")]
public class BreakableConfig : ScriptableObject
{
    public float knockbackForce;
    public AudioClip onBreakSound;
    public float knockbackOtherRadius;
}
