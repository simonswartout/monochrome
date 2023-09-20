using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    [SerializeField] private float timeToOpen;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private BtnClickInteractable pairedMechanism;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && pairedMechanism.IsActivated)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    // Lerp the position of the door to the target position
    public void OpenDoor()
    {
        StartCoroutine(MoveDoorCoroutine(startPosition, targetPosition, timeToOpen));
    }

    public void CloseDoor()
    {
        StartCoroutine(MoveDoorCoroutine(transform.position, startPosition, timeToOpen));
    }

    private IEnumerator MoveDoorCoroutine(Vector2 StartPosition, Vector2 TargetPosition, float TimeToOpen)
    {
        float elapsedTime = 0f;

        while (elapsedTime < TimeToOpen)
        {
            transform.position = Vector2.Lerp(StartPosition, TargetPosition, elapsedTime / TimeToOpen);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = TargetPosition;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, 0.5f);
    }
}
