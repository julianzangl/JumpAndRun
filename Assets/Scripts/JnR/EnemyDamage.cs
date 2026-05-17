using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 20f;

    public void DealDamage(Character character)
    {
        if (character != null)
        {
            character.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
