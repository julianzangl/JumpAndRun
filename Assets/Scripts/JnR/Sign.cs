using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.UIElements;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private LocalizedString dialogueText;

    private bool canInteract;
    private InputAction inputAction;


    private void Start()
    {
        inputAction = InputSystem.actions.FindAction("Attack");
        inputAction.performed += ToggleDialogueBox;

        this.dialogueBox.SetActive(false);
        this.canInteract = false;
    }

    private void ToggleDialogueBox(InputAction.CallbackContext context)
    {
        if (canInteract)
        {

            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);
            }
            else
            {
                dialogueBox.SetActive(true);
                var uiDocument = dialogueBox.GetComponent<UIDocument>();
                var label = uiDocument.rootVisualElement.Q<Label>();
                label.text = dialogueText.GetLocalizedString();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        canInteract = false;
        dialogueBox.SetActive(false);
    }
}
