using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask InteractuableLayer;

    private GameObject curInteractGameObject;
    private IInteractuable curInteractuable;

    public TextMeshProUGUI promptText;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // True every checkRate seconds
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, maxCheckDistance, InteractuableLayer)) 
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
                curInteractuable = null;
                curInteractGameObject = null;
                promptText.gameObject.SetActive(false); 
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[F]</b> {0}", curInteractuable.GetInteractPromt());
    }
}
