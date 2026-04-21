using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnTrigger : MonoBehaviour
{

    [SerializeField]
    private Transform respawnPoint;

    [SerializeField]
    private GameObject character;

    [SerializeField]
    private Lever lever;

    [SerializeField]
    private Key key;

    [SerializeField]
    private Stopwatch stopwatch;
    [SerializeField]
    private Chest chest;
    
    [SerializeField]
    private MovingPlatform[] movingPlatforms;
   
    private InputAction respawnAction;
    private CharacterController controller;


    private void RespawnCharacter()
    {
        controller = character.GetComponent<CharacterController>();
        controller.enabled = false;
        character.transform.position = respawnPoint.position;
        controller.enabled = true;
        character.GetComponent<Character>().ResetCharacter();
        character.GetComponent<Character>().ResetHealth();
    }

    private void ResetEverything()
    {
        RespawnCharacter();
        lever.ResetLever();
        key.ResetKey();
        chest.ResetChest();
        stopwatch.ResetTimer();
        foreach (var platform in movingPlatforms)
        {
            platform.ResetPlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            ResetEverything();
        }
    }

    private void Start()
    {
        respawnAction = InputSystem.actions.FindAction("Respawn");
    }

    void FixedUpdate()
    {
        if (respawnAction.WasPressedThisFrame())
        {
            ResetEverything();
        }
    }
}
