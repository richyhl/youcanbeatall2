using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public SOLevelsPool levelsPool;

    private bool isNewGame = true;

    private bool canSetupLevel = false;
    private int currentLevel = 0;
    private SOLevel currentLevelObject;
    private int totalLevels = 0;

    private bool canSetupSequence = false;
    private int currentSequence = 0;
    private SOSequence currentSequenceObject;
    private int totalSequencesInLevel = 0;

    private bool canSetupHorde = false;
    private int currentHorde = 0;
    private SOHorde currentHordeObject;
    private int totalHordesInSequence = 0;

    private bool isEnemiesSetup = false;
    private int totalEnemiesToDestroyInHorde = 0;
    private int enemiesDestroyed = 0;
    private GameObject playerGO;

    private string LevelMessage => "Level " + (currentLevel + 1);

    #region Static elements
    private static LevelManager self;
    private static int EnemiesDestroyed
    {
        get { return self.enemiesDestroyed; }
        set { self.enemiesDestroyed = value; }
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
        MainGameUI.onCountDownCompleted += MainGameUI_onCountDownCompleted;
        playerGO = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        playerGO.SetActive(false);
        StartCoroutine(MainGameUI.ToggleCountdown());
    }

    private void Update()
    {
        if (!MainGameUI.IsCountDownHappening && !GameManager.IsGamePaused)
        {
            CheckForLevelsCleared();
            CheckForSequencesCleared();
            CheckForHordesCleared();
            CheckForEnemiesDestroyed();
        }
    }

    private void CheckForEnemiesDestroyed()
    {
        if (isEnemiesSetup && enemiesDestroyed >= totalEnemiesToDestroyInHorde)
        {
            Debug.Log("Terminando horda " + currentHorde);
            currentHorde++;
            if (currentHorde < totalHordesInSequence)
            {
                currentHordeObject = currentSequenceObject[currentHorde];
            }
            canSetupHorde = true;
            isEnemiesSetup = false;
        }
    }

    private void CheckForHordesCleared()
    {
        if (canSetupHorde)
        {
            if (currentHorde >= totalHordesInSequence)
            {
                Debug.Log("Terminando secuencia " + currentSequence);
                currentSequence++;
                if (currentSequence < totalSequencesInLevel)
                {
                    currentSequenceObject = currentLevelObject[currentSequence];
                }
                canSetupSequence = true;
            }
            else
            {
                StartCoroutine(RunCurrentHorde());
            }
            canSetupHorde = false;
        }
    }

    private void CheckForSequencesCleared()
    {
        if (canSetupSequence)
        {
            if (currentSequence >= totalSequencesInLevel)
            {
                Debug.Log("Terminando nivel " + currentLevel);
                currentLevel++;
                if (currentLevel < totalLevels)
                {
                    currentLevelObject = levelsPool[currentLevel];
                }
                canSetupLevel = true;
            }
            else
            {
                StartCoroutine(RunCurrentSequence());
            }
            canSetupSequence = false;
        }
    }

    private void CheckForLevelsCleared()
    {
        if (canSetupLevel)
        {
            if (currentLevel >= totalLevels)
            {
                Debug.Log("Terminado level pool.");
                StartCoroutine(GameCompleted());
            }
            else
            {
                StartCoroutine(RunCurrentLevel());
            }
            canSetupLevel = false;
        }
    }

    private void MainGameUI_onCountDownCompleted()
    {
        if (isNewGame)
        {
            Debug.Log("Comenzando level pool.");
            isNewGame = false;
            currentLevel = 0;
            currentLevelObject = levelsPool[currentLevel];
            totalLevels = levelsPool.levels.Length;
            canSetupLevel = true;
        }
    }

    private IEnumerator GameCompleted()
    {
        Debug.Log("Juego Terminado");
        yield return MainGameUI.DisplayMessage("Game Finished", 3);
        GameManager.GameFinished();
    }

    private IEnumerator RunCurrentLevel()
    {
        Debug.Log("Comenzando nivel " + currentLevel);
        playerGO.SetActive(true);
        yield return MainGameUI.DisplayMessage(LevelMessage, 2);
        currentSequence = 0;
        currentSequenceObject = currentLevelObject[currentSequence];
        totalSequencesInLevel = currentLevelObject.sequences.Length;
        canSetupSequence = true;
    }

    private IEnumerator RunCurrentSequence()
    {
        Debug.Log("Comenzando secuencia " + currentSequence);
        yield return new WaitForSeconds(currentSequenceObject.waitTimeBetweenHordes);
        currentHorde = 0;
        currentHordeObject = currentSequenceObject[currentHorde];
        totalHordesInSequence = currentSequenceObject.hordes.Length;
        canSetupHorde = true;
    }

    private IEnumerator RunCurrentHorde()
    {
        Debug.Log("Comenzando horda " + currentHorde);
        enemiesDestroyed = 0;
        totalEnemiesToDestroyInHorde = currentHordeObject.amountOfEnemies;
        yield return currentHordeObject.RunHorde();
        isEnemiesSetup = true;
    }

    internal static void DroneKilled()
    {
        EnemiesDestroyed++;
    }
}
