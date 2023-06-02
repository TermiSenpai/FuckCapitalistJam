using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerMovementConfig", menuName = "PlayerConfig/Player Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    [Tooltip("Velocidad de movimiento del jugador")]
    public float movementSpeed;
}
