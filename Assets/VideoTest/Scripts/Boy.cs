using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public Animator animator;

    public void Happy()
    {
        //StartCoroutine(Delay());
        animator.SetTrigger("Happy");
    }

    public void Talking()
    {
        animator.SetTrigger("Talking");
    }

    //IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    animator.SetTrigger("Happy");
    //}

    public void Crying()
    {
        animator.SetTrigger("Cry");
    }
}
