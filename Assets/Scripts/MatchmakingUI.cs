using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchmakingUI : MonoBehaviour
{
    public static MatchmakingUI instance;

    [SerializeField]
    private GameObject MainMatchmakingPanel;

    [SerializeField]
    private GameObject UnsuccessfulPanel;

    [SerializeField]
    private GameObject MatchingInProgressPanel;

    [SerializeField]
    private GameObject SuccessfulMatchPanel;

    [SerializeField]
    private GameObject RedoQuestionnairePanel;

    [SerializeField]
    private TMP_Text matchName;
    [SerializeField]
    private TMP_Text matchId;
    [SerializeField]
    private TMP_Text matchGender;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    public void OpenMainMatchmakingPanel()
    {
        MainMatchmakingPanel.SetActive(true);
        UnsuccessfulPanel.SetActive(false);
        MatchingInProgressPanel.SetActive(false);
        RedoQuestionnairePanel.SetActive(false);
        SuccessfulMatchPanel.SetActive(false);
    }

    public void OpenMatchUnsuccessfulPanel()
    {
        MainMatchmakingPanel.SetActive(false);
        UnsuccessfulPanel.SetActive(true);
        MatchingInProgressPanel.SetActive(false);
        RedoQuestionnairePanel.SetActive(false);
        SuccessfulMatchPanel.SetActive(false);
    }

    public void MatchmakingInProgressPanel()
    {
        MainMatchmakingPanel.SetActive(false);
        UnsuccessfulPanel.SetActive(false);
        MatchingInProgressPanel.SetActive(true);
        RedoQuestionnairePanel.SetActive(false);
        SuccessfulMatchPanel.SetActive(false);
    }

    public void OpenRedoQuestionnairePanel()
    {
        MainMatchmakingPanel.SetActive(false);
        UnsuccessfulPanel.SetActive(false);
        MatchingInProgressPanel.SetActive(false);
        RedoQuestionnairePanel.SetActive(true);
        SuccessfulMatchPanel.SetActive(false);
    }

    public void OpenMatchSuccessPanel(string username, string gender)
    {
        MainMatchmakingPanel.SetActive(false);
        UnsuccessfulPanel.SetActive(false);
        MatchingInProgressPanel.SetActive(false);
        RedoQuestionnairePanel.SetActive(false);
        SuccessfulMatchPanel.SetActive(true);

        matchName.text = "MATCH USERNAME: " + username;
        matchGender.text = "MATCH GENDER: " + gender;
    }
}
