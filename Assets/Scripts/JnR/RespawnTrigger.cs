using System;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private GameObject character;

    private CharacterController controller;


    private void RespawnCharacter()
    {
        controller = character.GetComponent<CharacterController>();
        controller.enabled = false;
        character.transform.position = respawnPoint.position;
        controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        RespawnCharacter();
    }
}
