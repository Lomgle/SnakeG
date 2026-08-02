using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public int counter = 0;
    public bool flagged_showNumber = false;
    public GameObject hiddenButton;
    public TextMeshPro number_counter_display;
    public AudioSource gameMusic;

    void Start()
    {
        if (PlayerPrefs.HasKey("VisitCShrine"))
        {
            counter = 68;
            ShowHidden();
            flagged_showNumber = true;
        }
    }
    public void StartGame(){
        SceneManager.LoadScene("Gameplay");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowNumber()
    {
        if (!flagged_showNumber) number_counter_display.alpha = 255.0f;
        flagged_showNumber = true;
    }
    public void IncreaseCount()
    {
        counter++;
        number_counter_display.text = counter.ToString();
    }

    public void ShowHidden()
    {
        if (counter > 67)
        {
            number_counter_display.alpha = 0.0f;
            hiddenButton.SetActive(true);
            gameMusic.pitch = 0.6f;
        }
    }
}
