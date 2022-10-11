using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Users
{
    public string username;
    public string email;
    public bool isVerified;

    public Users(string username, string email, bool isVerified)
    {
        this.username = username;
        this.email = email;
        this.isVerified = isVerified;
    }
}
