using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public Transform walkStartPt, walkMidPt, walkEndPt;
    public GameManager gameManager;

    public void EnterCustomer()
    {
        transform.rotation = Quaternion.Euler(0 , 90, 0);

        LeanTween.move(gameObject, walkMidPt, 1).setOnComplete(() =>{
            LeanTween.rotate(gameObject, new Vector3(0, 180, 0), .2f).setOnComplete(() => {
                gameManager.CustomerEntered();
            });
        });
    }

    public void ExitCustomer()
    {
        LeanTween.rotate(gameObject, new Vector3(0, 90, 0), .2f).setOnComplete(() => {
            LeanTween.move(gameObject, walkEndPt, 1).setOnComplete(() => {
                gameManager.CustomerExited();
            });
        });
    }

    void Update()
    {
        
    }
}
