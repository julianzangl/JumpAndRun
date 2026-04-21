using System;
using Unity.VisualScripting;
using UnityEngine;

public class SawController : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float damagePerSecond = 10f;

    [Header("Spinning")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Vector3 spinAxis = Vector3.forward;

    [Header("Audio")]
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip cuttingSound;
    private AudioSource audioSource;

    [Header("Particles")]
    [SerializeField] private ParticleSystem sparklingParticles;

    private bool isCutting = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;


        sparklingParticles.Stop();
    }

    private void Start()
    {
        if (idleSound != null)
        {
            audioSource.clip = idleSound;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetState(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                character.TakeDamage(damagePerSecond * Time.fixedDeltaTime);
            }
        }
    }
    private void SetState(bool newState)
    {
        if (isCutting == newState)
            return;

        if (newState)
        {
            isCutting = true;
            audioSource.clip = cuttingSound;
            audioSource.Play();
            sparklingParticles.Play();
        }
        else
        {
            isCutting = false;
            audioSource.clip = idleSound;
            audioSource.Play();
            sparklingParticles.Stop();
        }
    }

    void Update()
    {
        transform.Rotate(spinAxis, rotationSpeed * Time.deltaTime);
    }
}
