using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float score;
    private int currHighScore;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI booText;
    public TextMeshProUGUI shieldText;
    //public TextMeshProUGUI bootsText;
    public GameObject playButton;
    public PlayerController player;
    public TMP_Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        currHighScore = PlayerPrefs.GetInt("NemoHighScore");
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    private void Awake()
    {
        Pause();
    }

    public void Play()
    {
        score = 0;
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

    public void GameOver()
    {
        if (currHighScore < score)
        {
            currHighScore = (int)score;
            PlayerPrefs.SetInt("NemoHighScore", currHighScore);
        }

        highscoreText.text = "HIGH SCORE: " + currHighScore;

        gameOverPanel.gameObject.SetActive(true);
        playButton.SetActive(true);
        booText.gameObject.SetActive(false);
        shieldText.gameObject.SetActive(false);
        //bootsText.gameObject.SetActive(false);

        MoveLeft[] obstacles = FindObjectsOfType<MoveLeft>();
        PowerupMoveLeft[] power = FindObjectsOfType<PowerupMoveLeft>();

        for (int i = 0; i < obstacles.Length; i++)
        {
            Destroy(obstacles[i].gameObject);
        }

        for (int i = 0; i < power.Length; i++)
        {
            Destroy(power[i].gameObject);
        }

        Debug.Log("Game Over!");
        Pause();
    }

    public int GetHighScore()
    {
        int temp = PlayerPrefs.GetInt("NemoHighScore");
        return temp;
    }
}
