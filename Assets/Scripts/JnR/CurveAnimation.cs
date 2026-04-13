using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CurveAnimation : MonoBehaviour
{

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float waitTime = 1f;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    void Start()
    {
       StartCoroutine(Animate());    
    }

    private IEnumerator Animate()
    {
        float t = 0f;
        while (t < 1f)
        {
            var pos = Vector3.Lerp(startPos, endPos, t);
            pos.y += curve.Evaluate(t);
            transform.position = pos;
            t += Time.deltaTime * speed;
            yield return null;
        }
        transform.position = endPos + Vector3.up * curve.Evaluate(1f);
        yield return new WaitForSeconds(waitTime);
    }
}
