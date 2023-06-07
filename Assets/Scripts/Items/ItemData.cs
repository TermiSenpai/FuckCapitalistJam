using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "New Interactuable Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;

}
