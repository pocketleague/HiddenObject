using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public Animator animator;

    public void Happy()
    {
        //animator.Play("Happy");
    }

    public void Crying()
    {
  
        animator.Play("Crying");
    }
}
