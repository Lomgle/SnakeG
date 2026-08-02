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
    void Start()
    {
        snake = GameObject.FindGameObjectWithTag("Player").GetComponent<SnakeMovement>();
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
        SceneManager.LoadScene("Menu");
        snake.paused = false;
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        gameMusic.Pause();
        Time.timeScale = 0.0f;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        gameMusic.UnPause();
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
    public void SpawnBoss()
    {
        if (score == 7 && PlayerPrefs.HasKey("VisitCShrine"))
        {
            ClearSnake();
            food.SetActive(false);
            transform.position = new Vector3(-6.0f, 0.0f, 0.0f);
            snake.direction = new Vector2(0.0f, 0.0f);
            snake.canMove = false;
        }
    }

    void Update()
    {
        SpawnBoss();
    }
}
