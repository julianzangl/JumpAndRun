using UnityEngine;

public class JewelController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private AudioSource audioSource;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (audioSource != null)
            audioSource.Play();

        UIManager.Instance.TriggerVictory();

        // Disable the jewel visually
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
