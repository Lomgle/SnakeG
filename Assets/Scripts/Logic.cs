using TMPro;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public TextMeshPro scoreDisplay;
    public TextMeshProUGUI finalScoreDisplay;
    public TextMeshProUGUI bestScoreDisplay;
    public int score;
    public GameObject gameOverCanvas;
    void Start()
    {
         if (!PlayerPrefs.HasKey("bestScore")) 
            PlayerPrefs.SetInt("bestScore", 0);
    }
    public void AddScore()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        finalScoreDisplay.text = score.ToString();
        if (score > PlayerPrefs.GetInt("bestScore")) PlayerPrefs.SetInt("bestScore", score);
        bestScoreDisplay.text = PlayerPrefs.GetInt("bestScore").ToString();
        gameOverCanvas.SetActive(true);
    }
    void Update()
    {
        
    }
}
