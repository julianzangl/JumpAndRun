using System;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField]
    private GameObject lid;

    [SerializeField]
    private Transform openPosition;
    [SerializeField]
    private Transform closedPosition;
    private bool isOpen = false;

    public void OpenChest()
    {
        if (!isOpen)
        {
            lid.transform.SetPositionAndRotation(openPosition.position, openPosition.rotation);
            isOpen = true;
        }
    }

    public void CloseChest()
    {
        if (isOpen)
        {
            lid.transform.SetPositionAndRotation(closedPosition.position, closedPosition.rotation);
            isOpen = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (other.gameObject.GetComponent<Character>().HasKey())
            {
                OpenChest();
            }
        }
    }
}
