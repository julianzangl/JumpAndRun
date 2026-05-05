using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    private PlayerStatistics statistics;
    [SerializeField] private TextMeshProUGUI coinCounterText;
    [SerializeField] private Character character;
    [SerializeField] private Image healthBar;
    [SerializeField] private CanvasGroup hudCanvasGroup;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private float fadingTime = 2f;
    private bool isFadingOver = false;

    private IEnumerator FadeInGameOverScreen()
    {
        isFadingOver = true;
        float timer = 0f;
        while (timer < fadingTime)
        {
            
            float alpha = Mathf.Clamp01(timer / fadingTime);
            hudCanvasGroup.alpha = 1 - alpha;
            gameOverCanvasGroup.alpha = alpha;
            yield return null;
            timer += Time.deltaTime;
        }
        this.hudCanvasGroup.alpha = 0f;
        this.gameOverCanvasGroup.alpha = 1f;
    }

    private void Awake()
    {
        instance = this;
        statistics = new PlayerStatistics() { coinCounter = 0 };
    }

    void Update()
    {
        float healthInPercent = this.character.GetCurrentHealth() / this.character.GetMaxHealth();
        healthBar.fillAmount = healthInPercent;
        if (healthInPercent <= 0f && !isFadingOver)
        {
            //StartCoroutine(FadeInGameOverScreen()); Not ready for Quest2
        }
    }

    public void CollectCoin()
    {
        statistics.coinCounter++;
        string coinText = $"Coins: {statistics.coinCounter}";
        coinCounterText.text = coinText;
    }

    private class PlayerStatistics
    {
        public int coinCounter = 0;
        //add more Statistics here as needed
    }
}
