using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float platformSpeed;
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 end;
    [SerializeField]
    private Lever lever;
    private Vector3 lastPosition;
    private float elapsedTime = 0.0f;

    public Vector3 GetVelocity()
    {
        return (transform.localPosition - lastPosition) / Time.fixedDeltaTime;
    }

    public void ResetPlatform()
    {
        elapsedTime = 0.0f;
        transform.localPosition = start;
    }

    void FixedUpdate()
    {
        lastPosition = transform.localPosition;
        if (lever.IsOn())
        {
            elapsedTime += Time.fixedDeltaTime;
            float pingPong = Mathf.PingPong(elapsedTime * platformSpeed, 1.0f);
            transform.localPosition = Vector3.Lerp(start, end, pingPong);
        }
    }
}