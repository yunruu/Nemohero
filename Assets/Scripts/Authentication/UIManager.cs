using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registrationPanel;

    [SerializeField]
    private GameObject emailVerificationPanel;

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

    public void OpenLoginPanel()
    {
        emailVerificationPanel.SetActive(false);
        registrationPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void OpenRegistrationPanel()
    {
        emailVerificationPanel.SetActive(false);
        registrationPanel.SetActive(true);
        loginPanel.SetActive(false);
    }

    public void OpenEmailVerificationPanel()
    {
        registrationPanel.SetActive(false);
        loginPanel.SetActive(false);
        emailVerificationPanel.SetActive(true);

        //if (isEmailSent)
        //{
        //    emailVerificationText.text = $"Please verify your email at {emailId}";
        //}
        //else
        //{
        //    emailVerificationText.text = errorMessage;
        //}
    }
}
