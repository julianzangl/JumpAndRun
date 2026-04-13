using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweensMovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private List<float> movingTimes;
    [SerializeField] private float waitTime = 1f;

    private Sequence sequence;
    private bool isPlaying = false;


    private void CreateSequence()
    {
        sequence = DOTween.Sequence();

        for (int i = 0; i < positions.Count; i++)
        {
            var tween = transform.DOMove(positions[i], movingTimes[i]);
            tween.SetEase(Ease.InOutQuint);
            sequence.Append(tween);
            sequence.AppendInterval(waitTime);
        }

        for (int i = positions.Count - 2; i >= 1; i--)
        {
            var tween = transform.DOMove(positions[i], movingTimes[i]);
            tween.SetEase(Ease.InOutQuint);
            sequence.Append(tween);
            sequence.AppendInterval(waitTime);
        }
    }

    private IEnumerator Play()
    {
        isPlaying = true;
        this.CreateSequence();
        sequence.Play();
        yield return sequence.WaitForCompletion();
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            StartCoroutine(Play());
        }
    }
}
