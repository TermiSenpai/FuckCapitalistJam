using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float movementSpeed;

    [Header("Needs")]
    [SerializeField] private CharacterController controller;
    private Vector2 curMovementInput;
   

    private void LateUpdate()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= movementSpeed;
        dir.y = controller.velocity.y;
        controller.Move(dir * Time.deltaTime);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                curMovementInput = context.ReadValue<Vector2>();
                break;

            case InputActionPhase.Canceled:
                curMovementInput = Vector2.zero;
                break;
        }
    }
}
