using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLookConfig", menuName = "Config/Player Look Config")]
public class PlayerLookConfig : ScriptableObject
{
    public float minYLook = -85f;
    public float maxYLook = 85f;
    [Range(0.1f, 1f)] public float lookSensityivity = 0.3f;
}
