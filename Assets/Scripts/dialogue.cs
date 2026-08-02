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
    public float transition_time = 1.5f;
    private int index;

    public GameObject questions;
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
    public void TriggerSequence()
    {
        StopAllCoroutines();
        StartCoroutine(PlaySequence());
    }
    IEnumerator PlaySequence()
    {
        anim.SetTrigger("Question");
        bg_song.Stop();
        yield return new WaitForSeconds(transition_time);
        gameObject.SetActive(false);
        TLST.Play();
        questions.SetActive(true);
    }
    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine()); 
        } else {
            TriggerSequence();
        }
    }
}
