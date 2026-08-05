using System.Collections;
using UnityEngine;

public class AnimEventLaserFlower : MonoBehaviour
{
    public Animator cameraAnim;

    public void shakeCamera()
    {
        StartCoroutine(shake());
    }
    IEnumerator shake()
    {
        cameraAnim.Play("Shake");
        yield return new WaitForSeconds(1f);
        cameraAnim.Play("Default");
    }
}
