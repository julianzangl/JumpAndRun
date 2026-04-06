using System;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private Lever lever;

    [SerializeField]
    private MovingPlatform[] movingPlatforms;

    private CharacterController controller;


    private void RespawnCharacter()
    {
        controller = character.GetComponent<CharacterController>();
        controller.enabled = false;
        character.transform.position = respawnPoint.position;
        controller.enabled = true;
        lever.ResetLever();
        foreach (var platform in movingPlatforms)
        {
            platform.ResetPlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RespawnCharacter();
    }
}
