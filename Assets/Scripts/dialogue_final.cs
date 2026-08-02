using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class dialogue_final : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    private int index;

    public GameObject CT;
    public GameObject backButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            } else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }
    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine()); 
        } else {
            gameObject.SetActive(false);
            CT.SetActive(false);
            backButton.SetActive(true);
            PlayerPrefs.SetInt("VisitCShrine", 1);
        }
    }
}
