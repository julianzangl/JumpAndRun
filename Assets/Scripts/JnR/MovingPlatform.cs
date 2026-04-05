using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float platformSpeed;
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 end;
    private Vector3 lastPosition;
    
    
    public Vector3 GetVelocity()
    {
        return (transform.localPosition - lastPosition) / Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        lastPosition = transform.localPosition;
        float pingPong = Mathf.PingPong(Time.fixedTime * platformSpeed,
        1.0f);
        transform.localPosition = Vector3.Lerp(start, end, pingPong);
    }
}