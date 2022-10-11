using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DatabaseAPI : MonoBehaviour
{
    DatabaseReference reference;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
}
