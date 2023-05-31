using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractuable
{
    [SerializeField] private ItemData item;

    public virtual string GetInteractPromt()
    {
        return string.Format($"Interact {item.displayName}");
    }

    public void OnInteract()
    {
        //Temporal
        Destroy(gameObject);
    }
}
