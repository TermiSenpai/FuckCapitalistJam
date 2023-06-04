using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour
{
    [SerializeField] Button computerBTN;

    public void enableComputerCanvas()
    {
        computerBTN.enabled = true;
    }

}
