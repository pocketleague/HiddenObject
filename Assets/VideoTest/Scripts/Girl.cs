using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    public Animator animator;

    void Start()
    {

    }

    public void Happy()
    {
        //animator.Play("Happy");
    }

    public void Crying()
    {
        //Vector3(-10f, -44f, 103f);

        transform.localPosition = new Vector3(-10f, -44f, 103f);

        animator.SetTrigger("cry");

        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(0, 128, 0);
        transform.rotation = rot;
    }

    public void StandUp()
    {
        //Quaternion rot = new Quaternion();
        //rot.eulerAngles = new Vector3(0, 180, 0);
        //transform.rotation = rot;

        LeanTween.rotate(gameObject, new Vector3(0, 180, 0), 0.3f);

        animator.SetTrigger("standup");

    }

    public void Walk()
    {
        //Quaternion rot = new Quaternion();
        //rot.eulerAngles = new Vector3(0, 270, 0);
        //transform.rotation = rot;

        LeanTween.rotate(gameObject, new Vector3(0, 240, 0), 0.3f);


        animator.SetTrigger("walk");
    }
}
