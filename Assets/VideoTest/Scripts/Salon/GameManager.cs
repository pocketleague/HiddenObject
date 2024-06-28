using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CustomerController customerController;
    public AnimationController animationController;

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetButtonDown("1")){

            animationController.FireTrigger("Walk");
            customerController.EnterCustomer();
        }

        if (Input.GetButtonDown("2"))
        {
            animationController.FireTrigger("Walk");
            customerController.ExitCustomer();
        }
    }

    public void CustomerEntered()
    {
        animationController.FireTrigger("Stand_Wave");
    }

    public void CustomerExited()
    {

    }
}
