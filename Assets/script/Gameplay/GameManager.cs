using UnityEngine;
using UnityEngine.SceneManagement;
using YouCanBeatAll.ScriptableObjects;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] AudioSource fxNegative;
    [SerializeField] AudioSource bgm;
    private bool allowInputs = false;
    private bool isGamePaused = false;
    private bool canReturnToMainMenu = false;
    private int enemiesDestroyed = 0;
    private int score = 0;

    private Cannon playerCannon;

    #region Static elements
    private static GameManager self;
    private static int EnemiesDestroyed
    {
        get { return self.enemiesDestroyed; }
        set { self.enemiesDestroyed = value; }
    }
    private static int Score
    {
        get { return self.score; }
        set
        {
            self.score = value;
            MainGameUI.SetScore(value);
        }
    }
    public static bool IsGamePaused => self.isGamePaused;
    public static bool AllowInputs
    {
        get { return self.allowInputs; }
        set { self.allowInputs = value; }
    }
    #endregion

    private void Awake()
    {
        if (self == null)
        {
            self = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        PlayerPrefs.SetInt(PlayerPreferences.CURRENT_SCORE, 0);
        allowInputs = true;
        playerCannon = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponentInChildren<Cannon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused && !MainGameUI.IsCountDownHappening && allowInputs)
        {
            if (Input.GetMouseButtonDown(0) && !playerCannon.IsShieldActive)
            {
                playerCannon.ShootProyectile(TipoDeBala.Blanca);
            }
            if (Input.GetMouseButtonDown(1) && !playerCannon.IsShieldActive)
            {
                playerCannon.ShootProyectile(TipoDeBala.Negra);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                playerCannon.ToggleShield(true);
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                playerCannon.ToggleShield(false);
            }
        }
        if (canReturnToMainMenu && !fxNegative.isPlaying)
        {
            SceneManager.LoadScene(GameScenes.MAIN_MENU, LoadSceneMode.Single);
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        pauseMenuUI.SetActive(isGamePaused);
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            bgm.volume = bgm.volume / 2;
        }
        else
        {
            Time.timeScale = 1f;
            bgm.volume = bgm.volume * 2;
        }
    }

    public void ReturnToMainMenu()
    {
        canReturnToMainMenu = true;
    }

    internal static void DroneKilled(int points)
    {
        Score += points;
        EnemiesDestroyed++;
    }

    internal static void GameFinished()
    {
        PlayerPrefs.SetInt(PlayerPreferences.CURRENT_SCORE, Score);
        SceneManager.LoadScene(GameScenes.GAME_OVER, LoadSceneMode.Single);
    }
}
