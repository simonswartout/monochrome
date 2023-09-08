using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private bool onGround;
    [SerializeField] private float friction;


    private void OnCollisionEnter2D(Collision2D other)
    {
        EvaluateCollision(other);
        RetrieveFriction(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EvaluateCollision(other);
        RetrieveFriction(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        onGround = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!onGround)
        {
            friction = 0f;
        }
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;

            // Check if the collision is from the top or bottom of the platform
            if (normal.y >= 0.9f)
            {
                onGround = true;
            }
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        if (onGround)
        {
            PhysicsMaterial2D material = collision.collider.sharedMaterial;

            if (material != null)
            {
                friction = material.friction;
            }
        }
    }



    public bool GetOnGround()
    {
        return onGround;
    }

    public float GetFriction()
    {
        return friction;
    }
}
