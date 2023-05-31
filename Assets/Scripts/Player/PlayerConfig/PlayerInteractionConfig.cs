using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInteractionConfig", menuName = "Config/Player Interaction Config")]
public class PlayerInteractionConfig : ScriptableObject
{
    public float checkRate = 0.05f;
    public float maxCheckDistance = 2;
    public LayerMask InteractuableLayers;
}
