using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void Walk(bool en)
    {
        if (en)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
       
    }

    public void Run(bool en)
    {
        if (en)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", true);
            
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
    public void Idle()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
    }
    public void Hit()
    {
        animator.SetTrigger("Hit");
      
    }
    public void Die()
    {
        animator.SetBool("Die", true);

    }
}
