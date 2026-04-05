using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float platformSpeed;
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 end;
    
    
    public Vector3 GetVelocity()
    {
        return (end - start).normalized * platformSpeed;
    }

    void FixedUpdate()
    {
        float pingPong = Mathf.PingPong(Time.fixedTime * platformSpeed,
        1.0f);
        var newPosition = Vector3.Lerp(start, end, pingPong);
        transform.localPosition = newPosition;
    }
}