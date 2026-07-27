using TMPro;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public TextMeshPro scoreDisplay;
    public int score;

    void Start()
    {
        
    }
    
    [ContextMenu("inc score")]
    public void AddScore()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }

    void Update()
    {
        
    }
}
