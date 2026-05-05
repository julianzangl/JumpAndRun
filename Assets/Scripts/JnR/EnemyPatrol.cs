using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private float speed = 2f;

    private Animator animator;
    private bool movingToB = true;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        transform.position = pointA;
    }

    void Update()
    {
        Vector3 target = movingToB ? pointB : pointA;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Face movement direction
        Vector3 direction = (target - transform.position).normalized;
        if (direction.sqrMagnitude > 0.01f)
        {
            direction.y = 0f;
            if (direction != Vector3.zero)
                transform.forward = direction;
        }

        // Set walking animation
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }

        // Switch direction when reaching the target
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            movingToB = !movingToB;
        }
    }
}
