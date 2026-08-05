using System.Collections;
using TMPro;
using UnityEngine;

public class ReceiveAnswer : MonoBehaviour
{
    public QnA[] listQnA;
    public TMP_InputField input;
    public TextMeshProUGUI questionText;
    public float delayTime = 2f;

    public GameObject correct;
    public GameObject fail;
    
    public int index = 0;

    public AudioSource TLST;
    public Animator anim;
    public float animTime = 2f;
    public GameObject finalDialogue;

    void Start()
    {
        LoadQuestion(0);
    }

    public void CoroutineCheckAnswer()
    {
        StartCoroutine(CheckAnswer());
    }
    IEnumerator CheckAnswer()
    {
        if (input.text.ToUpper() == listQnA[index].answer)
        {
            correct.SetActive(true);
            index++;
            yield return new WaitForSeconds(delayTime);
            LoadQuestion(index);
        } else
        {
            fail.SetActive(true);
            yield return new WaitForSeconds(delayTime);
            fail.SetActive(false);
        }
    }
    public void LoadQuestion(int index)
    {
        correct.SetActive(false);
        input.text = string.Empty;
        if (index >= listQnA.Length) StartCoroutine(FinalSequence());
        else questionText.text = listQnA[index].question;

    }

    IEnumerator FinalSequence()
    {
        gameObject.SetActive(false);
        TLST.Stop();
        anim.SetTrigger("Final");
        finalDialogue.SetActive(true);
        yield return new WaitForSeconds(animTime);
    }
}
