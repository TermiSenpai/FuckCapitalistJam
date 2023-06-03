using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerObject : ItemObject
{
    [SerializeField] CinemachineVirtualCamera computerCamera;
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] PlayerInteraction interaction;

    public override void OnInteract()
    {
        switchCam();
    }

    private void switchCam()
    {
        computerCamera.Priority += 1;
        interaction.enabled = false;
    }
}
