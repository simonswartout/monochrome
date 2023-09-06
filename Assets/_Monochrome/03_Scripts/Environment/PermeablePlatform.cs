using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermeablePlatform : MonoBehaviour
{
    [SerializeField] private Collider2D platformCollider;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovementController player))
        {
            platformCollider.isTrigger = false;
        }
    } 
}
