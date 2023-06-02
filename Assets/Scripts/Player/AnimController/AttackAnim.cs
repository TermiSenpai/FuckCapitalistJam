using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackAnim : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayerAnimController playerAnimController;

    [Header("Attacking")]
    [SerializeField] PlayerAttackConfig config;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount = 0;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(config.swing);

        if (attackCount == 0)
        {
            playerAnimController.ChangeAnimationState(playerAnimController.ATTACK1);
            attackCount++;
        }
        else
        {
            playerAnimController.ChangeAnimationState(playerAnimController.ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, config.attackDistance, config.attackLayer))
        {
            //HitTarget(hit.point);

            Debug.Log(hit.collider.name);
            if (hit.transform.TryGetComponent<BrokeItem>(out BrokeItem T))
            { T.breakItem(transform.forward); }


        }
    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(config.hitSound);

        GameObject GO = Instantiate(config.hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Attack();
                break;
        }
    }

}

