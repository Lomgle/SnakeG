using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public TextMeshPro scoreDisplay;
    public TextMeshPro hint_text;
    public TextMeshProUGUI finalScoreDisplay;
    public TextMeshProUGUI bestScoreDisplay;
    public int score;
    public GameObject food;
    public GameObject gameOverCanvas;
    public GameObject pauseCanvas;
    public SnakeMovement snake;
    public AudioSource overFX;
    public AudioSource gameMusic;
    public AudioSource bossMusic;
    public Boss boss;
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss1").GetComponent<Boss>();
        snake = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeMovement>();
        if (!PlayerPrefs.HasKey("bestScore")) 
            PlayerPrefs.SetInt("bestScore", 0);
    }
    public void AddScore()
    {
        score++;
        scoreDisplay.text = score.ToString();
        if (score == 1 && PlayerPrefs.HasKey("VisitCShrine"))
        {
            gameMusic.Stop();
            boss.TriggerSpawnBoss();
        }
    }
    public void GameOver()
    {
        gameMusic.Stop();
        overFX.Play();
        Time.timeScale = 0.0f;
        finalScoreDisplay.text = score.ToString();
        if (score > PlayerPrefs.GetInt("bestScore")) PlayerPrefs.SetInt("bestScore", score);
        bestScoreDisplay.text = PlayerPrefs.GetInt("bestScore").ToString();
        gameOverCanvas.SetActive(true);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1.0f;
    }
    public void Menu()
    {
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu");
        snake.paused = false;
        Time.timeScale = 1.0f;
    }
    public void PauseGame()
    {
        AudioListener.pause = true;
        Time.timeScale = 0.0f;
        pauseCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        AudioListener.pause = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        snake.paused = false;
    }
    public void ClearSnake()
    {
        for (int i = 1; i < snake.segmentList.Count; i++)
            {
                Destroy(snake.segmentList[i].gameObject);
            }
        snake.segmentList.Clear();
        snake.segmentList.Add(transform);
    }
}
