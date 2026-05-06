using UnityEngine;

public class RunDustParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private float emissionRate = 20f;

    private CharacterController controller;
    private ParticleSystem.EmissionModule emission;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        emission = dustParticles.emission;
        emission.rateOverTime = 0f;
    }

    void Update()
    {
        bool isMovingOnGround = controller.isGrounded && controller.velocity.magnitude > 0.5f;
        emission.rateOverTime = isMovingOnGround ? emissionRate : 0f;
    }
}
