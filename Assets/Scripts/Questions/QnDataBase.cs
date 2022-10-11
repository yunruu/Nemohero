using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;

public class QnDataBase : MonoBehaviour
{
    public string database_url = "";
    [SerializeField] TMP_Dropdown q1;
    [SerializeField] TMP_Dropdown q2;
    [SerializeField] TMP_Dropdown q3;
    [SerializeField] TMP_Dropdown q4;
    [SerializeField] TMP_Dropdown q5;
    [SerializeField] TMP_Dropdown q6;
    [SerializeField] TMP_Dropdown q7;
    [SerializeField] TMP_Dropdown q8;
    [SerializeField] TMP_Dropdown q9;
    [SerializeField] TMP_Dropdown q10;
    [SerializeField] TMP_Dropdown q11;
    [SerializeField] TMP_Dropdown q12;

    [SerializeField]
    private FirebaseManager firebaseManager;

    public DatabaseReference reference;
    public TMP_Text usernameHeader;

    private void Start()
    {
        usernameHeader.text = "Hello " + firebaseManager.GetUserName();
        reference = firebaseManager.GetDatabaseReference();
    }

    public void SaveDataToFirebase()
    {
        if (QnButtonUI.canSubmit == true)
        {
            QnUser user = new QnUser();
            //user.Q1 = q1.value.ToString();
            //user.Q2 = q2.options[q2.value].text;
            //user.Q3 = q3.value.ToString();
            //user.Q4 = q4.value.ToString();
            //user.Q5 = q5.options[q5.value].text;
            //user.Q6 = q6.options[q6.value].text;
            //user.Q7 = q7.value.ToString();
            //user.Q8 = q8.value.ToString();
            //user.Q9 = q9.value.ToString();
            //user.Q10 = q10.value.ToString();
            //user.Q11 = q11.value.ToString();
            //user.Q12 = q12.value.ToString();

            user.username = firebaseManager.GetUserName();
            user.preferredGender = q6.options[q6.value].text;
            user.userGender = q2.options[q2.value].text;

            string json = JsonUtility.ToJson(user);

            reference.Child("UsersInformation").Child(user.username).SetRawJsonValueAsync(json);

            Debug.Log("Sent To User Pool!");
        } 
    }
}
