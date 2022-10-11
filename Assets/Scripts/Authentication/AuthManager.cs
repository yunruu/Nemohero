using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class AuthManager : MonoBehaviour
{
    // Firebase
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    // Login
    [Space]
    [Header("Login")]
    public TMP_InputField emailLogin;
    public TMP_InputField passwordLogin;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    //public GameObject EnterMainPageButton;

    // Register
    [Space]
    [Header("Registration")]
    public TMP_InputField nameRegister;
    public TMP_InputField emailRegister;
    public TMP_InputField passwordRegister;
    public TMP_InputField confirmPasswordRegister;
    public TMP_Text warningRegisterText;

    // Email Verification
    [Space]
    [Header("Email Verification")]
    public TMP_Text emailVerificationText;

    private void Awake()
    {
        // Check all dependencies for Firebase are present in system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitialiseFirebase();
            }
            else
            {
                Debug.LogError("Cannot resolve all firebase dependences: " + dependencyStatus);
            }
        });
    }

    private void InitialiseFirebase()
    {
        Debug.Log("Setting up Firebase Authentication");
        auth = FirebaseAuth.DefaultInstance;
    }

    public void LoginButton()
    {
        StartCoroutine(Login(emailLogin.text, passwordLogin.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegister.text, passwordRegister.text, nameRegister.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to Register Task With {LoginTask.Exception}");
            FirebaseException firebaseExc = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError error = (AuthError)firebaseExc.ErrorCode;

            string errorMessage = "Login Failed.";
            switch (error)
            {
                case AuthError.MissingEmail:
                    errorMessage = "Missing Email.";
                    break;
                case AuthError.MissingPassword:
                    errorMessage = "Missing Password.";
                    break;
                case AuthError.WrongPassword:
                    errorMessage = "Wrong Password.";
                    break;
                case AuthError.InvalidEmail:
                    errorMessage = "Invalid Email.";
                    break;
                case AuthError.UserNotFound:
                    errorMessage = "Account Does Not Exist.";
                    break;
            }
            warningLoginText.text = errorMessage;
        }
        else
        {
            // User is logged in
            user = LoginTask.Result;

            if (user.IsEmailVerified)
            {
                Users currUser = new Users(user.DisplayName, _email, true);
                string json = JsonUtility.ToJson(currUser);
                FirebaseDatabase.DefaultInstance.GetReference("Users").Child(user.DisplayName).SetRawJsonValueAsync(json);

                Debug.LogFormat("User Signed In Successfully. {0} ({1})", user.DisplayName, user.Email);
                warningLoginText.text = "";
                confirmLoginText.text = "Logged In.";
                // EnterMainPageButton.SetActive(true);
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("MainPage");
            }
            else
            {
                SendEmailForVerification();
            }
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        FirebaseDatabase reference = FirebaseDatabase.DefaultInstance;
        bool nameExists = false;

        var CheckUsername = reference.GetReference("Users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted) {
                DataSnapshot ss = task.Result;

                foreach (DataSnapshot users in ss.Children)
                {
                    IDictionary dict = (IDictionary)users.Value;
                if ((string)dict["username"] == _username && (bool)dict["isVerified"] == true)
                    {
                        nameExists = true;
                    }
                }
            }
        });

        yield return new WaitUntil(predicate: () => CheckUsername.IsCompleted);

        if (_username == "")
        {
            warningRegisterText.text = "Missing Username.";
        }
        else if (passwordRegister.text != confirmPasswordRegister.text)
        {
            warningRegisterText.text = "Password Does Not Match.";
        }
        else if (nameExists)
        {
            Debug.Log("Username Already Exists");
            warningRegisterText.text = "Username Already Exists.";
        }
        else
        {
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                    Debug.LogWarning(message: $"Failed to Register Task With {RegisterTask.Exception}");
                    FirebaseException firebaseExc = RegisterTask.Exception.GetBaseException() as FirebaseException;
                    AuthError error = (AuthError)firebaseExc.ErrorCode;

                    string errorMessage = "Register Failed!";
                    switch(error)
                    {
                        case AuthError.MissingEmail:
                            errorMessage = "Missing Email.";
                            break;
                        case AuthError.MissingPassword:
                            errorMessage = "Missing Password.";
                            break;
                        case AuthError.WeakPassword:
                            errorMessage = "Weak Password.";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            errorMessage = "Email Already In Use.";
                            break;
                        case AuthError.InvalidEmail:
                            errorMessage = "Invalid Email.";
                            break;
                    }
                    warningRegisterText.text = errorMessage;
            }
            else
            {
                // User is now created
                user = RegisterTask.Result;

                if (user != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    var ProfileTask = user.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to Register Task With {ProfileTask.Exception}");
                        FirebaseException firebaseExc = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError error = (AuthError)firebaseExc.ErrorCode;
                        warningRegisterText.text = "Username Set Failed.";
                    }
                    else
                    {
                        Users currUser = new Users(_username, _email, false);
                        string json = JsonUtility.ToJson(currUser);
                        reference.GetReference("Users").Child(_username).SetRawJsonValueAsync(json);

                        // Username is set, go to questionnaire
                        warningRegisterText.text = "";

                        if (user.IsEmailVerified)
                        {
                            UIManager.instance.OpenLoginPanel();
                        }
                        else
                        {
                            SendEmailForVerification();
                        }
                    }
                }
            }
        }
    }

    public void SendEmailForVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());
    }

    private IEnumerator SendEmailForVerificationAsync()
    {
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                FirebaseException firebaseExc = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseExc.ErrorCode;

                string errorMessage = "Unknown Error: Please Try Again Later.";

                switch (error)
                {
                    case AuthError.Cancelled:
                        errorMessage = "Email Verification Was Cancelled.";
                        break;
                    case AuthError.TooManyRequests:
                        errorMessage = "Too Many Requests.";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        errorMessage = "Invalid Email.";
                        break;
                }

                emailVerificationText.text = errorMessage;
                UIManager.instance.OpenEmailVerificationPanel();
            }
            else
            {
                Debug.Log("Email is successfully sent.");
                emailVerificationText.text = $"Please verify your email at {user.Email}";
                UIManager.instance.OpenEmailVerificationPanel();
            }
        }
    }
}
