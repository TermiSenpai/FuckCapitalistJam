using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInteractionConfig", menuName = "PlayerConfig/Player Interaction Config")]
public class PlayerInteractionConfig : ScriptableObject
{
    [Tooltip("Tasa de refresco de comprobación")]
    public float checkRate = 0.05f;
    [Tooltip("Distancia máxima de comprobación")]
    public float maxCheckDistance = 2;
    [Tooltip("Layers interactuables. Admite varias simultaneamente")]
    public LayerMask InteractuableLayers;
}
