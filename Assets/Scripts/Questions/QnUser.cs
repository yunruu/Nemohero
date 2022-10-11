using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QnUser
{
    //public string Q1;
    //public string Q2;
    //public string Q3;
    //public string Q4;
    //public string Q5;
    //public string Q6;
    //public string Q7;
    //public string Q8;
    //public string Q9;
    //public string Q10;
    //public string Q11;
    //public string Q12;

    // only on clicking "match me now" will wantsToMatch = true;
    public string username;
    public string preferredGender;
    public string userGender;
    // public QnUser userMatch;
    public bool isMatched;
    public string matchUsername;
    public float flappyDoryHighScore = 0;
    public float oceanRushHighScore = 0;
    public float savingNemoHighScore = 0;

    public void IsMatched(bool matched)
    {
        this.isMatched = matched;
    }

    public QnUser(string username, string userGender, string preferredGender)
    {
        this.username = username;
        this.userGender = userGender;
        this.preferredGender = preferredGender;
    }

    public QnUser()
    {

    }
}
