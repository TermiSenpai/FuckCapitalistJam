using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInteractionConfig config;

    private float lastCheckTime;
    private GameObject curInteractGameObject;
    private IInteractuable curInteractuable;

    [SerializeField] private TextMeshProUGUI promptText;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // True every checkRate seconds
        if(Time.time - lastCheckTime > config.checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, config.maxCheckDistance, config.InteractuableLayers)) 
            {
                if(hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractuable = hit.collider.GetComponent<IInteractuable>();
                    SetPromptText();
                }
            }
            else
            {
                NullInteractuable();
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[F]</b> {0}", curInteractuable.GetInteractPromt());
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        switch (context.phase) 
        {
            case InputActionPhase.Started:
                if(curInteractuable != null)
                {
                    curInteractuable.OnInteract();
                    NullInteractuable();
                }
                break;
        }
    }

    private void NullInteractuable()
    {
        curInteractuable = null;
        curInteractGameObject = null;
        promptText.gameObject.SetActive(false);
    }
}
