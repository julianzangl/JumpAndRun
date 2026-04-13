using System;
using System.Collections;
using UnityEngine;

public class SmoothStep : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    private IEnumerator SmoothMove()
    {
        yield return new WaitForSeconds(waitTime);
        yield return this.StartCoroutine(SmoothStepForward());
        yield return new WaitForSeconds(waitTime);
        yield return this.StartCoroutine(SmoothStepBackward());
    }

    private IEnumerator SmoothStepForward()
    {
        //t = Time.time - startTime / duration
        float t = 0f;
        while (t < 1f)
        {
            float g = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(startPos, endPos, g);

            t += Time.deltaTime * speed;
            yield return null;
        }
        transform.position = endPos;
    }

    private IEnumerator SmoothStepBackward()
    {
        float t = 1f;
        while (t > 0f)
        {
            float g = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(startPos, endPos, g);

            t -= Time.deltaTime * speed;
            yield return null;
        }
        transform.position = startPos;
    }

    void Start()
    {
        this.transform.position = startPos;
        StartCoroutine(SmoothMove());
    }
}
