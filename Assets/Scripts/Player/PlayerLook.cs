using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Needs")]
    [SerializeField] Transform cameraContainer;
    [Header("Config")]
    [SerializeField] private PlayerLookConfig config;
    private float camCurXRot;

    private Vector2 mouseDelta;



    private void LateUpdate()
    {
        cameraLook();
    }

    private void cameraLook()
    {
        camCurXRot += mouseDelta.y * config.lookSensityivity;
        camCurXRot = Mathf.Clamp(camCurXRot, config.minYLook, config.maxYLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * config.lookSensityivity, 0);

    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
