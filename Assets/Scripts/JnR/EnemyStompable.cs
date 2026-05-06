using UnityEngine;
using DG.Tweening;

public class EnemyStompable : MonoBehaviour
{
    [SerializeField] private AudioClip squashSound;
    [SerializeField] private float squashDuration = 0.3f;
    [SerializeField] private float squashScaleY = 0.1f;

    private AudioSource audioSource;
    private bool isDead = false;
    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    public void ResetEnemy()
    {
        transform.DOKill();
        isDead = false;
        transform.localScale = originalScale;
        transform.position = originalPosition;
        gameObject.SetActive(true);

        var patrol = GetComponent<EnemyPatrol>();
        if (patrol != null) patrol.enabled = true;

        var animator = GetComponentInChildren<Animator>();
        if (animator != null) animator.enabled = true;
    }

    public void Stomp()
    {
        if (isDead) return;
        isDead = true;

        // Disable patrol movement
        var patrol = GetComponent<EnemyPatrol>();
        if (patrol != null) patrol.enabled = false;

        // Stop walking animation
        var animator = GetComponentInChildren<Animator>();
        if (animator != null) animator.enabled = false;

        // Play squash sound
        if (squashSound != null)
        {
            audioSource.PlayOneShot(squashSound);
        }

        // Squash tween animation
        Vector3 squashedScale = new Vector3(
            transform.localScale.x * 1.5f,
            transform.localScale.y * squashScaleY,
            transform.localScale.z * 1.5f
        );

        transform.DOScale(squashedScale, squashDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
