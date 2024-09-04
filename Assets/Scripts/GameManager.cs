using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeImage;
    public Text overgamescore;
    public Text gameOverText;
    public Button continueButton;

    private Blade blade;
    private Spawner spawner;
    private int score;
    private int overscore;

    public int currentLevel;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        overscore = 0;
        scoreText.text = score.ToString();
        overgamescore.text = "SCORE: " + overscore.ToString();

        ClearScene();

        gameOverText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        overgamescore.gameObject.SetActive(false);
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        overscore += amount;
        scoreText.text = score.ToString();
        overgamescore.text = "SCORE: " + overscore.ToString();

        PlayerPrefs.SetInt("Level" + currentLevel + "Score", score);
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        gameOverText.gameObject.SetActive(true);
        overgamescore.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);

        if (score >= 6)
        {
            UnlockNextLevel();
        }
    }

    void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    public void OnContinueButtonClicked()
    {
        PlayerPrefs.SetInt("FromGameOver", 1); // Indiquer que nous venons de Game Over
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        // Charger la scène Main Menu de manière asynchrone
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");

        // Attendre que la scène soit complètement chargée
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
