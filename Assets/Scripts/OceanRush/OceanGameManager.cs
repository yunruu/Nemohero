using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OceanGameManager : MonoBehaviour
{
    private float score;
    private float tempScore;
    private bool allMaxRate = true;
    private bool toggled = false;
    private float currHighScore;
    public OceanPlayer player;
    public GameObject playButton;
    //public GameObject startGame;
    public GameObject gameOver;
    public GameObject hardModeOn;
    public GameObject hardModeOff;
    public GameObject startGamePanel;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text healthText;
    public TMP_Text highscoreText;
    public FirebaseAuth auth;
    public FirebaseUser user;

    private void Start()
    {
        currHighScore = GetHighScore();
        this.score = 0f;
        this.tempScore = 10f;
        this.highscoreText.text = 0.ToString();
        startGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        hardModeOn.SetActive(false);

        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;

        //GetCurrentHighScore();

    }

    //public IEnumerator GetCurrentHighScore()
    //{
    //    FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
    //    var task = db.GetReference("UsersInformation").Child(user.DisplayName).GetValueAsync();
    //    yield return new WaitUntil(() => task.IsFaulted || task.IsCompleted);
    //    if (task.IsCompleted)
    //    {
    //        DataSnapshot res = task.Result;
    //        IDictionary dict = (IDictionary)res.Value;
    //        currHighScore = (float)dict["flappyDoryHighScore"];
    //    } else
    //    {
    //        throw new System.Exception();
    //    }
    //}

    public void Update()
    {
        this.score += Time.deltaTime;
        decimal temp = System.Math.Truncate((decimal)this.score);
        this.scoreText.text = "SCORE: " + temp.ToString();

        this.healthText.text = "HEALTH: " + player.health.ToString();

        if (this.score >= this.tempScore)
        {
            this.tempScore += 5f;
            OceanSpawner[] spawners = FindObjectsOfType<OceanSpawner>();

            foreach(OceanSpawner spawner in spawners)
            {
                if (spawner.MaxMoreThan(1))
                {
                    spawner.IncreaseMaxRate();
                    this.allMaxRate = false;
                }

                else
                {
                    this.allMaxRate = true;
                }

                if (spawner.MinMoreThan(1.5f))
                {
                    spawner.IncreaseMinRate();
                    this.allMaxRate = false;
                }

                else
                {
                    this.allMaxRate = true;
                }
            }

            if (this.allMaxRate == true && toggled == false)
            {
                this.hardModeOff.SetActive(false);
                this.hardModeOn.SetActive(true);
                toggled = true;
                Invoke("HardMode", 0);
            }

            else
            {
                this.allMaxRate = true;
            }
        }

        // Set high score

    }

    private void HardMode()
    {
        OceanObstacles[] obstacles = FindObjectsOfType<OceanObstacles>();

        foreach (OceanObstacles obs in obstacles) {
            obs.IncreaseSpeed();
        }

        this.player.IncreaseSpeed();

        Invoke("HardMode", Random.Range(3,8));
    }

    private void Awake()
    {
        Pause();
    }

    public void Play()
    {
        this.startGamePanel.SetActive(false);
        Reset();
        Time.timeScale = 1f;
        this.player.enabled = true;
    }

    public void Reset()
    {
        this.score = 0;
        this.toggled = false;
        this.gameOverPanel.SetActive(false);
        this.playButton.SetActive(false);
        this.hardModeOff.SetActive(true);
        this.hardModeOn.SetActive(false);
        OceanSpawner[] spawners = FindObjectsOfType<OceanSpawner>();

        foreach (OceanSpawner s in spawners)
        {
            s.Reset();
        }

        player.Restart();
    }

    public void RemoveMisc()
    {
        OceanObstacles[] obs = FindObjectsOfType<OceanObstacles>();
        OceanPowers[] powers = FindObjectsOfType<OceanPowers>();

        foreach (OceanObstacles ob in obs)
        {
            Destroy(ob.gameObject);
        }

        foreach (OceanPowers power in powers)
        {
            Destroy(power.gameObject);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        this.player.enabled = false;
    }

    public void GameOver()
    {

        if (score > currHighScore)
        {
            //DatabaseReference dbr = FirebaseDatabase.DefaultInstance.GetReference("UsersInformation");
            //Dictionary<string, object> res = new Dictionary<string, object>();
            PlayerPrefs.SetFloat("HighScore", score);
            currHighScore = score;
        }

        highscoreText.text = "HIGH SCORE: " + currHighScore;

        Pause();
        RemoveMisc();
        this.gameOverPanel.SetActive(true);
        this.playButton.SetActive(true);
        CancelInvoke("HardMode");
    }

    public float GetHighScore()
    {
        float temp = PlayerPrefs.GetFloat("HighScore");
        return temp;
    }
}
