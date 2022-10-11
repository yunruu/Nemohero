using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;

public class SignOut : MonoBehaviour
{
    //[SerializeField]
    //private SceneChanger sceneChanger;

    public void Logout()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignOut();
    }
}
