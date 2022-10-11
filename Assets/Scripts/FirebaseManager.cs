using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
    DatabaseReference reference;

    // public DependencyStatus dependencyStatus;
    [SerializeField]
    private FirebaseAuth auth;
    [SerializeField]
    private FirebaseUser user;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
    }

    private void Awake()
    {
    }

    public string GetUserName()
    {
        return user.DisplayName;
    }

    public string GetUserId()
    {
        return user.UserId;
    }

    public DatabaseReference GetDatabaseReference()
    {
        return reference;
    }
}
