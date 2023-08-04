using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject scoreGO;
    public GameObject newHighScoreGO;
    public TMP_Text scoreText;
    public AudioSource fxGoBack;

    private int finalScore = 0;
    private bool canGoBackToMenu = false;
    public int FinalScore
    {
        get { return finalScore; }
        set
        {
            finalScore = value;
            var storedHighScore = PlayerPrefs.GetInt(PlayerPreferences.HIGH_SCORE);
            var isNewHighScore = value > storedHighScore;
            if (isNewHighScore)
            {
                scoreGO.SetActive(false);
                newHighScoreGO.SetActive(true);
                PlayerPrefs.SetInt(PlayerPreferences.HIGH_SCORE, value);
            }
            else
            {
                scoreGO.SetActive(true);
                newHighScoreGO.SetActive(false);
            }
            scoreText.text = value.ToString();
        }
    }

    private void Awake()
    {
        #region Set current score as final score.
        FinalScore = PlayerPrefs.GetInt(PlayerPreferences.CURRENT_SCORE);
        PlayerPrefs.SetInt(PlayerPreferences.CURRENT_SCORE, 0);
        #endregion
    }

    private void Update()
    {
        if (canGoBackToMenu && !fxGoBack.isPlaying)
        {
            SceneManager.LoadScene(GameScenes.MAIN_MENU, LoadSceneMode.Single);
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(GameScenes.MAIN_GAME);
    }

    public void BackToMenu()
    {
        canGoBackToMenu = true;
    }
}
