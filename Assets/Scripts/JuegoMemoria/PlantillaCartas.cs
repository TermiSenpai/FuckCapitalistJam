using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ModeloCarta", menuName = "PlantillaCarta")]

public class PlantillaCartas : ScriptableObject
{
    public Sprite cardImage;
    public Sprite cardRevers;
    [Space(10)]
    public bool clicked;
}
