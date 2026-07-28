using TMPro;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public TextMeshPro scoreDisplay;
    public TextMeshProUGUI finalScoreDisplay;
    public int score;
    public GameObject gameOverCanvas;
    void Start()
    {
        
    }
    [ContextMenu("inc score")]
    public void AddScore()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        finalScoreDisplay.text = score.ToString();
        gameOverCanvas.SetActive(true);
    }
    void Update()
    {
        
    }
}
