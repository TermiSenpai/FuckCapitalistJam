using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractuable
{
    [SerializeField] private ItemData item;
    [SerializeField] protected bool isInteractuable = true;

    public virtual string GetInteractPromt()
    {
        if (isInteractuable)
            return string.Format($"Interact {item.displayName}");
        return string.Empty;
    }

    public virtual void OnInteract()
    {
        //Temporal
        Destroy(gameObject);
    }
}
