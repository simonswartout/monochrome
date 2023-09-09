using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;   

public class BaseInteractable : MonoBehaviour
{
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private bool canInteract;
    [SerializeField] private float interactionThreshold = 1f;
    public UnityEvent interactEvent;

    protected virtual void Start()
    {
        canInteract = false;
    }

    protected virtual void Update()
    {
        playerPosition = Game.Instance.GetPlayerPosition();

        if (Vector2.Distance(playerPosition, transform.position) < interactionThreshold)
        {
            canInteract = true;
        }
    }
    protected virtual void OnInteract()
    {
        if(canInteract)
        {
            interactEvent.Invoke();
        }
    }


    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionThreshold);
    }
}
