using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{

    bool isCountDownHappening = false;
    [SerializeField] GameObject uIMessage;
    [SerializeField] TMP_Text uIMessageText;
    [SerializeField] TMP_Text uIScoreText;

    #region Static items

    private static MainGameUI self;
    public static bool IsCountDownHappening
    {
        get { return self.isCountDownHappening; }
        private set { self.isCountDownHappening = value; }
    }
    private static GameObject UIMessage => self.uIMessage;
    private static TMP_Text UIMessageText => self.uIMessageText;
    private static TMP_Text UIScoreText => self.uIScoreText;
    #endregion

    #region Events

    public static event Action onCountDownCompleted;

    #endregion

    private void Awake()
    {
        if (self == null)
        {
            self = this;
        }
    }

    public static void SetScore(int score)
    {
        UIScoreText.text = score.ToString();
    }

    public static IEnumerator ToggleCountdown()
    {
        IsCountDownHappening = true;
        UIMessage.SetActive(true);
        UIMessageText.text = "Ready";
        yield return new WaitForSeconds(1);
        UIMessageText.text = "Set";
        yield return new WaitForSeconds(1);
        UIMessageText.text = "Go";
        yield return new WaitForSeconds(0.5f);
        UIMessage.SetActive(false);
        IsCountDownHappening = false;

        if (onCountDownCompleted != null)
        {
            onCountDownCompleted();
        }
    }

    public static IEnumerator DisplayMessage(string message, int seconds)
    {
        UIMessage.SetActive(true);
        UIMessageText.text = message;
        yield return new WaitForSeconds(seconds);
        UIMessage.SetActive(false);
    }
}
