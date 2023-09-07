using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermeablePlatform : MonoBehaviour
{
    [SerializeField] private Collider2D platformCollider;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private float footOffset = 1f;

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
    }
    
    private void FixedUpdate()
    {
        playerPosition = Game.Instance.GetPlayerPosition();
        
        
        if (playerPosition.y > transform.position.y + footOffset)
        {
            platformCollider.enabled = true;
        }
        else
        {
            platformCollider.enabled = false;
        }
    }

    private void Update()
    {

    }
    

}
