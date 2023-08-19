using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject highScoreGO;
    public TMP_Text highScoreText;

    public AudioSource fxQuitGame;

    bool canQuitGame = false;
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
        #region Check highscore.
        HighScore = PlayerPrefs.GetInt(PlayerPreferences.HIGH_SCORE);
        #endregion
    }

    private void Update()
    {
        if (canQuitGame && !fxQuitGame.isPlaying)
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(GameScenes.MAIN_GAME);
    }

    public void QuitGame()
    {
        canQuitGame = true;
    }
}
