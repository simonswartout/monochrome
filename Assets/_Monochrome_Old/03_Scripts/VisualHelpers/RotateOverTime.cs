using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    public enum RotateAxis
    {
        X,
        Y,
        Z
    }

    [SerializeField] private RotateAxis rotateAxis;
    [SerializeField] private BtnClickInteractable buttonClickInteractable;
    [SerializeField] private bool stopAtMaxSpeed;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float targetSpeed = 1f;
    [SerializeField] private float timeToReachTargetSpeed = 1f;

    private void Update()
    {
        if (buttonClickInteractable.IsActivated)
        {
            SlerpRotationSpeed();
        }
        Rotate();
    }

    public void Rotate()
    {
        if(stopAtMaxSpeed && rotateSpeed >= targetSpeed)
        {
            return;
        }

        switch (rotateAxis)
        {
            case RotateAxis.X:
                transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
                break;
            case RotateAxis.Y:
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                break;
            case RotateAxis.Z:
                transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    public void SlerpRotationSpeed()
    {
        StartCoroutine(SlerpRotationSpeedCoroutine(targetSpeed, timeToReachTargetSpeed));
    }

    private IEnumerator SlerpRotationSpeedCoroutine(float targetSpeed, float timeToReachTargetSpeed)
    {
        float elapsedTime = 0f;
        float startSpeed = rotateSpeed;

        while (elapsedTime < timeToReachTargetSpeed)
        {
            rotateSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / timeToReachTargetSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rotateSpeed = targetSpeed;
    }
  
}
