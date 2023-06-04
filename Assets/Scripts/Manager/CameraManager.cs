using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera currentCamera;
    [SerializeField] private CinemachineVirtualCamera mainCamera;

    private void Start()
    {
        currentCamera = mainCamera;
    }

    public void switchCam(CinemachineVirtualCamera newCam)
    {
        currentCamera.Priority = 0;
        currentCamera = newCam;
        currentCamera.Priority = 10;
    }
    public void switchCam()
    {
        currentCamera.Priority = 0;
        currentCamera = mainCamera;
        currentCamera.Priority = 10;
    }
}
