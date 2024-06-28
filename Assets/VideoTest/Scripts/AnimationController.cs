using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.SetTrigger("Body_Angry");
        animator.SetTrigger("Face_Smile");
        animator.SetBool("EyeRandom", true);

    }

    void Update()
    {
        
    }
}
