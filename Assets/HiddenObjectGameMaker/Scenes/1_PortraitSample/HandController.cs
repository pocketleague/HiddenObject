using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject hand;
    public GameObject particle;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LeanTween.scale(hand, new Vector3(5, 5, 5), 0.5f).setEasePunch();
        }
        hand.transform.position = Input.mousePosition;

      
        if(Input.GetKeyDown(KeyCode.A))
        {
            particle.SetActive(true);
            hand.SetActive(false);
        }
    }
}
