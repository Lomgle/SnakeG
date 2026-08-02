using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class dialogue : MonoBehaviour
{
    public AudioSource bg_song;
    public AudioSource TLST;
    public Animator anim;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
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
        index = 24;
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
            bg_song.Stop();
            TLST.Play();
            anim.SetTrigger("Question");
        }
    }
}
