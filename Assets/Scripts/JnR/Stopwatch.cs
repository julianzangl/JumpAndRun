using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;
    private bool timerRunning = true;
    private float elapsedTime = 0.0f;

    public void ResetTimer()
    {
        elapsedTime = 0.0f;
        timerRunning = true;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    void Update()
    {
        if (!timerRunning) return;
        elapsedTime += Time.deltaTime;

        int minutes = (int)(elapsedTime / 60f);
        int seconds = (int)(elapsedTime % 60f);
        int centiseconds = (int)(elapsedTime * 100f % 100f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
}
