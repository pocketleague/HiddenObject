using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public Animator animator;

    public void Happy()
    {
        StartCoroutine(Delay());
    }
        
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);

        animator.SetTrigger("Happy");

        yield return new WaitForSeconds(3);

        animator.SetTrigger("Talking");
    }

    public void Crying()
    {
        animator.SetTrigger("Cry");
    }
}
