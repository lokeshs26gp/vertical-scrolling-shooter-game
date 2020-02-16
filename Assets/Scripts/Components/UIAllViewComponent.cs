using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIAllViewComponent : MonoBehaviour
{
    [Header("Panels")]
    public GameObject LoadingPanel;
    public GameObject InitilizePanel;
    public GameObject StartPanel;
    public GameObject GameRunningPanel;
    public GameObject GameOverPanel;
    [Header("UIElements")]
    public Slider loadingProgress;
    public TextMeshProUGUI loadingText;

    [Header("GamePlay")]
    public Text highScoreText;
    public Text scoreText;
    public Text lifeText;
    public Slider healthBar;

    [Header("GameOver")]
    public Button playagainButton;
    public TextMeshProUGUI gameoverText;

    public void SetLoadingProgress(int completed, int count, bool success)
    {

        if (success)
        {
            loadingText.text = "Loading..";
            return;
        }
        float progress = (float)completed / (float)count;

        loadingText.text = string.Concat("Loading..", completed, "/", count);

        loadingProgress.value = progress;


    }

    public void ActiviateUIPanel(GameState state)
    {
        LoadingPanel.SetActive(state == GameState.LoadSystems || state == GameState.LoadResources);
        InitilizePanel.SetActive(state == GameState.Initilize);
        StartPanel.SetActive(state == GameState.Start);
        GameRunningPanel.SetActive(state == GameState.Running);
        GameOverPanel.SetActive(state == GameState.GameOver);
    }

    public void GamePlayUpdates(int highscore, int score, int life, int health, int maxhealth)
    {

        highScoreText.text = string.Concat("HIGHSCORE:", highscore);

        scoreText.text = string.Concat("SCORE:", score);

        lifeText.text = string.Concat("LIFE:", life);

        healthBar.value = (float)health / (float)maxhealth;

       
    }
    public void GameOverText(string text)
    {
        gameoverText.text = text;
    }
}
