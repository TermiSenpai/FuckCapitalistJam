using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform cameraContainer;
    [SerializeField] float minYLook = -85f;
    [SerializeField] float maxYLook = 85f;
    private float camCurXRot;

    [SerializeField, Range(0.1f, 1f)] float lookSensityivity = 0.3f;
    private Vector2 mouseDelta;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        cameraLook();
    }

    private void cameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensityivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minYLook, maxYLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensityivity, 0);

    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
