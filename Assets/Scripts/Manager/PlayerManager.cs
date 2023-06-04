using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerInput inputs;

    public void EnablePlayerInputs(bool value)
    {
        inputs.enabled = value;
    }


    private void Start()
    {
        changeCursorState(CursorLockMode.Locked);
    }

    public void changeCursorState(CursorLockMode value)
    {
        Cursor.lockState = value;
    }

    public void changeCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
