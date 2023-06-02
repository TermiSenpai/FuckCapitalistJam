using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGravity : MonoBehaviour
{
    private const float gravity = -15f;
    [SerializeField] private LayerMask groundLayer;

    CharacterController controller;

    Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        applyGravity();
    }

    private void applyGravity()
    {
        if (isGrounded() && velocity.y < 0)
            velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 
    }

    public bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f)  + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f)    + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f)   + (Vector3.up * 0.01f), Vector3.down)
        };

        foreach (Ray r in rays)        
            if (Physics.Raycast(r, 0.1f, groundLayer))
                return true;
        
        return false;
    }
}
