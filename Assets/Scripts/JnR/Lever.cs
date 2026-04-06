using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Lever : MonoBehaviour
{
    private bool on = false;
    private bool interpolating = false;
    private float currentInterpolationTime = 0.0f;
    private bool playerInRange = false;
    private InputAction interactAction;
    [SerializeField]
    private float switchTime;
    [SerializeField]
    private Transform onPosition;
    [SerializeField]
    private Transform offPosition;
    [SerializeField]
    private GameObject leverHandle;
    [SerializeField]
    private TMP_Text interactHint;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            playerInRange = true;
            if (interactHint != null) interactHint.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            playerInRange = false;
            if (interactHint != null) interactHint.gameObject.SetActive(false);
        }
    }

    public void ResetLever()
    {
        on = false;
        leverHandle.transform.SetPositionAndRotation(offPosition.position, offPosition.rotation);
    }

    public bool IsOn()
    {
        return on;
    }

    IEnumerator InterpolateLeverCoroutine()
    {
        interpolating = true;
        Vector3 startPosition, targetPosition;
        Quaternion startRotation, targetRotation;
        if (on)
        {
            startPosition = offPosition.position;
            startRotation = offPosition.rotation;
            targetPosition = onPosition.position;
            targetRotation = onPosition.rotation;
        }
        else
        {
            startPosition = onPosition.position;
            startRotation = onPosition.rotation;
            targetPosition = offPosition.position;
            targetRotation = offPosition.rotation;
        }
        currentInterpolationTime = 0.0f;
        while (currentInterpolationTime < switchTime)
        {
            float percentage = currentInterpolationTime / switchTime;
            var currentPosition = Vector3.Lerp(startPosition, targetPosition, percentage);
            var currentRotation = Quaternion.Slerp(startRotation, targetRotation, percentage);
            leverHandle.transform.SetPositionAndRotation(currentPosition, currentRotation);
            yield return null;
            currentInterpolationTime += Time.deltaTime;
        }
        leverHandle.transform.SetPositionAndRotation(targetPosition, targetRotation);
        interpolating = false;
    }
    void ToggleLever()
    {
        on = !on;
        StartCoroutine(InterpolateLeverCoroutine());
    }

    void FixedUpdate()
    {
        if (interactAction.WasPressedThisFrame() && !interpolating && playerInRange)
        {
            ToggleLever();
        }
    }

}