using UnityEngine;

public class JewelController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float riseSpeed = 2f;
    [SerializeField] private float bobAmplitude = 0.25f;
    [SerializeField] private float bobFrequency = 1.5f;
    [SerializeField] private Transform endPosition;

    private enum State { Inactive, Rising, Floating }
    private State state = State.Inactive;
    private Vector3 floatBasePosition;
    private Collider jewelCollider;

    void Awake()
    {
        jewelCollider = GetComponent<Collider>();
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        state = State.Rising;
        if (jewelCollider != null)
            jewelCollider.enabled = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (state == State.Rising)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, riseSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPosition.position) < 0.01f)
            {
                transform.position = endPosition.position;
                floatBasePosition = endPosition.position;
                state = State.Floating;
                if (jewelCollider != null)
                    jewelCollider.enabled = true;
            }
        }
        else if (state == State.Floating)
        {
            float yOffset = Mathf.Sin(Time.time * bobFrequency * Mathf.PI * 2f) * bobAmplitude;
            transform.position = floatBasePosition + new Vector3(0f, yOffset, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (state != State.Floating) return;
        if (!other.CompareTag("Player")) return;

        UIManager.Instance.TriggerVictory();

        GetComponent<Renderer>().enabled = false;
        if (jewelCollider != null)
            jewelCollider.enabled = false;
    }
}
