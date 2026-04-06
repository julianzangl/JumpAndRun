using System;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField]
    private GameObject lid;

    [SerializeField]
    private Transform openPosition;
    [SerializeField]
    private Transform closedPosition;
    [SerializeField]
    private Stopwatch stopwatch;
    [SerializeField]
    private TMP_Text hint;

    private bool isOpen = false;

    public void OpenChest()
    {
        if (!isOpen)
        {
            lid.transform.SetPositionAndRotation(openPosition.position, openPosition.rotation);
            isOpen = true;
            stopwatch.StopTimer();
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

    public void ResetChest()
    {
        CloseChest();
        if (hint != null)
        {
            hint.text = "You need a key to open the chest!";
            hint.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (other.gameObject.GetComponent<Character>().HasKey())
            {
                OpenChest();
                if (hint != null) {
                    hint.text = "You opened the chest! Time: " + stopwatch.GetElapsedTime().ToString("F2") + " seconds";
                    hint.gameObject.SetActive(true);
                }
            }
            else
            {
                if (hint != null) hint.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (hint != null) hint.gameObject.SetActive(false);
        }
    }
}
