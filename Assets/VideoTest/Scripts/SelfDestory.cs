using UnityEngine;

public class SelfDestory : MonoBehaviour
{
    public int timer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //animator.SetTrigger("out");
    }

    public void Hide()
    {
        animator.SetTrigger("out");

        //Destroy(gameObject);
    }
}
