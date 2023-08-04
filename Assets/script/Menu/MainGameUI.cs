using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{

    bool isCountDownHappening = false;
    [SerializeField] GameObject uIMessage;
    [SerializeField] GameObject uIPauseButton;
    [SerializeField] TMP_Text uIMessageText;
    [SerializeField] TMP_Text uIScoreText;
    [SerializeField] AudioSource fxCountDown;
    [SerializeField] AudioSource fxMessage;

    #region Static items

    private static MainGameUI self;
    public static bool IsCountDownHappening
    {
        get { return self.isCountDownHappening; }
        private set { self.isCountDownHappening = value; }
    }
    private static GameObject UIMessage => self.uIMessage;
    private static GameObject UIPauseButton => self.uIPauseButton;
    private static TMP_Text UIMessageText => self.uIMessageText;
    private static TMP_Text UIScoreText => self.uIScoreText;
    private static AudioSource FxCountDown => self.fxCountDown;
    private static AudioSource FxMessage => self.fxMessage;
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
        FxCountDown.Play();
        yield return new WaitForSeconds(1);
        UIMessageText.text = "Set";
        FxCountDown.Play();
        yield return new WaitForSeconds(1);
        UIMessageText.text = "Go";
        FxCountDown.Play();
        yield return new WaitForSeconds(1f);
        UIMessage.SetActive(false);
        IsCountDownHappening = false;

        if (onCountDownCompleted != null)
        {
            onCountDownCompleted();
        }
    }

    public static IEnumerator DisplayMessage(string message, int seconds)
    {
        GameManager.AllowInputs = false;
        UIPauseButton.SetActive(false);
        UIMessage.SetActive(true);
        UIMessageText.text = message;
        FxMessage.Play();
        yield return new WaitForSeconds(seconds);
        UIMessage.SetActive(false);
        UIPauseButton.SetActive(true);
        GameManager.AllowInputs = true;
    }
}
