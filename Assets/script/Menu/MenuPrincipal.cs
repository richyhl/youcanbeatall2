using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject highScoreGO;
    public TMP_Text highScoreText;

    public AudioSource backgroundMusic;
    public AudioSource fxStartGame;
    public AudioSource fxQuitGame;

    int highScore = 0;
    public int HighScore
    {
        get { return highScore; }
        set
        {
            highScore = value;
            var showHighScore = value > 0;
            highScoreGO.SetActive(showHighScore);
            if (showHighScore)
            {
                highScoreText.text = value.ToString();
            }
        }
    }

    private void Awake()
    {

        #region Play background music.
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
        #endregion

        #region Check highscore.
        HighScore = PlayerPrefs.GetInt(PlayerPreferences.HIGH_SCORE);
        #endregion
    }

    public void StartGame()
    {
        StopBackgroundMusic();
        if (fxStartGame != null)
        {
            fxStartGame.Play();
        }
        SceneManager.LoadScene(GameScenes.MAIN_GAME, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        StopBackgroundMusic();
        if (fxQuitGame != null)
        {
            fxQuitGame.Play();
        }
        Application.Quit();
    }

    private void StopBackgroundMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
    }
}
