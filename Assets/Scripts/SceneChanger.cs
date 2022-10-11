using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToMinigames()
    {
        SceneManager.LoadScene("Minigames");
    }

    public void ChangeToFlappyDory()
    {
        SceneManager.LoadScene("FlappyDory");
    }

    public void ChangeToSavingNemo()
    {
        SceneManager.LoadScene("SavingNemo");
    }

    public void ChangeToOceanRush()
    {
        SceneManager.LoadScene("OceanRushGame");
    }

    public void ChangeToMatchmaking()
    {
        SceneManager.LoadScene("Matchmaking");
    }

    public void ChangeToMessaging()
    {
        SceneManager.LoadScene("Messaging");
    }

    public void ChangeToModerator()
    {
        SceneManager.LoadScene("Moderator");
    }

    public void ChangeToMainPage()
    {
        SceneManager.LoadScene("MainPage");
    }

    public void ChangeToLoginPage()
    {
        SceneManager.LoadScene("LoginPage");
    }

    public void ChangeToQuestionnaire()
    {
        SceneManager.LoadScene("Questionnaire");
    }

    public void ChangeToStoryline()
    {
        SceneManager.LoadScene("Storyline");
    }
}
