using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerObject : ItemObject
{
    [SerializeField] CinemachineVirtualCamera computerCamera;
    [SerializeField] CameraManager camManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] ComputerManager computerManager;

    public override void OnInteract()
    {
        camManager.switchCam(computerCamera);
        playerManager.EnablePlayerInputs(false);
        playerManager.changeCursorState(CursorLockMode.None);
        //isInteractuable = false;
    }
}
