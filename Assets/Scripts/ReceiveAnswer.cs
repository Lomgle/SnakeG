using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReceiveAnswer : MonoBehaviour
{
    public QnA[] listQnA;
    public TMP_InputField input;
    public TextMeshProUGUI questionText;
    
    private int index = 0;

    public void CheckAnswer()
    {
        if (input.text.ToUpper() == listQnA[index].answer)
        {
            index++;
            LoadQuestion(index);
        } 
    }
    public void LoadQuestion(int index)
    {
        if (index >= listQnA.Length) FinalSequence();
        else questionText.text = listQnA[index].question;

    }

    public void FinalSequence(){}
}
