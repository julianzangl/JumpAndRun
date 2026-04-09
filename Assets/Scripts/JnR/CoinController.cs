using UnityEngine;

public class CoinController : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        audioSource.Play();
        
        Destroy(gameObject);
    }
}
