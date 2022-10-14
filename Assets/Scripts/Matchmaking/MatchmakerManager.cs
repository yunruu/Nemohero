using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class MatchmakerManager : MonoBehaviour
{
    [SerializeField]
    private FirebaseManager firebaseManager;
    [SerializeField]
    private MatchmakingUI matchmakingUI;
    [SerializeField]
    private DatabaseReference reference;
    [SerializeField]
    private FirebaseAuth auth;
    [SerializeField]
    private FirebaseUser authUser;

    private void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        authUser = auth.CurrentUser;
    }

    public void MatchmakeUser()
    {
        StartCoroutine(MatchmakeUserEnum());
    }

    public IEnumerator MatchmakeUserEnum()
    {
        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;

        // Retrieve current user's data from Firebase.
        string userGender = "";
        string preferredGender = "";
        QnUser currUser = new QnUser();
        bool matched = false;
        Dictionary<string, object> userInfo = new Dictionary<string, object>();

        var getInfoTask = db.GetReference("UsersInformation").Child(authUser.DisplayName).GetValueAsync();
        yield return new WaitUntil(() => getInfoTask.IsCompleted || getInfoTask.IsFaulted);

        if (getInfoTask.IsCompleted)
        {
            DataSnapshot res = getInfoTask.Result;
            IDictionary dict = (IDictionary)res.Value;
            userGender = (string)dict["userGender"];
            preferredGender = (string)dict["preferredGender"];
            bool isMatched = (bool)dict["isMatched"];
            string matchUsername = (string)dict["matchUsername"];

            Debug.Log("Is user matched: " + isMatched);

            userInfo.Add("isMatched", isMatched);
            userInfo.Add("matchUsername", matchUsername);
            userInfo.Add("userGender", userGender);
            userInfo.Add("preferredGender", preferredGender);
            currUser = new QnUser(authUser.DisplayName, userGender, preferredGender);

            Debug.Log(userGender + " " + preferredGender);
        }
        else
        {
            throw new System.Exception();
        }

        // Matchmaking process.
        Debug.Log("Start Matchmaking.");

        var getTask = db.GetReference("UsersInformation").GetValueAsync();
        yield return new WaitUntil(() => getTask.IsCompleted || getTask.IsFaulted);

        if (getTask.IsCompleted)
        {
            DataSnapshot res = getTask.Result;

            foreach (DataSnapshot child in res.Children)
            {
                IDictionary dict = (IDictionary)child.Value;

                Debug.Log((string)dict["username"] + (string)dict["userGender"] + (string)dict["preferredGender"]);

                if ((bool)dict["isMatched"] == false && (string)dict["username"] != authUser.DisplayName &&
                    (string)dict["userGender"] == preferredGender && (string)dict["preferredGender"] == userGender)
                {
                    Debug.Log("User's match name: " + (string)userInfo["matchUsername"] + (bool)userInfo["isMatched"]);
                    Debug.Log("old match name: " + (bool)dict["isMatched"]);

                    if ((bool)userInfo["isMatched"] == true)
                    {
                        string oldMatchName = (string)userInfo["matchUsername"];
                        QnUser oldMatch = new QnUser(oldMatchName, preferredGender, userGender);
                        oldMatch.isMatched = false;
                        oldMatch.matchUsername = "";
                        string matchJson = JsonUtility.ToJson(oldMatch);
                        db.GetReference("UsersInformation").Child(oldMatchName).SetRawJsonValueAsync(matchJson);
                    }
                    Debug.Log("Match exists");
                    QnUser userMatch = new QnUser((string)dict["username"], preferredGender, userGender);
                    userMatch.isMatched = true;
                    userMatch.matchUsername = authUser.DisplayName;
                    currUser.matchUsername = (string)dict["username"];
                    currUser.isMatched = true;

                    string json = JsonUtility.ToJson(currUser);
                    db.GetReference("UsersInformation").Child(currUser.username).SetRawJsonValueAsync(json);

                    json = JsonUtility.ToJson(userMatch);
                    db.GetReference("UsersInformation").Child(userMatch.username).SetRawJsonValueAsync(json);

                    matched = true;
                    break;
                }
            }

            if (matched == false)
            {
                Debug.Log("Match does not exist");
                Debug.Log("User's match name: " + (string)userInfo["matchUsername"]);

                if ((bool)userInfo["isMatched"] == true)
                {
                    string matchUsername = (string)userInfo["matchUsername"];
                    QnUser userMatch = new QnUser(matchUsername, preferredGender, userGender);
                    userMatch.isMatched = false;
                    userMatch.matchUsername = "";
                    string matchJson = JsonUtility.ToJson(userMatch);
                    db.GetReference("UsersInformation").Child(matchUsername).SetRawJsonValueAsync(matchJson);
                }

                currUser.isMatched = false;

                string json = JsonUtility.ToJson(currUser);
                db.GetReference("UsersInformation").Child(currUser.username).SetRawJsonValueAsync(json);
            }
        }

        Debug.Log("Matched: " + matched);
    }

    public void CheckMatchStatus()
    {
        StartCoroutine(CheckMatchStatusEnum());
    }

    public IEnumerator CheckMatchStatusEnum()
    {
        FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
        string matchName = "";
        string matchGender = "";
        bool matched = false;

        var CheckStatus = db.GetReference("UsersInformation").Child(firebaseManager.GetUserName()).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Checking match status task is faulted. Please try again.");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ss = task.Result;
                IDictionary dict = (IDictionary)ss.Value;

                if ((bool)dict["isMatched"] == true)
                {
                    Debug.Log("Match Exists.");
                    matchName = (string)dict["matchUsername"];
                    matchGender = (string)dict["preferredGender"];
                    matched = true;
                }
                else
                {
                    Debug.Log("No Match.");
                }
            }
        });

        yield return new WaitUntil(predicate: () => CheckStatus.IsCompleted);

        if (matched)
        {
            matchmakingUI.OpenMatchSuccessPanel(matchName, matchGender);
        }
        else
        {
            matchmakingUI.OpenMatchUnsuccessfulPanel();
        }
    }
}
