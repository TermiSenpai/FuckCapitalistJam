using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerObject : ItemObject
{
    [SerializeField] CinemachineVirtualCamera computerCamera;
    CameraManager camManager;
    PlayerManager playerManager;

    private void Start()
    {
        camManager = FindObjectOfType<CameraManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public override void OnInteract()
    {
        gameObject.layer = 0;
        camManager.switchCam(computerCamera);
        playerManager.EnablePlayerInputs(false);
        playerManager.changeCursorState(CursorLockMode.None);
    }
}
