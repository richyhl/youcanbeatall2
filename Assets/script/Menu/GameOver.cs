using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject scoreGO;
    public GameObject newHighScoreGO;
    public TMP_Text highScoreText;

    public AudioSource fxGameOver;
    public AudioSource fxReplayGame;
    public AudioSource fxBackToMenu;

    int finalScore = 0;
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
                highScoreText.text = value.ToString();
                PlayerPrefs.SetInt(PlayerPreferences.HIGH_SCORE, value);
            }
            else
            {
                scoreGO.SetActive(true);
                newHighScoreGO.SetActive(false);
                highScoreText.text = storedHighScore.ToString();
            }
        }
    }

    private void Awake()
    {

        #region Play Game Over sound.
        if (fxGameOver != null)
        {
            fxGameOver.Play();
        }
        #endregion

        #region Set current score as final score.
        FinalScore = PlayerPrefs.GetInt(PlayerPreferences.CURRENT_SCORE);
        PlayerPrefs.SetInt(PlayerPreferences.CURRENT_SCORE, 0);
        #endregion
    }

    public void ReplayGame()
    {
        if (fxReplayGame != null)
        {
            fxReplayGame.Play();
        }
        SceneManager.LoadScene(GameScenes.MAIN_GAME, LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        if (fxBackToMenu != null)
        {
            fxBackToMenu.Play();
        }
        SceneManager.LoadScene(GameScenes.MAIN_MENU, LoadSceneMode.Single);
    }
}
