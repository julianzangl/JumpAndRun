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
    [SerializeField] private CanvasGroup victoryCanvasGroup;
    [SerializeField] private RespawnTrigger respawnTrigger;
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
        this.gameOverCanvasGroup.interactable = true;
        this.gameOverCanvasGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOutGameOverScreen()
    {
        float timer = 0f;
        this.gameOverCanvasGroup.interactable = false;
        this.gameOverCanvasGroup.blocksRaycasts = false;
        while (timer < fadingTime)
        {
            float alpha = Mathf.Clamp01(timer / fadingTime);
            hudCanvasGroup.alpha = alpha;
            gameOverCanvasGroup.alpha = 1 - alpha;
            yield return null;
            timer += Time.deltaTime;
        }
        this.hudCanvasGroup.alpha = 1f;
        this.gameOverCanvasGroup.alpha = 0f;
        isFadingOver = false;
    }

    private IEnumerator FadeInVictoryScreen()
    {
        isFadingOver = true;
        float timer = 0f;
        while (timer < fadingTime)
        {
            float alpha = Mathf.Clamp01(timer / fadingTime);
            hudCanvasGroup.alpha = 1 - alpha;
            victoryCanvasGroup.alpha = alpha;
            yield return null;
            timer += Time.deltaTime;
        }
        this.hudCanvasGroup.alpha = 0f;
        this.victoryCanvasGroup.alpha = 1f;
    }

    public void TriggerGameOver()
    {
        if (!isFadingOver)
        {
            StartCoroutine(FadeInGameOverScreen());
        }
    }

    public void TriggerVictory()
    {
        if (!isFadingOver)
        {
            StartCoroutine(FadeInVictoryScreen());
        }
    }

    public void OnRespawnButtonPressed()
    {
        Debug.Log("Respawn button pressed");
        respawnTrigger.ResetEverything();
        StartCoroutine(FadeOutGameOverScreen());
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
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
    }

    public void CollectCoin()
    {
        statistics.coinCounter++;
        string coinText = $"Coins: {statistics.coinCounter}";
        coinCounterText.text = coinText;
    }

    public void ResetCoins()
    {
        statistics.coinCounter = 0;
        coinCounterText.text = "Coins: 0";
    }

    private class PlayerStatistics
    {
        public int coinCounter = 0;
    }
}
