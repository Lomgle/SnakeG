using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public float transition_time = 1f;

    public void HiddenRoom()
    {
        StartCoroutine(LoadHiddenRoom());
    }

    IEnumerator LoadHiddenRoom()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transition_time);
        SceneManager.LoadScene("HiddenRoom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
