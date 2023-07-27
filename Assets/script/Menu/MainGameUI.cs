using System.Collections;
using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{

    [SerializeField] GameObject uIMessage;
    [SerializeField] TMP_Text uIMessageText;

    #region Static items

    private static MainGameUI self;
    private static GameObject UIMessage => self.uIMessage;
    private static TMP_Text UIMessageText => self.uIMessageText;
    #endregion



    private void Awake()
    {
        self = this;
    }

    public static IEnumerator ToggleCountdown()
    {
        UIMessage.SetActive(true);
        UIMessageText.text = "Ready?";
        yield return new WaitForSeconds(1);
        UIMessageText.text = "Set...";
        yield return new WaitForSeconds(1);
        UIMessageText.text = "Go!";
        yield return new WaitForSeconds(1);
        UIMessage.SetActive(false);
    }
}
