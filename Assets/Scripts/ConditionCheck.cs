using UnityEngine;

public class ConditionCheck : MonoBehaviour
{
    public GameObject CT;
    public GameObject backButton;
    public GameObject dialogue;
    public GameObject secretText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("VisitCShrine"))
        {
            CT.SetActive(false);
            dialogue.SetActive(false);
            backButton.SetActive(true);
            secretText.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
