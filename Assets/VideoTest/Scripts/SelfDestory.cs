using UnityEngine;

public class SelfDestory : MonoBehaviour
{
    public int timer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //animator.SetTrigger("out");

        if(timer != 0)
            Invoke("Delay", timer);
    }

    public void Hide()
    {
        animator.SetTrigger("out");
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    void Delay()
    {

    }
}
