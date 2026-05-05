using UnityEngine;

public class StompDetector : MonoBehaviour
{
    [SerializeField] private float stompThreshold = -1f;
    [SerializeField] private float bounceForce = 5f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            float dotUp = Vector3.Dot(hit.normal, Vector3.up);
            if (dotUp > 0.5f)
            {
                var stompable = hit.gameObject.GetComponent<EnemyStompable>();
                if (stompable != null)
                {
                    stompable.Stomp();
                }
            }
        }
    }
}
