using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInteractionConfig", menuName = "PlayerConfig/Player Interaction Config")]
public class PlayerInteractionConfig : ScriptableObject
{
    [Tooltip("Tasa de refresco de comprobaci�n")]
    public float checkRate = 0.05f;
    [Tooltip("Distancia m�xima de comprobaci�n")]
    public float maxCheckDistance = 2;
    [Tooltip("Layers interactuables. Admite varias simultaneamente")]
    public LayerMask InteractuableLayers;
}
