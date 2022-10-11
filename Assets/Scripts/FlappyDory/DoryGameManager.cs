using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoryGameManager : MonoBehaviour
{
    private int score;
    private int currHighScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel; 
    public GameObject playButton;  
    public DoryPlayerController player;  
    public bool gameActive;
    public TMP_Text highscoreText;

    void Start()
    {
        currHighScore = PlayerPrefs.GetInt("DoryHighScore");

    }

    void Update()
    {
        
    }

    private void Awake()
    {
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (score > currHighScore)
        {
            PlayerPrefs.SetFloat("DoryHighScore", score);
            currHighScore = score;
        }

        highscoreText.text = "HIGH SCORE: " + currHighScore;

        gameActive = false;
        gameOverPanel.gameObject.SetActive(true);
        playButton.SetActive(true);
        MoveCorals[] corals = FindObjectsOfType<MoveCorals>();
        for (int i = 0; i < corals.Length; i++)
        {
            Destroy(corals[i].gameObject);
        }
        
        Debug.Log("Game Over!");
        Pause();
    }

    public float GetHighScore()
    {
        float temp = PlayerPrefs.GetFloat("DoryHighScore");
        return temp;
    }

}
