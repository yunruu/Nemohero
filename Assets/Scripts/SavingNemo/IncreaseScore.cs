using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private float score;
    private float scoreToIncrease;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreToIncrease = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ((int)score).ToString();
        score += scoreToIncrease * Time.deltaTime;
    }
}
