using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 20f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Don't damage if player is stomping from above
        Vector3 dirToPlayer = other.transform.position - transform.position;
        if (dirToPlayer.y > 0.5f) return;

        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            character.TakeDamage(damagePerSecond * Time.fixedDeltaTime);
        }
    }
}
