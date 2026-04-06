using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour
{
    [SerializeField]
    private TMP_Text interactHint;
    private bool keyCollected = false;
    private InputAction interactAction;
    private bool playerInRange = false;
    [SerializeField]
    private Character character;


    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == character.gameObject.layer)
        {
            playerInRange = true;
            if (interactHint != null && !keyCollected) interactHint.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == character.gameObject.layer)
        {
            playerInRange = false;
            if (interactHint != null && !keyCollected) interactHint.gameObject.SetActive(false);
        }
    }

    public void ResetKey()
    {
        keyCollected = false;
        playerInRange = false;
        if (interactHint != null) interactHint.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (interactAction.WasPressedThisFrame() && !keyCollected && playerInRange && character != null)
        {
            character.CollectKey();
            interactHint.gameObject.SetActive(false);
            keyCollected = true;
            gameObject.SetActive(false);
        }
    }

}
