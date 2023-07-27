using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public SOLevelsPool levels;

    private bool isNewGame = true;
    private int score = 0;
    private int currentLevel = 0;

    private string LevelMessage => "Level " + (currentLevel + 1);

    // Start is called before the first frame update
    void Start()
    {
        if (isNewGame)
        {
            isNewGame = false;
            score = 0;
            MainGameUI.ToggleCountdown();
            RunGame();
        }
    }

    private void RunGame()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
