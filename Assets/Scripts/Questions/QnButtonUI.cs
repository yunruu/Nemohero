using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QnButtonUI : MonoBehaviour
{

    public TextMeshProUGUI submitsuccText;
    public TextMeshProUGUI submitunsuccText;
    public GameObject goBackText;
    public QnUser user;
    public TMP_Dropdown[] dropdowns;
    public static bool canSubmit;

    public void SetText(string text)
    {
        int unsucc = 0;

        //for (int i = 0; i < dropdowns.Length; i++)
        for (int i = 0; i < 11; i++)
        {
            if (dropdowns[i].value == 0)
            {
                unsucc++;
            } 
        }

        if (unsucc > 0)
        {
            canSubmit = false;
            submitunsuccText.gameObject.SetActive(true);
        }
        else
        {
            canSubmit = true;
            submitunsuccText.gameObject.SetActive(false);
            submitsuccText.gameObject.SetActive(true);
            goBackText.gameObject.SetActive(true);
        }
    }


    /**public void NewGameButton()
    {
        SceneManager.LoadScene(homePage);
    }*/
}
